using OssyriaDEV;
using System;

namespace Channel
{
    public class _0x38 : IHandler
    {
        public void Invoke(User u, Reader r)
        {
            Player p = u.getPlayer();
            Map m = p.getMap();
            Script s = p.getNpc() as Script;
            if (s == null || !p.alive())
            {
