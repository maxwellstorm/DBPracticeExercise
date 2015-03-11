﻿using System;
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
           
            //Equipment test = new Equipment();
            try
            {
                MySQLDatabase test = new MySQLDatabase("root", "M3312140m", "travel2");
                test.Connect();
                test.PrintMetadata("select * from trips");
                Console.WriteLine();
                test.PrintFormat("select * from trips");
            }
            catch (DLException e)
            {
                Console.WriteLine(e.Message);
            }

            //test.EquipID = 568;
          //  test.fetch();
          //  Console.WriteLine("Equipment ID: " + test.EquipID);
          //  Console.WriteLine("Equipment Desciption: " + test.EquipmentDescription);
          //  Console.WriteLine("Equipment Name: " + test.EquipmentName);
          //  Console.WriteLine("Equipment capacity: " + test.EquipmentCapacity);
            Console.ReadKey();
        }
    }
}
