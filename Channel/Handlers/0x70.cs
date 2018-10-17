using OssyriaDEV;

namespace Channel
{
    public class _0x70 : IHandler
    {
        public void Invoke(User u, Reader r)
        {
            Player p = u.getPlayer();
            Map m = p.getMap();
            int operation = r.readByte();
            switch (operation)
            {
                case 1: // Create
                    {
                        if (p.getParty() == null)
                        {
                            /*Party party = Ossyria.getWorld().doParty(p);

                            Writer w = new Writer(0x3B);
                            w.Create(new ByteData(8));
                            w.Create(new IntData(party.getId()));
                            w.Create(new BytesData(new byte[] { (byte)0xFF, (byte)0xC9, (byte)0x9A, 0x3B }));
                            w.Create(new BytesData(new byte[] { (byte)0xFF, (byte)0xC9, (byte)0x9A, 0x3B }));
                            w.Create(new HexaStringData("FC BE 63 17"));
                            p.send(w);*/
                        }
                        else
                        {
                            p.send(Packets.notice("You can't create a party as you are already in one."));
                        }
                        break;
                    }
                case 2: // Leave
                    {
                        if (p.getParty() != null)
                        {
                            /*Party party = p.getParty();
                            if (p.getId() == party.getLeader().getId())
                            {
                                Ossyria.getWorld().stopParty(party.getId());
                            }
                            else
                            {
