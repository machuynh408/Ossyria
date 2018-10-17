using System;
using System.IO;
using System.Text;

namespace OssyriaDEV
{
    public class Reader : IDisposable
    {
        private BinaryReader br;
        private MemoryStream ms;

        public Reader(byte[] src)
        {
            ms = new MemoryStream(src);
            br = new BinaryReader(ms);
        }
        public MemoryStream getStream()
        {
            return ms;
        }
        public byte readByte()
		{
            return br.ReadByte();
		}
        public sbyte readSByte()
        {
            return br.ReadSByte();
        }

        public byte[] readBytes(int size)
        {
            return br.ReadBytes(size);
