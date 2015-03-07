using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SQLConnect
{
    class MySQLDatabase
    {
        public string Username, Password, DatabaseName;
        public Boolean Connected;
        private MySqlConnection conn;

        public MySQLDatabase(string Username, string Password, string DatabaseName)
        {
            this.Username = Username;
            this.Password = Password;
            this.DatabaseName = DatabaseName;
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
            }
            return Connected;
        }

        public List<List<object>> GetData(string SQLStr)
        {
            List<List<object>> toReturn = new List<List<object>>();
            MySqlCommand cmd = new MySqlCommand(SQLStr, conn);
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
                    toReturn.Add(temp);
                    temp = null;
                }
                return toReturn;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            reader.Close();


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
                Console.WriteLine(e.Message);
                return false;
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
                return false;
            }
        }

        public void PrintMetadata(string SQLStr)
        {
            try
            {
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand(SQLStr, conn);
                reader = cmd.ExecuteReader();
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
                Console.WriteLine(e.Message);
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
                Console.WriteLine(e.Message);
            }
        }
    }
}
