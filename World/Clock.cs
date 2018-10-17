using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace World
{
    public class Clock
    {
        public delegate void OnClockHandler(int i);
        public event OnClockHandler OnClock;

        private int id = -1;
        private Timer clock = null;
