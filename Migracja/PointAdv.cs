using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace Migracja
{
    class PointAdv
    {
        private Point fpoint;
        private int fkdPointType; //Cst.c_geoPxSimple/c_geoPxStartEnd/c_geoPxGroupBorder
        private bool DelSimplifiedPhase1 = false; // usunięty w 1 fazie upraszczania
        private bool DelSimplifiedPhase2 = false; // usunięty w 2 fazie upraszczania
        internal List<GeoEdgePart> geoEdgePartList = new List<GeoEdgePart>();
        public PointAdv(int X, int Y, int akdPointType)
        {
            fpoint = new Point(X, Y);
            fkdPointType = akdPointType;
        }
        public PointAdv(Point point, int akdPointType)
        {
            fpoint = point;
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

        public bool IsDelSimplifiedPhase1()
        {
            return DelSimplifiedPhase1;
        }

        public bool IsDelSimplifiedPhase2()
        {
            return DelSimplifiedPhase2;
        }

        public bool IsDelSimplified()
        {
            return IsDelSimplifiedPhase1() || IsDelSimplifiedPhase2();
        }

        public void DelSimplifyPhase1()
        {
            if (CanBeDelSimplified())
            {
                DelSimplifiedPhase1 = true;
            }
            else
            {
                Debug.Assert(false, String.Format("Nie można ustawić flagi 'DelSimplifyPhase1' dla punktu ({0},{1}) o typie {2}.", fpoint.X, fpoint.Y, GetKdPointType()));
            }
        }

        public void DelSimplifyPhase2()
        {
            if (CanBeDelSimplified())
            {
                DelSimplifiedPhase2 = true;
            }
            else
            {
                Debug.Assert(false, String.Format("Nie można ustawić flagi 'DelSimplifyPhase2' dla punktu ({0},{1}) o typie {2}.", fpoint.X, fpoint.Y, GetKdPointType()));
            }
        }

        public bool CanBeDelSimplified()
        {
            return GetKdPointType() == Cst.c_geoPxSimple;
        }

        public int X{get{return fpoint.X;}}

        public int Y{get{return fpoint.Y;}}
    }
}
