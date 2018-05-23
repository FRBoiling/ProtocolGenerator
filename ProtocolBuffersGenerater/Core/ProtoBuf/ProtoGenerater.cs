/************************************************************************ 
 * 项目名称 :  ProtocolGenerater.ProtoBuf       
 * 类 名 称 :  ProtoBufGenerater 
 * 类 描 述 :  生成.proto文件
 * 版 本 号 :  v1.0.0.0  
 * 说    明 :      
 * 作    者 :  FReedom 
 * 创建时间 :  2018/4/20 星期五 18:49:43 
 * 更新时间 :  2018/4/20 星期五 18:49:43 
************************************************************************ 
 * Copyright @ BoilingBlood 2018. All rights reserved. 
************************************************************************/

namespace ProtocolGenerater.ProtoBuf
{
    public class ProtoGenerater : IGenerater
    {
        public void Generate(Data data)
        {
            ProtoFileGenerater fileGenerater = new ProtoFileGenerater(data);
            fileGenerater.Generate();
        }

        public void Generate()
        {
            return;
        }
    }
}
