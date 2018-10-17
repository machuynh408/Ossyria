using OssyriaDEV;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace World
{
    public class Ossyria
    {
        private static ConcurrentDictionary<int, int> connected = new ConcurrentDictionary<int, int>()
        {
            [1] = 0,
            [2] = 0,
            [3] = 0,
            [4] = 0,
        };

        private static ConcurrentDictionary<int, Transition> transitions = new ConcurrentDictionary<int, Transition>(); // for transitioning between server and auth
        private static ConcurrentDictionary<int, Clock> transitionClocks = new ConcurrentDictionary<int, Clock>(); // for transitioning between server and auth

        public static State getState()
        {
            State state = null;
            if (Monitor.TryEnter(connected, 150))
            {
                try
                {
                    state = new State();
                    foreach (KeyValuePair<int, int> c in connected)
                        state.insert(c.Key, c.Value);
                }
                finally
                {
                    Monitor.Exit(connected);
                }
            }
