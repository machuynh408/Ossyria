using OssyriaDEV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Channel
{
    public class _0x62 : IHandler
    {
        public void Invoke(User u, Reader r)
        {
            Player p = u.getPlayer();
            Map m = p.getMap();

            byte action = r.readByte();
            short quest = r.readShort();

            Console.WriteLine("(Quest) Action: " + action + " Quest: " + quest);
            switch(action)
            {
                case 1: // Start
                    {
                        int npcId = r.readInt();
                        r.readInt();
                        p.getQuestsManager().startQuest(quest);

                        Writer w = new Writer(0x24);
                        w.Create(new ByteData(1));
                        w.Create(new ShortData(quest));
                        w.Create(new ShortData(1));
