using System;
using System.Collections.Generic;
using System.Drawing;
using System.Timers;

namespace OssyriaDEV
{
    public class Summon : Spawn
    {
        public delegate void OnSummonHandler(int key);
        public event OnSummonHandler OnSummon;

        private Player source = null;
        private int id = -1, level, time = 0;
        private Timer clock = null;
        private Dictionary<string, int> info = null;
        private Rectangle r;

        public Summon(Player source, int id, int level, int time)
        {
            this.source = source;
            this.id = id;
            this.level = level;
            this.time = time;
            this.clock = new Timer(time);
            this.clock.Elapsed += Elapsed;
            this.info = new Dictionary<string, int>();
        }

        public int getSource()
        {
            return this.source.getId();
        }
        public int getId()
        {
            return id;
        }
        public int getLevel()
        {
            return level;
        }
        public int getTime()
        {
            return time;
        }
     
        public Rectangle getRange()
        {
            return r;
        }
        public void setRange(Rectangle r)
        {
            this.r = r;
        }

        public void start()
        {
            clock.Start();
        }

