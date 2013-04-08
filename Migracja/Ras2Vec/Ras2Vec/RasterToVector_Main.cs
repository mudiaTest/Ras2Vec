using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;

namespace Ras2Vec
{
    public class RasterToVectorSettings
    {
        //źródłowa bitmapa rastrowa
        private Bitmap fsourceBmp;
        public Bitmap sourceBmp{get{return fsourceBmp;} 
                                set{fsourceBmp = value;
                                    fsrcWidth = fsourceBmp.Width;
                                    fsrcHeight = fsourceBmp.Height;
                                }
        }
        //szerokość i wysokość żródłowego obrazu (rastra) w px - upraszczają, bo nie trzeba odwoływać się przez sourceBmp
        private int fsrcWidth;
        private int fsrcHeight;
        public int srcWidth { get{return fsrcWidth;} }
        public int srcHeight { get{return fsrcHeight;} }

        //wartości współrzędnych w rogach mapy w stopniach z cześcią dziesiętną
        public float geoLeftUpX { get; set; }
        public float geoLeftUpY { get; set; }
        public float geoRightDownX { get; set; }
        public float geoRightDownY { get; set; }

        //ile stopni (wertość float) zawiera jeden pixel żdódłowego obrazu
        public float xGeoPX { get; set; }
        public float yGeoPX { get; set; }

        public void ReadGeoCorners(String stLeftUpX, String stLeftUpY, String stRightDownX, String stRightDownY)
        {
            geoLeftUpX = DecodeGeoStr(stLeftUpX);
            geoLeftUpY = DecodeGeoStr(stLeftUpY);
            geoRightDownX = DecodeGeoStr(stRightDownX);
            geoRightDownY = DecodeGeoStr(stRightDownY);
        }
        private float DecodeGeoStr(string aGeoPoint)
        {
            Debug.Assert(aGeoPoint != "", "aGeoPoint jest pusty");
            String[] tmp = aGeoPoint.Split(',');
            return float.Parse(tmp[0]) + float.Parse(tmp[1]) / 60 + float.Parse(tmp[2]) / 3600 + float.Parse(tmp[3]) / 360000;
        }
        public void CalculateGeoPx()
        {
            Debug.Assert(srcWidth <= 0 | srcHeight <= 0, "Szorokość lub wysokość obrazu żródłowgo jest zerowa: " + srcWidth.ToString() + "," + srcHeight.ToString());
            xGeoPX = (geoRightDownX - geoLeftUpX) / (srcWidth + 1);
            yGeoPX = (geoLeftUpY - geoRightDownY) / (srcHeight + 1);
        }
    }

    public class RasterToVectorRunner
    {
        public static void RunRasterToVectorMainThread(RasterToVectorSettings aSettings)
        {
            RasterToVectorFactory singleThreadFactory = new RasterToVectorFactory();
            singleThreadFactory.Init(aSettings);
        }

        public static void RunRasterToVectorSeparateThread()
        {
            RasterToVectorFactory separateThreadFactory = new RasterToVectorFactory();
        }
    }
    class RasterToVectorFactory
    {
        /*private float fgeoLeftUpX;
        private float fgeoLeftUpY;
        private float fgeoRightDownX;
        private float fgeoRightDownY;*/
        RasterToVectorSettings settings;

        public void Init(RasterToVectorSettings aSettings)
        {
            settings = aSettings;
            settings.CalculateGeoPx();
        }

        
    }
}
