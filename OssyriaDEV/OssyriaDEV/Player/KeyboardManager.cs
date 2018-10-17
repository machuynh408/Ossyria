using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssyriaDEV
{
    public class KeyboardManager
    {
        private Player p = null;
        private Dictionary<int, Key> keys = null;
        public KeyboardManager(Player p)
        {
            this.p = p;
            this.keys = new Dictionary<int, Key>();
