using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssyriaDEV
{
    public enum RespawnPriority
    {
        LOW,
        MEDIUM,
        HIGH,
        SEVERE,
        FULL,
    }
    public class RespawnManager
    {
        private Map m = null;
        private Dictionary<int, Platform> platforms = null;
        private Platform bottom = null;
        public RespawnManager(Map m)
        {
            this.m = m;
            this.platforms = new Dictionary<int, Platform>();
        }
        public List<Platform> getPlatforms()
        {
            return platforms.Values.ToList();
        }
        public List<Platform> monsterPlatforms()
        {
            return platforms.Values.Where(x => x.containsMonsters()).ToList();
        }
        public void add(Platform p)
        {
            platforms.Add(p.getId(), p);
        }

        private List<Platform> hasFoothold(int fh)
        {
            return platforms.Values.Where(x => x.contains(fh)).ToList();
        }

        private static int compareMonster(Monster a, Monster b)
        {
            return (a.getPosition().getFh() < b.getPosition().getFh() ? -1 : (a.getPosition().getFh() == b.getPosition().getFh() ? 0 : 1));
        }

        public void build(List<Monster> monsters)
        {
            Dictionary<int, List<Monster>> data = new Dictionary<int, List<Monster>>();
