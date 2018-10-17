using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssyriaDEV
{
    public class Shop
    {
        private int id = -1;
        private Player p = null;
        private Dictionary<int, int> items = null;

        public Shop(int id, Player p, Dictionary<int, int> items)
        {
            this.id = id;
            this.p = p;
            this.items = items;
        }
        public void open()
        {
            Writer w = new Writer(0xEE);
            w.Create(new IntData(id));
            w.Create(new ShortData(items.Count));
            foreach (KeyValuePair<int, int> item in items)
            {
                w.Create(new IntData(item.Key));
                w.Create(new IntData(item.Value));
                if (Tools.parseItem(item.Key).Contains("rechargable"))
                {
                    ItemWz itemWz = Library.getItem(item.Key);
                    double unitPrice = itemWz.getDouble("unitPrice");
                    double slotMax = (short)itemWz.getSlotMax();
                    short value = Convert.ToInt16(Math.Floor(unitPrice * slotMax));
                    w.Create(new ShortData(0));
                    w.Create(new IntData(0));
                    Console.WriteLine("value: " + value);
                    w.Create(new ShortData(value));
                    w.Create(new ShortData(itemWz.getSlotMax()));
                }
                else
                {
                    w.Create(new ShortData(1));
                    w.Create(new ShortData(1000)); // 1,000 max items
                }
            }
            p.send(w);
        }
        public void close()
        {
            p.setNpc(null);
        }

        public void buy(int id, short quantity)
        {
            if (!items.ContainsKey(id))
                return;
