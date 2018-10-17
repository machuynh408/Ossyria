using System;
using System.Collections.Generic;
using System.Timers;

namespace OssyriaDEV
{
    public class Buff
    {
        public delegate void OnBuffHandler(int i);
        public event OnBuffHandler OnBuff;

        private string name = "";
        private int id = -1, level, time = 0;
        private DateTime date;
        private Timer clock = null;
        private Update update = null;
        private Dictionary<string, int> info = null;
        public Buff(int id, int level, int time)
        {
            this.id = id;
            switch(id)
            {
                case 1101004: // Sword Booster
                case 1101005: // Axe Booster
                case 1201004: // Sword Booster
                case 1201005: // BW Booster
                case 1301004: // Spear Booster
                case 1301005: // Pole Arm Booster
                case 2111005: // Spell Booster
                case 2211005: // Spell Booster
                case 3101002: // Bow Booster
                case 3201002: // Crossbow Booster
                case 4101003: // Claw Booster
                case 4201002: // Dagger Booster
                case 5101006: // Knuckler Booster
                case 5201003: // Gun Booster
                    {
                        name = "Booster";
                        break;
                    }
                case 1121000: // Maple Warrior
                case 1221000: // Maple Warrior
                case 1321000: // Maple Warrior
                case 2121000: // Maple Warrior
                case 2221000: // Maple Warrior
                case 2321000: // Maple Warrior
                case 3121000: // Maple Warrior
                case 3221000: // Maple Warrior
                case 4121000: // Maple Warrior
                case 4221000: // Maple Warrior
                case 5121000: // Maple Warrior
                case 5221000: // Maple Warrior
                    {
                        name = "Maple Warrior";
                        break;
                    }
