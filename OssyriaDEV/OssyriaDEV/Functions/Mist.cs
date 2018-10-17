using System;
using System.Collections.Generic;
using System.Drawing;
using System.Timers;

namespace OssyriaDEV
{
    public class Mist : Spawn
    {
        public delegate void OnMistHandler(int key);
        public event OnMistHandler OnMist;

        private int source = -1;
        private int id = -1, level = 0, duration, time = 0;
        private int prop;
        private Rectangle range;
        private DateTime date;
        private Timer clock = null;

        public Mist(Player source, int id, int level, int time, Rectangle range, int prop)
        {
            this.source = source.getId();
            this.setPosition(source.getPosition());
            this.id = id;
            this.level = level;
            this.duration = time;
            Console.WriteLine("Time: " + time);
            this.time = time;
            this.clock = new Timer(250); 
            this.clock.Elapsed += Elapsed;
            this.range = range;
            this.prop = prop;
        }

        public int getSource()
        {
            return source;
        }
        public int getId()
        {
            return id;
        }

        public int getLevel()
        {
            return level;
        }
        public int getDuration()
        {
            return duration;
        }
        public int getTime()
        {
            return time;
        }

