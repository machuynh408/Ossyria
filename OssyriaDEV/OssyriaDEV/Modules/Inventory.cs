using System;
using System.Collections.Generic;
using System.Linq;

namespace OssyriaDEV
{
    public class Inventory
    {
        private Player p = null;
        private string name = "";
        private Dictionary<byte, Item> items = null;
        private int capacity;
        public Inventory(Player p, string name, int capacity)
        {
            this.p = p;
            this.name = name;
            this.items = new Dictionary<byte, Item>();
            this.capacity = capacity;
        }
        public string getName()
        {
            return name;
        }

        public int getType()
        {
            switch (this.getName())
            {
                case "equip":
                    return 1;
                case "use":
                    return 2;
                case "set-up":
                    return 3;
                case "etc":
                    return 4;
                case "cash":
                    return 5;
            }
            return -1;
        }
        public int getCapacity()
        {
            return capacity;
        }
        public Item getByPos(byte pos)
        {
            Item i = null;
            items.TryGetValue(pos, out i);
            return i;
        }
        public Item getById(int id)
        {
            return getItems().Where(i => i.getId() == id).FirstOrDefault();
        }

        public bool hasItem(int id)
        {
            return getById(id) != null;
        }

        public List<Item> getItems()
        {
            return items.Values.ToList();
        }

        public List<Item> getItems(bool sorted = false)
        {
            if(!sorted)
                return items.Values.ToList();
            else
            {
                List<Item> data = new List<Item>();
                data.AddRange(items.Values.ToList());
                data.Sort(compareByPosition);
                return data;
            }
        }

        public List<Item> getItems(int id, bool sorted = false)
        {
            if(!sorted)
                return getItems().Where(i => i.getId() == id).ToList();
            else
            {
                List<Item> data = new List<Item>();
                data.AddRange(getItems().Where(i => i.getId() == id).ToList());
                data.Sort(compareByPosition);
                return data;
            }
        }

        private int compareByPosition(Item x, Item y)
        {
            return x.CompareTo(y);
        }

        public int getTotalQuantity(int id)
        {
            int quantity = 0;
            getItems(id).ForEach(i => { quantity += i.getQuantity(); });
            return quantity;
        }

        public void add(Item i)
        {
            if (items.ContainsKey(i.getByte("position")))
                return;
            i.insert("type", "inventory\\" + name);
            items.Add(i.getPosition(), i);
        }
        public void remove(byte pos)
        {
            if (!items.ContainsKey(pos))
                return;
            items.Remove(pos);
        }
        public void remove(Item i)
        {
            if (!items.ContainsKey(i.getPosition()))
                return;
            items.Remove(i.getPosition());
        }

        public void updateQuantity(byte pos, short quantity)
        {
            Item i = getByPos(pos);
            if (i == null)
                return;
            i.setQuantity(quantity);
            items[pos] = i;
            p.send(getConsume(i, false));
        }

        public void delete(byte pos, bool remove = false)
        {
            Item i = getByPos(pos);
            if (i == null)
                return;
            this.remove(pos);
            p.send(getConsume(i, remove));
        }

        public void consume(byte slot)
        {
            Item i = getByPos(slot);
            consume(i);
        }

        public void consume(int itemId)
        {
            Item i = getById(itemId);
            consume(i);
        }
        public void consume(Item i)
        {
            if (i == null)
                return;
            if (i.getQuantity() <= 0)
                return;

            i.setQuantity((short)(i.getQuantity() - 1));

            if (i.getQuantity() <= 0)
            {
                remove(i.getPosition());
                p.send(getConsume(i, true));
            }
            else
            {
                p.send(getConsume(i, false));
            }
        }

        public void consume(Item i, int quantity, bool delete = false) // consumes without removing
        {
            if (i == null)
                return;
            if (i.getQuantity() <= 0)
                return;

            if (!delete)
            {
                if (i.getQuantity() - quantity < 0) // allowed zero
                    return;
                i.setQuantity((short)(i.getQuantity() - quantity));
                p.send(getConsume(i, false));
            }
            else // will update/delete upon the quantity remainning
            {
                i.setQuantity((short)(i.getQuantity() - quantity));
                Console.WriteLine("Consume: " + i.getQuantity());
                if (i.getQuantity() <= 0)
                {
