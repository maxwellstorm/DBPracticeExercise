using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLConnect
{
    class Equipment
    {
        private MySQLDatabase MySQL = new MySQLDatabase("root", "M3312140m", "travel2");  
        public int EquipID { get;set; }
        public string EquipmentName { get; set; }
        public string EquipmentDescription { get; set; }
        public int EquipmentCapacity { get; set; }

        public Equipment() { }

        public Equipment(int EquipID)
        {
            this.EquipID = EquipID;
        }

        public Equipment(int EquipID, string EquipmentName, string EquipmentDescription, int EquipmentCapcity)
        {
            this.EquipID = EquipID;
            this.EquipmentName = EquipmentName;
            this.EquipmentDescription = EquipmentDescription;
            this.EquipmentCapacity = EquipmentCapcity;
        }

        public void fetch() // use equipID to update all other attributes in this class
        {
            try
            {
                MySQL.Connect();
                List<Object> row = MySQL.GetData("SELECT EquipmentName,EquipmentDescription,EquipmentCapacity from Equipment where EquipID = " + EquipID)[0];
                EquipmentName = (string)row[0];
                EquipmentDescription = (string)row[1];
                EquipmentCapacity = (int)row[2];

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            MySQL.Close();
        }

        public void put()//update exsisting record wth equipID with this classes current attibutes
        {
            if (MySQL.Connect())
            {
                MySQL.SetData("UPDATE Equipment set EquipmentName = '" + EquipmentName + "', EquipmentDescription = '" + EquipmentDescription + "',EquipmentCapacity = " + EquipmentCapacity + " where EquipID = " + EquipID);
                MySQL.Close();
            }
        }

        public void post()//insert new record with objects attributes
        {
            if (MySQL.Connect())
            {
                MySQL.SetData("insert into equipment (EquipID,EquipmentName,EquipmentDescription,EquipmentCapacity) Values (" + EquipID + ",'" + EquipmentName + "','" + EquipmentDescription + "'," + EquipmentCapacity + ")");
                MySQL.Close();
            }
        }

        public void delete()//delete object with current equipId
        {
            if (MySQL.Connect())
            {
                MySQL.SetData("delete from Equipment where EquipId = " + EquipID);
                MySQL.Close();
            }

        }
    }
}
