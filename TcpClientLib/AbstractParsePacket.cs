using ProtoBuf;
using System.IO;

namespace TcpLib
{

    public abstract class AbstractParsePacket : IPacketOperate, IProtocolProcess
    {
        ProtocolProcessHandler _handler;

        public abstract void PackPacket<T>(T msg, out MemoryStream head, out MemoryStream body) where T : IExtensible;
        public abstract void CryptoPackPacket<T>(T msg, out MemoryStream head, out MemoryStream body) where T : IExtensible;

        public abstract void PackPacket<T>(T msg, int uid, out MemoryStream head, out MemoryStream body) where T : IExtensible;
        public abstract int UnpackPacket(MemoryStream stream);

        //public BlowFish BlowFishHandler;

        public AbstractParsePacket()
        {
            _handler = new ProtocolProcessHandler();
        }


        public void AddProcesser(uint id, ProtocolProcessHandler.Processer processer)
        {
            _handler.Add(id, processer);
        }
        public bool Process(uint id,MemoryStream stream,int uid = 0)
        {
            if (_handler.Process(id,stream,uid))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public abstract void Process();
    }
}
