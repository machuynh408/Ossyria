using MapleLib.WzLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace OssyriaDEV
{
    public class Library
    {
        private static Dictionary<int, CharacterWz> equipCache = new Dictionary<int, CharacterWz>();
        private static Dictionary<int, ItemWz> itemCache = new Dictionary<int, ItemWz>();
        private static Dictionary<int, SkillWz> skillCache = new Dictionary<int, SkillWz>();
        private static Dictionary<int, MobSkillWz> mobSkillCache = new Dictionary<int, MobSkillWz>();
        private static Dictionary<int, MapWz> mapCache = new Dictionary<int, MapWz>();
        private static Dictionary<int, MobWz> mobCache = new Dictionary<int, MobWz>();
        private static Dictionary<int, QuestWz> questCache = new Dictionary<int, QuestWz>();
        private static Dictionary<string, Dictionary<int, StringWz>> stringCache = new Dictionary<string, Dictionary<int, StringWz>>();
        private static Dictionary<int, LootWz> lootCache = new Dictionary<int, LootWz>();
        private static Dictionary<int, CommodityWz> commodityCache = new Dictionary<int, CommodityWz>();
        public static void load()
        {
            loadCharacterWz();
            loadItemWz();
            loadMobWz();
            loadSkillWz();
            loadMobSkillWz();
            loadMapWz();
            loadQuestWz();
            loadStringWz();
            loadLootWz();
            loadCommodityWz();
        }

        public static void dump()
        {
            dumpCharacterWz();
            dumpItemWz();
            dumpMobWz();
            dumpMapWz();
            dumpSkillWz();
            dumpMobSkillWz();
            dumpQuestWz();
            dumpStringWz();
            dumpLootWz();
            dumpCommodityWz();
        }     
        public static void dumpItemWz()
        {
            Console.WriteLine("Item!");
            if (!Directory.Exists(Settings.LIB_PATH + @"\Item\"))
                Directory.CreateDirectory(Settings.LIB_PATH + @"\Item\");
            WzFile w = new WzFile(Settings.MAPLE_PATH + @"\Item.wz", (short)Settings.VERSION, WzMapleVersion.GMS);
            w.ParseWzFile();
            foreach (WzDirectory dir in w.WzDirectory.WzDirectories)
            {
                switch (dir.Name)
                {
                    case "Cash":
                    case "Consume":
                    case "Etc":
                    case "Install":
                        {
                            foreach (WzImage img in dir.WzImages)
                            {
                                foreach (IWzImageProperty child in img.WzProperties)
                                {
                                    int id = Convert.ToInt32(child.Name.Replace(".img", ""));
                                    IWzImageProperty info = child.WzProperties.Where(x => x.Name.Equals("info")).FirstOrDefault();
                                    IWzImageProperty spec = child.WzProperties.Where(x => x.Name.Equals("spec")).FirstOrDefault();
                                    List<String> data = new List<string>();
                                    data.Add("<" + img.ObjectType + " name=\"" + id + "\">");
                                    writeSec("\t", "", data, info);
                                    if(spec != null)
                                        writeSec("\t", "", data, spec);
                                    data.Add("</" + img.ObjectType + ">");
                                    File.WriteAllLines(Settings.LIB_PATH + @"\Item\" + id + ".xml", data);
                                }
                            }
                            break;
                        }
                    case "Pet":
                        {
                            foreach (WzImage img in dir.WzImages)
                            {
                                IWzImageProperty info = img.WzProperties.Where(x => x.Name.Equals("info")).FirstOrDefault();
                                List<String> data = new List<string>();
                                data.Add("<" + img.ObjectType + " name=\"" + Convert.ToInt32(img.Name.Replace(".img", "")) + "\">");
                                writeSec("\t", "", data, info);
                                data.Add("</" + img.ObjectType + ">");
                                File.WriteAllLines(Settings.LIB_PATH + @"\Item\" + Convert.ToInt32(img.Name.Replace(".img", "")) + ".xml", data);
                            }
                            break;
                        }
                }
            }
            w.Remove();
            Console.WriteLine("Stop!");
        }
        public static void loadItemWz()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            foreach(string filePath in Directory.GetFiles(Settings.LIB_PATH + @"\Item\"))
            {
                ItemWz i = null;
                using (XmlReader r = XmlReader.Create(filePath))
                {
                    while(r.Read())
                    {
                        switch (r.NodeType)
                        {
                            case XmlNodeType.Element:
                                {
                                    if (r.IsStartElement() && r.Name.Equals("Image"))
                                    {
                                        i = new ItemWz(Convert.ToInt32(r["name"]));
                                    }
                                    else if (r.IsStartElement() && r.Name.Equals("info"))
                                    {
                                        Info info = read("info", r.ReadSubtree());
                                        foreach (KeyValuePair<string, object> k in info.getData())
                                            i.insert(k.Key, k.Value);
                                    }
                                    else if (r.IsStartElement() && r.Name.Equals("spec"))
                                    {
                                        Info spec = read("spec", r.ReadSubtree());
                                        i.setSpec(spec);
                                    }
                                    break;
                                }
                        }
                    }
                }
                itemCache.Add(i.getId(), i);
            }
            watch.Stop();
            Console.WriteLine("(" + DateTime.Now.ToShortTimeString() + ") " + " Items (" + itemCache.Count + ") in " + watch.Elapsed);
        }
        public static void dumpCharacterWz()
        {
            Console.WriteLine("Character!");
            if(!Directory.Exists(Settings.LIB_PATH + @"\Character\"))
                Directory.CreateDirectory(Settings.LIB_PATH + @"\Character\");

            WzFile w = new WzFile(Settings.MAPLE_PATH + @"\Character.wz", (short)Settings.VERSION, WzMapleVersion.GMS);
            w.ParseWzFile();
            foreach (WzDirectory dir in w.WzDirectory.WzDirectories)
            {
                switch (dir.Name)
                {
                    case "Accessory":
                    case "Cap":
                    case "Cape":
                    case "Coat":
                    case "Face":
                    case "Glove":
                    case "Hair":
                    case "Longcoat":
                    case "Pants":
                    case "PetEquip":
                    case "Ring":
                    case "Shield":
                    case "Shoes":
                    case "TamingMob":
                    case "Weapon":
                        {
                            foreach (WzImage img in dir.WzImages)
                            {
                                List<String> data = new List<string>();
                                data.Add("<" + img.ObjectType + " name=\"" + Convert.ToInt32(img.Name.Replace(".img", "")) + "\">");
                                IWzImageProperty info = img.WzProperties.Where(x => x.Name.Equals("info")).FirstOrDefault();
                                writeSec("\t", "", data, info);
                                data.Add("</" + img.ObjectType + ">");
                                File.WriteAllLines(Settings.LIB_PATH + @"\Character\" + Convert.ToInt32(img.Name.Replace(".img", "")) + ".xml", data);
                            }
                            break;
                        }
                }
            }
            w.Remove();
            Console.WriteLine("Stop!");
        }
        public static void loadCharacterWz()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            foreach (string filePath in Directory.GetFiles(Settings.LIB_PATH + @"\Character\"))
            {
                CharacterWz c = null;
                using (XmlReader r = XmlReader.Create(filePath))
                {
                    while (r.Read())
                    {
                        switch (r.NodeType)
                        {
                            case XmlNodeType.Element:
                                {
                                    if (r.IsStartElement() && r.Name.Equals("Image"))
                                    {
                                        c = new CharacterWz(Convert.ToInt32(r["name"]));
                                    }
                                    else if (r.IsStartElement() && r.Name.Equals("info"))
                                    {
                                        Info info = read("info", r.ReadSubtree());
                                        foreach (KeyValuePair<string, object> k in info.getData())
                                            c.insert(k.Key, k.Value);
                                    }
                                    break;
                                }
                        }
                    }
                }
                equipCache.Add(c.getId(), c);
            }
            watch.Stop();
            Console.WriteLine("(" + DateTime.Now.ToShortTimeString() + ") " + " Equips (" + equipCache.Count + ") in " + watch.Elapsed);
        }
        public static void dumpMobWz()
        {
            Console.WriteLine("Mob!");
            if (!Directory.Exists(Settings.LIB_PATH + @"\Mob\"))
                Directory.CreateDirectory(Settings.LIB_PATH + @"\Mob\");
            WzFile w = new WzFile(Settings.MAPLE_PATH + @"\Mob.wz", (short)Settings.VERSION, WzMapleVersion.GMS);
            w.ParseWzFile();
            foreach(WzImage img in w.WzDirectory.WzImages)
            {
                List<String> data = new List<string>();
                data.Add("<" + img.ObjectType + " name=\"" + Convert.ToInt32(img.Name.Replace(".img", "")) + "\">");

                IWzImageProperty info = img.WzProperties.Where(x => x.Name.Equals("info")).FirstOrDefault();
                writeSec("\t", "", data, info);

                string end = data.ElementAt(data.Count - 1);
                data.RemoveAt(data.Count - 1);

                IWzImageProperty die1 = img.WzProperties.Where(x => x.Name.Equals("die1")).FirstOrDefault();

                int deathAnimation = 0;
                if(die1 != null)
                {
                    foreach (IWzImageProperty prop in die1.WzProperties)
                    {
                        IWzImageProperty delay = prop.WzProperties.Where(x => x.Name == "delay").FirstOrDefault();
                        if(delay != null)
                        {
                            deathAnimation += Convert.ToInt32(delay.WzValue);
                        }
                    }
                    data.Add("\t\t" + "<" + "CompressedInt" + " name=\"" + "deathAnimation" + "\"" + " value=\"" + deathAnimation + "\"/>");
                }

                data.Add(end);
                IWzImageProperty mobSkill = info.WzProperties.Where(x => x.PropertyType == WzPropertyType.SubProperty && x.Name == "skill").FirstOrDefault();

                if (mobSkill != null)
                {
                    foreach (IWzImageProperty prop in mobSkill.WzProperties)
                    {
                        string skill = Convert.ToString(prop.WzProperties.Where(x => x.Name == "skill").FirstOrDefault().WzValue);
                        writeSec("\t", "MobSkill", data, prop, skill);
                    }
                }

                data.Add("</" + img.ObjectType + ">");
                File.WriteAllLines(Settings.LIB_PATH + @"\Mob\" + Convert.ToInt32(img.Name.Replace(".img", "")) + ".xml", data);
            }
            Console.WriteLine("Stop!");
        }
        public static void loadMobWz()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            foreach (string filePath in Directory.GetFiles(Settings.LIB_PATH + @"\Mob\"))
            {
                MobWz m = null;
                using (XmlReader r = XmlReader.Create(filePath))
                {
                    while (r.Read())
                    {
                        switch (r.NodeType)
                        {
                            case XmlNodeType.Element:
                                {
                                    if (r.IsStartElement() && r.Name.Equals("Image"))
                                    {
                                        m = new MobWz(Convert.ToInt32(r["name"]));
                                    }
                                    else if (r.IsStartElement() && r.Name.Equals("info"))
                                    {
                                        Info info = read("info", r.ReadSubtree());
                                        foreach (KeyValuePair<string, object> k in info.getData())
                                            m.insert(k.Key, k.Value);
                                    }
                                    else if (r.IsStartElement() && r.Name.Equals("MobSkill"))
                                    {
                                        int id = Convert.ToInt32(r["name"]);
                                        Info info = read("MobSkill", r.ReadSubtree());
                                        MobSkill mobSkill = new MobSkill(id);
                                        foreach (KeyValuePair<string, object> k in info.getData())
                                            mobSkill.insert(k.Key, k.Value);
                                        m.giveSkill(mobSkill);
                                    }
                                    break;
                                }
                        }
                    }
                }
                mobCache.Add(m.getId(), m);
            }
            watch.Stop();
            Console.WriteLine("(" + DateTime.Now.ToShortTimeString() + ") " + " Mobs (" + mobCache.Count + ") in " + watch.Elapsed);
        }
        public static void dumpSkillWz()
        {
            Console.WriteLine("Skill!");
            if (!Directory.Exists(Settings.LIB_PATH + @"\Skill\"))
                Directory.CreateDirectory(Settings.LIB_PATH + @"\Skill\");
            WzFile w = new WzFile(Settings.MAPLE_PATH + @"\Skill.wz", (short)Settings.VERSION, WzMapleVersion.GMS);
            w.ParseWzFile();
            IWzImageProperty skill = null;
            foreach (WzImage img in w.WzDirectory.WzImages)
            {
                try
                {
                    skill = img.WzProperties.Where(x => x.Name.Equals("skill")).FirstOrDefault();
                    if (skill == null)
                        continue;
                }
                catch { continue; }
                foreach (IWzImageProperty child in skill.WzProperties)
                {
                    List<String> data = new List<string>();
                    int id = Convert.ToInt32(child.Name.Replace(".img", ""));
                    data.Add("<" + img.ObjectType + " name=\"" + id + "\">");
                    IWzImageProperty level = child.WzProperties.Where(x => x.Name.Equals("level")).FirstOrDefault();
                    foreach(IWzImageProperty l in level.WzProperties)
                    {
                        writeSec("\t", "level", data, l);
                    }
                    data.Add("</" + img.ObjectType + ">");
                    File.WriteAllLines(Settings.LIB_PATH + @"\Skill\" + id + ".xml", data);
                }
            }
            w.Remove();
            Console.WriteLine("Stop!");
        }
        public static void loadSkillWz()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            foreach (string filePath in Directory.GetFiles(Settings.LIB_PATH + @"\Skill\"))
            {
                SkillWz s = null;
                using (XmlReader r = XmlReader.Create(filePath))
                {
                    while (r.Read())
                    {
                        switch (r.NodeType)
                        {
                            case XmlNodeType.Element:
                                {
                                    if (r.IsStartElement() && r.Name.Equals("Image"))
                                    {
                                        s = new SkillWz(Convert.ToInt32(r["name"]));
                                    }
                                    else if (r.IsStartElement() && r.Name.Equals("level"))
                                    {
                                        int name = Convert.ToInt32(r["name"]);
                                        Info level = read("level", r.ReadSubtree());
                                        s.insert(name, level);
                                    }
                                    break;
                                }
                        }
                    }
                }
                skillCache.Add(s.getId(), s);
            }
            watch.Stop();
            Console.WriteLine("(" + DateTime.Now.ToShortTimeString() + ") " + " Skills (" + skillCache.Count + ") in " + watch.Elapsed);
        }
        public static void dumpMobSkillWz()
        {
            Console.WriteLine("MobSkill!");
            if (!Directory.Exists(Settings.LIB_PATH + @"\MobSkill\"))
                Directory.CreateDirectory(Settings.LIB_PATH + @"\MobSkill\");
            WzFile w = new WzFile(Settings.MAPLE_PATH + @"\Skill.wz", (short)Settings.VERSION, WzMapleVersion.GMS);
            w.ParseWzFile();
            WzImage MobSkill = w.WzDirectory.WzImages.Where(x => x.Name == "MobSkill.img").FirstOrDefault();
            foreach (IWzImageProperty img in MobSkill.WzProperties)
            {
                List<String> data = new List<string>();

                int id = Convert.ToInt32(img.Name);
                data.Add("<" + MobSkill.ObjectType + " name=\"" + id + "\">");
                IWzImageProperty level = img.WzProperties.Where(x => x.Name.Equals("level")).FirstOrDefault();
                foreach (IWzImageProperty l in level.WzProperties)
                {
                    writeSec("\t", "level", data, l);
                }
                data.Add("</" + MobSkill.ObjectType + ">");
                File.WriteAllLines(Settings.LIB_PATH + @"\MobSkill\" + id + ".xml", data);
            }
            w.Remove();
            Console.WriteLine("Stop!");
        }
        public static void loadMobSkillWz()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            foreach (string filePath in Directory.GetFiles(Settings.LIB_PATH + @"\MobSkill\"))
            {
                MobSkillWz m = null;
                using (XmlReader r = XmlReader.Create(filePath))
                {
                    while (r.Read())
                    {
                        switch (r.NodeType)
                        {
                            case XmlNodeType.Element:
                                {
                                    if (r.IsStartElement() && r.Name.Equals("Image"))
                                    {
                                        m = new MobSkillWz(Convert.ToInt32(r["name"]));
                                    }
                                    else if (r.IsStartElement() && r.Name.Equals("level"))
                                    {
                                        int name = Convert.ToInt32(r["name"]);      
                                        Info level = read("level", r.ReadSubtree());
                                        m.insert(name, level);
                                    }
                                    break;
                                }
                        }
                    }
                }
                mobSkillCache.Add(m.getId(), m);
            }
            watch.Stop();
            Console.WriteLine("(" + DateTime.Now.ToShortTimeString() + ") " + " MobSkills (" + mobSkillCache.Count + ") in " + watch.Elapsed);
        }
        public static void dumpMapWz()
        {
            Console.WriteLine("Map!");
            if (!Directory.Exists(Settings.LIB_PATH + @"\Map\"))
                Directory.CreateDirectory(Settings.LIB_PATH + @"\Map\");
            WzFile w = new WzFile(Settings.MAPLE_PATH + @"\Map.wz", (short)Settings.VERSION, WzMapleVersion.GMS);
            w.ParseWzFile();
            WzDirectory map = w.WzDirectory.WzDirectories.Where(x => x.Name.Equals("Map")).FirstOrDefault();
            foreach(WzDirectory i in map.WzDirectories)
            {
                foreach (WzImage img in i.WzImages)
                {
                    List<String> data = new List<string>();
                    int id = Convert.ToInt32(img.Name.Replace(".img", ""));
                    data.Add("<" + img.ObjectType + " name=\"" + id + "\">");

                    IWzImageProperty info = img.WzProperties.Where(x => x.Name.Equals("info")).FirstOrDefault();
                    if (info != null)
                        writeSec("\t", "", data, info);
                    IWzImageProperty life = img.WzProperties.Where(x => x.Name.Equals("life")).FirstOrDefault();
                    if (life != null)
                    {
                        life.WzProperties.ForEach(x =>
                        {
