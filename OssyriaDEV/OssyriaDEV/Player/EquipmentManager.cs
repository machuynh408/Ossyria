using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssyriaDEV
{
    public class EquipmentManager
    {
        private Player p = null;
        private Equipment equipment = null;
        public EquipmentManager(Player p)
        {
            this.p = p;
            this.equipment = new Equipment(p, "equip");
        }

        public Equipment getEquipment()
        {
            return equipment;
        }

        public void equip(short s, short d)
        {
            byte src = (byte)(s < 0 ? (s * -1) : s);
            byte dst = (byte)(d < 0 ? (d * -1) : d);

            Console.WriteLine("(Equipping) src: " + src + " dst: " + dst);

            Item source = p.getInventory("equip").getByPos(src);
            Item target = equipment.getByPos(dst);

            if (source == null)
                return;


            switch(dst)
            {
                case 5:
                    {
                        Item top = p.getEquipment().getByPos(5);
                        Item bottom = p.getEquipment().getByPos(6);

                        if (top != null && (Tools.parseEquip(source.getId()).Contains("overall")))
                        {
                            if (p.getInventory("equip").full((bottom != null && Tools.parseEquip(source.getId()).Contains("overall")) ? 1 : 0))
                            {
                                p.send(Inventory.getFull());
                                p.send(Inventory.getMessage(0xFF));
                                return;
                            }
                            //unequip(p, -5, p.getInventory().get("equip").getOpenSlot());
                        }

                        if (bottom != null && (Tools.parseEquip(source.getId()).Contains("overall")))
                        {
                            if (p.getInventory("equip").full())
                            {
                                p.send(Inventory.getFull());
                                p.send(Inventory.getMessage(0xFF));
                                return;
                            }
                            //unequip(p, -5, p.getInventory().get("equip").getOpenSlot());
                        }
                        break;
                    }
                case 6:
                    {
                        Item top = p.getEquipment().getByPos(5);
                        if (top != null && (Tools.parseEquip(top.getId()).Contains("overall")))
                        {
                            if (p.getInventory("equip").full())
                            {
                                p.send(Inventory.getFull());
                                p.send(Inventory.getMessage(0xFF));
                                return;
                            }
                            //unequip(p, -5, p.getInventory().get("equip").getOpenSlot());
                        }
                        break;
                    }
                case 10:
                    {
