using OssyriaDEV;
using System;

namespace Channel
{
    public class _0x39 : IHandler
    {
        public void Invoke(User u, Reader r)
        {
            Player p = u.getPlayer();
            Map m = p.getMap();
            byte mode = r.readByte();
            Shop s = p.getNpc() as Shop;
            if(s == null)
            {
