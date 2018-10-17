using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssyriaDEV
{
    public class InventoryManager
    {
        private Player p = null;
        private Inventory equip = null, use = null, setup = null, etc = null, cash = null;
        public InventoryManager(Player p)
        {
