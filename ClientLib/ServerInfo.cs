using System;

namespace ClientLib
{
    public class ServerInfo
    {
        public string Ip { get; set; }
        public string Type { get;  set; }
        public ushort Port { get;  set; }

        internal object GetServerTagString()
        {
            return Ip + Type + Port;
        }
    }
}