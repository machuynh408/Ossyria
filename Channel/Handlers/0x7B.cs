using OssyriaDEV;
using System.Collections.Generic;

namespace Channel
{
    public class _0x7B : IHandler
    {
        public void Invoke(User u, Reader r)
        {
            Player p = u.getPlayer();
            if(r.available() != 8)
            {
                r.readInt();
                int changes = r.readInt();
                for(int c = 0; c < changes; c++)
                {
                    int code = r.readInt();
                    int type = r.readByte();
                    int action = r.readInt();
                    switch (type)
                    {
                        case 0:
                            {
                                if(p.getKeyboardManager().get(code) != null)
                                {
                                    Key x = p.getKeyboardManager().get(code);
                                    if(x.getInt("type") == 1)
                                    {
