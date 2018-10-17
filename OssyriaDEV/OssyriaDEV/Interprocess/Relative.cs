using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssyriaDEV
{
    [Serializable]
    public class Relative
    {
        private int id = -1;
        private string name = "";
        private int c, level, exp, fame;
        private int hp, maxHP, mp, maxMP;
        private int channel, map;
        private bool online = false;
        public Relative(int id, string name, int c, int level, int exp, int fame, 
            int hp, int maxHP, int mp, int maxMP, int channel, int map)
        {
            this.id = id;
            this.name = name;
            this.c = c;
            this.level = level;
            this.exp = exp;
            this.fame = fame;
            this.hp = hp;
            this.maxHP = maxHP;
