using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssyriaDEV
{
    public class MobSkillWz : Info
    {
        private int id = -1;
        private Dictionary<int, Info> levels = null;
        public MobSkillWz(int id)
