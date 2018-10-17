using OssyriaDEV;
using System;
using System.Threading;

namespace Channel
{
    public class _0xC6 : IHandler
    {
        public void Invoke(User u, Reader r)
        {
            Player p = u.getPlayer();
            int action = r.readByte();
            switch(action)
            {
                case 3: // Buy
                    {
                        r.readByte();
                        int type = r.readInt();
                        Console.WriteLine("type: " + type); // 2 = maple points // nexon game card cash = 4 // paypal/paybycash = 1
                        int sn = r.readInt();
                        CommodityWz commodityWz = Library.getCommodityWz(sn);
                        if (commodityWz == null)
                            return;
                        int itemId = commodityWz.getInt("ItemId");
                        int count = commodityWz.getInt("Count");
                        int period = commodityWz.getInt("Period");
                        string name = Tools.parseInventory(itemId);
                        Item item = Factory.createItem(itemId, 1);
                        item.insert("sn", sn);
                        u.send(getBuy(u.getId(), sn, itemId));
                        u.send(_0x25.getCash(u));
                        Console.WriteLine("itemId: " + itemId + " sn: " + sn);
                        p.getCashInventoryManager().add(item);
                        break;
                    }
                case 0x0C: 
                    {
                        //Console.WriteLine(BitConverter.ToString(r.getPacket()).Replace("-", " "));
                        try
                        {
                            int i = r.readInt();
                            r.readBytes(6);
                            byte pos = r.readByte();
