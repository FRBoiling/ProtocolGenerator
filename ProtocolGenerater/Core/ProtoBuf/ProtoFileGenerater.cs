/************************************************************************ 
 * 项目名称 :  ProtocolGenerater.ProtoBuf       
 * 类 名 称 :  ProtoFileGenerater 
 * 类 描 述 :  生成.proto文件
 * 版 本 号 :  v1.0.0.0  
 * 说    明 :      
 * 作    者 :  FReedom 
 * 创建时间 :  2018/4/20 星期五 18:53:05 
 * 更新时间 :  2018/4/20 星期五 18:53:05 
************************************************************************ 
 * Copyright @ BoilingBlood 2018. All rights reserved. 
************************************************************************/
using System.Text;

namespace ProtocolGenerater.ProtoBuf
{
    public class ProtoFileGenerater: AbstractFileModel
    {
        public string fileName=string.Empty;
        public string fileSuffix = ".proto";
        Data _data = null;

        public ProtoFileGenerater(Data data)
        {
            _data = data;
            string tempPath = _data.ProtoPackageName.Replace('.', '\\');
            m_filePath = Program.OutputPath + @"Proto\" + tempPath + @"\";
            m_fileName = data.ProtoFileKey;
            m_fileSuffix = ".proto";
        }

        protected override StringBuilder GenerateClassCode()
        {
            return _data.ProtoCode;
        }
    }
}
