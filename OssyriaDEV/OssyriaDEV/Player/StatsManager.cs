using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssyriaDEV
{
    public class StatsManager : Info
    {
        private Player p = null;
        private Dictionary<string, int> stats = new Dictionary<string, int>();
        public StatsManager(Player p)
        {
            this.p = p;
            stats.Add("hp", -1);
            stats.Add("maxHP", -1);
            stats.Add("mp", -1);
            stats.Add("maxMP", -1);
        }
        public void giveExp(int normalExp, int partyExp, int partyBonus)
        {
            if (!p.alive())
                return;
            if (!p.connected())
                return;
            int total = normalExp + partyExp + partyBonus;

            bool nextLevel = false;

            int level = this.getLevel();
            if (level >= 200)
                return;

            if (normalExp != 0)
                p.send(getGain(3, normalExp));
            if (partyExp != 0)
                p.send(getGain(3, partyExp, true));
            if (partyBonus != 0)
                p.send(getGain(3, partyBonus, false, false, true));

            int exp = getExp();
            int required = Tools.getPlayerExp(level);

            if (exp + total >= required) // do level up
            {
                nextLevel = true;
                level += 1;

                int amount = (exp + total) - required;

                insert("level", level);
                insert("exp", amount);


                Update u = new Update();
                u.insert("level", p.getLevel());

                int hp = 0, maxHP = 0, mp = 0, maxMP = 0;
                int l = 0, extra = 0;
                Skill s = null;
                switch (p.getClass())
                {
                    case 0:
                        {
                            maxHP = Tools.getInt(12, 16);
                            maxMP = Tools.getInt(10, 12) + (p.getInt() / 10);
                            break;
                        }
                    case 100:
                    case 110:
                    case 111:
                    case 112:
                    case 120:
                    case 121:
                    case 122:
                    case 130:
                    case 131:
                    case 132:
                        {
                            maxHP = Tools.getInt(24, 28);
                            if (p.getClass() == 130)
                                maxHP = Tools.getInt(30, 34);
                            else if(p.getClass() == 131)
                                maxHP = Tools.getInt(40, 44);
                            else if (p.getClass() == 132)
                                maxHP = Tools.getInt(50, 54);
                            maxMP = Tools.getInt(4, 6) + (calcTotalStat("int") / 10);
                            s = p.getSkillsManager().getSkill(1000001);
                            if (s != null)
                            {
                                l = s.getLevel();
                                extra = Library.getSkill(s.getId()).getLevel(s.getLevel()).getInt("x");
                                maxHP += extra;
                            }
                            break;
                        }
                    case 200:
                    case 210:
                    case 211:
                    case 212:
                    case 220:
                    case 221:
                    case 222:
                    case 230:
                    case 231:
                    case 232:
                        {
                            maxHP = Tools.getInt(10, 14);
                            maxMP = Tools.getInt(22, 24) + (p.getInt() / 10);
                            s = p.getSkillsManager().getSkill(2000001);
                            if (s != null)
                            {
                                l = s.getLevel();
                                extra = Library.getSkill(s.getId()).getLevel(s.getLevel()).getInt("x");
                                maxMP += ((2 * extra) + (p.getInt() / 10));
                            }
                            break;
                        }
                    case 300:
                    case 310:
                    case 311:
                    case 312:
                    case 320:
                    case 321:
                    case 322:
                    case 400:
                    case 410:
                    case 411:
                    case 412:
                    case 420:
                    case 421:
                    case 422:
                        {
                            // * NOTE: When change jobs from beginner to rouge give an aditional 160-165 maxHP and maxMP 55-60
                            // * NOTE: When change jobs from rouge to second job give an aditional 305-310 maxHP and maxMP 175-180
                            maxHP = Tools.getInt(20, 24);
                            maxMP = Tools.getInt(14, 16);
                            break;
                        }
                }
              
                insert("maxHP", Math.Min(30000, maxHP + getInt("maxHP")));
                insert("maxMP", Math.Min(30000, maxMP + getInt("maxMP")));
                insert("hp", getInt("maxHP"));
                insert("mp", getInt("maxMP"));

                Buff b = p.getBuff("Hyper Body");
                if (b != null)
                {
                    b.pause();
                    b.reset();
                }

                maxHP = getInt("maxHP");
                maxMP = getInt("maxMP");

                hp = maxHP;
                mp = maxMP;
                setHp(hp);
                setMaxHP(maxHP);
                setMp(mp);
                setMaxMP(maxMP);

                u.insert("hp", hp);
                u.insert("maxHP", maxHP);
                u.insert("mp", mp);
                u.insert("maxMP", maxMP);
                u.insert("ap", p.getAp() + 5);
                u.insert("sp", p.getSp() + 3);
                u.insert("exp", amount);
                p.send(Packets.updateStats(u));

                if (b != null)
                {
                    SkillWz hyperBody = Library.getSkill(b.getId());
                    Info hyperBodyInfo = hyperBody.getLevel(b.getLevel());
                    int x = hyperBodyInfo.getInt("x");
                    int y = (getInt("maxHP") * x) / 100;
                    int z = (getInt("maxMP") * x) / 100;

                    maxHP = Math.Min(30000, y + getInt("maxHP"));
                    maxMP = Math.Min(30000, z + getInt("maxMP"));

