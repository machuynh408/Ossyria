using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OssyriaDEV
{
    public class StatusManager
    {
        private Monster m = null;
        private ConcurrentDictionary<string, object> info = null;
        private ConcurrentDictionary<string, Status> statuses = null;
        public StatusManager(Monster m)
        {
            this.m = m;
            this.info = new ConcurrentDictionary<string, object>();
            this.statuses = new ConcurrentDictionary<string, Status>();
        }

        public List<Status> getStatuses()
        {
            List<Status> data = new List<Status>();
            foreach (Status status in this.statuses.Values)
                data.Add(status);
            return data;
        }
        public Status getStatus(string name)
        {
            Status s = null;
            statuses.TryGetValue(name, out s);
            return s;
        }

        private void startStatus(Status status)
        {
            if (status != null)
            {
                if (getStatus(status.getName()) == null)
                {
                    status.OnStatus += OnStatus;
                    status.start();
                    statuses.GetOrAdd(status.getName(), status);
                    if (status.getName() == "hypnotize")
                    {
                        Player x = m.getMap().getPlayer(status.getInt("source"), true);
                        x.send(Packets.getStatus(m, status));
                        info.GetOrAdd("hypnotizedBy", x.getId());
                    }
                    else
                    {
                        m.broadcast(Packets.getStatus(m, status));
                    }
                }
            }
        }

        public void stopStatus(string name)
        {
            Status status = getStatus(name);
            if (status == null)
                return;
            status.stop();
        }
        public void giveStatus(Player p, int id, int level, int time, int value = 0)
        {
            Status status = null;
            switch (id)
            {
                case 100:
                case 110:
                    {
                        status = new Status(id, level, "incPAD", time, time);
                        status.insert("incPAD", 1);
                        status.insert("MobSkill", 1);
                        break;
                    }
                case 101:
                case 111:
                    {
                        status = new Status(id, level, "incMAD", time, time);
                        status.insert("incMAD", 1);
                        status.insert("MobSkill", 1);
                        break;
                    }
                case 102:
                case 112:
                    {
                        status = new Status(id, level, "incPDD", time, time);
                        status.insert("incPDD", 1);
                        status.insert("MobSkill", 1);
                        break;
                    }
