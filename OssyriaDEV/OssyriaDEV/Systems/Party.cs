using System.Collections.Generic;
using System.Linq;

namespace OssyriaDEV
{
    public enum PartyOperation
    {
        CREATE,
        DISBAND,
        EXPEL,
        LEAVE,
        JOIN,
        STATUS,
        UPDATE,
        HEALTH,
    }
    public class Party
    {
        int id;
        private int leader = -1;
        private List<int> members = null;

        public Party(int id, Player p)
        {
            this.id = id;
            this.leader = p.getId();
            this.members = new List<int>();
            this.members.Add(p.getId());
            p.setParty(this);
        }
        public int getId()
        {
            return id;
        }

        public int getLeader()
        {
            return leader;
        }

        private void setLeader(int id)
        {
            this.leader = id;
        }

        public List<int> getMembers()
        {
            return members.ToList();
        }
        public int getSize()
        {
            return members.Count;
        }
        public bool contains(int id)
        {
            return members.Contains(id);
        }
        public void remove(int id)
        {
            this.members.Remove(id);
        }
        public void join(int id)
        {
            /*if (!members.Contains(p.getId()))
            {
                this.members.GetOrAdd(p.getId(), p);
                p.setParty(this);

                Writer w = new Writer(0x3B);
                w.Create(new ByteData(0xF));
                w.Create(new IntData(40546));
                w.Create(new AsciiStringData(p.getName()));
                getStatus(w, p.getChannel());

                getMembers().ForEach(x => { x.send(w); });
            }*/
        }

        public void leave(int id)
        {
            /*Player member = null;
            if(this.members.ContainsKey(id))
