using OssyriaDEV;
using System;
using System.Drawing;

namespace Channel
{
    public class _0x2B : IHandler
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

            Console.WriteLine("Magic: " + attack.getSkill());

            Mist mist = null;
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

