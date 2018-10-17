using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssyriaDEV
{
    public class Attack
    {
        private Spawn source;
        private int size, monsterCount, attacksPerMonster;
        private int skill, level = 0, projectile = 0;
        private byte speed, direction, stance;
        private int charge = -1;
        private Dictionary<int, List<int>> data;
        private int statusTime = 0, statusValue = 0;

        public Attack(Spawn source)
        {
            this.source = source;
            data = new Dictionary<int, List<int>>();
            List<int> values = new List<int>();
        }
        public Attack(Reader r, Player p, bool range)
        {
            this.source = p;
            data = new Dictionary<int, List<int>>();
            r.readByte();
            size = r.readByte(); // numAttackedAndDamage
            monsterCount = (size >> 4) & 0xF; // numAttacked
            attacksPerMonster = size & 0xF; // numDamage
            skill = r.readInt();
            switch (skill)
            {
                case 2121001: // Big Bang
                case 2221001: // Big Bang
                case 2321001: // Big Bang
                case 5101004: // Corkscrew Blow
                case 5201002: // Grenade
                    {
                        charge = r.readInt();
                        break;
                    }
                default:
                    {
                        charge = 0;
                        break;
                    }
            }
            r.readByte();
            stance = r.readByte();

            if (skill == 4211006)
            {
                if (size == 0)
                {
                    r.readBytes(10);
                    int mesoCount = r.readByte();
                    for (int meso = 0; meso < mesoCount; meso++)
                    {
                        data.Add(r.readInt(), null);
                        r.readByte();
                    }
                    return;
                }
                else
                {
                    r.readBytes(6);
                }
                for (int index = 0; index < monsterCount + 1; index++)
                {
                    int key = r.readInt();
                    if (index < monsterCount)
                    {
                        r.readBytes(12);
