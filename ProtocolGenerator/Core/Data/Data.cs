/************************************************************************ 
 * 项目名称 :  ProtocolGenerator   
 * 项目描述 :      
 * 类 名 称 :  Data 
 * 版 本 号 :  v1.0.0.0  
 * 说    明 :      
 * 作    者 :  FReedom 
 * 创建时间 :  2018/4/20 星期五 16:48:51 
 * 更新时间 :  2018/4/20 星期五 16:48:51 
************************************************************************ 
 * Copyright @ BoilingBlood 2018. All rights reserved. 
************************************************************************/
using System.Collections.Generic;
using System.Text;

namespace ProtocolGenerator
{
    public class Data
    {
        string protoFullPath = string.Empty;

        /// <summary>
        ///"protoModel.code" just the "protoModel"
        /// </summary>
        string protoFileKey = string.Empty;
        /// <summary>
        /// package
        /// </summary>
        string protoPackageName = string.Empty;
        /// <summary>
        /// .proto content
        /// </summary>
        StringBuilder protoCode = new StringBuilder();
        /// <summary>
        /// Key is msgName,Value is msgId
        /// </summary>
        Dictionary<string,string>  dicName2Id = new Dictionary<string, string>();
        /// <summary>
        /// Key is msgId,Value is msgName
        /// </summary>
        Dictionary<string,string>  dicId2Name = new Dictionary<string, string>();


        public string ProtoFileKey { get => protoFileKey; set => protoFileKey = value; }
        public string ProtoPackageName { get => protoPackageName; set => protoPackageName = value; }
        public StringBuilder ProtoCode { get => protoCode; set => protoCode = value; }
        public Dictionary<string, string> DicName2Id { get => dicName2Id; set => dicName2Id = value; }
        public Dictionary<string, string> DicId2Name { get => dicId2Name; set => dicId2Name = value; }
    }
}
