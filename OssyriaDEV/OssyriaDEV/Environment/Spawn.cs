using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace OssyriaDEV
{
    public abstract class Spawn
    {
        private int key = -1, channel = -1;
        private Map map = null;
        private Position origin, position = null;
        private ConcurrentDictionary<int, Spawn> fov = null;

        public Spawn()
        {
            this.fov = new ConcurrentDictionary<int, Spawn>();
        }
        public Spawn(Position position)
        {
            this.origin = position;
            this.position = position;
            this.fov = new ConcurrentDictionary<int, Spawn>();
        }

        public void updateKey(int key)
        {
            this.key = key;
        }
        public int getKey()
        {
            return key;
        }
        public void setChannel(int channel)
        {
            this.channel = channel;
        }

        public int getChannel()
        {
