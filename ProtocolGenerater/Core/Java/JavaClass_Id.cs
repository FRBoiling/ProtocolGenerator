/************************************************************************ 
 * 项目名称 :  ProtocolGenerater.Core.Java       
 * 类 名 称 :  JavaClass_Id 
 * 类 描 述 : 
 * 版 本 号 :  v1.0.0.0  
 * 说    明 :      
 * 作    者 :  FReedom 
 * 创建时间 :  2018/4/21 星期六 11:28:25 
 * 更新时间 :  2018/4/21 星期六 11:28:25 
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
    public class JavaClass_Id : AbstractFileModel
    {
        JavaCodeGenerater _generater = new JavaCodeGenerater();
        public const string msgIdPackageName = "Protocol.MsgId";
        public const string className = "Id";

        public JavaClass_Id()
        {
            string tempPath = msgIdPackageName.Replace('.', '\\');
            m_filePath = Program.OutputPath + @"Java\" + tempPath + @"\";
            m_fileName = "Id";
            m_fileSuffix = ".java";
        }

        protected override StringBuilder GenerateClassCode()
        {
            StringBuilder fileContent = new StringBuilder();
            StringBuilder fileComments = _generater.ClassCommentsFrame();
            StringBuilder fileIncludeHeads = _generater.IncludeHeadFrame(getInclude());
            StringBuilder classFrame = _generater.ClassFrame("class " + className, getAttrs(), getMethods(),0);
            fileContent.Append(fileComments);
            fileContent.Append(fileIncludeHeads);
            fileContent.Append(classFrame);
            return fileContent;
        }

        private List<string> getInclude()
        {
            //package Protocol;
            //import java.util.HashMap;
            //import com.google.protobuf.MessageLite;
            List<string> list = new List<string>();
            list.Add("package " + msgIdPackageName + ";");
            list.Add("import java.util.HashMap;");
            list.Add("import com.google.protobuf.MessageLite;");
            return list;
        }

        private List<StringBuilder> getMethods()
        {
            List<StringBuilder> attrs = new List<StringBuilder>();
            attrs.Add(_generater.AttrFrame("return", "inst", 1));
            List<StringBuilder> methods = new List<StringBuilder>();
            //public static final Id getInst()
            //{
            //    return inst;
            //}
            StringBuilder classMethod = _generater.MethodFrame("static final Id", "getInst()", attrs,1);
            //public int getMessageId(Class<?> clazz)
            //{
            //    return clazzIdMap.get(clazz);
            //}
            attrs = new List<StringBuilder>();
            attrs.Add(_generater.AttrFrame("return", "clazzIdMap.get(clazz)", 1));
            methods.Add(classMethod);
            classMethod = _generater.MethodFrame("int", "getMessageId(Class<?> clazz)", attrs,1);
            methods.Add(classMethod);
            //public int SetMessage(Class<? extends MessageLite> msgClass, int messageId)
            //{
            //    return clazzIdMap.put(msgClass, messageId);
            //}
            attrs = new List<StringBuilder>();
            attrs.Add(_generater.AttrFrame("return", "clazzIdMap.put(msgClass, messageId)", 1));
            classMethod = _generater.MethodFrame("int", "SetMessage(Class<? extends MessageLite> msgClass, int messageId)", attrs,1);

            methods.Add(classMethod);
            return methods;
        }

        public List<StringBuilder> getAttrs()
        {
            List<StringBuilder> attrs = new List<StringBuilder>();

            //private static final Id inst = new Id();
            StringBuilder classAttr = _generater.AttrFrame("private static final Id", "inst = new Id()",1);
            attrs.Add(classAttr);
            //private HashMap<Class<? extends MessageLite>, Integer> clazzIdMap = new HashMap<Class<? extends MessageLite>, Integer>();
            classAttr = _generater.AttrFrame("private HashMap<Class<? extends MessageLite>, Integer>", "clazzIdMap = new HashMap<Class<? extends MessageLite>, Integer>()",1);
            attrs.Add(classAttr);
            return attrs;
        }
    }
}
