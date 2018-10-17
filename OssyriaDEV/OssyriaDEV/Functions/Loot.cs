using System;
using System.Collections.Generic;
using System.Timers;

namespace OssyriaDEV
{
    public class Loot : Spawn
    {
        public delegate void OnLootHandler(int key);
        public event OnLootHandler OnLoot;

        private int restriction = -1;
        private object source = null, value = null;
        private Timer clock = null;
        private Position origin = null;

        private int type = 0, displayAnimation = 1, destroyAnimation = 1;
        public Loot(int restriction, object source, Position origin)
        {
            this.restriction = restriction;
            this.source = source;
            this.origin = origin;
            this.clock = new Timer(180000); // 180000
            this.clock.Elapsed += Elapsed;
        }
        public int getRestriction()
        {
            return restriction;
        }

        public void setType(int type)
        {
            this.type = type;
        }
        public int getType()
        {
            return type;
        }
        public void setValue(object value)
        {
            this.value = value;
        }

        public Monster getMonster()
        {
            return source as Monster;
