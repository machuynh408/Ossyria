using OssyriaDEV;
using System;
using System.Threading.Tasks;

namespace Channel
{
    public class _0x14 : IHandler
    {
        public void Invoke(User u, Reader r)
        {
            using (r)
            {
                int id = r.readInt();

                Writer w = new Writer(0x7B);
                w.Create(new IntData(id));
                Task<object> x = u.request(w);
                Transition y = x.Result as Transition;
                if (y.getId() == -1)
                {
                    u.dc();
                    return;
                }
                if (!u.getAddress().Contains(y.getAddress()))
                {
                    u.dc();
                    return;
                }
                string username = y.getUsername();
