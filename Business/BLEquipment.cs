using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLConnect.Data;

namespace SQLConnect.Business
{
    class BLEquipment : DLEquipment
    {
        private BLuser user;
        public BLEquipment(BLuser user,MySQLDatabase db) : base(db)
        {
            this.user = user;
        }
        public bool save()
        {
            try
            {
                user.login();
                return user.access == "Editor" || user.access == "Admin";
      
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
