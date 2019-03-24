/************************************************************************ 
 * 项目名称 :  ProtocolGenerator   
 * 项目描述 :      
 * 类 名 称 :  GeneratorManager 
 * 版 本 号 :  v1.0.0.0  
 * 说    明 :      
 * 作    者 :  Boiling 
 * 创建时间 :  2018/4/20 星期五 15:55:08 
 * 更新时间 :  2018/4/20 星期五 15:55:08 
************************************************************************ 
 * Copyright @ BoilingBlood 2018. All rights reserved. 
************************************************************************/
using ProtocolGenerator.Core;
using ProtocolGenerator.Core.CSharp;
using ProtocolGenerator.Core.Java;
using ProtocolGenerator.Core.Lua;
using ProtocolGenerator.ProtoBuf;
using System.Collections.Generic;

namespace ProtocolGenerator
{
    public class GeneratorManager
    {
        private static GeneratorManager _inst;
        private GeneratorManager()
        {
        }
        public static GeneratorManager GetInstance()
        {
            if (_inst == null)
            {
                _inst = new GeneratorManager();
            }
            return _inst;
        }

        private Dictionary<GeneratorType, IGenerator> _generaters = new Dictionary<GeneratorType, IGenerator>();

        public void Init(string type)
        {
            switch (type)
            {
                case "all":
                    Add(GeneratorType.Proto,new ProtoGenerator());
                    Add(GeneratorType.CSharp, new CSharpGenerator());
                    Add(GeneratorType.Java, new JavaGenerator());
                    Add(GeneratorType.Lua,new LuaGenerator());
                    break;
                case "java":
                    Add(GeneratorType.Proto, new ProtoGenerator());
                    Add(GeneratorType.Java, new JavaGenerator());
                    break;
                case "CSharp":
                    Add(GeneratorType.Proto, new ProtoGenerator());
                    Add(GeneratorType.CSharp, new CSharpGenerator());
                    break;
                case "Lua":
                    Add(GeneratorType.Proto, new ProtoGenerator());
                    Add(GeneratorType.Lua,new LuaGenerator());
                    break;
                case "default":
                    Add(GeneratorType.Proto,new ProtoGenerator());
                    Add(GeneratorType.CSharp, new CSharpGenerator());
                    break;
                case "CSharpId":
                    Add(GeneratorType.CSharp, new CSharpGenerator());
                    break;
                case "JavaId":
                    Add(GeneratorType.Java, new JavaGenerator());
                    break;
                default:
                    break;
            }
        }

        private void Add(GeneratorType type, IGenerator Generator)
        {
            if (_generaters.ContainsKey(type))
            {

            }
            else
            {
                _generaters.Add(type, Generator);
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
