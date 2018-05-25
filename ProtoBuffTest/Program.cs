using ClientLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProtoBuffTest
{
    class Program
    {
        static GateServer API;

        static void Main(string[] args)
        {
            string ip = "127.0.0.1";
            ushort port = 8201;
            API= new GateServer();
            try
            {
                API.Init(ip, port);
                API.ReConnect();
            }
            catch (Exception e)
            {
                Console.WriteLine("init failed:{0}", e.ToString());
                API.Exit();
                return;
            }

            Thread workThread = new Thread(ThreadMethod);
            workThread.Start();
            Console.WriteLine("client is ready...");
            while (true)
            {
                string cmd = Console.ReadLine().ToLower().Trim();
                switch (cmd)
                {
                    case "send":
                        API.Request_Test();
                        break;
                    default:
                        break;
                }
            }
            //Console.ReadLine();
        }

        static bool IsWorking = true;
        static void ThreadMethod()
        {
            IsWorking = true;

            while (IsWorking)
            {
                API.Process();
            }
            API.Exit();

            Console.WriteLine("client is exit...");
        }

    }
}
