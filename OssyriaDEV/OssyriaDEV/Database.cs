using MapleLib.WzLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace OssyriaDEV
{
    public class Database
    {
        private string path = "";
        private List<Info> data = new List<Info>();
        public Database(string path = "")
        {
            if (string.IsNullOrEmpty(path))
                this.path = Settings.DATABASE_PATH;
            else
                this.path = path;
        }

        public bool search(string name)
        {
            return deepSearch(Directory.GetDirectories(this.path), name);
        }

        private bool deepSearch(string[] dir, string name)
        {
            bool found = false;
            foreach (string a in dir)
            {
                foreach(string f in Directory.GetFiles(a))
                {
                    string file = Path.GetFileName(f);
                    if (file.Equals(name))
                    {
                        found = true;
                        break;
                    }
                }
                if (found)
                    break;
                foreach (string p in Directory.GetDirectories(a))
                {
                    found = deepSearch(Directory.GetDirectories(p), name);
                    if (found)
                        break;
                }
                if (found)
                    break;
            }
            return found;
        }

        public List<Info> getData()
        {
            return data;
        }
