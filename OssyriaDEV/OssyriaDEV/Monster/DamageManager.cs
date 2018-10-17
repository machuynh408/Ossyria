using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssyriaDEV
{
    public class DamageManager : Info
    {
        private Monster m = null;
        private ConcurrentDictionary<int, int> damages = null;
        public DamageManager(Monster m)
        {
            this.m = m;
            this.damages = new ConcurrentDictionary<int, int>();
        }

        public void initialize(MobWz mobWz)
        {
            insert("level", mobWz.getLevel());
            insert("hp", mobWz.getMaxHP());
            insert("maxHP", mobWz.getMaxHP());
            insert("mp", mobWz.getMaxMP());
            insert("maxMP", mobWz.getMaxMP());
            insert("exp", mobWz.getExp());
        }
        public int getLevel()
        {
            return getInt("level");
        }
        public int getHp()
        {
            return getInt("hp");
        }
        public int getMaxHP()
        {
            return getInt("maxHP");
        }
        public int getMp()
        {
            return getInt("mp");
        }
        public int getMaxMP()
        {
            return getInt("maxMP");
        }
        public int getExp()
        {
            return getInt("exp");
        }
        public bool process(Player p, Attack attack = null, Status status = null)
        {
            if (status == null)
            {
                if (!m.getAggro())
                {
                    if (m.getMaster().isEquals(p))
                        m.setAggro(true);
                    else
                        m.switchMaster(p, true);
                }
            }

            MobWz mobWz = Library.getMob(m.getId());
            int hp = m.getHp();
            int maxHP = m.getMaxHP();

            if (attack != null)
            {
                int damage = attack.getTotal(m.getKey());
                double pre = (hp / maxHP) * 100.0;

                if (hp - damage > 0)
                {
                    hp -= damage;
                    insert(p.getId(), damage);
                    insert("hp", Convert.ToInt32(hp));
                    double percent = (((double)hp) / ((double)maxHP)) * 100.0;
                    p.send(getHealth(Convert.ToByte(Math.Floor(percent))));

                    if (!mobWz.getBool("boss"))
                    {
                        if (attack.getStatusTime() != 0)
                            m.giveStatus(p, attack.getSkill(), attack.getLevel(), attack.getStatusTime(), attack.getStatusValue());
                    }
                }
                else if (hp - damage <= 0)
                {
                    insert(p.getId(), Convert.ToInt32(hp));
                    insert("hp", 0);
                    kill(p);
                    return true;
                }
            }
            else if (status != null)
            {
                if (hp <= 1)
                    return false;

                string name = status.getName();
                int damage = status.getInt(name);
                m.broadcast(Packets.harm(m.getKey(), damage));

                if (hp - damage > 1)
                {
                    insert(status.getInt("source"), damage);
                    insert("hp", Convert.ToInt32(hp - damage));
