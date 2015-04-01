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
           
            //Equipment test = new Equipment();
            try
            {
                MySQLDatabase test = new MySQLDatabase("root", "M3312140m", "travel2");
                test.Connect();
                test.GetData("Select * from equipment",true);
                test.Close();
            }
            catch (DLException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}
