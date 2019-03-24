/************************************************************************ 
 * 项目名称 :  ProtocolGenerator.ProtoBuf       
 * 类 名 称 :  ProtoFileGenerator 
 * 类 描 述 :  生成.proto文件
 * 版 本 号 :  v1.0.0.0  
 * 说    明 :      
 * 作    者 :  FReedom 
 * 创建时间 :  2018/4/20 星期五 18:53:05 
 * 更新时间 :  2018/4/20 星期五 18:53:05 
************************************************************************ 
 * Copyright @ BoilingBlood 2018. All rights reserved. 
************************************************************************/
using System;
using System.Text;

namespace ProtocolGenerator.ProtoBuf
{
    public class ProtoFileGenerator : AbstractFileModel
    {
        public string fileName = string.Empty;
        public string fileSuffix = ".proto";
        Data _data = null;

        public ProtoFileGenerator(Data data)
        {
            _data = data;
            m_fileName = data.ProtoFileKey;
            m_fileSuffix = ".proto";

            string tempPath = _data.ProtoPackageName.Replace('.', '\\');
            #region 指定路径
            //m_filePath = Program.OutputPath + @"Proto\" + tempPath + @"\";
            #endregion
            #region 当前路径与code同路径
            string tempfilename = m_fileName + ".code";
            m_filePath = Program.Filename.Replace(tempfilename, "");
            #endregion
       
           // Console.WriteLine("!!!m_filePath {0} m_fileName {1} tempfilename{2}", m_filePath, m_fileName, tempfilename);
        }

        protected override StringBuilder GenerateClassCode()
        {
            return _data.ProtoCode;
        }
    }
}
