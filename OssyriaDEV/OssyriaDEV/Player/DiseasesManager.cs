using System.Collections.Generic;

namespace OssyriaDEV
{
    public class DiseasesManager
    {
        private Player p = null;
        private Dictionary<string, Disease> diseases = new Dictionary<string, Disease>();
        public DiseasesManager(Player p)
        {
            this.p = p;
        }

        public void stopAllDiseases()
        {
            getDiseases().ForEach(x => 
            {
                try
                {
                    x.stop();
                }
                finally
                {
                    x.terminate();
                }
            });
        }
        public Disease getDisease(string name)
        {
            Disease d = null;
            diseases.TryGetValue(name, out d);
            return d;
        }
        public List<Disease> getDiseases()
        {
            List<Disease> data = new List<Disease>();
            data.AddRange(diseases.Values);
            return data;
        }
        public void giveDisease(int id, int level, int time, int value)
        {
            Disease disease = null;
            switch (id)
            {
                case 121: // Darkness
                    {
                        disease = new Disease(id, level, "darkness", time);
                        break;
                    }
                case 122: // Weakness
                    {
                        disease = new Disease(id, level, "weakness", time);
                        break;
