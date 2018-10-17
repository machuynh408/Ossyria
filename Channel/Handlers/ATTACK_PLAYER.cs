using OssyriaDEV;
using System;

namespace Channel
{
    public class _0x2D : IHandler
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
            r.readInt();
            int type = r.readByte();
            r.readByte();
            int damage = r.readInt();

            int id = -1, key = -1;
            byte direction = 0;
            Monster monster = null;
            if (type == 254)
            {

            }
            else
            {
                id = r.readInt();
                key = r.readInt();
                monster = p.getMap().getMonster(key);
                direction = r.readByte();
                r.readShort();
                if (r.readByte() == 1) // Stance
                {
                    switch (p.getClass())
                    {
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
                                if (p.getSkillsManager().getSkill(1121002) != null ||
                                    p.getSkillsManager().getSkill(1221002) != null ||
                                    p.getSkillsManager().getSkill(1321002) != null)
                                {
                                    Skill powerStance = null;
                                    if (p.getSkillsManager().getSkill(1121002) != null)
                                        powerStance = p.getSkillsManager().getSkill(1121002);
                                    else if (p.getSkillsManager().getSkill(1221002) != null)
                                        powerStance = p.getSkillsManager().getSkill(1221002);
                                    else if (p.getSkillsManager().getSkill(1321002) != null)
                                        powerStance = p.getSkillsManager().getSkill(1321002);
                                    p.broadcast(Packets.getEffect(p, 5, powerStance.getId(), powerStance.getLevel(), 3));
                                }
                                break;
                            }
                        case 500:
                        case 510:
                        case 511:
                        case 512:
                            {
                                if (p.getPassive(5110001) != null) // Energy Charge (Stance)
                                {
                                    Skill energyCharge = p.getSkillsManager().getSkill(5110001);
                                    p.broadcast(Packets.getEffect(p, 5, energyCharge.getId(), energyCharge.getLevel(), 3));
                                }
                                break;
                            }
                    }
                }
            }
