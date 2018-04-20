/************************************************************************ 
 * 项目名称 :  ProtocolGenerater   
 * 项目描述 :      
 * 类 名 称 :  GeneraterManager 
 * 版 本 号 :  v1.0.0.0  
 * 说    明 :      
 * 作    者 :  Boiling 
 * 创建时间 :  2018/4/20 星期五 15:55:08 
 * 更新时间 :  2018/4/20 星期五 15:55:08 
************************************************************************ 
 * Copyright @ BoilingBlood 2018. All rights reserved. 
************************************************************************/
using ProtocolGenerater.Core;
using ProtocolGenerater.CSharp;
using ProtocolGenerater.ProtoBuf;
using System.Collections.Generic;

namespace ProtocolGenerater
{
    public class GeneraterManager
    {
        private static GeneraterManager _inst;
        private GeneraterManager()
        {
        }
        public static GeneraterManager GetInstance()
        {
            if (_inst == null)
            {
                _inst = new GeneraterManager();
            }
            return _inst;
        }

        private Dictionary<GeneraterType, IGenerater> _generaters = new Dictionary<GeneraterType, IGenerater>();

        public void Init(string type)
        {
            switch (type)
            {
                case "all":
                    Add(GeneraterType.Proto,new ProtoGenerater());
                    Add(GeneraterType.CSharp, new CSharpGenerater());
                    //Add(GeneraterType.Java, new CSharpGenerater());
                    //Add(GeneraterType.CPlusPlus,new CSharpGenerater());
                    break;
                case "default":
                    Add(GeneraterType.Proto,new ProtoGenerater());
                    Add(GeneraterType.CSharp, new CSharpGenerater());
                    //Add(GeneraterType.Java, new CSharpGenerater());
                    //Add(GeneraterType.CPlusPlus,new CSharpGenerater());
                    break;
                case "id":
                    Add(GeneraterType.CSharp, new CSharpGenerater());
                    //Add(GeneraterType.Java, new CSharpGenerater());
                    break;
                //case "2":
                //    Add(GeneraterType.CSharp, new CSharpGenerater());
                //    Add(GeneraterType.CPlusPlus,new CSharpGenerater());
                //    break;
                default:
                    break;
            }
        }

        private void Add(GeneraterType type, IGenerater generater)
        {
            if (_generaters.ContainsKey(type))
            {

            }
            else
            {
                _generaters.Add(type, generater);
            }
        }

        public void GenerateId()
        {
            foreach (var gen in _generaters)
            {
                gen.Value.Generate();
            }
        }

        public void Run()
        {
            Dictionary<string, Data> dates = DataManager.GetInstance().DataDic;
            if (dates == null)
            {
                return;
            }

            foreach (var item in dates)
            {
                foreach (var gen in _generaters)
                {
                    gen.Value.Generate(item.Value);
                }
            }
        }

    }
}
