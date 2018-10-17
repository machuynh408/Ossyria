using OssyriaDEV;
using System;

namespace Channel
{
    public class _0x55 : IHandler
    {
        public void Invoke(User u, Reader r)
        {
            Player p = u.getPlayer();
            Map m = p.getMap();

            Action action = new Action(() =>
            {
                int skill = r.readInt();
                int level = r.readByte();
                byte flag = r.readByte();
                int speed = r.readByte();
