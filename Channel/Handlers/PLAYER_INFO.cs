using OssyriaDEV;
using System;
namespace Channel
{
    public class _0x59 : IHandler
    {
        public void Invoke(User u, Reader r)
        {
            Player p = u.getPlayer();
            Map m = p.getMap();
            Action action = new Action(()=> 
            {
                r.readInt();
                int id = r.readInt();
                Player x = m.getPlayer(id, true);
                if (x == null)
