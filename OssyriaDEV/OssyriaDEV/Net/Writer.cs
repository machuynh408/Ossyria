using System;
using System.IO;

namespace OssyriaDEV
{
    public class Writer : IDisposable
    {
        MemoryStream ms = null;
        BinaryWriter bw = null;
        public Writer()
        {
            ms = new MemoryStream();
            bw = new BinaryWriter(ms);
        }
        public Writer(short opcode)
        {
            ms = new MemoryStream();
            bw = new BinaryWriter(ms);
            bw.Write(opcode);
        }
        public MemoryStream getStream()
        {
            return ms;
        }
        public void Create(params Data[] datas)
        {
            foreach (Data d in datas)
            {
                if (d is ByteData)
                    bw.Write(Convert.ToByte(d.getValue()));
                else if (d is SByteData)
                    bw.Write(Convert.ToSByte(d.getValue()));
                else if (d is BytesData)
                    bw.Write((byte[])d.getValue());
                else if (d is ShortData)
                    bw.Write(Convert.ToInt16(d.getValue()));
                else if (d is UShortData)
                    bw.Write(Convert.ToUInt16(d.getValue()));
                else if (d is UIntData)
                    bw.Write(Convert.ToInt32(d.getValue()));
                else if (d is IntData)
                    bw.Write(Convert.ToInt32(d.getValue()));
                else if (d is LongData)
                    bw.Write(Convert.ToInt64(d.getValue()));
                else if (d is UInt64Data)
                    bw.Write(Convert.ToUInt64(d.getValue()));
                else if (d is StringData)
                    bw.Write(Convert.ToString(d.getValue()));
                else if (d is AsciiStringData)
                {
                    string ascii = Convert.ToString(d.getValue());
                    short size = (short)ascii.Length;
                    bw.Write(size);
                    bw.Write(ascii.ToCharArray());
                }
                else if (d is PadStringData)
                {
                    string ascii = Convert.ToString(d.getValue());
                    int size = ((PadStringData)d).getSize() - ascii.Length;
                    bw.Write(ascii.ToCharArray());
                    bw.Write(new byte[size]);
                }
                else if (d is HexaStringData)
                {
                    string[] ascii = Convert.ToString(d.getValue()).Split(' ');
                    foreach (string s in ascii)
                        bw.Write(Byte.Parse(s, System.Globalization.NumberStyles.HexNumber));
                    
                }
            }
        }

        public bool isEmpty()
        {
            return ms.Length == 0;
        }
        public byte[] getPacket()
        {
            return ms.ToArray();
        }
        public void Dispose()
        {
            try
            {
