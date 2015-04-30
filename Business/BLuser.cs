using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLConnect.Data;

namespace SQLConnect.Business
{
    class BLuser : SQLConnect.Data.DLusers
    {
        public BLuser(MySQLDatabase db) : base(db) { }
        public BLuser (MySQLDatabase db,int id): base( db, id){}
        public BLuser(MySQLDatabase db, int id, string Name) : base(db, id, Name) { }
        public BLuser(MySQLDatabase db, int id, string Name,string password) : base(db, id, Name,password) { }
        public BLuser(MySQLDatabase db, int id, string Name,string password,string access) : base(db, id, Name,password,access) { }

      
    }
}
