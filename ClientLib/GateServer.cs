using System;
using TcpLib;

namespace ClientLib
{
    public partial class GateServer:AbstractTcpClient
    {
        ServerInfo _tag = new ServerInfo();
        public ServerInfo Tag
        {
            get { return _tag; }
        }

        public GateServer()
        {
            _tag.Type = ServerType.Gate;
        }

        public GateServer(string ip, ushort port)
            : base(ip, port)
        {
            _tag.Ip = ip;
            _tag.Port = port;
        }

        protected override void BindResponser()
        {
            Protocol.Gate.Client.IdGenerater.GenerateId();
            Protocol.Client.Gate.IdGenerater.GenerateId();
        }

        protected override void ConnectedComplete()
        {
            Console.WriteLine("connected to {0}", Tag.Type);
        }

        protected override void ReConnectedComplete()
        {
            Console.WriteLine("re connected to {0}", Tag.Type);
        }

        protected override void ProcessLogic()
        {

        }

        protected override void DisconnectComplete()
        {
            Console.WriteLine("switch off from {0}", Tag.GetServerTagString());
        }
    }
}
