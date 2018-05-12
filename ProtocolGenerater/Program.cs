/************************************************************************ 
 * 项目名称 :  ProtocolGenerater   
 * 项目描述 :      
 * 类 名 称 :  Program 
 * 版 本 号 :  v1.0.0.0  
 * 说    明 :      
 * 作    者 :  Boiling 
 * 创建时间 :  2018/4/20 星期五 15:55:08 
 * 更新时间 :  2018/4/20 星期五 15:55:08 
************************************************************************ 
 * Copyright @ BoilingBlood 2018. All rights reserved. 
************************************************************************/
using System;

namespace ProtocolGenerater
{
    class Program
    {
        public static string InputPath = null;
        public static string OutputPath = null;
        public static string Filename = null;


        static void Main(string[] args)
        {
            //1 >>> --input_path = D:\GitHub\ProtocolGenerater\ProtocolForClient\
            //1 >>> --output_path = D:\GitHub\ProtocolGenerater\Bin\
            //1 >>> --filename = D:\GitHub\ProtocolGenerater\ProtocolForClient\Client\C2G.code
            string operationType = "default";

            foreach (string arg in args)
            {
                //Console.WriteLine(">>{0} ", arg);
                string lhs = arg, rhs = "";
                int index = arg.IndexOf('=');
                if (index > 0)
                {
                    lhs = arg.Substring(0, index);
                    rhs = arg.Substring(index + 1);
                }

                switch (lhs)
                {
                    case "":
                        break;
                    case "--operate_type":
                        operationType = rhs;
                        break;
                    case "--input_path":
                        InputPath = rhs;
                        break;
                    case "--output_path":
                        OutputPath = rhs;
                        break;
                    case "--filename":
                        if (rhs.ToLower().EndsWith(".code".ToLower()))
                        {
                            Filename = rhs;
                        }
                        else
                        {
                            Console.WriteLine("File type error :{0}  ( mast ends with .code ) ", rhs);
                            return;
                        }
                        break;
                    default:
                        break;
                }
            }

            if (!DataManager.GetInstance().CheckFileChange(Filename))
            {
                //Console.WriteLine("File no change: {0}", Filename);
                return;
            }

            GeneraterManager.GetInstance().Init(operationType);

            if (operationType == "all")
            {
                DataManager.GetInstance().Load(Filename);
            }
            else if (operationType == "id")
            {
                GeneraterManager.GetInstance().GenerateId();
            }
            else if (operationType == "default")
            {
                GeneraterManager.GetInstance().GenerateId();
                if (InputPath == null)
                {
                    Console.WriteLine("operationType got an error. InputPath = null");
                    return;
                }
                else
                {
                    try
                    {
                        string[] files = FileUtil.FindFiles(InputPath, "*.code");
                        foreach (var file in files)
                        {
                            DataManager.GetInstance().Load(file);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("operationType got an error. {0}", e.Message);
                        return;
                    }
                }
            }
            else
            {
                DataManager.GetInstance().Load(Filename);
            }
            GeneraterManager.GetInstance().Run();
        }
    }
}