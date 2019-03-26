/************************************************************************ 
 * 项目名称 :  ProtocolGenerator.CSharp       
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
using System.Collections.Generic;
using System.Text;

namespace ProtocolGenerator.Core.CSharp
{
    class CSharpClass_Id:AbstractFileModel
    {
        CSharpCodeGenerator _generater = new CSharpCodeGenerator();
        public const string msgIdPackageName = "Message.IdGenerator";
        public const string className = "Id";

        public CSharpClass_Id()
        {
            string tempPath = msgIdPackageName.Replace('.', '\\');
            m_filePath = Program.OutputPath + @"\CSharp\"+ tempPath+@"\";
            m_fileName = "Id";
            m_fileSuffix = ".cs";
        }

        protected override StringBuilder GenerateClassCode()
        {
            StringBuilder fileContent = new StringBuilder();
            StringBuilder fileComments = _generater.ClassCommentsFrame();
            StringBuilder fileIncludeHeads = _generater.IncludeHeadFrame(null);
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
