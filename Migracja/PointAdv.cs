using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Migracja
{
    class PointAdv
    {
        private Point fpoint;
        private int fkdPointType; //Cst.c_geoPxSimple/c_geoPxStartEnd/c_geoPxGroupBorder
        public PointAdv(int X, int Y, int akdPointType)
        {
            fpoint = new Point(X, Y);
            fkdPointType = akdPointType;
        }

        public Point GetPoint()
        {
            return fpoint;
        }

        public int GetKdPointType()
        {
            return fkdPointType;
        }

        public int X{get{return fpoint.X;}}

        public int Y{get{return fpoint.Y;}}
    }
}
