using OssyriaDEV;
using System;
namespace Channel
{
    public class _0x36 : IHandler
    {
        public void Invoke(User u, Reader r)
        {
            Player p = u.getPlayer();
            Map m = p.getMap();

            if(!p.alive())
            {
                p.send(Packets.enableActions());
                return;
            }
            Action action = new Action(() => { handle(p, r); });
            m.processAction(action);
