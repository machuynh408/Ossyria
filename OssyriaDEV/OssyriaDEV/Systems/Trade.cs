using System;
using System.Collections.Generic;
using System.Timers;

namespace OssyriaDEV
{
    public class Trade
    {
        // This acts like a 5 minute timer
        public delegate void OnInvitationHandler(long id);
        public event OnInvitationHandler OnInvitation;

        private long id;
        private Player one = null, two = null;
        private bool active = false;


        private Timer clock = null;
        private Dictionary<int, Dictionary<byte, Item>> items = null;
        private Dictionary<int, int> mesos = null;
        private Dictionary<int, bool> locked = null;
        public Trade(long id, Player p)
        {
            this.id = id;
            this.one = p;
            this.items = new Dictionary<int, Dictionary<byte, Item>>();
            this.mesos = new Dictionary<int, int>();
            this.locked = new Dictionary<int, bool>();
            if (!this.items.ContainsKey(p.getId()))
                this.items.Add(p.getId(), new Dictionary<byte, Item>());
            if (!this.mesos.ContainsKey(p.getId()))
                this.mesos.Add(p.getId(), 0);
            if (!this.locked.ContainsKey(p.getId()))
                this.locked.Add(p.getId(), false);
            p.setTrade(this);

            Writer w = new Writer(0xF5);
            w.Create(new ByteData(5));
            w.Create(new ByteData(3));
            w.Create(new ByteData(2));
            w.Create(new ByteData(0));
            w.Create(new ByteData(0));
            Packets.getVisual(w, p, false);
            w.Create(new AsciiStringData(p.getName()));
            w.Create(new ByteData(0xFF));
            p.send(w);
        }

        public long getId()
        {
            return id;
        }

        public bool isActive()
        {
            return active;
        }

        public void run()
        {
            clock = new Timer(180000);
            clock.Elapsed += OnElapsed;
            clock.Start();
        }

        private void OnElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                clock.Dispose();
                clock = null;
            }
            finally
            {
                OnInvitation?.Invoke(id);
            }
        }

        public bool invite(Player p)
        {
            if (p.getTrade() == null)
            {
                p.send(invitation());
                return true;
            }
            else
            {
                one.send(Packets.notice("The other player is already trading with someone else."));
            }
            return false;
        }


        private Writer invitation()
        {
            Writer w = new Writer(0xF5);
            w.Create(new ByteData(2));
            w.Create(new ByteData(3));
            w.Create(new AsciiStringData(one.getName()));
            w.Create(new IntData(one.getKey()));
            return w;
        }

        public void terminate()
        {
            try
            {
                clock.Elapsed -= OnElapsed;
                clock.Stop();
                clock.Dispose();
                clock = null;
            }
            catch { }
            if (one != null)
            {
                Dictionary<byte, Item> data = null;
                items.TryGetValue(one.getId(), out data);

                foreach (Item i in data.Values)
                {
                    string name = i.getString("type").Replace(@"inventory\", "");
                    if (name == "cash")
                        continue;
                    Inventory inventory = one.getInventory(name);
                    inventory.add(i);
                    switch (name)
                    {
                        case "equip":
                            {
                                returnEquip(one, inventory, i);
                                break;
                            }
                        case "use":
                        case "set-up":
                        case "etc":
                            {
                                returnItem(one, inventory, i);
                                break;
                            }
                    }
                }
                int mesos = 0;
                this.mesos.TryGetValue(one.getId(), out mesos);
                one.gainMesos(mesos);
            }
            if (two != null)
            {
                Dictionary<byte, Item> data = null;
                items.TryGetValue(two.getId(), out data);

                foreach (Item i in data.Values)
                {
                    string name = i.getString("type").Replace(@"inventory\", "");
                    if (name == "cash")
                        continue;
                    Inventory inventory = two.getInventory(name);
                    inventory.add(i);
                    switch (name)
                    {
                        case "equip":
                            {
                                returnEquip(two, inventory, i);
                                break;
                            }
                        case "use":
                        case "set-up":
                        case "etc":
                            {
                                returnItem(two, inventory, i);
                                break;
                            }
                    }
                }
                int mesos = 0;
                this.mesos.TryGetValue(two.getId(), out mesos);
                two.gainMesos(mesos);
            }

            Writer w = new Writer(0xF5);
            w.Create(new ByteData(0xA));
            w.Create(new ByteData(0));
            w.Create(new ByteData(2));

            if (one != null)
            {
                one.send(w);
