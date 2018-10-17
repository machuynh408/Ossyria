using OssyriaDEV;
using System;

namespace Auth
{
    public class _0x01 : IHandler
    {
        public void Invoke(User u, Reader r)
        {
            using (r)
            {
                string username = r.readMapleAsciiString();
                string password = r.readMapleAsciiString();
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                    return;

                int status = u.load(username);

                if (status != 0)
