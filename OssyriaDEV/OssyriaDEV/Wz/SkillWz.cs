using System.Collections.Generic;

namespace OssyriaDEV
{
    public class SkillWz : Info
    {
        private int id = -1;
        private Dictionary<int, Info> levels = null;
        public SkillWz(int id)
        {
            this.id = id;
            this.levels = new Dictionary<int, Info>();
