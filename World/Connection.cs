using OssyriaDEV;
using System;
using System.Net.Sockets;

namespace World
{ 
    public class Connection
    {
        private Socket s = null;
        private string address = "";
        private int port = -1;
        private byte[] buffer = null;
        private Processor processor = null;
        public Connection(Socket s, string address, int port)
        {
            this.s = s;
            this.address = address;
            this.port = port;
            this.listen(9999);
            this.processor = new Processor(this);
        }

        public string getAddress()
        {
            return address;
        }
