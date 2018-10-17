using OssyriaDEV;
using System;
using System.Linq;

namespace Channel
{
    public class _0x5C : IHandler
    {
        public void Invoke(User u, Reader r)
        {
            r.readByte();
            string name = r.readMapleAsciiString();
            r.readByte();
            r.readByte();
            Player p = u.getPlayer();
            Map m = p.getMap();

            switch(name)
            {
                case "market00":
