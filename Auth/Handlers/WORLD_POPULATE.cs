using OssyriaDEV;
using System.Threading.Tasks;

namespace Auth
{
    public class _0x0B : IHandler
    {
        public void Invoke(User u, Reader r)
        {
            Task<object> x = u.request(0x01);
            State state = x.Result as State;
            Writer w = new Writer(0x0A);
