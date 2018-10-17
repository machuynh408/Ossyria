using OssyriaDEV;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Channel
{
    public class _0x53 : IHandler
    {
        public void Invoke(User u, Reader r)
        {
            Player p = u.getPlayer();
            Map m = p.getMap();

            Action action = new Action(() => { handle(p, r); });
            m.processAction(action);
        }

        private void handle(Player p, Reader r)
        {
            r.readShort();
            r.readShort();
            int skillId = r.readInt();
            int level = r.readByte();

            Skill s = p.getSkillsManager().getSkill(skillId);
            if (s == null)
                return;
            if (s.getLevel() != level)
                return;
            SkillWz skillWz = Library.getSkill(skillId);
            Info i = skillWz.getLevel(level);

            int time = i.getInt("time") * 1000;

            int hpCon = i.getInt("hpCon");
            int mpCon = i.getInt("mpCon");
            int cooltime = i.getInt("cooltime");

            if (p.getCooldown(skillId) != null)
            {
                p.send(Packets.enableActions());
                return;
            }

            if (skillId != 2121004 && skillId != 2221004 && skillId != 2321004) // the Infinity on intial don't cost mana to use
            {
                if (p.getBuff(2121004) == null && p.getBuff(2221004) == null && p.getBuff(2321004) == null) // if the infinity buff isnt active 
                {
                    if (i.contains("mpCon") && i.contains("hpCon"))
                    {
                        if (p.getHp() - hpCon < 0)
                        {
                            p.send(Packets.enableActions());
                            return;
                        }

                        Buff concentrate = p.getBuff(3121008);
                        if (concentrate != null) // if the Concentrate buff is on
                        {
                            double y = Math.Floor(((double)mpCon) * (((double)concentrate.getInt("x")) / 100.0));
                            int z = Convert.ToInt32(y);
                            mpCon -= z;
                        }
                        if (p.getMp() - mpCon < 0)
                        {
                            p.send(Packets.enableActions());
                            return;
                        }
                        p.unheal(hpCon, mpCon);
                    }
                    else
                    {
                        Buff concentrate = p.getBuff(3121008);
                        if (concentrate != null) // if the Concentrate buff is on
                        {
                            double y = Math.Floor(((double)mpCon) * (((double)concentrate.getInt("x")) / 100.0));
                            int z = Convert.ToInt32(y);
                            mpCon -= z;
                        }
                        if (p.getMp() - mpCon < 0)
                        {
                            p.send(Packets.enableActions());
                            return;
                        }
                        p.unheal(-1, mpCon);
                    }
                }
            }

            if (skillId == 1004 && p.getMount() != null)
            {
                if (p.getBuff(skillId) != null)
                    p.getBuff(skillId).stop();
                Packets.enableActions();
                return;
            }

            if (skillId == 5221006 && p.getMount() != null) // Battleship
            {
                Packets.enableActions(); // can't use battleship while on a mount
                return;
            }

            cancel(p, skillId);

            switch (skillId)
            {
                case 2121005: // Elquines
                case 2221005: // Ifrit
                case 2321003: // Bahamut
                case 3121006: // Phoenix
                case 3221005: // Frostprey
                case 2311006: // Summon Dragon
                case 3111005: // Silver Hawk
                case 3211005: // Golden Eagle
                    {
                        p.removeSummon();
                        break;
                    }
            }

            Buff b = null;
            int effect = 1;

            Console.WriteLine("Buff: " + skillId);
            switch (skillId)
            {
                case 1001003: // Iron Body
                    {
                        b = new Buff(skillId, level, time);
                        b.getUpdate().insert("pdd", i.getInt("pdd"));
                        effect = 0;
                        break;
                    }
                case 1101006: // Rage
                    {
                        b = new Buff(skillId, level, time);
                        b.getUpdate().insert("pad", i.getInt("pad"));
                        b.getUpdate().insert("pdd", i.getInt("pdd"));
                        effect = 2;
                        break;
                    }
                case 1101007: // Power Guard
                case 1201007: // Power Guard
                    {
                        b = new Buff(skillId, level, time);
                        b.getUpdate().insert("power guard", i.getInt("x"));
                        effect = 1;
                        break;
                    }
                case 1111002: // Combo Attack
                    {
                        b = new Buff(skillId, level, time);
                        b.insert("orbCount", 0);
                        b.getUpdate().insert("combo attack", 1);
                        effect = 1;
                        break;
                    }
                case 1111007: // Armor Crash
                case 1211009: // Magic Crash
                case 1311007: // Power Crash
                    {

                        int prop = i.getInt("prop");
                        int mobCount = i.getInt("mobCount");
                        Position position = null;

                        int count = 0;
                        Rectangle range = s.calcRange(p.getPosition());

                        if(Tools.isProc(prop))
                        {
                            foreach (Monster monster in p.getMap().getMonsters())
                            {
                                position = monster.getPosition();

                                if (range.Contains(position.getX(), position.getY()))
                                {
                                    if (skillId == 1111007 && monster.containsStatus("incPDD"))
                                    {
                                        monster.stopStatus("incPDD");
                                        count++;
                                    }
                                    if (skillId == 1211009 && monster.containsStatus("incMDD"))
                                    {
                                        monster.stopStatus("incMDD");
                                        count++;
                                    }
                                    if (skillId == 1311007 && monster.containsStatus("incPAD"))
                                    {
                                        monster.stopStatus("incPAD");
                                        count++;
                                    }
                                    if (skillId == 1311007 && monster.containsStatus("incMAD"))
                                    {
                                        monster.stopStatus("incMAD");
                                        count++;
                                    }
                                }
                                if (count == mobCount)
                                    break;
                            }
                        }
                        p.broadcast(Packets.getEffect(p, effect, skillId, level, 3));
                        return;
                    }
                case 1121010: // Enrage
                    {
                        if (p.getBuff(1111002) == null)
                        {
                            p.send(Packets.enableActions());
                            return;
                        }

                        int orbCount = p.getBuff(1111002).getInt("orbCount");
                        if (orbCount != 10)
                        {
                            p.send(Packets.enableActions());
                            return;
                        }
                        _0x29.updateComboAttack(p, 0);

                        b = new Buff(skillId, level, time);
                        b.getUpdate().insert("pad", i.getInt("pad"));
                        effect = 1;
                        break;
                    }

                case 1211003: // Fire Charge: Sword
                case 1211005: // Ice Charge: Sword
                case 1211007: // Thunder Charge: Sword
                case 1221003: // Holy Charge : Sword
                    {
                        Item weapon = p.getEquipment().getByPos(11);
                        if (weapon == null)
                        {
                            p.send(Packets.enableActions());
                            return;
                        }

                        string type = Tools.parseEquip(weapon.getId());
                        if (!type.Contains("sword"))
                        {
                            p.send(Packets.enableActions());
                            return;
                        }
                        b = new Buff(skillId, level, time);
                        b.getUpdate().insert("charge", i.getInt("x"));
                        effect = 1;
                        break;
                    }
                case 1211004: // Flame Charge: BW
                case 1211006: // Blizzard Charge: BW
                case 1211008: // Lightning Charge: BW
                case 1221004: // Divine Charge : BW
                    {
                        Item weapon = p.getEquipment().getByPos(11);
                        if (weapon == null)
                        {
                            p.send(Packets.enableActions());
                            return;
                        }
                        string type = Tools.parseEquip(weapon.getId());
                        if (!type.Contains("blunt weapon"))
                        {
                            p.send(Packets.enableActions());
                            return;
                        }
                        b = new Buff(skillId, level, time);
                        b.getUpdate().insert("charge", i.getInt("x"));
                        effect = 1;
                        break;
                    }
                case 1301006: // Iron Will
                    {
                        b = new Buff(skillId, level, time);
                        b.getUpdate().insert("pdd", i.getInt("pdd"));
                        b.getUpdate().insert("mdd", i.getInt("mdd"));
                        effect = 2;
                        break;
                    }
                case 1101004: // Sword Booster
                case 1101005: // Axe Booster
                case 1201004: // Sword Booster
                case 1201005: // BW Booster
                case 1301004: // Spear Booster
                case 1301005: // Pole Arm Booster
                case 2111005: // Spell Booster
                case 2211005: // Spell Booster
                case 3101002: // Bow Booster
                case 3201002: // Crossbow Booster
                case 4101003: // Claw Booster
                case 4201002: // Dagger Booster
                case 5101006: // Knuckler Booster
                case 5201003: // Gun Booster
                    {
                        b = new Buff(skillId, level, time);
                        b.getUpdate().insert("booster", i.getInt("x"));
                        effect = 1;
                        break;
                    }
                case 5121009: // Speed Infusion
                    {
                        b = new Buff(skillId, level, time);
                        b.getUpdate().insert("speed infusion", 1);
                        effect = 1;
                        break;
                    }
                case 1301007: // Hyper Body
                case 9001008: // Hyper Body
                case 9101008: // Hyper Body
                    {
                        b = new Buff(skillId, level, time);

                        SkillWz hyperBody = Library.getSkill(skillId);
                        Info hyperBodyInfo = hyperBody.getLevel(level);
                        int x = hyperBodyInfo.getInt("x");
                        int y = (p.getStatsManager().getInt("maxHP") * x) / 100;
                        int z = (p.getStatsManager().getInt("maxMP") * x) / 100;

                        int maxHP = Math.Min(30000, p.getStatsManager().getInt("maxHP") + y);
                        int maxMP = Math.Min(30000, p.getStatsManager().getInt("maxMP") + z);

                        p.setMaxHP(maxHP);
                        p.setMaxMP(maxMP);
                        b.getUpdate().insert("maxHP", hyperBodyInfo.getInt("x"));
                        b.getUpdate().insert("maxMP", hyperBodyInfo.getInt("y"));
                        break;
                    }
                case 1311008: // Dragon Blood
                    {
                        if (p.getHp() - hpCon <= 0)
                        {
                            p.send(Packets.enableActions());
                            return; // trying to buff without hp
                        }

                        b = new Buff(skillId, level, time); // dragon blood procs hp loss every 4 seconds
                        b.getUpdate().insert("dragon blood", i.getInt("x"));
                        b.getUpdate().insert("pad", i.getInt("pad"));
