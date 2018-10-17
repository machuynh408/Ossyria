using System;
using System.Collections.Generic;
using System.Timers;

namespace OssyriaDEV
{
    public class Status
    {
        public delegate void OnStatusHandler(string s);
        public event OnStatusHandler OnStatus;

        private int id, level;
        private string name = "";
        private int time = 0, interval = 0;
        private Timer clock = null;
        private Dictionary<string, int> info = null;
        public Status(int id, int level, string name, int time, int interval)
        {
            this.id = id;
            this.level = level;
            this.name = name;
            this.time = time;
            this.interval = interval;
            this.clock = new Timer(interval);
            this.clock.Elapsed += Elapsed;
            this.info = new Dictionary<string, int>();

            switch (name)
            {
                case "pad":
                    insert("status", 0x1);
                    break;
                case "pdd":
                    insert("status", 0x2);
                    break;
                case "mad":
                    insert("status", 0x4);
                    break;
                case "mdd":
                    insert("status", 0x8);
                    break;
                case "acc":
                    insert("status", 0x10);
                    break;
                case "eva":
                    insert("status", 0x20);
                    break;
                case "speed":
                    insert("status", 0x40);
                    break;
                case "stun":
                    insert("status", 0x80);
                    break;
                case "freeze":
                    insert("status", 0x100);
