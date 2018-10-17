using OssyriaDEV;
using System;

namespace Channel
{
    public class _0x23 : IHandler
    {
        public void Invoke(User u, Reader r)
        {
            Player p = u.getPlayer();
            Map m = p.getMap();
            Map destination = null;

            if(r.available() == 0)
            {
                // save
                u.send(Packets.getInfo(p));
                return;
            }

