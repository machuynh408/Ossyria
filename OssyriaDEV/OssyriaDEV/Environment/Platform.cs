using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssyriaDEV
{
    public class Platform
    {
        private int id = -1;
        private Map m = null;
        private int minSpawn = 0, maxSpawn = 0;
        private int height = 0;
        private Dictionary<int, FootholdWz> footholds = null;

        private Dictionary<int, Monster> white = null; // on the map
        private Queue<Monster> black = null; // waiting...
        private RespawnPriority respawnPriority = RespawnPriority.LOW;
        public Platform(int id, Map m)
        {
            this.id = id;
            this.m = m;
            this.footholds = new Dictionary<int, FootholdWz>();
            this.white = new Dictionary<int, Monster>();
            this.black = new Queue<Monster>();
        }

        public int getId()
        {
            return id;
        }

        public int activeMonsterSize()
        {
            return white.Count;
        }

        public int platformHeight()
        {
            return height;
        }

        public int minMonsterSize()
        {
            return minSpawn;
        }
