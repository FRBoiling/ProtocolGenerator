using Google.Protobuf;
using System;
using System.IO;

namespace ProtobufHelper
{
    public class ProtobufHelper
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="stream"></param>
        public static void Serialize<T>(T message, MemoryStream outStream) where T : Google.Protobuf.IMessage
        {
            message.WriteTo(outStream);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inStream"></param>
        /// <returns></returns>
        public static T Deserialize<T>(MemoryStream inStream) where T : Google.Protobuf.IMessage
        {
            T msg = Activator.CreateInstance<T>();
            msg.MergeFrom(inStream.GetBuffer(), (int)inStream.Position, (int)inStream.Length);
            return msg;
        }
    }
}
