using OssyriaDEV;
using System;
using System.Threading;

namespace Channel
{
    public class _0x95 : IHandler
    {
        public void Invoke(User u, Reader r)
        {
            Player p = u.getPlayer();
            Map m = p.getMap();

            Action action = new Action(() => { handle(p, r); });
            m.processAction(action);
        }

        private void handle(Player p, Reader r)
        {
            int key = r.readInt();
            Summon summon = p.getSummon();

            if (summon == null)
                return;
