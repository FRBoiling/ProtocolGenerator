using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
/************************************************************************ 
* 项目名称 :  UnitTest   
* 类 名 称 :  UnitTest
* 类 描 述 :  生成协议Id的.cs文件
* 版 本 号 :  v1.0.0.0 
* 说    明 :  写一些测试函数    
* 作    者 :  Boiling
* 创建时间 :  2018/5/25 14:35:44 
************************************************************************ 
* Copyright @ BoilingBlood 2018. All rights reserved.
************************************************************************/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using protocol.server.client;

namespace UnitTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestMethod()
        {
            CSLoginInfo info = new CSLoginInfo();
            send(info);
        }

        public void send<T>(T msg)
        {
            string strMsg = msg.ToString();
            Type type =  msg.GetType();
            string name = type.Name;

            Console.WriteLine("{0}{1}{2}",strMsg,type,name);
        }

        [TestMethod]
        public void ReadLinesFromFileTest()
        {
            ReadLinesFromFile(@"D:\GitHub\ProtocolGenerater\Protocol\temp\" , @"C2S" ,@".tmp");
        }

        public static List<string> ReadLinesFromFile(string filePath, string fileName, string fileSuffix)
        {
            try
            {
                List<string> lines = new List<string>();

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                string fileFullName = filePath + fileName + fileSuffix;
                FileStream fsRead = new FileStream(fileFullName, FileMode.Open, FileAccess.Read);

                StreamReader sr = new StreamReader(fsRead);
                string s = "";
                do
                {
                    s = sr.ReadLine();
                    if (s != null)
                    {
                        lines.Add(s);
                    }
                } while (s != null);

                sr.Close();
                fsRead.Close();
                return lines;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        [TestMethod]
        public void ReadFromFileTest()
        {
            ReadFromFile(@"D:\GitHub\ProtocolGenerater\Protocol\temp\", @"C2S", @".tmp");
        }

        public static string ReadFromFile(string filePath, string fileName, string fileSuffix)
        {
            try
            {
                string fileFullName = filePath + fileName + fileSuffix;

                FileInfo fi = new FileInfo(fileFullName);
                if (fi.Exists)
                {
                    FileStream fsRead = fi.OpenRead();
                    StreamReader sr = new StreamReader(fsRead);
                    string s = sr.ReadLine();
                    sr.Close();
                    fsRead.Close();
                    return s;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        [TestMethod]
        public void IsRightId()
        {
            string str = "0xFF0001";
            string head = str.Substring(0, 2);
            if (!head .Equals("0x"))
            {
                return;
            }
            else
            {
                string body = str.Substring(2);
                //32位去掉符号位 31位。索性取7位16进制
                if (body.Length >7 )
                {
                    return;
                }
                const string PATTERN = @"[A-Fa-f0-9]+$";
                bool bo = System.Text.RegularExpressions.Regex.IsMatch(str, PATTERN);
                
            }
           

        }
    }
}
