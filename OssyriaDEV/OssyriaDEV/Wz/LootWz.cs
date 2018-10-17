using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssyriaDEV
{
    public class LootWz
    {
        private int id = -1;
        private Dictionary<int, int> data = null;
        public LootWz(int id)
        {
            this.id = id;
            this.data = new Dictionary<int, int>();
        }
        public int getId()
        {
            return id;
        }        

        public void insert(int id, int chance)
        {
            if (data.ContainsKey(id) && data[id] == chance)
                return;
            data.Add(id, chance);
        }
        public Dictionary<int, int> getData()
        {
            return data;
        }
