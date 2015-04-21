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
            }
            Console.ReadKey();
        }
    }
}
