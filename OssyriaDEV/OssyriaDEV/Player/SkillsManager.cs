using System;
using System.Collections.Generic;
using System.Linq;

namespace OssyriaDEV
{
    public class SkillsManager : Info
    {
        private Player p = null;
        private Dictionary<int, Skill> skills = null;
        private Dictionary<int, Cooldown> cooldowns = null;
        private Dictionary<int, Buff> buffs = null;
        private Dictionary<int, Passive> passives = null;
        private Mount mount = null;
        private Summon summon = null;

        public SkillsManager(Player p)
        {
            this.p = p;
            this.skills = new Dictionary<int, Skill>();
            this.cooldowns = new Dictionary<int, Cooldown>();
            this.buffs = new Dictionary<int, Buff>();
            this.passives = new Dictionary<int, Passive>();
        }

        public List<Skill> getSkills()
        {
            return skills.Values.ToList();
        }

        public Skill getSkill(int skillId)
        {
            return getSkills().Where(x => x.getId() == skillId).FirstOrDefault();
        }
        public void giveSkill(int skillId)
        {
            Skill s =Factory.createSkill(skillId);
            if(s == null)
                return;
            if(!skills.ContainsKey(skillId))
                skills.Add(skillId, s);
        }
        public void giveSkill(Skill s)
        {
            if (!skills.ContainsKey(s.getId()))
                skills.Add(s.getId(), s);
        }
        public void removeSkill(int id)
        {
            if(skills.ContainsKey(id))
                skills.Remove(id);
        }

        public void stopBuff(int id)
        {
            Buff b = getBuff(id);
            if (b == null)
                return;
            try
            {
                b.stop();
            }
            finally
            {
                b.terminate();
            }
        }
        public void stopAllBuffs()
        {
            buffs.Values.ToList().ForEach(x =>
            {
                try
                {
                    x.stop();
                }
                finally
                {
                    x.terminate();
                }
            });
        }
        public Buff getBuff(int id)
        {
            Buff b = null;
            buffs.TryGetValue(id, out b);
            return b;
        }
        public Buff getBuff(string name)
        {
            return getBuffs().Where(x => x.getName() == name).FirstOrDefault();
        }
        public void giveBuff(Buff b)
        {
            p.send(Packets.getBuff(b, b.getInt("buff") == 1));
            b.OnBuff += OnBuff;
            b.start();
            this.buffs.Add(b.getId(), b);
        }
        private void OnBuff(int i)
        {
            Buff b = getBuff(i);
            if (!this.buffs.Remove(i))
            {
                p.send(Packets.enableActions());
                return;
            }

            if (b.getId() == 1004)
            {
                //getMount().withdraw();
                //setMount(null);
            }
            else if (b.getId() == 1311008)
            {
                removePassive(b.getId());
            }
            Update u = new Update();
            Writer w = new Writer(0x1E);
            w.Create(new LongData(0), new LongData(b.getUpdate().getMask()), new ByteData(3));
            p.send(w);

            if (b.getId() == 1004 || b.getId() == 1111002 || // Combo Attack
                b.getId() == 3101004 || b.getId() == 3201004 || // Soul Arrow
                b.getId() == 4001003 || b.getId() == 4111002 || // Dark Sight // Shadow Partner
                b.getId() == 5111005 || b.getId() == 5121003 ||
                b.getId() == 5221006)   // Transformation // Supertransformation
            {
                w = new Writer(0x9B);
                w.Create(new IntData(p.getId()));
                w.Create(new LongData(0), new LongData(b.getUpdate().getMask()));
                p.broadcast(w);
            }

            switch (b.getId())
            {
                case 1301007: // Hyper Body
                case 9001008: // Hyper Body
                case 9101008: // Hyper Body
                    {   
                        int hp = p.getHp();
                        int mp = p.getMp();
