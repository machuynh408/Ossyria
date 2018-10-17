using OssyriaDEV;
using System.Collections.Generic;

namespace Channel
{
    public class _0x6B : IHandler
    {
        public void Invoke(User u, Reader r)
        {
            Player p = u.getPlayer();

            int type = r.readByte();
            int size = r.readByte();
            List<int> targets = new List<int>();
