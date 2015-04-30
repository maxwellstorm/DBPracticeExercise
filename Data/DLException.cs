using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SQLConnect.Data
{
    class DLException : Exception
    {
        const string logPath = @"log.txt";
        public string Message{get;set;} 
        Exception e;
        string[] Params;
        public DLException(Exception e)
        {
            Message = "unable to complete operation";
            this.e = e;
            log();
        }

        public DLException(Exception e, params string[] paramList)
        {
            Message = "unable to complete operation";
            this.e = e;
            this.Params = paramList;
            log();
        }

        private void log()
        {
            try
            {
                string toWrite = this.e.Message;
                if(this.Params!=null)
                    foreach(var st in  this.Params)
                        toWrite += " " + st;
                StringBuilder sb = new StringBuilder();
                StreamWriter sw = File.AppendText(logPath);
                DateTime now = DateTime.Now;
                sb.AppendFormat("{0,-20} {1,-50}", now,toWrite);
                sw.WriteLine(sb.ToString());
                sw.Flush();
                sw.Close();
            }
            catch (IOException ie)
            {
                Console.WriteLine("IO exception when logging");
            }
            catch (Exception e)
            {
                Console.WriteLine("exception occured during logging");
            }
            
        }
    }
}
