using OssyriaDEV;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Channel
{
    public class _0x2A : IHandler
    {
        public void Invoke(User u, Reader r)
        {
            Player p = u.getPlayer();
            Map m = p.getMap();

            Action action = new Action(() =>{ handle(p, r); });
            m.processAction(action);

        }

        private void handle(Player p, Reader r)
        {
            Attack attack = new Attack(r, p, true);

            Console.WriteLine("Ranged: " + attack.getSkill());

            Item weapon = p.getEquipment().getByPos(11);

            if (weapon == null)
                return;

            int bulletCon = 0; // default

            switch (attack.getSkill())
            {
                case 0:
                case 3001004: // Arrow Blow
                case 3101003: // Power Knock-Back
                case 3101005: // Arrow Bomb : Bow
                case 3111003: // Inferno
                case 3121003: // Dragon's Breath
                case 3121004: // Hurricane
                case 3201003: // Power Knock-Back
                case 3201005: // Iron Arrow : Crossbow
                case 3211003: // Blizzard
                case 3221001: // Piercing Arrow
                case 3221003: // Dragon's Breath
                case 3221007: // Snipe
                case 4101005: // Drain
                    bulletCon = 1;
                    break;
                case 3001005: // Double Shot
                case 4001344: // Lucky Seven
                    bulletCon = 2;
                    break;
                case 4111005: // Avenger
                case 4121007: // Triple Throw
                    bulletCon = 3;
                    break;
                case 3111006: // Strafe
                case 3211006: // Strafe
                    bulletCon = 4;
                    break;
            }

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

                // Battleship Cannon              // Battleship Torpedo
                if ((attack.getSkill() == 5221007 || attack.getSkill() == 5221008) && p.getBuff(5221006) == null)
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
