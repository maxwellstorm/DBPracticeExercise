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
            Equipment test = new Equipment();
            test.EquipID = 568;
            test.fetch();
            Console.WriteLine("Equipment ID: " + test.EquipID);
            Console.WriteLine("Equipment Desciption: " + test.EquipmentDescription);
            Console.WriteLine("Equipment Name: " + test.EquipmentName);
            Console.WriteLine("Equipment capacity: " + test.EquipmentCapacity);
            Console.ReadKey();
        }
    }
}
