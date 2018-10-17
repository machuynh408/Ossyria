using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace OssyriaDEV
{
    public class Mount
    {
        private Item item = null;
        private int level = 1, exp = 0;
        private int tiredness;
        private Player master = null;
        private bool active = false;
        private Timer clock = null;
        public Mount(Player master)
        {
            this.master = master;
        }

        public int getId()
        {
            if (item == null)
                return -1;
            else
                return item.getId();
        }   
        public int getLevel()
        {
            return level;
        }
        public int getExp()
        {
