using ProtoBuf;

namespace TcpLib
{
    public interface ITcpClient
    {
        void Process();
        bool Send<T>(T msg) where T : IExtensible;

        void Connect(string ip, ushort port);
        void InitParser();
        void InitTcp();

        void Exit();

    }
}