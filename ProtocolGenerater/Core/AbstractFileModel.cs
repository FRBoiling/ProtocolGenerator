/************************************************************************ 
 * 项目名称 :  ProtocolGenerater       
 * 类 名 称 :  AbstratFileModel 
 * 类 描 述 : 
 * 版 本 号 :  v1.0.0.0  
 * 说    明 :      
 * 作    者 :  FReedom 
 * 创建时间 :  2018/4/20 星期五 18:58:29 
 * 更新时间 :  2018/4/20 星期五 18:58:29 
************************************************************************ 
 * Copyright @ BoilingBlood 2018. All rights reserved. 
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolGenerater
{
    public abstract class AbstractFileModel
    {
        protected string m_filePath = string.Empty;
        protected string m_fileName = string.Empty;
        protected string m_fileSuffix = string.Empty;
        public string FileFullName { get => m_filePath+ m_fileName+ m_fileSuffix;}
        public string FileSimpleName { get =>  m_fileName + m_fileSuffix; }
        protected abstract StringBuilder GenerateClassCode();
    
        public bool Generate()
        {
            StringBuilder fileContent = GenerateClassCode();
            if (FileUtil.WriteToFile(fileContent, m_filePath, m_fileName, m_fileSuffix))
            {
                Console.WriteLine("{0} generate sucess", FileSimpleName);
                Console.WriteLine(">>{0}", FileFullName);
                return true;
            }
            else
            {
                Console.WriteLine("{0} generate fail", FileSimpleName);
                return false;
            }
        }
    }
}
