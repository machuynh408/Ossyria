using OssyriaDEV;
using System;
using System.Net.Sockets;
using System.Threading;

namespace Auth
{
    public class Connection : IConnection
    {
        private enum SocketState
        {
            LISTEN,
            DECODE
        }

        public delegate void OnDisconnectHandler(string message = "");
        public event OnDisconnectHandler OnDisconnect;

        private Socket s = null;
        private string address = "";
        private byte[] buffer = null;
        private Encryption recv = null;
        private Encryption send = null;
        private SocketState state = SocketState.LISTEN;
        private Processor processor = null;

        public Connection(User u, Socket s)
        {
            this.s = s;
            this.address = s.RemoteEndPoint.ToString();
            byte[] a = new byte[4];
            byte[] b = new byte[4];
            Random random = new Random(DateTime.Now.Millisecond);
            random.NextBytes(a);
            random.NextBytes(b);
            recv = new Encryption(a);
            send = new Encryption(b);
            listen(4);
            s.Send(getHello());
            this.processor = new Processor(u);
        }

        public Socket getSocket()
        {
            return s;
        }

        private byte[] getHello()
        {
            Writer w = new Writer();
            w.Create(
                new ShortData(0x0D),
                new ShortData((short)Settings.VERSION),
