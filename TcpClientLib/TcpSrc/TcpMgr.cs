using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace TcpLib.TcpSrc
{
    public class TcpMgr
    {
        private static TcpMgr _inst;
        private static readonly object _sysLock = new object();
        public static TcpMgr Inst
        {
            get
            {
                if (_inst == null)
                {
                    lock (_sysLock)
                    {
                        if (_inst == null)
                        {
                            _inst = new TcpMgr();
                        }
                    }
                }
                return _inst;
            }
        }

        Dictionary<ushort, Socket> _listeners = new Dictionary<ushort, Socket>();

        public delegate void ListenHeartBeatCallBack(ushort port); //监听心跳回调
        Dictionary<ushort, ListenHeartBeatCallBack> _heartBeatListenCallBacks = new Dictionary<ushort, ListenHeartBeatCallBack>();

        private int _backLog = 10;


        public void AddListenSocket(ushort port,Socket socket)
        {
            _listeners.Add(port, socket);
        }

        public Socket GetListenSocket(ushort port)
        {
            Socket socket = null;
            if (_listeners.TryGetValue(port,out socket))
            {
                return socket;
            }
            return null;
        }
        private int _listenBackLog = 10;

        public Socket Bind(ushort port, int backLog = 10)
        {
            _backLog = backLog;
            Socket listenSocket = GetListenSocket(port);
            if (listenSocket == null)
            {
                listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ep = new IPEndPoint(IPAddress.Any, port);
                listenSocket.Bind(ep);
                listenSocket.Listen(_backLog);
                AddListenSocket(port, listenSocket);
                return listenSocket;
            }
            else
            {
                return listenSocket;
            }
        }

        public void Listen(ushort port, ListenHeartBeatCallBack callBack)
        {
            ListenHeartBeatCallBack heartBeatCallBack;
            if (_heartBeatListenCallBacks.TryGetValue(port, out heartBeatCallBack))
            {
                for (int i = 0; i < _backLog; i++)
                {
                    heartBeatCallBack(port);
                }
            }
            else
            {
                _heartBeatListenCallBacks.Add(port, callBack);
                for (int i = 0; i < _backLog; i++)
                {
                    callBack(port);
                }
            }
        }

        public void Listen(ushort port)
        {
            ListenHeartBeatCallBack listenCallBack;
            if (_heartBeatListenCallBacks.TryGetValue(port, out listenCallBack))
            {
                listenCallBack(port);
            }
            else
            {
                _heartBeatListenCallBacks.Add(port, DefaultListen);
                DefaultListen(port);
            }
        }

        void DefaultListen(ushort port)
        {
            new Tcp().Accept(port);
        }


        private Socket GetListen(ushort port)
        {
            Socket listenSocket = GetListenSocket(port);
            if (listenSocket == null)
            {
                listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ep = new IPEndPoint(IPAddress.Any, port);
                listenSocket.Bind(ep);
                listenSocket.Listen(_listenBackLog);
                AddListenSocket(port, listenSocket);
                return listenSocket;
            }
            return listenSocket;
        }


        public delegate void CallBackDisconnect();
        ConcurrentQueue<TcpAsyncCallBack.AsyncDisconnectCallback> _disconnectCallBacks = new ConcurrentQueue<TcpAsyncCallBack.AsyncDisconnectCallback>();
        public void AddDisconnectExec(TcpAsyncCallBack.AsyncDisconnectCallback callback)
        {
            _disconnectCallBacks.Enqueue(callback);
        }

        public void Update()
        {
            TcpAsyncCallBack.AsyncDisconnectCallback callback;
            while (_disconnectCallBacks.Count>0)
            {
                if (_disconnectCallBacks.TryDequeue(out callback))
                {
                    if (callback!=null)
                    {
                        try
                        {
                            callback();
                        }
                        catch (Exception e)
                        {
                        }
                    }
                }
            }
        }


    }
}
