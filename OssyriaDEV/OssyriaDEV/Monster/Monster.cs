using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace OssyriaDEV
{
    public class Monster : Spawn
    {
        private int id, spawn = -1;
        private Platform platform = null;
        private ConcurrentDictionary<string, object> info = null;

        private DamageManager damageManager = null;
        private MobSkillManager mobSkillManager = null;
        private StatusManager statusManager = null;
        public Monster(int id, Position position) : base(position)
        {
            this.id = id;
            this.info = new ConcurrentDictionary<string, object>();
            this.info.GetOrAdd("new", true);
            this.info.GetOrAdd("aggro", false);
            this.info.GetOrAdd("marked", false);
            this.damageManager = new DamageManager(this);
            this.mobSkillManager = new MobSkillManager(this);
            this.statusManager = new StatusManager(this);
        }
        public int getId()
        {
            return id;
        }

        public void setSpawn(int spawn)
        {
            this.spawn = spawn;
        }
        public int getSpawn()
        {
            return spawn;
        }
        public void setPlatform(Platform platform)
        {
            this.platform = platform;
        }

        public Platform getPlatform()
        {
            return platform;
        }
        public void initalize(MobWz mobWz)
        {
            this.damageManager.initialize(mobWz);
        }
        public Spawn getMaster()
        {
            object o = null;
            info.TryGetValue("master", out o);
            return (o as Spawn);
        }

        public void setMaster(Spawn s)
        {
            if (!info.ContainsKey("master"))
                info.GetOrAdd("master", s);
            else
            {
                Spawn x = getMaster();
                info.TryUpdate("master", s, x);
            }
        }
        public int getLevel()
        {
            return this.damageManager.getLevel();
        }
        public int getHp()
        {
            return this.damageManager.getHp();
        }
        public int getMaxHP()
        {
            return this.damageManager.getMaxHP();
        }
        public int getMp()
        {
            return this.damageManager.getMp();
        }
        public int getMaxMP()
        {
            return this.damageManager.getMaxMP();
        }
        public int getExp()
        {
            return this.damageManager.getExp();
        }
        public int getInt(string name)
        {
            object o = null;
            if (info.TryGetValue(name, out o))
                return Convert.ToInt32(o);
            else
                return -1;
        }
