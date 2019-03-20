using ProtocolGenerater;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolBuffersGenerater.Core.Lua
{
        public class LuaGenerater : IGenerater
        {
            public void Generate()
            {
            //JavaClass_Id classGen = new JavaClass_Id();
            //classGen.Generate();
            Console.WriteLine("Generate");
            }
            public void Generate(Data data)
            {
                //JavaClass_IdGenerater classGen = new JavaClass_IdGenerater(data);
                //classGen.Generate();
                Lua_Proto protoGen = new Lua_Proto(data);
                protoGen.GenerateProto();
            }
        }
}
