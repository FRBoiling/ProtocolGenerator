using System;
using System.IO;
using System.Net.Sockets;

namespace TcpLib.TcpSrc
{
    public interface ITcp
    {
        Socket GetWorkSoket();
        bool NeedListenHeartBeat { get; set; }
        TcpAsyncCallBack.AsyncAcceptCallback OnAccept { get; set; }
        TcpAsyncCallBack.AsyncConnectCallback OnConnect { get; set; }
        TcpAsyncCallBack.AsyncDisconnectCallback OnDisconnect { get; set; }
        TcpAsyncCallBack.AsyncReadCallback OnRecv { get; set; }

        Socket Listen(ushort port, int backLog = 10);
        bool Accept(ushort port);
        bool Connect(string ip, ushort port);
        void Disconnect();

        bool Send(ArraySegment<byte> head, ArraySegment<byte> body);
        bool Send(MemoryStream stream);
        bool Send(MemoryStream head, MemoryStream body);
    }
}