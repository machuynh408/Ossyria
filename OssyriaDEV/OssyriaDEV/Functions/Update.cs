using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssyriaDEV
{
    public class Update
    {
        private Dictionary<string, int> data = null;
        private Writer w = null;

        public Update()
        {
            data = new Dictionary<string, int>();
        }

        public Dictionary<string, int> getData()
        {
            return data;
        }

        public void insert(string name, int value)
        {
            if (!data.ContainsKey(name))
                data.Add(name, value);
            else
                data[name] = value;
        }

        public int getSize()
        {
            int size = 0, i = 0;

            w = new Writer();
            foreach(KeyValuePair<string, int> d in data)
            {
                switch(d.Key)
                {
                    case "skin":
                        i = 0x1;
                        w.Create(new ShortData(d.Value));
                        break;
                    case "face":
                        i = 0x2;
                        w.Create(new IntData(d.Value));
                        break;
                    case "hair":
                        i = 0x4;
                        w.Create(new IntData(d.Value));
                        break;
                    case "level":
                        i = 0x10;
                        w.Create(new ByteData(d.Value));
                        break;
                    case "class":
                        i = 0x20;
                        w.Create(new ShortData(d.Value));
                        break;
                    case "str":
                        i = 0x40;
                        w.Create(new ShortData(d.Value));
                        break;
                    case "dex":
                        i = 0x80;
                        w.Create(new ShortData(d.Value));
                        break;
                    case "int":
                        i = 0x100;
                        w.Create(new ShortData(d.Value));
                        break;
                    case "luk":
                        i = 0x200;
                        w.Create(new ShortData(d.Value));
                        break;
                    case "hp":
                        i = 0x400;
                        w.Create(new ShortData(d.Value));
                        break;
                    case "maxHP":
                        i = 0x800;
                        w.Create(new ShortData(d.Value));
                        break;
