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
        private GeoEdgePoint fpoint;        
        protected int fkdPointType; //Cst.c_geoPxSimple/c_geoPxStartEnd/c_geoPxDontDelete
        private bool DelSimplifiedPhase1 = false; // usunięty w 1 fazie upraszczania
        private bool DelSimplifiedPhase2 = false; // usunięty w 2 fazie upraszczania
        private bool fcheckedPhase2 = false;
        internal List<GeoEdgePart> geoEdgePartList = new List<GeoEdgePart>();
 
        public PointAdv(GeoEdgePoint point)
        {
            fpoint = point;
            if (fpoint != null)
            {
                fkdPointType = fpoint.GetKdPointType();
            }
        }

        public GeoEdgePoint GetGeoEdgePoint()
        {
            return fpoint;
        }

        public virtual Point GetPoint()
        {
            return fpoint.ToPoint();
        }

        public virtual int GetKdPointType()
        {
            Debug.Assert(fpoint!=null, "Podobiekt fpoint (GeoEdgePoint) nie został zainicjalizowany");
            return fkdPointType;
        }

        public void SetCheckedPhase2()
        {
            Debug.Assert(fcheckedPhase2 == false, String.Format("Próba ponownego uproszczenia geopunktu ({0},{1})", fpoint.fPictX, fpoint.fPictY));
            fcheckedPhase2 = true;
        }

        public Boolean IsCheckedPhase2()
        {
            return fcheckedPhase2;
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
            if (GetKdPointType() == Cst.c_geoPxSimple || GetKdPointType() == Cst.c_geoPxDonePhase2) 
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

        public virtual int X{get{return fpoint.pictX;}}

        public virtual int Y { get { return fpoint.pictY; } }
    }

    class ScaledPointAdv: PointAdv
    {
        private Point fsimplePoint;
        public ScaledPointAdv(int X, int Y, int akdPointType): base(null)
        {
            fsimplePoint = new Point(X, Y);
            fkdPointType = akdPointType;
        }

        public override Point GetPoint()
        {
            return fsimplePoint;
        }

        public override int X { get { return fsimplePoint.X; } }

        public override int Y { get { return fsimplePoint.Y; } }

        public override int GetKdPointType()
        {
            Debug.Assert(fsimplePoint != null, "Podobiekt fpoint (GeoEdgePoint) nie został zainicjalizowany");
            return fkdPointType;
        }
    }
}
