using OssyriaDEV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Channel
{
    public class _0x4F : IHandler
    {
        public void Invoke(User u, Reader r)
        {
            try
            {
                Player p = u.getPlayer();
                r.readInt();
                byte src = (byte)r.readShort();
                sbyte dst = r.readSByte();
                r.readByte();
                Console.WriteLine("src: " + src + " dst: " + dst);
                bool useWhiteScroll = false;
                if (((byte)r.readShort() & 2) == 2)
                    useWhiteScroll = true;

                if (useWhiteScroll)
                {
                    List<Item> whiteScrolls = p.getInventory("use").getItems(2340000);
                    if (whiteScrolls.Count == 0)
                        return;
                    if (whiteScrolls.Where(x => x.getQuantity() > 0).ToList().Count == 0)
                        return;
                }

                Item equip = p.getEquipment().getByPos((byte)(dst < 0 ? (dst * -1) : dst));
                if (equip == null)
                    return;
                int tuc = equip.getInt("tuc");
                if (tuc < 1)
                    return;
                Item scroll = p.getInventory("use").getByPos(src);
                if (scroll == null)
                    return;
                if (scroll.getQuantity() <= 0)
                    return;


                int result = 0;

                ItemWz itemWz = Library.getItem(scroll.getId());
                if (itemWz == null)
                    return;
                int success = itemWz.getInt("success");
                int cursed = itemWz.contains("cursed") ? itemWz.getInt("cursed") : 0;
                Console.WriteLine("success: " + success + " curse: " + cursed);
                if (scroll.getId() == 2049000 || scroll.getId() == 2049001 || scroll.getId() == 2049002 || scroll.getId() == 2049003)
                {
                    // Clean Slate Scrolls
                    int recover = itemWz.getInt("recover");
                    CharacterWz characterWz = Library.getEquip(equip.getId());
                    if (characterWz == null)
                        return;

                    int maxRecover = characterWz.getInt("tuc") - tuc;
                    maxRecover -= equip.getInt("enhanceLVL"); // no slots to recover
                    if (maxRecover <= 0)
                        return;

                    if (Tools.isProc(success))
                    {
                        result = 1;
                        equip.insert("tuc", equip.getInt("tuc") + recover);
                    }
                    else
                    {
                        if (Tools.isProc(cursed))
                        {
                            result = -1;
                            p.getEquipmentManager().destroy((byte)(dst < 0 ? (dst * -1) : dst));
                        }
