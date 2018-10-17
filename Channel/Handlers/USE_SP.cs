using OssyriaDEV;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Channel
{
    public class _0x52 : IHandler
    {
        public void Invoke(User u, Reader r)
        {
            Player p = u.getPlayer();
            Map m = p.getMap();
            r.readInt();
            int id = r.readInt();
            int sp = p.getSp();
            if (sp <= 0)
