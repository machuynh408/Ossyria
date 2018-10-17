using OssyriaDEV;
using System;

namespace Channel
{
    public class _0xA3 : IHandler
    {
        public void Invoke(User u, Reader r)
        {
            try
            {
                Player p = u.getPlayer();
                Map m = p.getMap();
                Console.WriteLine("(" + p.getName() + ") -> " + BitConverter.ToString(r.getPacket()).Replace("-", " "));

                Monster x = m.getMonster(r.readInt());
                if (x == null)
                    return;
                int source = r.readInt();
                Monster y = m.getMonster(r.readInt());
                if (y == null)
