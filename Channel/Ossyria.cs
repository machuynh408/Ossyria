using OssyriaDEV;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Channel
{
    public class Ossyria
    {
        private static int id = -1;

        private static ConcurrentDictionary<int, Player> connected = new ConcurrentDictionary<int, Player>();
        private static ConcurrentDictionary<int, Map> maps = new ConcurrentDictionary<int, Map>();

        public static int getId()
        {
            return id;
        }
        public static void setId(int i)
        {
            id = i;
        }

        public static void insert(Player p)
        {
            if (Monitor.TryEnter(connected, 150))
            {
                try
                {
                    if (!connected.ContainsKey(p.getId()))
                        connected.GetOrAdd(p.getId(), p);
                }
                finally
                {
                    Monitor.Exit(connected);
                }
            }
        }
        public static void remove(Player p)
        {
            if (Monitor.TryEnter(connected, 150))
            {
                try
                {
                    Player x = null;
