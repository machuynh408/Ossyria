using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace OssyriaDEV
{
    public class Disease
    {
        public delegate void OnDiseaseHandler(string s);
        public event OnDiseaseHandler OnDisease;

        private int id, level;
        private string name = "";
        private int time = 0, interval = 0;
        private Timer clock = null;
        private Dictionary<string, object> info = null;

        public Disease(int id, int level, string name, int time)
        {
            this.id = id;
            this.level = level;
            this.name = name;
            this.time = time;
            this.clock = new Timer(time);
            this.clock.Elapsed += Elapsed;
            this.info = new Dictionary<string, object>();

            switch (name)
            {
                case "slow":
                    insert("disease", 0x1);
                    break;
                case "seduce":
                    insert("disease", 0x80);
                    break;
                case "stun":
                    insert("disease", 0x2000000000000L);
                    break;
                case "poison":
