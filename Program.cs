using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLConnect.Data;
using SQLConnect.Business;

namespace SQLConnect
{
    class Program
    {

        static void Main(string[] args)
        {
            MySQLDatabase MySQL = new MySQLDatabase("root", "M3312140m", "travel2");  
            Console.BufferWidth = 1000;
            /*try
            {
                string prevName;
                Equipment test = new Equipment();
                test.connect();
                test.EquipID = 894;
                test.fetch();
                Console.WriteLine("orignal values before swapping:\n\nequipId = " + test.EquipID + " equipName = " + test.EquipmentName);
                test.EquipID = 1256;
                test.fetch();
                Console.WriteLine("equipId = " + test.EquipID + " equipName = " + test.EquipmentName );
                test.EquipID = 894;
                test.fetch();
                test.swap(1256);
                test.EquipID = 894;
                test.fetch();
                Console.WriteLine("\nvalues after swapping:\n\nequipId = " + test.EquipID + " equipName = " + test.EquipmentName);
                test.EquipID = 1256;
                test.fetch();
                Console.WriteLine("equipId = " + test.EquipID + " equipName = " + test.EquipmentName);

            }
            catch (DLException e)
            {
                Console.WriteLine(e.Message);
            }*/
            MySQL.Connect();
            BLuser user = new BLuser(MySQL, 3);
            user.Password = "guest";
            
            BLEquipment equip = new BLEquipment(user,MySQL);
            equip.EquipID = 568;
            equip.fetch();
            Console.WriteLine("displaying results for guest");
            Console.WriteLine("ID = " + equip.EquipID + "\nname = " + equip.EquipmentName + "\ndescription = " + equip.EquipmentDescription + "\ncapacity = " + equip.EquipmentCapacity);
            Console.WriteLine("attempting save with admin user");
            user = new BLuser(MySQL, 1);
            user.Password = "admin";
            equip = new BLEquipment(user, MySQL);
            if (equip.save())
            {
                equip.EquipID = 1256;
                equip.fetch();
                equip.swap(3644);
                Console.WriteLine("swap successful");
            }
            else
            {
                Console.WriteLine("accedd denied");
            }
            Console.ReadKey();
        }
    }
}
