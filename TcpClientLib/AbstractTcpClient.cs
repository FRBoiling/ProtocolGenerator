using System;
using System.IO;
using System.Threading;
using TcpLib.TcpSrc;


namespace TcpLib
{
    public abstract class AbstractTcpClient : ITcpClient
    {
        private bool _needReConnected = true;
        private ITcp _tcp = new Tcp();

        private string _ip;
        public string Ip
        {
            get { return _ip; }
        }

        private ushort _port;
        public ushort Port
        {
            get { return _port; }
        }

        IPacketOperate _packetOperate;
        IProtocolProcess _protocolProcess;

        public AbstractTcpClient()
        {
        }

        public AbstractTcpClient(string ip,ushort port)
        {
            _ip = ip;
            _port = port;

            InitParser();
            BindResponser();
            InitTcp();
        }

        public void Init(string ip, ushort port)
        {
            _ip = ip;
            _port = port;

            InitParser();
            BindResponser();
            InitTcp();
        }

        protected abstract void BindResponser();

        public void InitTcp()
        {
            _tcp.OnConnect = OnConnect;
            _tcp.OnRecv = OnRecv;
            _tcp.OnDisconnect = OnDisconnect;
        }


        public void ReConnect()
        {
            _needReConnected = true;
            Connect(Ip, Port);
        }

        public void Connect(string ip,ushort port)
        {
            if (_tcp == null)
            {
                _tcp = new Tcp();
                InitTcp();
            }

            if (!_tcp.Connect(ip, port))
            {
                Connect(ip,port);
            }
            else
            {
                //连接
                Thread.Sleep(1000);
            }
        }

        private bool OnConnect(bool ret)
        {
            if (ret)
            {
                ConnectedComplete();
                _needReConnected = ret;
            }
            else
            {
                if (_needReConnected)
                {
                    ReConnectedComplete();
                    ReConnect();
                }
            }

            return ret;
        }

        /// <summary>
        /// 已经连接，发包或者信息记录（具体内容需要根据实际具体需求实现）
        /// </summary>
        protected abstract void ConnectedComplete();
        protected abstract void ReConnectedComplete();

        private void OnRecv(MemoryStream stream)
        {
            int offset = _packetOperate.UnpackPacket(stream);
            stream.Seek(offset, SeekOrigin.Begin);
        }

        private void ProcessProtocal()
        {
            _protocolProcess.Process();
        }

        protected abstract void ProcessLogic();

        protected void AddProcesser(uint msgId, ProtocolProcessHandler.Processer processer)
        {
            _protocolProcess.AddProcesser(msgId, processer);
        }

        public bool Send<T>(T msg) where T : global::ProtoBuf.IExtensible
        {
            MemoryStream head,body;
            //if (((AbstractParsePacket)_packetOperate).BlowFishHandler == null)
            {
                _packetOperate.PackPacket(msg, out head, out body);
            }
            //else
            //{
            //    _packetOperate.CryptoPackPacket(msg, out head, out body);
            //}
            return Send(head, body);
        }


        private bool Send(MemoryStream head, MemoryStream body)
        {
            if (_tcp == null)
            {
                return false;
            }
            return _tcp.Send(head, body);
        }

        private bool Send(MemoryStream stream)
        {
            if (_tcp == null)
            {
                return false;
            }
            return _tcp.Send(stream);
        }

        protected abstract void DisconnectComplete();
        private bool OnDisconnect()
        {
            if (_needReConnected)
            {
                Console.WriteLine("try to reconnect to server again!");
                ReConnect();
            }
            else
            {
                DisconnectComplete();
            }

            return true;
        }

        public void Process()
        {
            ProcessProtocal();
            ProcessLogic();
        }

        public void InitParser()
        {
            Packet packet = new Packet();
            _packetOperate = packet;
            _protocolProcess = packet;
        }

        public void SetParser(AbstractParsePacket packet)
        {
            _packetOperate = packet;
            _protocolProcess = packet;
        }

        //public void SetBlowFish(BlowFish blowFish)
        //{
        //   ((AbstractParsePacket)_packetOperate).BlowFishHandler = blowFish;
        //}

        public void Exit()
        {
            _needReConnected = false;
            _tcp.Disconnect();
        }
    }
}
