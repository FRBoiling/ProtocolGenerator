using ProtocolGenerator;
using System;
using System.IO;

namespace ProtocolBuffersGenerator.Core
{
    public class GeneratorCache
    {
        string cacheFileName = "tmp";
        string cacheFilePostfix = ".tmp";
        string cacheFilePath = Program.OutputPath+@"tmp\";

        string cacheFileFullName = "";

        /// <summary>
        /// 缓存是否有效
        /// </summary>
        /// <returns> true 存在， false 不存在</returns>
        public bool CheckCache(string fileFullName)
        {
            int index = fileFullName.LastIndexOf("\\");
            if (index < 0)
            {
                Console.WriteLine("CheckFileChange error : fileFullName format wrong .({0})", fileFullName);
                return false;
            }
            bool isPast = true; //缓存失效

            cacheFileName = fileFullName.Substring(index + 1);
            index = cacheFileName.LastIndexOf(".");
            cacheFileName = cacheFileName.Substring(0, index);

            string newHashValue = FileUtil.GetFileHashValue(fileFullName);

            //cacheFileFullName = cacheFilePath + fileFullName.Replace(Program.InputPath, "").Replace(".code",cacheFilePostfix);
            cacheFileFullName = cacheFilePath + cacheFileName+ cacheFilePostfix;

            if (string.IsNullOrEmpty(newHashValue))
            {
                Console.WriteLine("CheckFileChange error : {0} is null", fileFullName);
                return false;
            }
            try
            {
                FileInfo fi = new FileInfo(cacheFileFullName);
                if (fi.Exists)
                {
                    FileStream fsRead = fi.OpenRead();
                    StreamReader sr = new StreamReader(fsRead);
                    string oldHashValue = sr.ReadLine();
                    bool isSame = CompareHashValue(oldHashValue, newHashValue);

                    DateTime lastWriteTime = fi.LastWriteTime; //缓存文件上次写入时间
                    sr.Close();
                    fsRead.Close();

                    if (isSame)
                    {
                        isPast = CompareCacheTime(fi.LastWriteTime);
                    }
                    else
                    {
                        isPast = true;
                    }
                }
                else
                {
                    isPast = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            if (isPast)
            {
                //Console.WriteLine( "{0} write success", fileFullName);
                FileUtil.WriteToFile(newHashValue, cacheFilePath, cacheFileName, cacheFilePostfix);
            }
            else
            {
                
            }
            return isPast;
        }


        /// <summary>
        /// 比较hash值
        /// </summary>
        /// <param name="oldHash"></param>
        /// <param name="newHash"></param>
        /// <returns>true 表示不需要生成</returns>
        private bool CompareHashValue(string oldHash, string newHash)
        {
            if (string.IsNullOrEmpty(newHash))
            {
                return true;
            }
            //比较两个哈希值
            if (oldHash == newHash)
            {
                //Console.WriteLine("两个文件相等");
                return true;
            }
            else
            {
                //Console.WriteLine("两个文件不等");
                return false;
            }
        }

        /// <summary>
        /// 比较缓存时间
        /// </summary>
        /// <param name="oldHash"></param>
        /// <param name="newHash"></param>
        /// <returns></returns>
        private bool CompareCacheTime(DateTime time)
        {
            ///十分钟失效
            if ((DateTime.Now - time).TotalMinutes > 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
