using OssyriaDEV;
using System;

namespace Channel
{
    public class _0x54 : IHandler
    {
        public void Invoke(User u, Reader r)
        {
            Player p = u.getPlayer();
            Map m = p.getMap();

            Action action = new Action(() =>
            {
