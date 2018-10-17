using OssyriaDEV;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Timers;
using System.Xml;

namespace Channel
{
    public class User : Info
    {
        private Interprocess interprocess = null; // Connect to World Server
        private Connection connection = null;

        private int id = -1;
        private int channel = -1;
        private Player player = null;
        private Inventory cash = null;

        public User(Socket s)
        {
            this.createConnection(s);
        }
        public void createConnection(Socket s)
        {
            this.connection = new Connection(this, s);
            this.connection.OnDisconnect += OnDisconnect;
        }

        private void OnDisconnect(string message)
        {
            connection.OnDisconnect -= OnDisconnect;
            switch (getString("state"))
            {
                case "CHANNEL":
                case "TRANSITION":
                    {
                        insert("state", "-");
                        save();
                        break;
                    }
            }
            if(player != null)
            {
                Map m = player.getMap();
                m.leave(player);
                Party party = player.getParty();
                 if (party != null)
                 {
                     if (player.holdsLeadership("party"))
                         party.autoLeader();
                     party.update(player);
                 }
                 Trade trade = player.getTrade();
                 if (trade != null)
                     trade.terminate();
                player.getSkillsManager().stopAllBuffs();
                player.getCooldowns().ForEach(x =>
                {
                    x.stop();
                    int time = x.getTime() / 1000;
                    player.getSkillsManager().getSkill(x.getId()).insert("cooltime", time);
                    x.terminate();
                });

                player.getStatsManager().insert("hp", player.getHp());
                player.getStatsManager().insert("mp", player.getMp());
                PortalWz portalWz = m.calcSpawn(player.getPosition());
                player.getStatsManager().insert("spawn", portalWz != null ? portalWz.getId() : 0);
                player.save();
                Ossyria.remove(player);
                if (player.getInterprocess() != null)
                    player.getInterprocess().close();
            }
            if (this.interprocess != null)
                this.interprocess.terminate();
            Console.WriteLine("(" + DateTime.Now.ToShortTimeString() + ") " + " Disconnection - " + this.getAddress());
        }

        public Connection getConnection()
        {
            return connection;
        }

        public async Task<object> request(short opcode)
        {
            object x = await this.interprocess.request(new Writer(opcode));
            return x;
        }
        public async Task<object> request(Writer w)
        {
            object x = await this.interprocess.request(w);
            return x;
        }

        public void dc()
        {
            this.connection.terminate();
