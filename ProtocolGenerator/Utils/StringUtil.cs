using System;
using System.Text.RegularExpressions;

namespace ProtocolBuffersGenerator.Utils
{
    public class StringUtil
    {
        /// <summary>
        /// 判断是否十六进制格式 的ID串
        /// <param name="str"></param>
        /// <returns></returns>
        //public static bool IsHexadecimal(string str)
        public static bool IsHexadecimalId(string str)
        {
            string head = str.Substring(0, 2);
            if (!head.Equals("0x"))
            {
                return false;
            }
            else
            {
                string body = str.Substring(2);
                body = body.TrimStart('0');
                //考虑到32位去掉符号位为31位。作为协议Id索性取7位16进制
                if (body.Length > 7 ||body.Length<1)
                {
                    Console.WriteLine("error id {0} ,maximum number must not exceed 7-bit hexadecimal!", str);
                    return false;
                }
                const string PATTERN = @"[A-Fa-f0-9]+$";
                return Regex.IsMatch(str, PATTERN);
            }
        }
    }
}
