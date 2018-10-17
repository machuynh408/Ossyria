using OssyriaDEV;
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;

namespace Auth
{
    public class Session
    {
        private ManualResetEvent handle = new ManualResetEvent(false);
        private Socket s = null;
        private byte[] buffer = null;
        private object data = null;
        public Session(string address)
        {
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
