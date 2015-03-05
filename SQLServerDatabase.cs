using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SQLConnect
{
   
    class SQLServerDatabase
    {
        public string Username, Password, DatabaseName;
        public Boolean Connected;
        private SqlConnection conn;
        private SqlCommand com;
       
        public SQLServerDatabase(string Username,string Password, string DatabaseName)
        {
            this.Username = Username;
            this.Password = Password;
            this.DatabaseName = DatabaseName;
            conn = new SqlConnection("Data Source=theodore.it.rit.edu;Database=" + DatabaseName + ";User ID=" + Username + ";Password=" + Password);
        }

        public Boolean Connect()
        {
            try
            {
                conn.Open();
                Console.WriteLine("connection successsful to sql server");
                Connected = true;
            }
            catch (Exception e)
            {
                Connected = false;
                Console.WriteLine(e.Message + "\nconnection failed to sql server");
            }
            return Connected;
        }

        public Boolean Close()
        {
            try
            {
                conn.Close();
                Console.WriteLine("connection closed successfully to sql server");
                Connected = false;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\nclosing connection failed to sql server");
                return false;
            }
        }
    }
}
