using OssyriaDEV;
using System;

namespace Channel
{
    public class _0x43 : IHandler
    {
        public void Invoke(User u, Reader r)
        {
            Player p = u.getPlayer();
            Map m = p.getMap();

            r.readInt();
            short slot = r.readShort();

            int id = r.readInt();
            Inventory inventory = p.getInventory("use");
            Item i = inventory.getByPos((byte)slot);
            if (i == null)
                return; // player doesn't have this item packet edit
            if (i.getId() != id)
                return; // player getting this item packet edited the id 
            if (i.getQuantity() <= 0) // bad coding on my part because item has no quantity???
                return;

            inventory.consume((byte)slot);

            ItemWz itemWz = Library.getItem(id);
            Info spec = itemWz.getSpec();
            if(spec != null)
            {
                if (spec.contains("time")) // meaning this is an item buff 
                {
                    if (p.getBuff(id) != null)
                        p.getBuff(id).stop(); // stop the old buff and remove it before doing it again

