using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLConnect
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BufferWidth = 1000;
            try
            {
                string prevName;
                Equipment test = new Equipment();
                test.EquipID = 894;
                test.fetch();
                Console.WriteLine("running fetch with id " + test.EquipID);
                Console.WriteLine("equipID = {0} \nequip Descprition = {1} \nequipName = {2} \nequip Capacity = {3}\n",test.EquipID,test.EquipmentDescription,test.EquipmentName, test.EquipmentCapacity);
                prevName = test.EquipmentName;
                test.EquipmentName = "somthing else ";
                Console.WriteLine("running put method with new name = " + test.EquipmentName);
                test.put();
                test.EquipmentName = prevName;
                test.put();
                test.EquipID = 1234;
                test.EquipmentDescription = "this is a test";
                test.EquipmentName = "test";
                Console.WriteLine("inserting new record");
                test.post();
                Console.WriteLine("deleting record");
                test.delete();

            }
            catch (DLException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}
