using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssyriaDEV
{
    public class MobSkillManager
    {
        private Monster m = null;
        private ConcurrentDictionary<int, Cooldown> cooldowns = null;
        public MobSkillManager(Monster m)
        {
            this.m = m;
            this.cooldowns = new ConcurrentDictionary<int, Cooldown>();
        }

        public void giveCooldown(int i, int time)
        {
