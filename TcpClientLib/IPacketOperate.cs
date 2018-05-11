using System.IO;
using ProtoBuf;

namespace TcpLib
{
    public interface IPacketOperate
    {
        void PackPacket<T>(T msg, out MemoryStream head, out MemoryStream body) where T : IExtensible;
        void CryptoPackPacket<T>(T msg, out MemoryStream head, out MemoryStream body) where T : IExtensible;
        void PackPacket<T>(T msg,int uid, out MemoryStream head, out MemoryStream body) where T : IExtensible;
        int UnpackPacket(MemoryStream stream);
    }
}