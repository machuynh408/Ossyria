using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssyriaDEV
{
    public class MobSkill : Info
    {
        private int id;
        public MobSkill(int id) : base("MobSkill")
        {
            this.id = id;
        }
        public int getId()
        {
            return this.id;
