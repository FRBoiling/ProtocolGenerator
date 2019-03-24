
using System;

namespace ProtocolGenerator.Core.Lua
{
    public class LuaGenerator : IGenerator
    {
        public void Generate()
        {
            //JavaClass_Id classGen = new JavaClass_Id();
            //classGen.Generate();
            Console.WriteLine("LuaGenerater Generate()");
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
