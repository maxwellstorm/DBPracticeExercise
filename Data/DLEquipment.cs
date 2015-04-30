using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLConnect.Data
{
    class DLEquipment
    {
        private MySQLDatabase db;
        public int EquipID { get;set; }
        public string EquipmentName { get; set; }
        public string EquipmentDescription { get; set; }
        public int EquipmentCapacity { get; set; }

        public DLEquipment(MySQLDatabase db) 
        {
            this.db = db;
        }

        public DLEquipment(MySQLDatabase db,int EquipID)
        {
            this.db = db;
            this.EquipID = EquipID;
        }

        public DLEquipment(MySQLDatabase db, int EquipID, string EquipmentName, string EquipmentDescription, int EquipmentCapcity)
        {
            this.db = db;
            this.EquipID = EquipID;
            this.EquipmentName = EquipmentName;
            this.EquipmentDescription = EquipmentDescription;
            this.EquipmentCapacity = EquipmentCapcity;
        }

        public void fetch() // use equipID to update all other attributes in this class
        {
            try
            {
                //List<Object> row = MySQL.GetData("SELECT EquipmentName,EquipmentDescription,EquipmentCapacity from Equipment where EquipID = " + EquipID)[0];
                Dictionary<string,object> vals = new Dictionary<string,object>();
                vals.Add("@equipID",this.EquipID);
                List<object> row = db.GetData("SELECT EquipmentName,EquipmentDescription,EquipmentCapacity from Equipment where EquipID = @equipID", vals)[0];
                EquipmentName = (string)row[0];
                EquipmentDescription = (string)row[1];
                EquipmentCapacity = (int)row[2];

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void put()//update exsisting record wth equipID with this classes current attibutes
        {
        
            
                Dictionary<string, object> vals = new Dictionary<string, object>();
                vals.Add("@equipName", this.EquipmentName);
                vals.Add("@equipDesc", this.EquipmentDescription);
                vals.Add("@equipCap", this.EquipmentCapacity);
                vals.Add("@equipID", this.EquipID);
                db.SetData("UPDATE Equipment set EquipmentName = @equipName, EquipmentDescription = @equipDesc, EquipmentCapacity =  @equipCap where EquipID = @equipID",vals,false);

            
        }

        public void post()//insert new record with objects attributes
        {
                Dictionary<string, object> vals = new Dictionary<string, object>();
                vals.Add("@equipID", this.EquipID);
                vals.Add("@equipName",this.EquipmentName);
                vals.Add("@equipDesc", this.EquipmentDescription);
                vals.Add("@equipCap", this.EquipmentCapacity);
                db.SetData("insert into equipment (EquipID,EquipmentName,EquipmentDescription,EquipmentCapacity) Values (@equipID, @equipName , @equipDesc , @equipCap)",vals,false);
            
        }

        public void delete()//delete object with current equipId
        {
                Dictionary<string, object> vals = new Dictionary<string, object>();
                vals.Add("@equipID", this.EquipID);
                db.SetData("delete from Equipment where EquipId = @equipID",vals,false );
           
        }


        public void swap(int otherID)
        {
                try
                {
                    int firstID = this.EquipID;
                    Dictionary<string, object> vals = new Dictionary<string, object>();
                    vals.Add("@equipID", otherID);
                    vals.Add("@equipName", this.EquipmentName);
                    this.EquipID = otherID;
                    this.fetch();
                    db.startTrans();
                    db.SetData("update equipment set equipmentName = @equipName where equipID = @equipID",vals,true);
                    vals["@equipID"] = firstID;
                    vals["@equipName"] = this.EquipmentName;
                    db.SetData("update equipment set equipmentName = @equipName where equipID = @equipID", vals, true);
                    db.endTrans();
                }
                catch (Exception e)
                {
                    db.rollbackTrans();
                }
            
        }

        public void test()
        {
            db.startTrans();
            Dictionary<string, object> vals = new Dictionary<string, object>();
            vals.Add("@equipId", 1);
            db.SetData("insert into equipment (equipId) values (@equipId)",vals,true);
            vals["@equipId"] = 2;
            db.SetData("insert into equipment (equipId) values (@equipId)",vals,true);
            db.endTrans();
        }
     

        
    }
}
