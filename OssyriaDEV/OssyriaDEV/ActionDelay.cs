using System;
using System.Timers;

namespace OssyriaDEV
{
    public class ActionDelay
    {
        private Timer clock = null;
        private Action action = null;
        public ActionDelay(Action action, int delay)
