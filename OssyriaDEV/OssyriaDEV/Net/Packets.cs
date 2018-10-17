using System;
using System.Collections.Generic;
using System.Net;

namespace OssyriaDEV
{
    public class Packets
    {
        private static long getKoreanTime(long t)
        {
            long time = (t / 1000 / 60); // convert to minutes
            return ((time * 600000000) + 116444448000000000L); // PST
        }
        public static long getTime(long t)
        {
            long time = (t / 1000); // convert to seconds
            return ((time * 10000000) + 116444448000000000L); // PST
        }
        public static void getVisual(Writer w, Player p, bool megaphone)
        {
            w.Create(
                new ByteData(p.getGender()),
                new ByteData(p.getSkin()),
                new IntData(p.getFace()),
                new ByteData((byte)(megaphone ? 1 : 0)),
                new IntData(p.getHair())
            );
            getEquips(w, p);
        }
        public static void getStats(Writer w, Player p)
        {
            w.Create(
                new IntData(p.getId()),
                new PadStringData(13, p.getName()),
                new ByteData(p.getGender()),
                new ByteData(p.getSkin()),
                new IntData(p.getFace()),
                new IntData(p.getHair()),
                new LongData(0), new LongData(0), new LongData(0),

                new ByteData(p.getLevel()),
                new ShortData(p.getClass()),
                new ShortData(p.getStr()),
                new ShortData(p.getDex()),
                new ShortData(p.getInt()),
                new ShortData(p.getLuk()),
                new ShortData(p.getHp()),
                new ShortData(p.getMaxHP()),
                new ShortData(p.getMp()),
                new ShortData(p.getMaxMP()),
                new ShortData(p.getAp()),
                new ShortData(p.getSp()),

                new IntData(p.getExp()),
                new ShortData(p.getFame()),
                new IntData(0), // gacha exp
                new IntData(p.getStatsManager().getInt("map")),
                new ByteData(p.getStatsManager().getByte("spawn")),
                new IntData(0)
            );           
        }

        public static void getEquips(Writer w, Player p)
        {
            Equipment e = p.getEquipment();
            Dictionary<int, int> normal = new Dictionary<int, int>();
            Dictionary<int, int> masked = new Dictionary<int, int>();

            e.getItems().ForEach(x =>
            {
                byte pos = x.getPosition();

                if (pos < 100 && !normal.ContainsKey(pos))
                {
                    normal.Add(pos, x.getId());
                }
                else if (pos > 100 && pos != 111)
                {
                    pos -= 100;
                    if (normal.ContainsKey(pos))
                    {
                        masked.Add(pos, normal[pos]);
                    }
                    normal[pos] = x.getId();
                }
                else if (normal.ContainsKey(pos))
                {
                    masked.Add(pos, x.getId());
                }              
            });

            foreach (KeyValuePair<int, int> x in normal)
                w.Create(new ByteData(x.Key), new IntData(x.Value));
            w.Create(new ByteData(0xFF));

            foreach (KeyValuePair<int, int> x in masked)
                w.Create(new ByteData(x.Key), new IntData(x.Value));
            w.Create(new ByteData(0xFF));

            Item weaponMask = e.getByPos(111);
            w.Create(new IntData(weaponMask != null ? weaponMask.getId() : 0));
            w.Create(new BytesData(12));
        }

        public static Writer getAddress(int port, int id, bool newChannel = false)
        {
            Writer w = null;
            string address = Settings.ADDRESS;
            if (address == "192.168.1.90")
                address = "99.119.8.53";
            if (!newChannel)
            {
                w = new Writer(0x0C);
                w.Create(
                    new ShortData((short)0),
                    new BytesData(IPAddress.Parse(address).GetAddressBytes()),
                    new ShortData((short)port),
                    new IntData(id),
                    new BytesData(5)
                );
            }
            else
            {
                w = new Writer(0x10);
                w.Create(
                    new ByteData(1),
                    new BytesData(IPAddress.Parse(address).GetAddressBytes()),
                    new ShortData((short)port)
                );
            }
            return w;
        }

