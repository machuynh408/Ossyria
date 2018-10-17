using OssyriaDEV;
using System;
using System.Collections.Generic;

namespace Channel
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
                [0x14] = new _0x14(),
                [0x23] = new _0x23(),
                [0x24] = new _0x24(),
                [0x25] = new _0x25(),
                [0x26] = new _0x26(),
                [0x27] = new _0x27(),
                [0x28] = new _0x28(),
                [0x29] = new _0x29(),
                [0x30] = new _0x30(),
                [0x36] = new _0x36(),
                [0x38] = new _0x38(),
                [0x39] = new _0x39(),
                [0x42] = new _0x42(),
                [0x43] = new _0x43(),
