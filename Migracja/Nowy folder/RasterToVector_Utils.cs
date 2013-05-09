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

    class ColorPx
    {
        public int color{get;set;}
        public int? colorN {get;set;}
        public int? colorS {get;set;}
        public int? colorW {get;set;}
        public int? colorE {get;set;}
        public int group  {get;set;}
        public int? groupN {get;set;}
        public int? groupS {get;set;}
        public int? groupW {get;set;}
        public int? groupE {get;set;}
        public bool borderNS {get;set;}
        public bool borderSN {get;set;}
        public bool borderEW {get;set;}
        public bool borderWE {get;set;}
        public bool candidate {get;set;}
        public bool used {get;set;}
    }
}
