using System.Collections.Generic;
using System.Timers;

namespace OssyriaDEV
{
    public class Passive
    {
        public delegate void OnPassiveHandler(int i);
        public event OnPassiveHandler OnPassive;

        private int id = -1, level = 0, interval = 0;
        private Timer clock = null;
        private Dictionary<string, int> info = null;

        public Passive(int id, int level, int interval)
        {
            this.id = id;
            this.level = level;
            this.interval = interval;
            this.clock = new Timer(interval);
            this.clock.Elapsed += Elapsed;
            this.info = new Dictionary<string, int>();
        }
