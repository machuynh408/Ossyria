using System;
using System.Drawing;

namespace OssyriaDEV
{
    public class Skill : Info
    {
        private int id;

        public Skill() : base("Skill")
        {

        }
        public Skill(int id, string type) : base("Skill")
        {
            this.id = id;
            this.insert("id", this.id);
            this.insert("level", 0);
            this.insert("type", type);
        }
        public Skill(int id, int level, string type) : base("Skill")
        {
            this.id = id;
            this.insert("id", this.id);
            this.insert("level", level);
            this.insert("type", type);
        }
