/************************************************************************ 
 * 项目名称 :  ProtocolGenerator   
 * 项目描述 :      
 * 类 名 称 :  IGenerator 
 * 版 本 号 :  v1.0.0.0  
 * 说    明 :      
 * 作    者 :  Boiling 
 * 创建时间 :  2018/4/20 星期五 15:55:08 
 * 更新时间 :  2018/4/20 星期五 15:55:08 
************************************************************************ 
 * Copyright @ BoilingBlood 2018. All rights reserved. 
************************************************************************/
namespace ProtocolGenerator
{
    public interface IGenerator
    {
        void Generate();
        void Generate(Data data);
    }
}
