using OssyriaDEV;
using System;

namespace Channel
{
    public class _0x42 : IHandler
    {
        public void Invoke(User u, Reader r)
        {
            Player p = u.getPlayer();

            r.readBytes(4);
            byte type = r.readByte();
            short src = r.readShort();
            short dst = r.readShort();
            short quantity = r.readShort();
            Console.WriteLine("src: " + src + " dst: " + dst + " quantity: " + quantity);
            if (src < 0 && dst > 0)
                p.getEquipmentManager().unequip(src, dst);
            else if (dst < 0)
                p.getEquipmentManager().equip(src, dst);
            else if (dst == 0)
            {
                drop(p, type, src, quantity);
            }
            else
                move(p, type, src, dst);
            p.send(Packets.enableActions());
        }

        private void drop(Player p, byte type, short s, short quantity)
        {
            Inventory inventory = p.getInventory(Tools.parseInventory(type));
            if(inventory == null)
            {
                p.send(Packets.enableActions());
                return;
            }
            byte src = (byte)(s < 0 ? (s * -1) : s);

            if (src <= 0 || src > inventory.getCapacity())
                return;

            Item item = inventory.getByPos(src);
            if (item == null)
                return;
            if (quantity <= 0)
                return;

            if(!item.isRechargable())
            {
                if (item.getQuantity() - quantity < 0)
