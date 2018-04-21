/************************************************************************ 
 * 项目名称 :  ProtocolGenerater.Core.Java       
 * 类 名 称 :  JavaClass_IdGenerater 
 * 类 描 述 : 
 * 版 本 号 :  v1.0.0.0  
 * 说    明 :      
 * 作    者 :  FReedom 
 * 创建时间 :  2018/4/21 星期六 12:32:13 
 * 更新时间 :  2018/4/21 星期六 12:32:13 
************************************************************************ 
 * Copyright @ BoilingBlood 2018. All rights reserved. 
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolGenerater.Core.Java
{
    public class JavaClass_IdGenerater : AbstractFileModel
    {
        JavaCodeGenerater _generater = new JavaCodeGenerater();
        string className = "IdGenerater";
        Data _data = null;
        public JavaClass_IdGenerater(Data data)
        {
            _data = data;
            string tempPath = _data.ProtoPackageName.Replace('.', '\\');
            m_filePath = Program.OutputPath + @"Java\" + tempPath + @"\";
            m_fileName = _data.ProtoFileKey;
            m_fileSuffix = "IdGenerater.java";
            className = m_fileName + className;
        }
        private StringBuilder GenerateIdKey(string key)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Id.getInst().SetMessage(");
            sb.Append(_data.ProtoPackageName+"."+_data.ProtoFileKey+".");
            sb.Append(key);
            sb.Append(".class,");
            return sb;
        }

        private StringBuilder GenerateIdValue(string value)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(value+")");
            return sb;
        }

        private List<string> getInclude()
        {
            //package Protocol.Client.C2G;
            //import Protocol.Id;
            //import Protocol.Client.C2G.CGCode.MSG_C2G_Heartbeat;
            List<string> list = new List<string>();
            list.Add("package " + _data.ProtoPackageName  + ";");
            list.Add("import "+ JavaClass_Id.msgIdPackageName + "." + JavaClass_Id.className + ";");
            //list.Add("import com.google.protobuf.MessageLite;");
            return list;
        }

        protected override StringBuilder GenerateClassCode()
        {
            StringBuilder fileContent = new StringBuilder();
            StringBuilder fileComments = _generater.ClassCommentsFrame();
            StringBuilder fileIncludeHeads = _generater.IncludeHeadFrame(getInclude());

            List<StringBuilder> attrs = new List<StringBuilder>();
            foreach (var item in _data.DicName2Id)
            {
                StringBuilder classAttrFrame = _generater.AttrFrame(GenerateIdKey(item.Key).ToString(), GenerateIdValue(item.Value).ToString(), 1);
                attrs.Add(classAttrFrame);
            }

            StringBuilder classMethodFrame = _generater.MethodFrame("static void", "GenerateId()", attrs, 1);
            List<StringBuilder> methods = new List<StringBuilder>();
            methods.Add(classMethodFrame);

            StringBuilder classFrame = _generater.ClassFrame("class " + className, null, methods,0);

            fileContent.Append(fileComments);
            fileContent.Append(fileIncludeHeads);
            fileContent.Append(classFrame);
            return fileContent;
        }
    }
}
