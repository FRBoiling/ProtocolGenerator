using System;
using System.Collections.Generic;
using System.IO;

namespace TcpLib
{
    public class ProtocolProcessHandler
    {
        public delegate void Processer(MemoryStream stream, int uid = 0);
        private Dictionary<uint, Processer> _processers = new Dictionary<uint, Processer>();
        public void Add(uint id, Processer processer)
        {
            _processers.Add(id, processer);
        }

        public bool Process(uint id, MemoryStream stream, int uid = 0)
        {
            Processer processer;
            Console.WriteLine("process msg id {0}", id);
            if (_processers.TryGetValue(id, out processer))
            {
                processer(stream, uid);
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
