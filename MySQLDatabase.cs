using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace SQLConnect
{
    class MySQLDatabase
    {
        public string Username, Password, DatabaseName;
        public Boolean Connected;
        private MySqlConnection conn;
        private Dictionary<string,MySqlCommand> prepStatements;
        private MySqlTransaction trans;

        public MySQLDatabase(string Username, string Password, string DatabaseName)
        {
            this.Username = Username;
            this.Password = Password;
            this.DatabaseName = DatabaseName;
            prepStatements = new Dictionary<string, MySqlCommand>();
            conn = new MySqlConnection("Data Source=localhost;" + "User ID=" + Username + ";Password=" + Password+";database="+DatabaseName);
        }

        public Boolean Connect()
        {
            try
            {
                conn.Open();
                Connected = true;
            }
            catch (Exception e)
            {
                Connected = false;
                throw new DLException(e);
            }
            return Connected;
        }


        private void addSQLData(MySqlCommand cmd, List<List<object>> toAdd)
        {
            MySqlDataReader reader;
            try
            {
                List<object> temp;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    temp = new List<object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        temp.Add(reader.GetValue(i));
                    }
                    toAdd.Add(temp);
                    temp = null;
                }
                reader.Close();
            }
            catch (Exception e)
            {
                throw new DLException(e);
            }
        }


        public List<List<object>> GetData(string SQLStr)
        {
            List<List<object>> toReturn = new List<List<object>>();
            MySqlCommand cmd = new MySqlCommand(SQLStr, conn);
            addSQLData(cmd, toReturn);
            return toReturn;
        }

      

        public  List<List<object>> GetData(string SQLStr,Boolean ShowColNames)
        {
            List<List<object>> toReturn = new List<List<object>>();
            MySqlCommand cmd = new MySqlCommand(SQLStr, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (ShowColNames)
            {
                List<object> temp = new List<object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    temp.Add(reader.GetName(i));
                }
                toReturn.Add(temp);
                reader.Close();
            }
            addSQLData(cmd, toReturn);
            return toReturn;

        }

        public List<List<object>> GetData(string sqlStr,Dictionary<string,object> vals)
        {
            try
            {
                List<List<object>> toReturn = new List<List<object>>();
                MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
                cmd = this.prepare(sqlStr, vals,cmd);
                addSQLData(cmd, toReturn);
                return toReturn;
            }
            catch (Exception e)
            {
                throw new DLException(e);
            }
        }

        public bool SetData(string SQLStr)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand(SQLStr, conn);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch(Exception e)
            {
                throw new DLException(e);
            }


            
        }

        public bool SetData(string sqlStr, Dictionary<string, object> vals,Boolean trans)
        {
            try
            {

                MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
                this.prepare(sqlStr, vals, cmd);
                if (trans)
                    cmd.Transaction = this.trans;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public object executeStmt(string sqlStr, Dictionary<string, object> vals)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
                this.prepare(sqlStr,vals,cmd);
                return cmd.ExecuteReader();
            }
            catch (Exception e)
            {
                throw new DLException(e);
            }
        }
        public Boolean Close()
        {
            try
            {
                conn.Close();
                Connected = false;
                return true;
            }
            catch (Exception e)
            {
                throw new DLException(e);
            }
        }

        public void PrintMetadata(string SQLStr)
        {
            try
            {
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand(SQLStr, conn);
                reader = cmd.ExecuteReader();
                foreach(DataColumn  d in reader.GetSchemaTable().Columns){
                    Console.WriteLine(d.ToString());
                }
                Console.WriteLine("Field Count: " + reader.FieldCount+"\n");
                Console.WriteLine("{0,-20}  {1,-10}\n", "Field Name","Field Type");
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.WriteLine("{0,-20}  {1,-10}", reader.GetName(i), reader.GetFieldType(i).ToString().Substring(reader.GetFieldType(i).ToString().IndexOf('.')+1).ToUpper());
                }
                reader.Close();

            }
            catch (Exception e)
            {

                throw new DLException(e);
          
            
            }
        }

        public void PrintFormat(string SQLStr)
        {
            try
            {
                StringBuilder sb =  new StringBuilder();
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand(SQLStr, conn);
                reader = cmd.ExecuteReader();
                string fieldName;
                List<string>fieldNames = new List<string>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                   fieldName = reader .GetName(i);
                   sb.AppendFormat("{0," + -28 + "}", fieldName);
                }
                Console.WriteLine(sb.ToString());
          
                Console.WriteLine();
                while (reader.Read())
                {
                    sb = new StringBuilder();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        sb.AppendFormat("{0,"+-28+"}",reader.GetValue(i));
                    }
                    Console.WriteLine(sb.ToString());
                }
                reader.Close();
                
              
            }
            catch(Exception e)
            {
                throw new DLException(e);
            }
        }

        private MySqlCommand prepare(string sqlStr, Dictionary<string, object> vals, MySqlCommand cmd)
        {
        
            try
            {
                MySqlParameter param;
               
                bool duplicateSQlStr = prepStatements.ContainsKey(sqlStr);
                if (duplicateSQlStr)
                    cmd = prepStatements[sqlStr];
                foreach (KeyValuePair<string, object> val in vals)
                {
                    if (!duplicateSQlStr)
                    {
                        param = new MySqlParameter(val.Key, val.Value);
                        cmd.Parameters.Add(param);
                    }
                    else
                    {
                        cmd.Parameters[val.Key].Value = val.Value;
                    }
                }
                if (!duplicateSQlStr)
                    prepStatements.Add(sqlStr, cmd);
                cmd.Prepare();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return cmd;
            
        }


        public void startTrans()
        {
            trans = conn.BeginTransaction();
        }

        public void endTrans()
        {
            trans.Commit();
        }

        public void rollbackTrans()
        {
            trans.Rollback();
        }


    }
}
