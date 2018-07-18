/************************************************************************ 
 * 项目名称 :  ProtocolGenerater   
 * 项目描述 :      
 * 类 名 称 :  DataParser 
 * 版 本 号 :  v1.0.0.0  
 * 说    明 :      
 * 作    者 :  FReedom 
 * 创建时间 :  2018/4/20 星期五 16:55:16 
 * 更新时间 :  2018/4/20 星期五 16:55:16 
************************************************************************ 
 * Copyright @ BoilingBlood 2018. All rights reserved. 
************************************************************************/
using ProtocolBuffersGenerater.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolGenerater
{
    public class DataParser
    {
        public static Data ParseCodeFile(string fileFullName)
        {
            Data data = new Data();
            if (fileFullName == null)
            {
                Console.WriteLine("ParseCodeFile error : fileFullName is null.");
                return null;
            }

            int index = fileFullName.LastIndexOf("\\");
            if (index<0)
            {
                Console.WriteLine("ParseCodeFile error : fileFullName format wrong .({0})", fileFullName);
                return null;
            }

            data.ProtoFileKey = fileFullName.Substring(index + 1);
            index = data.ProtoFileKey.LastIndexOf(".");
            data.ProtoFileKey = data.ProtoFileKey.Substring(0, index);

            IEnumerable<string> lines = File.ReadAllLines(fileFullName);
            foreach (string line in lines)
            {
                string strProtoline = "";
                if (line.StartsWith("package"))
                {
                    strProtoline = line;
                    data.ProtoCode.Append(strProtoline);
                    data.ProtoCode.Append(Environment.NewLine);

                    index = line.IndexOf(" ");
                    string packageName = string.Empty;
                    packageName = line.Substring(index);
                    packageName = packageName.Replace(" ", "");
                    packageName = packageName.Replace(";", "");
                    data.ProtoPackageName = packageName;
                }
                else if (line.StartsWith("message"))
                {
                    strProtoline = line;
                    var arr = strProtoline.Split(' ');
                    List<string> formatArr = new List<string>();
                    if (arr.Length > 2)
                    {
                        foreach (var v in arr)
                        {
                            if (v == " ")
                            {
                            }
                            else
                            {
                                formatArr.Add(v);
                            }
                        }
                        if (formatArr.Count == 3)
                        {
                            if (arr[0] == "message")
                            {
                                if (data.DicName2Id.ContainsKey(arr[1]))
                                {
                                    Console.WriteLine("protobuf data got an error at line ->{0}<- : repeat name {1}", line,arr[1]);
                                    return null;
                                }
                                else
                                {
                                    if (data.DicId2Name.ContainsKey(arr[2]))
                                    {
                                        Console.WriteLine("protobuf data got an error at line ->{0}<- : repeat id {1}", line,arr[2]);
                                        return null;
                                    }
                                    else
                                    {
                                        if (StringUtil.IsHexadecimalId(arr[2]))
                                        {
                                            data.DicId2Name.Add(arr[2], arr[1]);
                                        }
                                        else
                                        {
                                            int id;
                                            if (int.TryParse(arr[2], out id))
                                            {
                                                data.DicId2Name.Add(arr[2], arr[1]);
                                            }
                                            else
                                            {
                                                Console.WriteLine("protobuf data got an error at line ->{0}<- : wrong id {1}", line, arr[2]);
                                                return null;
                                            }
                                        }
                                        data.DicName2Id.Add(arr[1], arr[2]);
                                    }
                                }
                                strProtoline = strProtoline.Replace(arr[2], "");
                                data.ProtoCode.Append(Environment.NewLine);
                                data.ProtoCode.Append(strProtoline);
                                data.ProtoCode.Append(Environment.NewLine);
                            }
                            else
                            {
                                Console.WriteLine("protobuf data got an error format {0}",line);
                                return null;
                            }
                        }
                    }
                }
                else if (line.StartsWith("//"))
                {
                    //The line is annotation 
                }
                else
                {
                    strProtoline = line;
                    if (string.IsNullOrEmpty(strProtoline) || string.IsNullOrWhiteSpace(strProtoline))
                    {
                    }
                    else
                    {
                        data.ProtoCode.Append(strProtoline);
                        data.ProtoCode.Append(Environment.NewLine);
                    }
                }
            }
            return data;
        }
    }
}
