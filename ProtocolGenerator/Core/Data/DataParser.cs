/************************************************************************ 
 * 项目名称 :  ProtocolGenerator   
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
using ProtocolBuffersGenerator.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProtocolGenerator
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

                    List<string> formatArr = strProtoline.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    if (formatArr.Count>1)
                    {
                        data.ProtoPackageName = formatArr[1].Replace(";", "");
                    }
                    else
                    {
                        Console.WriteLine("please check the package name!");
                    }
                }
                else if (line.StartsWith("message"))
                {
                    strProtoline = line;
                    List<string> formatArr = strProtoline.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    if (formatArr.Count > 2)
                    {
                        if (formatArr.Count >= 2)
                        {
                            if (formatArr[0] == "message")
                            {
                                if (data.DicName2Id.ContainsKey(formatArr[1]))
                                {
                                    Console.WriteLine("protobuf data got an error at line ->{0}<- : repeat name {1}", line, formatArr[1]);
                                    return null;
                                }
                                else
                                {
                                    if (formatArr.Count >2)
                                    {
                                        if (StringUtil.IsHexadecimalId(formatArr[2]))
                                        {
                                            if (data.DicId2Name.ContainsKey(formatArr[2]))
                                            {
                                                Console.WriteLine("protobuf data got an error at line ->{0}<- : repeat id {1}", line, formatArr[2]);
                                                return null;
                                            }
                                            else
                                            {
                                                data.DicId2Name.Add(formatArr[2], formatArr[1]);
                                                data.DicName2Id.Add(formatArr[1], formatArr[2]);
                                            }
                                        }
                                        else
                                        {
                                            int id;
                                            if (int.TryParse(formatArr[2], out id))
                                            {
                                                data.DicId2Name.Add(formatArr[2], formatArr[1]);
                                                data.DicName2Id.Add(formatArr[1], formatArr[2]);
                                            }
                                            else
                                            {
                                                Console.WriteLine("protobuf data got an error at line ->{0}<- : wrong id {1}", line, formatArr[2]);
                                                return null;
                                            }
                                        }
                                    }
                                    strProtoline = formatArr[0] + " " + formatArr[1];
                                    data.ProtoCode.Append(Environment.NewLine);
                                    data.ProtoCode.Append(strProtoline);
                                    data.ProtoCode.Append(Environment.NewLine);
                                }
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
                
                    int tmpIndex = strProtoline.IndexOf("//");
                    if (tmpIndex == -1)
                    {
                    }
                    else
                    {
                        strProtoline = strProtoline.Substring(0, tmpIndex);
                    }
                    if (string.IsNullOrEmpty(strProtoline) )
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
