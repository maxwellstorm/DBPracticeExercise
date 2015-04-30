using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLConnect.Data
{
    class DLusers
    {
        public int id { get; set;}
        public string Name { get; set; }
        public string Password { get; set; }
        public string access { get; set; }

        private MySQLDatabase db;

        public DLusers(MySQLDatabase db)
        {
            this.db = db;
        }
        public DLusers(MySQLDatabase db, int id)
        {
            this.db = db;
            this.id = id;
        }

        public DLusers(MySQLDatabase db,int id, string name)
        {
            this.db = db;
            this.id = id;
            this.Name = name;
        }

        public DLusers(MySQLDatabase db,int id, string name, string password)
        {
            this.db = db;
            this.id = id;
            this.Name = name;
            this.Password = password;
        }

        public DLusers(MySQLDatabase db,int id,string name, string password,string access)
        {
            this.db = db;
            this.id = id;
            this.Name = name;
            this.Password = password;
            this.access = access;
        }

        public bool login()
        {
            try
            {
                Dictionary<string,object> vals = new Dictionary<string,object>();
                vals.Add("@id", this.id);
                vals.Add("@password", this.Password);
                List<List<Object>> returned = db.GetData("select Name,access from users where id = @id and password = @password", vals);
                if (returned.Count == 1)
                {
                    this.Name = (string)returned[0][0];
                    this.access = (string)returned[0][1];
                    return true;
                }
                return false;

            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}
