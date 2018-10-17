using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssyriaDEV
{
    public class QuestsManager
    {
        private Player p = null;
        public Dictionary<int, Quest> quests = null;
        public QuestsManager(Player p)
        {
            this.p = p;
            this.quests = new Dictionary<int, Quest>();
        }
        public List<Quest> getQuests()
        {
            return quests.Values.ToList();
        }
        public Quest getQuest(int id)
