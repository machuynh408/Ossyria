using OssyriaDEV;
using System;

namespace Channel
{
    public class _0xAB : IHandler
    {
        public void Invoke(User u, Reader r)
        {
            Player p = u.getPlayer();
            Map m = p.getMap();

            Action action = new Action(() =>
            {
                byte mode = r.readByte();
                r.readInt();
                r.readInt();
                int key = r.readInt();
