using System;

namespace OssyriaDEV
{
    public class FootholdWz : Info, IComparable<FootholdWz>
    {
        private int id = -1;
        public FootholdWz(int id)
        {
            this.id = id;
        }

        public int getId()
        {
            return id;
        }
        public bool isWall()
        {
