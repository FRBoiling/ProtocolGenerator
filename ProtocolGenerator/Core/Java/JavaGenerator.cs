/************************************************************************ 
 * 项目名称 :  ProtocolGenerator.CSharp       
 * 类 名 称 :  CSharpGenerator 
 * 类 描 述 : 
 * 版 本 号 :  v1.0.0.0  
 * 说    明 :      
 * 作    者 :  FReedom 
 * 创建时间 :  2018/4/21 星期五 12:28:28 
 * 更新时间 :  2018/4/21 星期五 12:28:28 
************************************************************************ 
 * Copyright @ BoilingBlood 2018. All rights reserved. 
************************************************************************/

namespace ProtocolGenerator.Core.Java
{
    public class JavaGenerator : IGenerator
    {
        public void Generate()
        {
            JavaClass_Id classGen = new JavaClass_Id();
            classGen.Generate();
        }
        public void Generate(Data data)
        {
            JavaClass_IdGenerator classGen = new JavaClass_IdGenerator(data);
            classGen.Generate();
            JavaClass_Proto protoGen = new JavaClass_Proto(data);
            protoGen.GenerateProto();
        }
    }
}
