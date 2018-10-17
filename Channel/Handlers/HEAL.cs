using OssyriaDEV;
using System;

namespace Channel
{
    public class _0x51 : IHandler
    {
        public void Invoke(User u, Reader r)
        {
            Player p = u.getPlayer();
            Map m = p.getMap();

            Action action = new Action(() =>
            {
                r.readByte();
