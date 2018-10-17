using OssyriaDEV;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Channel
{
    public class _0x9D : IHandler
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
            int key = r.readInt();
            short moveId = r.readShort();

            Monster monster = p.getMap().getMonster(key);
            if (monster == null)
            {
                p.remove(monster);
                return;
            }

            byte use = r.readByte();
            sbyte skill = r.readSByte();
            byte a = r.readByte();
            byte b = r.readByte();
            byte c = r.readByte();
            byte d = r.readByte();
            r.readByte();
            r.readInt();
            short x = r.readShort();
            short y = r.readShort();

            Position origin = new Position(x, y);
            Movement movement = new Movement(r);

            if (monster.getMaster() == null)
            {
                monster.stop(p);
                monster.setAggro(false);
                monster.setMarked(false);
                return;
            }
            else if (!monster.getMaster().isEquals(p))
            {
                if (monster.containsAttacker(p.getId()))
                    monster.switchMaster(p, true);
                else
                    return;
            }

            bool aggro = monster.getAggro();

            MobSkill mobSkill = null;

            if (use == 1)
            {
                MobWz mobWz = Library.getMob(monster.getId());
                if (mobWz.getSkills() != 0)
                {
                    mobSkill = mobWz.getRandomSkill();
                    MobSkillWz mobSkillWz = Library.getMobSkill(mobSkill.getId());
                    Info info = mobSkillWz.getLevel(mobSkill.getLevel());

                    if (info.contains("hp"))
                    {
                        double max = ((double)info.getInt("hp")) / 100.0;
                        double min = ((double)monster.getHp()) / ((double)monster.getMaxHP());

                        if (min > max)
                            mobSkill = null;
                    }
                    else
                    {
