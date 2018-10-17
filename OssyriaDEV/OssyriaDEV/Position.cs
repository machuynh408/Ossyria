using System;

namespace OssyriaDEV
{ 
    public class Position : Info
    {
        public Position() : base()
        {
        }
        public Position(int x, int y) : base()
        {
            insert("x", x);
            insert("y", y);
