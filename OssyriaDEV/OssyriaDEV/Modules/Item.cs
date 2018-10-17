using System;

namespace OssyriaDEV
{
    public class Item : Info, IComparable
    {
        private int id;

        public Item() : base("Item")
        {

        }
        public Item(int id, byte position) : base("Item")
        {
            this.id = id;
            this.insert("id", this.id);
            this.insert("position", position);
        }
        public Item(int id, short quantity) : base("Item")
        {
            this.id = id;
            this.insert("id", this.id);
            this.insert("quantity", quantity);
        }
