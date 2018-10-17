using OssyriaDEV;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Timers;
using System.Xml;

namespace Auth
{
    public class User : Info
    {
        private Session session = null; // Connect to World Server
        private Connection connection = null;

        private int id = -1;
        private int channel = -1;
        private Dictionary<int, Player> players = null;

        public User(Socket s)
        {
            this.createConnection(s);
        }
        public void createConnection(Socket s)
        {
            this.connection = new Connection(this, s);
            this.connection.OnDisconnect += OnDisconnect;
            this.session = new Session(s.RemoteEndPoint.ToString());
        }

        public Connection getConnection()
        {
            return connection;
        }

        public async Task<object> request(short opcode)
        {
            object x = await this.session.request(new Writer(opcode));
            return x;
        }
        public async Task<object> request(Writer w)
        {
            object x = await this.session.request(w);
            return x;
        }

        public void dc()
        {
            try
            {
                this.connection.terminate();
            }
            catch { }
        }

        public int getId()
        {
            return id;
        }

        public void setId(int id)
        {
            this.id = id;
            this.insert("id", id);
        }

        public string getUsername()
        {
            return this.getString("username");
        }

        public string getAddress()
        {
            if (connection != null)
                return connection.getAddress();
            else
                return "";
        }
        public int getChannel()
        {
