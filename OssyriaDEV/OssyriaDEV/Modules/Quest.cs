using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssyriaDEV
{
    public class Quest : Info
    {
        private int id = -1;
        private Dictionary<int, int> items = null;
        private Dictionary<int, int> mobs = null;

        public Quest()
        {
            this.items = new Dictionary<int, int>();
            this.mobs = new Dictionary<int, int>();
        }
        public Quest(int id)
        {
            this.id = id;
            this.items = new Dictionary<int, int>();
            this.mobs = new Dictionary<int, int>();
            insert("id", id);
