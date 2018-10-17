using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace OssyriaDEV
{
    public class Map
    {
        private MapWz wz = null;
        private int id;
        private int channel;
        private int code;
        private ConcurrentDictionary<int, Spawn> spawns = null;
        private RespawnManager respawnManager = null;

        public Map(int id)
        {
            this.id = id;
            this.spawns = new ConcurrentDictionary<int, Spawn>();
            this.respawnManager = new RespawnManager(this);
        }
        public Map(MapWz wz, int id, int channel)
        {
            this.wz = wz;
            this.id = id;
            this.channel = channel;
            this.spawns = new ConcurrentDictionary<int, Spawn>();
            this.respawnManager = new RespawnManager(this);
        }

        public MapWz getWz()
        {
            return wz;
        }
        public int getId()
        {
            return id;
        }

        public void setChannel(int channel)
        {
            this.channel = channel;
        }

        public int getChannel()
        {
            return channel;
        }

        public List<Monster> getMonsters()
        {
            List<Monster> data = new List<Monster>();
            spawns.Values.Where(x => x is Monster).ToList().ForEach(x => { data.Add((Monster)x); });
            return data;
        }

        public Monster getMonster(int key)
        {
            return getMonsters().Where(x => x.getKey() == key).FirstOrDefault();
        }

        public Loot getLoot(int key)
        {
            return getLoots().Where(x => x.getKey() == key).FirstOrDefault();
        }

        public List<Loot> getLoots()
        {
            List<Loot> data = new List<Loot>();
            spawns.Values.Where(x => x is Loot).ToList().ForEach(x => { data.Add((Loot)x); });
            return data;
        }

        public List<Npc> getNpcs()
        {
            List<Npc> data = new List<Npc>();
            spawns.Values.Where(x => x is Npc).ToList().ForEach(x => { data.Add((Npc)x); });
            return data;
        }
        public Npc getNpc(int key)
        {
            return getNpcs().Where(x => x.getKey() == key).FirstOrDefault();
        }
        public List<Player> getPlayers()
        {
            List<Player> data = new List<Player>();
            spawns.Values.Where(x => x is Player).ToList().ForEach(x => { data.Add((Player)x); });
            return data;
        }

        public Player getPlayer(int i, bool id = false)
        {
            if (!id)
                return getPlayers().Where(x => x.getKey() == i).FirstOrDefault();
            else
                return getPlayers().Where(x => x.getId() == i).FirstOrDefault();
        }

        public List<Mist> getMists()
        {
            List<Mist> data = new List<Mist>();
            spawns.Values.Where(x => x is Mist).ToList().ForEach(x => { data.Add((Mist)x); });
            return data;
        }

        private Mist getMist(int key)
        {
            return getMists().Where(x => x.getKey() == key).FirstOrDefault();
        }
        public List<Spawn> getSpawns()
        {
            return spawns.Values.ToList();
        }
        public void processAction(Action action)
        {
            if (Monitor.TryEnter(spawns, 150))
            {
                try
                {
                    action.Invoke();
                }
                finally
                {
                    Monitor.Exit(spawns);
                }
            }
        }

        public void enter(Player p)
        {
            p.updateKey(this.generate());
            p.updateMap(this);

            this.getPlayers().ForEach(x => { x.create(p); p.create(x); });
            if (!spawns.ContainsKey(p.getKey()))
            {
                p.create(p);
                p.remove(p.getKey());
                spawns.GetOrAdd(p.getKey(), p);
            }
            this.getLoots().ForEach(x => { x.create(p); });
