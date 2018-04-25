using Protocol.Client.Gate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoBuffTest
{
    class Program
    {
        static void Main(string[] args)
        {
            C2GTest1 test1 = new C2GTest1();
            tttt(test1.c2gTest2Lists);
            Console.ReadLine();
        }

        static void tttt(List<C2GTest2> TTT2)
        {
            C2GTest2 t2 = new C2GTest2();
            t2.Testint1 = 1;
            t2.Testint2 = 22;
            TTT2.Add(t2);
        }
    }
}
