using System.IO;

namespace TcpLib
{
    public class TcpAsyncCallBack
    {
        public delegate bool AsyncConnectCallback(bool ret);
        public delegate bool AsyncAcceptCallback(bool ret);
        public delegate void AsyncReadCallback(MemoryStream stream);
        public delegate bool AsyncDisconnectCallback();
    }
}
