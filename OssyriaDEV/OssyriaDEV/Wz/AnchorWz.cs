using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssyriaDEV
{
    public class AnchorWz
    {
        private AnchorWz northWest = null;
        private AnchorWz northEast = null;
        private AnchorWz southWest = null;
        private AnchorWz southEast = null;
        private List<FootholdWz> fhs = null;

        Position lowerCorners = new Position();
        Position upperCorners = new Position();
        private Position center;
        private int depth = 0, maxDepth = 8;
        private int maxDropX, minDropX;

        public AnchorWz()
        {

        }
        public AnchorWz(Position lowerCorners, Position upperCorners)
        {
            this.lowerCorners = lowerCorners;
            this.upperCorners = upperCorners;
            this.fhs = new List<FootholdWz>();
            center = new Position((upperCorners.getX() - lowerCorners.getX()) / 2,
                (upperCorners.getY() - lowerCorners.getY()) / 2);
        }
        public AnchorWz(Position lowerCorners, Position upperCorners, int depth)
        {
            this.lowerCorners = lowerCorners;
            this.upperCorners = upperCorners;
            this.depth = depth;
            this.fhs = new List<FootholdWz>();
            center = new Position((upperCorners.getX() - lowerCorners.getX()) / 2,
                (upperCorners.getY() - lowerCorners.getY()) / 2);
        }

        public List<FootholdWz> getFootholds()
        {
            return fhs;
        }

        public int getLowerX()
        {
            return lowerCorners.getX();
        }

        public int getUpperX()
        {
            return upperCorners.getX();
        }

        public int getLowerY()
        {
            return lowerCorners.getY();
        }

