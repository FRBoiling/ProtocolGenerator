/************************************************************************ 
 * 项目名称 :  ProtocolGenerater.CSharp       
 * 类 名 称 :  CSharpGenerater 
 * 类 描 述 : 
 * 版 本 号 :  v1.0.0.0  
 * 说    明 :      
 * 作    者 :  FReedom 
 * 创建时间 :  2018/4/21 星期五 12:28:28 
 * 更新时间 :  2018/4/21 星期五 12:28:28 
************************************************************************ 
 * Copyright @ BoilingBlood 2018. All rights reserved. 
************************************************************************/

namespace ProtocolGenerater.Core.Java
{
    public class JavaGenerater : IGenerater
    {
        public void Generate()
        {
            JavaClass_Id classGen = new JavaClass_Id();
            classGen.Generate();
        }
        public void Generate(Data data)
        {
            JavaClass_IdGenerater classGen = new JavaClass_IdGenerater(data);
            classGen.Generate();
            JavaClass_Proto protoGen = new JavaClass_Proto(data);
            protoGen.GenerateProto();
        }
    }
}
