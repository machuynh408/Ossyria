using OssyriaDEV;
using System;

namespace Channel
{
    public class _0x6F : IHandler
    {
        public void Invoke(User u, Reader r)
        {
            Player p = u.getPlayer();
            Map m = p.getMap();
            Console.WriteLine("[" + p.getName() + "] - " + BitConverter.ToString(r.getPacket()).Replace("-", " "));
            byte mode = r.readByte();
            switch (mode)
            {
                case 0: // Start
                    {
                        byte action = r.readByte();
                        if (action == 3) // Create 
                        {
                            if (p.getTrade() == null)
                                new Trade(Tools.getLong(), p);
                            else
                                p.send(Packets.notice("You are already in a trade."));
                        }
                        break;
                    }
                case 2: // Invite
                    {
                        int target = r.readInt();
                       /* Player partner = Ossyria.getWorld().getPlayer(target);
                        if (partner == null) // Player isn't online
                        {
                            Packets.enableActions();
                        }
                        else if (p.getMap().getId() != partner.getMap().getId()) // Player isn't in the same map...
                        {
                            Packets.enableActions();
                        }
                        else
                        {
                            bool valid = p.getTrade().invite(partner);
                            if (valid)
                            {
                                Trade trade = p.getTrade();
                             //   trade.OnInvitation += OnTradeInvitation;
                                trade.run();
                            }
                        }*/
                        break;
                    }
                case 3: // Decline
                    {
                        int target = r.readInt();
                        Player partner = m.getPlayer(target);
                        if (partner != null)
                        {
                            partner.send(Packets.notice(p.getName() + " has declined your trade request."));
                            partner.getTrade().terminate();
                        }
                        else
