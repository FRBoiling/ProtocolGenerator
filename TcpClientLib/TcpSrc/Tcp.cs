using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace TcpLib.TcpSrc
{
    public partial class Tcp : ITcp
    {
        private Socket _workSocket = null;
        private ushort _listenPort;
        public Socket GetWorkSoket()
        {
            return _workSocket;
        }
        public TcpAsyncCallBack.AsyncAcceptCallback OnAccept {get; set; }
        public TcpAsyncCallBack.AsyncConnectCallback OnConnect { get; set; }
        public TcpAsyncCallBack.AsyncDisconnectCallback OnDisconnect { get; set; }
        public TcpAsyncCallBack.AsyncReadCallback OnRecv { get; set; }
        public bool NeedListenHeartBeat { get; set; }

        public Tcp()
        {
            OnAccept = DefaultOnAccept;
            OnConnect = DefaultOnConnect;
            OnDisconnect = DefaultOnDisconnect;
            OnRecv = DefaultOnRecv;
            NeedListenHeartBeat = true;
        }

        private static bool DefaultOnConnect(bool ret)
        {
           Console.WriteLine("default on DefaultOnConnect function called,check it");
            return false;
        }

        static private bool DefaultOnAccept(bool ret)
        {
           Console.WriteLine("default on DefaultOnAccept function called,check it");
            return false;
        }

        static private void DefaultOnRecv(MemoryStream stream)
        {
            stream.Seek(0, SeekOrigin.End);
        }

        static private bool DefaultOnDisconnect()
        {
           Console.WriteLine("default on DefaultOnDisconnect function called,check it");
            return false;
        }

        public Socket Listen(ushort port, int backLog = 10)
        {
            _listenPort = port;
            Socket listenSocket = TcpMgr.Inst.GetListenSocket(port);
            if (listenSocket == null)
            {
                listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ep = new IPEndPoint(IPAddress.Any, port);
                listenSocket.Bind(ep);
                listenSocket.Listen(backLog);
                TcpMgr.Inst.AddListenSocket(port, listenSocket);
                return listenSocket;
            }
            else
            {
                return listenSocket;
            }
        }

        public bool Accept(ushort port)
        {
            Socket listenr = Listen(port);
            try
            {
                listenr.BeginAccept(new AsyncCallback(AcceptCallback), listenr);
            }
            catch (Exception e)
            {
               Console.WriteLine("accept error:{0}", e.ToString());
                return false;
            }
            return true;
        }

        public bool Connect(string ip, ushort port)
        {
            if (_workSocket != null)
            {
                return false;
            }
            else
            {
                IPAddress ipAddress = IPAddress.Parse(ip);
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);
                try
                {
                    // Create a TCP/IP socket.     
                    Socket socket = null;
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), socket);
                }
                catch (Exception e)
                {
                   Console.WriteLine("connect error:{0}", e.ToString());
                    return false;
                }
            }
            return true;
        }

        IList<ArraySegment<byte>> _sendStreams = new List<ArraySegment<byte>>();
        IList<ArraySegment<byte>> _waitStreams = new List<ArraySegment<byte>>();

        public void Disconnect()
        {
            if (_workSocket == null)
            {
                return;
            }
            else
            {
                lock (this)
                {
                    _workSocket.Close();
                    _workSocket = null;
                    _waitStreams.Clear();
                    _sendStreams.Clear();
                    if (OnDisconnect != null)
                    {
                        TcpMgr.Inst.AddDisconnectExec(OnDisconnect);
                    }
                }
            }
        }

        public bool Send(MemoryStream stream)
        {
            //// Convert the string data to byte data using ASCII encoding.     
            //byte[] byteData = Encoding.ASCII.GetBytes(data);
            if (stream.Length == 0)
            {
                return true;
            }
            ArraySegment<byte> segment = new ArraySegment<byte>(stream.GetBuffer(), 0, (int)stream.Length);
            if (_sendStreams.Count == 0)
            {
                _sendStreams.Add(segment);
                try
                {
                    _workSocket.BeginSend(_sendStreams, 0, new AsyncCallback(SendCallback), _workSocket);
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            else
            {
                _waitStreams.Add(segment);
            }
            return true;
        }

        public bool Send(MemoryStream head, MemoryStream body)
        {
            head.Seek(0, SeekOrigin.Begin);
            body.Seek(0, SeekOrigin.Begin);
            if (body.Length == 0)
            {
                return Send(head);
            }
            lock (this)
            {
                ArraySegment<byte> arrHead = new ArraySegment<byte>(head.GetBuffer(), 0, (int)head.Length);
                ArraySegment<byte> arrBody = new ArraySegment<byte>(body.GetBuffer(), 0, (int)body.Length);

                if (_sendStreams.Count == 0)
                {
                    _sendStreams.Add(arrHead);
                    _sendStreams.Add(arrBody);
                    try
                    {
                        _workSocket.BeginSend(_sendStreams, SocketFlags.None, SendCallback, _workSocket);
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }
                else
                {
                    _waitStreams.Add(arrHead);
                    _waitStreams.Add(arrBody);
                }
            }
            return true;
        }

        public bool Send(ArraySegment<byte> head, ArraySegment<byte> body)
        {
            lock (this)
            {
                if (_sendStreams.Count == 0)
                {
                    _sendStreams.Add(head);
                    _sendStreams.Add(body);
                    try
                    {
                        _workSocket.BeginSend(_sendStreams, SocketFlags.None, SendCallback, null);
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }
                else
                {
                    _waitStreams.Add(head);
                    _waitStreams.Add(body);
                }
            }
            return true;
        }


        private void ConnectCallback(IAsyncResult ar)
        {
            // Retrieve the socket from the state object.     
            Socket socket = (Socket)ar.AsyncState;
            if (socket == null)
            {
                OnConnect(false);
                return;
            }
            try
            {
                // Complete the connection.     
                socket.EndConnect(ar);
               Console.WriteLine("socket connected to {0}", socket.RemoteEndPoint.ToString());
                Recv(socket);
                OnConnect(true);
            }
            catch (Exception e)
            {
                //Log.Error("ConnectCallback error:{0}",e.ToString());
                OnConnect(false);
            }
        }


        private void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                Socket listener = (Socket)ar.AsyncState;
                if (NeedListenHeartBeat)
                {
                    TcpMgr.Inst.Listen(_listenPort);
                }

                Socket handler = listener.EndAccept(ar);
                Recv(handler);
                OnAccept(true);
            }
            catch (Exception e)
            {
               Console.WriteLine("AcceptCallback error:{0}", e.ToString());
                OnAccept(false);
            }
            return;
        }

        private void Recv(Socket socket)
        {
            try
            {
                // Create the state object.     
                StateObject state = new StateObject();
                state.workSocket = socket;
                _workSocket = socket;
                // Begin receiving the data from the remote device.     
                socket.BeginReceive(state.buffer, 0, StateObject.BufferSize, SocketFlags.None, new AsyncCallback(RecvCallback), state);
            }
            catch (Exception e)
            {
               Console.WriteLine(e.ToString());
            }
        }

        private void RecvCallback(IAsyncResult ar)
        {
            SocketError errorCode;
            // Retrieve the state object and the handler socket     
            // from the asynchronous state object.     
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;
            if (handler == null)
            {
               Console.WriteLine("Accept socket is null");
                return;
            }

            try
            {
                // Read data from the client socket.     
                int len = handler.EndReceive(ar, out errorCode);
                if (len <= 0)
                {
                   Console.WriteLine("RecvCallback warning : len = {0} （len <= 0）", len);
                    Disconnect();
                    return;
                }
               Console.WriteLine("Recv {0} bytes.", len);
                len = state.offset + len;

                MemoryStream transferred = new MemoryStream(state.buffer, 0, len, true, true);
                if (OnRecv != null)
                {
                    OnRecv(transferred);
                }
                state.offset = len - (int)transferred.Position;
                if (state.offset < 0)
                {
                   Console.WriteLine("RecvCallback warning : state.offset = {0} (state.offset < 0)", state.offset);
                    Disconnect();
                    return;
                }
                int size = 16384;
                if (transferred.Position == 0)
                {
                    size = (int)(transferred.Length * 2);
                }
                if (size > 65535)
                {
                   Console.WriteLine("RecvCallback warning : size = {0} (size > 65535) ", size);
                    Disconnect();
                    return;
                }
                byte[] buffer = new byte[size];
                Array.Copy(state.buffer, transferred.Position, buffer, 0, state.offset);
                state.buffer = buffer;

                handler.BeginReceive(state.buffer, state.offset, size - state.offset, SocketFlags.None, new AsyncCallback(RecvCallback), state);
            }
            catch (Exception e)
            {
                //Log.Error("RecvCallback error:{0}", e.ToString());
                Disconnect();
            }
            return;
        }


        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.     
                Socket handler = (Socket)ar.AsyncState;
                // Complete sending the data to the remote device.     
                int len = handler.EndSend(ar);
               Console.WriteLine("Send {0} bytes.", len);

                _sendStreams.Clear();
                if (_waitStreams.Count > 0)
                {
                    IList<ArraySegment<byte>> temp = _sendStreams;
                    _sendStreams = _waitStreams;
                    _waitStreams = temp;
                    _workSocket.BeginSend(_sendStreams, SocketFlags.None, SendCallback, handler);
                }
                //handler.Shutdown(SocketShutdown.Both);
                //handler.Close();
            }
            catch (Exception e)
            {
               Console.WriteLine(e.ToString());
            }
        }


    }
}
