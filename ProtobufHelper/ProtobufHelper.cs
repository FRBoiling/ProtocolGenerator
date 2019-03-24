﻿using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static void ToStream<T>(T message, MemoryStream stream) where T : Google.Protobuf.IMessage
        {
            message.WriteTo(stream);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static T FromStream<T>(T message, MemoryStream stream) where T : Google.Protobuf.IMessage
        {
            message.MergeFrom(stream.GetBuffer(), (int)stream.Position, (int)stream.Length);
            return message;
        }
    }
}
