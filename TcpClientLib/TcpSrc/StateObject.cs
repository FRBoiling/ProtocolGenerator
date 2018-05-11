using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace TcpLib.TcpSrc
{
    // State object for reading client data asynchronously     
    public class StateObject
    {
        // Client socket.     
        public Socket workSocket = null;
        // Size of receive buffer.     
        public const int BufferSize = 2048;
        public int offset;

        // Receive buffer.     
        public byte[] buffer = new byte[BufferSize];
        // Received data string.     
        public StringBuilder sb = new StringBuilder();
    }
}
