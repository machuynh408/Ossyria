using OssyriaDEV;

namespace Channel
{
    public class _0x25 : IHandler
    {
        public void Invoke(User u, Reader r)
        {
            Player p = u.getPlayer();
            Map m = p.getMap();
            if (!p.alive())
            {
                p.dispose();
                return;
            }

            //p.debuff();
            m.leave(p);

            p.send(Packets.notice("Hello, " + p.getName() + " please wait as the Cash Shop is loading!"));

            Writer w = new Writer(0x5E);
            w.Create(new LongData(-1));
            Packets.getStats(w, p);
            w.Create(new ByteData(20)); // buddy slots nigggga
            Packets.getInventory(w, p);
            Packets.getSkills(w, p);
