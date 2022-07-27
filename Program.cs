using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.IO;

namespace PingHosts
{
    class Program
    {
        static void Main(string[] args)
        {
            Ping myPing = new Ping();
            PingOptions myOptions = new PingOptions();
            string data = "icmp_send_request";
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string myLog = @"\PingKenk.txt";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int myLength = args.Length;
            DateTime currentTime;
            Console.WriteLine("Путь к папке МоиДокументы текущено пользователья: {0}", path);
            StreamWriter myStream = new StreamWriter(path + myLog);
            for (int i = 0; i < myLength; i++)
            {
                //ping args[0]
                //...
                //ping args[myLength - 1]
                myOptions.DontFragment = false;
                currentTime = DateTime.Now;
                PingReply reply = myPing.Send(args[0], 120, buffer, myOptions);
                if (reply.Status.ToString() == "Success")
                {
                    Console.WriteLine("{0}\tУзел {1} доступен\tip-адрес: {2}", currentTime, args[i], reply.Address.ToString());
                    myStream.WriteLine("{0}\tУзел {1} доступен\tip-адрес: {2}", currentTime, args[i], reply.Address.ToString());
                    myStream.Close();
                }
                else
                {
                    Console.WriteLine("{0}\tУзел {1} не доступен!", currentTime, args[i]);
                    myStream.WriteLine("{0}\tУзел {1} не доступен!", currentTime, args[i]);
                }
            }
            myStream.Close();
        }
    }
}
