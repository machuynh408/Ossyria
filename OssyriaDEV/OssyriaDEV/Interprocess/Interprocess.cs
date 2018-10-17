using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OssyriaDEV
{
    public class Interprocess
    {
        private ManualResetEvent handle = new ManualResetEvent(false);
        private Player p = null;
        private Socket s = null;
        private byte[] buffer = null;
        private short header;
        public Interprocess(Player p)
        {
            this.p = p;
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            s.Connect(new IPEndPoint(IPAddress.Parse(Settings.ADDRESS), 1337));
        }
        public async Task<object> request(short opcode)
