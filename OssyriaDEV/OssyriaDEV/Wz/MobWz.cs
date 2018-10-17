using System.Collections.Generic;
using System.Linq;

namespace OssyriaDEV
{
    public class MobWz : Info
    {
        private int id = -1;
        private Position position = null;
        private Dictionary<int, MobSkill> skills = null;
        public MobWz(int id) : base()
        {
            this.id = id;
            this.skills = new Dictionary<int, MobSkill>();
        }

        public int getId()
        {
            return id;
        }
        public void setPosition(Position position)
        {
            this.position = position;
        }

