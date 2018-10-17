using System.Collections.Generic;
using System.Linq;

namespace OssyriaDEV
{
    public class Npc : Spawn
    {
        private int id;
        private Spawn master = null;
        public Npc(int id, Position position) : base(position)
        {
            this.id = id;
        }

        public int getId()
        {
            return id;
        }

        public void setMaster(Spawn master)
        {
            this.master = master;
        }

        public Spawn getMaster()
        {
            return master;
        }

        public void autoControl(Player x, List<Player> players, bool leave)
        {
            Spawn z = null;
            double distance = 0.0;
            if (this.getMaster() != null)
            {
                if (!x.isEquals(this.getMaster())) // Only the original master can call the autoControl because he is the current controller
                    return;
            }

            foreach (Player y in players)
            {
                distance = y.getPosition().calcDistance(this.getPosition());

                if (distance > 850)
                    continue;

                if (y.contains(this.getKey()))
                {
                    z = y;
                    break;
                }
            }

            if (leave)
            {
                if (z != null)
                {
                    setMaster(z);
                    control(z as Player);
                }
                else
                    setMaster(null);
