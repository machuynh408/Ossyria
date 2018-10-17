using OssyriaDEV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Channel
{
    public class _1012000 : Script
    {
        private int id = 1012000;
        private Player p = null;

        private int[] maps = new int[] { 104000000, 102000000, 101000000, 103000000, 120000000 };
        private int[] cost = new int[] { 1200, 1200, 800, 1000, 1200 };
        private int state, mapId = -1, price = 0;
        public _1012000(Player p)
        {
            this.p = p;
        }

        public int getState()
        {
            return state;
        }
