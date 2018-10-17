using OssyriaDEV;
using System;
using System.Collections.Generic;

namespace Auth
{
    public class Processor
    {
        private User u = null;
        private readonly Dictionary<int, IHandler> handlers = null;
        public Processor(User u)
        {
            this.u = u;
            this.handlers = new Dictionary<int, IHandler>()
            {
                [0x01] = new _0x01(),
                [0x09] = new _0x09(),
                [0x04] = new _0x0B(),
