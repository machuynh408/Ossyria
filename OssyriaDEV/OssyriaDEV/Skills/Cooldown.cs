using System.Timers;

namespace OssyriaDEV
{
    public class Cooldown
    {
        public delegate void OnCooldownHandler(int i);
        public event OnCooldownHandler OnCooldown;

        private int id = -1, time = 0, interval = 0;
        private Timer clock = null;
        public Cooldown(int id, int time, int interval = 0)
        {
            this.id = id;
            this.time = time;
            this.interval = interval;
            if (interval == 0)
                this.clock = new Timer(time);
            else if (interval > 0)
                this.clock = new Timer(interval);
            this.clock.Elapsed += Elapsed;
