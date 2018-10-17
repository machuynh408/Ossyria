using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssyriaDEV
{
    public class Movement
    {
        private Writer w = new Writer();
        private Position position = new Position();
        public Movement(Reader r)
        {
            int size = r.readByte();
            w.Create(new ByteData(size));
            for (int index = 0; index < size; index++)
            {
                byte type = r.readByte();
                w.Create(new ByteData(type));
                switch (type)
                {
                    case 0: // Absolute
                    case 5:
                    case 17:
                        {
                            short x = r.readShort();
                            short y = r.readShort();
                            position.insert("x", x);
                            position.insert("y", y);
                            w.Create(new ShortData(x), new ShortData(y), new BytesData(r.readBytes(6)));
                            byte stance = r.readByte();
                            position.insert("stance", stance);
                            w.Create(new ByteData(stance), new ShortData(r.readShort()));
                            break;
                        }
