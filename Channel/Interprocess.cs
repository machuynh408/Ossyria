using OssyriaDEV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Channel
{
    public class Interprocess
    {
        private ManualResetEvent handle = new ManualResetEvent(false);
        private Socket s = null;
        private byte[] buffer = null;
        private object data = null;
        public Interprocess(string address)
