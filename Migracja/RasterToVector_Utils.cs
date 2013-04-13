using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Ras2Vec
{
    class RasterToVector_Utils
    {
        public static int Round(double d)
        {
            return (int)(d + 0.5);
        }
    }

    public class VectorRectangeGroup : Dictionary<int, Vector_Rectangle>
    {
    }

    //na razie nie jest rozbudowany, ale bedzie potem
    public class GeoPoint
    {
        private float X;
        private float Y;
        public GeoPoint() { }
        public GeoPoint(float x, float y)
        {
            X = x;
            Y = y;
        }
        public Point ToPoint()
        {
            return new Point(RasterToVector_Utils.Round(X), RasterToVector_Utils.Round(Y));
        }
    }

    public class ColorPx
    {
        public int color{get;set;}
        public int colorN {get;set;}
        public int colorS {get;set;}
        public int colorW {get;set;}
        public int colorE {get;set;}
        public int group  {get;set;}
        public int groupN {get;set;}
        public int groupS {get;set;}
        public int groupW {get;set;}
        public int groupE {get;set;}
        public bool borderNS {get;set;}
        public bool borderSN {get;set;}
        public bool borderEW {get;set;}
        public bool borderWE {get;set;}
        public bool canidate {get;set;}
        public bool used {get;set;}
    }
}
