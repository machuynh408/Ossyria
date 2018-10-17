using OssyriaDEV;
using System;

namespace Auth
{
    public class _0x16 : IHandler
    {
        public void Invoke(User u, Reader r)
        {
            string name = r.readMapleAsciiString();
            if (!_0x15.check(name))
                return;
            Player p = new Player(u.getConnection());
            p.setUsername(u.getUsername());
            p.getStatsManager().insert("id", 1337);
            p.getStatsManager().insert("name", name);
            p.getStatsManager().insert("face", r.readInt());
            int hair = r.readInt();
            int color = r.readInt();
            p.getStatsManager().insert("hair", hair + color);
            p.getStatsManager().insert("skin", r.readInt());

            int top = r.readInt();
            int pants = r.readInt();
            int shoes = r.readInt();
            int weapon = r.readInt();
            p.getStatsManager().insert("gender", r.readByte());

            int strength = r.readByte();
            int dexterity = r.readByte();
            int intelligence = r.readByte();
            int luk = r.readByte();

            bool valid = true;

            int total = strength + dexterity + intelligence + luk;
            if (total != 25 || strength < 4 || dexterity < 4 || intelligence < 4 || luk < 4)
            {
                valid = false;
            }
            if (p.getGender() == 0)
            {
                if (p.getFace() != 20000 && p.getFace() != 20001 && p.getFace() != 20002)
                {
                    valid = false;
                }
                if (p.getHair() != 30000 && p.getHair() != 30020 && p.getHair() != 30030)
                {
                    valid = false;
                }
