/************************************************************************ 
 * 项目名称 :  ProtocolGenerator       
 * 类 名 称 :  FileUtil 
 * 类 描 述 :  文件操作工具类
 * 版 本 号 :  v1.0.0.0  
 * 说    明 :      
 * 作    者 :  FReedom 
 * 创建时间 :  2018/4/20 星期五 18:06:35 
 * 更新时间 :  2018/4/20 星期五 18:06:35 
************************************************************************ 
 * Copyright @ BoilingBlood 2018. All rights reserved. 
************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProtocolGenerator
{
    public class FileUtil
    {
        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="fileContent">写入内容</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="fileName">文件名</param>
        /// <param name="fileSuffix">文件后缀</param>
        /// <returns></returns>
        public static bool WriteToFile(StringBuilder fileContent, string filePath, string fileName, string fileSuffix)
        {
            try
            {
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                string fileFullName = filePath + fileName + fileSuffix;
                FileInfo fileInfo = new FileInfo(fileFullName);
                StreamWriter writer = fileInfo.CreateText();
                writer.WriteLine(fileContent.ToString());
                writer.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public static bool WriteToFile(string fileContent, string filePath, string fileName, string fileSuffix)
        {
            try
            {
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                string fileFullName = filePath + fileName + fileSuffix;
                FileInfo fileInfo = new FileInfo(fileFullName);
                StreamWriter writer = fileInfo.CreateText();
                writer.WriteLine(fileContent);
                writer.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }


        public static string[] FindFiles(string path,string key)
        {
            //string[] files = Directory.GetFiles(path, "*.code", SearchOption.AllDirectories);
            string[] files = Directory.GetFiles(path, key, SearchOption.AllDirectories);
            return files;
        }

        public static string GetFileHashValue(string fileFullName)
        {
            //计算第一个文件的哈希值
            var hash = System.Security.Cryptography.HashAlgorithm.Create();
            var stream = new System.IO.FileStream(fileFullName, System.IO.FileMode.Open);
            byte[] hashByte = hash.ComputeHash(stream);
            stream.Close();
            string hashValue = BitConverter.ToString(hashByte);
            return hashValue;
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

      

    }
}
