using System;
using System.Collections.Generic;
using System.IO;

namespace TcpLib
{
    public class Packet : AbstractParsePacket
    {
        private Queue<KeyValuePair<UInt32, MemoryStream>> m_msgQueue = new Queue<KeyValuePair<uint, MemoryStream>>();
        private Queue<KeyValuePair<UInt32, MemoryStream>> m_deal_msgQueue = new Queue<KeyValuePair<uint, MemoryStream>>();

        public override int UnpackPacket(MemoryStream stream)
        {
            byte[] buffer = stream.GetBuffer();
            int offset = 0;
            int pos = 0;
            while (stream.Length > sizeof(ushort))
            {
                ushort size = BitConverter.ToUInt16(buffer, offset);
                offset += sizeof(ushort);
                if (size > stream.Length - offset)
                {
                    break;
                }

                UInt32 msg_id = BitConverter.ToUInt32(buffer, offset);
                offset += sizeof(UInt32);
                //byte[] content = new byte[size];
                //Array.Copy(buffer, offset , content, 0, size);
                MemoryStream msg = new MemoryStream(buffer, offset, size, true, true);
                lock (m_msgQueue)
                {
                    m_msgQueue.Enqueue(new KeyValuePair<uint, MemoryStream>(msg_id, msg));
                }
                offset += size ;
                pos = offset;
            }

            return pos;
        }

        public override void Process()
        {
            lock (m_msgQueue)
            {
                while (m_msgQueue.Count > 0)
                {
                    var msg = m_msgQueue.Dequeue();
                    m_deal_msgQueue.Enqueue(msg);
                }
            }
            while (m_deal_msgQueue.Count > 0)
            {
                var msg = m_deal_msgQueue.Dequeue();
                Process(msg.Key, msg.Value);
            }
        }

        public override void PackPacket<T>(T msg, out MemoryStream head, out MemoryStream body)
        {
            body = new MemoryStream();
            ProtoBuf.Serializer.Serialize(body, msg);

            head = new MemoryStream(sizeof(ushort) + sizeof(uint));
            ushort len = (ushort)body.Length;
            head.Write(BitConverter.GetBytes(len), 0, sizeof(ushort));
            head.Write(BitConverter.GetBytes(Protocol.MsgId.Id<T>.Value), 0, sizeof(uint));
        }

        public override void CryptoPackPacket<T>(T msg, out MemoryStream head, out MemoryStream body)
        {
            body = new MemoryStream();
            ProtoBuf.Serializer.Serialize(body, msg);
            ushort sourceStreamLen = (ushort)body.Length;
            //if (BlowFishHandler != null && sourceStreamLen>0)
            //{
            //    body = BlowFishHandler.Encrypt_CBC(body);
            //}

            head = new MemoryStream(sizeof(ushort) + sizeof(uint));
            ushort len = (ushort)body.Length;
            head.Write(BitConverter.GetBytes(len), 0, sizeof(ushort));
            head.Write(BitConverter.GetBytes(sourceStreamLen), 0, sizeof(ushort));
            head.Write(BitConverter.GetBytes(Protocol.MsgId.Id<T>.Value), 0, sizeof(uint));
        }

        public override void PackPacket<T>(T msg, int uid, out MemoryStream head, out MemoryStream body)
        {
            throw new NotImplementedException();
        }
    }
}
