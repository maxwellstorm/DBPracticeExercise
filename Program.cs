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
                test.EquipID = 568;
                test.fetch();
                test.swap(894);

            }
            catch (DLException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}
