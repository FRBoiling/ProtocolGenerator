using System;
using System.IO;
using Google.Protobuf;
using Message.Server.Register;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestProtobuf()
        {
            MSG_Server_Register fromMsg = new MSG_Server_Register();
            fromMsg.Tag = new Server_Tag();
            fromMsg.Tag.GroupId = 1;
            fromMsg.Tag.SubId = 11;
            fromMsg.Tag.ServerType = 2;

            MemoryStream outputstream = new MemoryStream();
            ProtobufHelper.ProtobufHelper.ToStream(fromMsg, outputstream);

            outputstream.Seek(0, SeekOrigin.Begin);

            MSG_Server_Register toMsg = MSG_Server_Register.Parser.ParseFrom(outputstream);
        }

        [TestMethod]
        public void TestProtobufHelper()
        {
            MSG_Server_Register fromMsg = new MSG_Server_Register();
            fromMsg.Tag = new Server_Tag();
            fromMsg.Tag.GroupId = 1;
            fromMsg.Tag.SubId = 11;
            fromMsg.Tag.ServerType = 2;

            MemoryStream outputstream = new MemoryStream();
            ProtobufHelper.ProtobufHelper.ToStream(fromMsg, outputstream);

            outputstream.Seek(0, SeekOrigin.Begin);
            //MSG_Server_Register toMsg = MSG_Server_Register.Parser.ParseFrom(outputstream);

            MSG_Server_Register toMsg = new MSG_Server_Register();
            ProtobufHelper.ProtobufHelper.FromStream(toMsg, outputstream);
        }

  
    }
}