        public static Writer getInfo(Player p)
        {
            Writer w = new Writer(0x5C);
            w.Create(new IntData(p.getChannel() - 1)); // this is channel (1) - 1
            w.Create(new ByteData(1), new ByteData(1), new ShortData(0));

            w.Create(new IntData(Tools.getInt()));
            w.Create(new HexaStringData("F8 17 D7 13 CD C5 AD 78"));
            w.Create(new LongData(-1));
            getStats(w, p);
            w.Create(new ByteData(20)); // buddy slots nigggga
            getInventory(w, p);
            getSkills(w, p);
            getQuests(w, p);
            w.Create(new ShortData(0)); // Some Mini Games shit

            // Rings 
            w.Create(new ShortData(0));
            w.Create(new ShortData(0));
            w.Create(new ShortData(0));

            for (int i = 0; i < 15; i++)
            {
                w.Create(new IntData(999999999));
            }

            w.Create(new IntData(0), new LongData(DateTime.Now.ToFileTimeUtc()));
            return w;
        }
        private static void getQuests(Writer w, Player p)
        {
            w.Create(new ShortData(0)); // started quests count
            w.Create(new ShortData(0)); // completed quests count
        }

        public static void getInventory(Writer w, Player p)
        {
            w.Create(new IntData(p.getMesos())); // Mesos
            w.Create(
                new ByteData(p.getInventory("equip").getCapacity()),
                new ByteData(p.getInventory("use").getCapacity()),
                new ByteData(p.getInventory("set-up").getCapacity()),
                new ByteData(p.getInventory("etc").getCapacity()),
                new ByteData(p.getInventory("cash").getCapacity())
            );
            List<Item> e = new List<Item>();
            e.AddRange(p.getEquipment().getItems());

            e.ForEach(x => getEquip(w, x));
            w.Create(new ShortData(0));

            p.getInventory("equip").getItems().ForEach(x => getEquip(w, x));
            w.Create(new ByteData(0));

            p.getInventory("use").getItems().ForEach(x => getItem(w, x));
            w.Create(new ByteData(0));

            p.getInventory("set-up").getItems().ForEach(x => getItem(w, x));
            w.Create(new ByteData(0));

            p.getInventory("etc").getItems().ForEach(x => getItem(w, x));
            w.Create(new ByteData(0));

            p.getInventory("cash").getItems().ForEach(x => getItem(w, x));
        }

        public static void getEquip(Writer w, Item i)
        {
            getEquip(w, i, true);
        }
        public static void getEquip(Writer w, Item i, byte pos)
        {
            w.Create(new ByteData(pos));
            getEquip(w, i, false);
        }
        public static void getEquip(Writer w, Item i, bool pos)
        {
            bool isMasked = false;
            bool isRing = i.getBool("ring");
            bool isEquipped = i.getString("type").Contains("equipment");

            if (pos)
            {
                byte position = i.getByte("position");

                if(position > 100 || isRing)
                {
                    isMasked= true;
                    w.Create(new ByteData(0), new ByteData(position - 100));
                }
                else
                {
                    w.Create(new ByteData(position));
                }
            }

            w.Create(new ByteData(1));
            w.Create(new IntData(i.getId()));

            if(isMasked && !isRing)
            {
                w.Create(new HexaStringData("01 41 B4 38 00 00 00 00 00 80 20 6F"));
                getExpiration(w, 0, false);
            }
            else if(isRing)
            {
                w.Create(new LongData(getKoreanTime((long)(DateTime.Now.ToFileTimeUtc() * 1.2))));
            }
            else
            {
                w.Create(new ShortData(0)); // cash
                w.Create(new HexaStringData("80 05")); // no expiration
                getExpiration(w, 0, false);
            }
            w.Create(new ByteData(i.getByte("tuc")));
            w.Create(new ByteData(i.contains("enhanceLVL") ? i.getInt("enhanceLVL") : 0)); // how many upgraded slots worked
            w.Create(new ShortData(i.getShort("incSTR")));
            w.Create(new ShortData(i.getShort("incDEX")));
            w.Create(new ShortData(i.getShort("incINT")));
            w.Create(new ShortData(i.getShort("incLUK")));
            w.Create(new ShortData(i.getShort("incMHP")));
            w.Create(new ShortData(i.getShort("incMMP")));
            w.Create(new ShortData(i.getShort("incPAD")));
            w.Create(new ShortData(i.getShort("incMAD")));
            w.Create(new ShortData(i.getShort("incPDD")));
            w.Create(new ShortData(i.getShort("incMDD")));
            w.Create(new ShortData(i.getShort("incACC")));
            w.Create(new ShortData(i.getShort("incEVA")));
            w.Create(new ShortData(0));// hands
            w.Create(new ShortData(i.getShort("incSpeed")));
            w.Create(new ShortData(i.getShort("incJump")));
            w.Create(new AsciiStringData(""));
            w.Create(new ByteData(0)); // locked

            if (isRing && !isEquipped)
                w.Create(new ByteData(0));

            if (!isMasked && !isRing)
                w.Create(new ByteData(0), new LongData(0));
        }
