/************************************************************************ 
 * 项目名称 :  ProtocolGenerater.CSharp       
 * 类 名 称 :  CSharpClass_Id 
 * 类 描 述 :  生成静态字段Id类
 * 版 本 号 :  v1.0.0.0  
 * 说    明 :      
 * 作    者 :  FReedom 
 * 创建时间 :  2018/4/20 星期五 17:44:03 
 * 更新时间 :  2018/4/20 星期五 17:44:03 
************************************************************************ 
 * Copyright @ BoilingBlood 2018. All rights reserved. 
************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolGenerater.CSharp
{
    class CSharpClass_Id:AbstractFileModel
    {
        CSharpCodeGenerater _generater = new CSharpCodeGenerater();
        public const string msgIdPackageName = "Protocol.MsgId";
        public const string className = "Id";

        public CSharpClass_Id()
        {
            m_filePath = Program.OutputPath + @"CSharp\";
            m_fileName = "Id";
            m_fileSuffix = ".cs";
        }

        protected override StringBuilder GenerateClassCode()
        {
            StringBuilder fileContent = new StringBuilder();
            StringBuilder fileComments = _generater.ClassCommentsFrame();
            StringBuilder fileIncludeHeads = _generater.IncludeHeadFrame();
            StringBuilder classAttr = _generater.AttrFrame("public static uint", "Value");
            List<StringBuilder> attrs = new List<StringBuilder>();
            attrs.Add(classAttr);
            List<StringBuilder> methods = new List<StringBuilder>();
            StringBuilder classFrame = _generater.ClassFrame("static class " + className + "<T>", attrs, methods);
            StringBuilder nameSpaceFrame = _generater.NameSpaceFrame(msgIdPackageName, classFrame);

            fileContent.Append(fileComments);
            fileContent.Append(fileIncludeHeads);
            fileContent.Append(nameSpaceFrame);

            return fileContent;
        }
    }
}
