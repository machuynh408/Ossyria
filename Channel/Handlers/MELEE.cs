using OssyriaDEV;
using System;

namespace Channel
{
    public class _0x29 : IHandler
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
            Attack attack = new Attack(r, p, false);

            Console.WriteLine("Melee: " + attack.getSkill());

            if (attack.getSkill() > 0)
            {
                SkillWz skillWz = Library.getSkill(attack.getSkill());
                Skill s = p.getSkillsManager().getSkill(attack.getSkill());
                if (s == null)
                    return; // player doesn't have the skill
                if (s.getLevel() <= 0)
                    return; // trying to use a skill that isn't atleast level 1
                attack.setLevel(s.getLevel());
                Info i = skillWz.getLevel(s.getLevel());

                int hpCon = i.getInt("hpCon");
                int mpCon = i.getInt("mpCon");
                int x = i.getInt("x");
                int prop = i.getInt("prop");
                int time = i.getInt("time") * 1000; // Status effects for monsters
                int mobCount = i.getInt("mobCount");
                int attackCount = i.getInt("attackCount");
                int cooltime = i.getInt("cooltime");

                if (p.getCooldown(attack.getSkill()) != null)
                {
                    p.send(Packets.enableActions());
                    return;
                }

                if (i.contains("mobCount"))
                {
                    if (attack.getMonsterCount() > mobCount)
                    {
                        p.send(Packets.enableActions());
                        return; // the player hit more monsters than 6 for this skill
                    }
                }

                if (i.contains("attackCount"))
                {
                    if (attack.getAtacksPerMonster() > attackCount)
                    {
                        p.send(Packets.enableActions());
                        return; // the player hit more monsters than 6 for this skill
                    }
                }

                if (attack.getSkill() == 1311005)
                {
                    if (attack.getMonsterCount() == 1)
                    {
                        int damage = attack.getTotal(attack.getSingle());
                        double d = ((double)x) / 100.0;
                        hpCon = Convert.ToInt32(Math.Round(((double)p.getHp()) * d));
                        if (p.getHp() - hpCon <= 0)
                        {
                            p.send(Packets.enableActions());
                            return;
                        }
                    }
                    else
                    {
                        hpCon = -1;
                    }
                    p.unheal(hpCon, mpCon);
                }
                else if (attack.getSkill() == 1311006) // Dragon Roar
                {
                    double d = ((double)x) / 100.0;
                    hpCon = Convert.ToInt32(Math.Round(((double)p.getHp()) * d));
                    if (p.getHp() - hpCon < 0)
                    {
                        p.send(Packets.enableActions());
                        return;
                    }
                    if (p.getMp() - mpCon < 0)
                    {
                        p.send(Packets.enableActions());
                        return;
                    }
                    p.unheal(hpCon, mpCon);
                }
                else if (i.contains("mpCon") && i.contains("hpCon"))
                {
                    if (p.getHp() - hpCon < 0)
                    {
