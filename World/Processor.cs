using OssyriaDEV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace World
{
    public class Processor
    {
        private Connection c = null;
        public Processor(Connection c)
        {
            this.c = c;
        }

        public void invoke(short opcode, Reader r)
        {
            switch (opcode)
            {
                case 0x01: // Register Connection to the world
