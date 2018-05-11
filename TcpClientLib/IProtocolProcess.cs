using System.IO;

namespace TcpLib
{
    public interface IProtocolProcess
    {
        void AddProcesser(uint id, ProtocolProcessHandler.Processer processer);
        bool Process(uint id, MemoryStream stream, int uid = 0);
        void Process();
    }
}