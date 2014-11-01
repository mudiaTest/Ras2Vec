using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Migracja
{
    class RasterToVector_Utils
    {
        public static int Round(double d)
        {
            return (int)(d + 0.5);
        }
    }

    class ColorGroupList: Dictionary<int, VectoredRectangleGroup>
    {
        public Color colorPx{ get; set;}
        public string colorMP{get; set;}
        public string colorTyp{get; set;}
        public int idMPGroup{get; set;}

        public ColorGroupList(int aIdMPGroup)
        {
            idMPGroup = aIdMPGroup;
        }

        public void SetColorPx(Color aColorPx)
        {
            if (colorMP == "")
            {
                colorPx = aColorPx;
               
                colorMP = "0x"+idMPGroup.ToString("X").PadRight(4,'0');
                string stColor = int.Parse(aColorPx.R.ToString() + aColorPx.G.ToString() + aColorPx.B.ToString()).ToString("X").PadRight(6, '0');
                /*stBlue = copy(stColor, 1, 2);
                stGreen = copy(stColor, 3, 2);
                stRed = copy(stColor, 5, 2);
                fcolorTyp = stRed + stGreen + stBlue;*/
                colorTyp = stColor;
            } 
            //else
            //Assert(colorPx = acolorPx, "Przypisujemy inny kolor. Było: " + colorPx.ToString() + " a ma być:" + aColorPx.ToString()) ;
        
        }
    }

    class RectGroupsByColorMap: Dictionary<Color, ColorGroupList>
    {
    
    }

    //na razie nie jest rozbudowany, ale bedzie potem
    class GeoPoint
    {
        //współrzędne geograficzne punktu 
        internal float X; 
        internal float Y;
        //współrzędne punktu na źródłowej bitmapie
        internal int fPictX;
        internal int fPictY;
        public GeoPoint() { }
        public GeoPoint(float x, float y, int aPictX, int aPictY)
        {
            X = x;
            Y = y;
            fPictX = aPictX;
            fPictY = aPictY;
        }
        public Point ToPoint()
        {
            return new Point(RasterToVector_Utils.Round(X), RasterToVector_Utils.Round(Y));
        }
        public Point ToPictPoint()
        {
            return new Point(fPictX, fPictY);
        }

        public int pictX { get { return fPictX; } }
        public int pictY { get { return fPictY; } }
    }

    class GeoEdgePoint : GeoPoint
    {
        private int kdPointType; //Cst.c_geoPxSimple/c_geoPxStartEnd/c_geoPxStartEnd
        public GeoEdgePoint(float x, float y, int aPictX, int aPictY, int akdPointType /*= Cst.c_geoPxSimple*/): base(x, y, aPictX, aPictY)
        {
            kdPointType = akdPointType;
        }

        public PointAdv PointToPointAdv()
        {
            return new PointAdv(RasterToVector_Utils.Round(X), RasterToVector_Utils.Round(Y), kdPointType);
        }
        public PointAdv PictPointToPointAdv()
        {
            return new PointAdv(fPictX, fPictY, kdPointType);
        }
    }

    class ColorPx
    {
        //kolor danego px            
        public int color{get;set;}
        public int? colorN {get;set;}
        public int? colorS {get;set;}
        public int? colorW {get;set;}
        public int? colorE {get;set;}
        //numer grupy do której należy dany px. Tę daną wyełniamy już po grupowaniu px
        public int group  {get;set;}
        public int? groupN {get;set;}
        public int? groupS {get;set;}
        public int? groupW {get;set;}
        public int? groupE {get;set;}
        public bool borderNS {get;set;}
        public bool borderSN {get;set;}
        public bool borderEW {get;set;}
        public bool borderWE {get;set;}
        //kandydat do bycia cześcią granicy grupy
        public bool candidate {get;set;}
        //px został użyty jako cześć granicy grupy
        public bool used {get;set;}
    }

    class R2VUtils
    {
        public static string PointListToString(List<Point> aPointList) 
        {
            string result = "";
            foreach (Point point in aPointList)
            {
                if (result != "")
                {
                    result += ", ";
                }
                result += String.Format("({0}, (1))", point.X, point.Y);
            }
            return result;
        }
    }

    internal delegate DateTime UpdateInfoBoxTimeDelegate(string aText = "", bool aBlNewLine = true, DateTime? aDatePrv = null);
}
