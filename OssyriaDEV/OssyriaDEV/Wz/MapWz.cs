using System;
using System.Collections.Generic;
using System.Linq;

namespace OssyriaDEV
{
    public class MapWz : Info
    {
        private int id = -1;
        private AnchorWz anchor = null;
        private List<MobWz> monsters = new List<MobWz>();
        private List<NpcWz> npcs = new List<NpcWz>();
        private List<PortalWz> portals = new List<PortalWz>();
        public MapWz(int id)
        {
            this.id = id;
        }
        public int getId()
        {
            return id;
        }

        public AnchorWz getAnchorWz()
        {
            return anchor;
        }

        public void updateAnchor(AnchorWz anchor)
        {
            this.anchor = anchor;
        }

        public List<MobWz> getMonsters()
        {
            return monsters;
