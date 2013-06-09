using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;

namespace Migracja
{
    class RasterToVectorRunner
    {
        public static MapFactory RunRasterToVectorMainThread(RasterToVectorSettings aSettings, UpdateInfoBoxTimeDelegate aFunct)
        {

            for (int y = 0; y < aSettings.sourceBmp.Height; y += aSettings.sliceHeight)
            {
                for (int x = 0; x < aSettings.sourceBmp.Width; y+=aSettings.sliceWidth)
                {
                    Bitmap sliceSrcBmp = new Bitmap(aSettings.sliceWidth, aSettings.sliceHeight);
                    Graphics sliceSrcGraphics = Graphics.FromImage(sliceSrcBmp);
                    sliceSrcGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    sliceSrcGraphics.DrawImage(aSettings.sourceBmp,
                                                );
            }


            MapFactory singleThreadFactory = new MapFactory(aSettings) { infoBoxUpdateFunct  = aFunct };
            DateTime datePrv = DateTime.Now;
            singleThreadFactory.PrzygotujMapFactory();
            datePrv = aFunct("'PrzygotujMapFactory'", false, datePrv);
            singleThreadFactory.GroupRect();
            datePrv = aFunct("'GroupRect'", false, datePrv);
            singleThreadFactory.FillColorArr();
            datePrv = aFunct("'FillColorArr'", false, datePrv);
            singleThreadFactory.MakeEdgesForGroups();
            datePrv = aFunct("'MakeEdgesForGroups'", false, datePrv);
            singleThreadFactory.UpdateColorArr();
            datePrv = aFunct("'UpdateColorArr'", false, datePrv);
            singleThreadFactory.MakeInnerEdgesForGroups();
            aFunct("'MakeInnerEdgesForGroups'", false, datePrv);
            //singleThreadFactory.Init(aSettings);
            return singleThreadFactory;
        }

        public static void RunRasterToVectorSeparateThread()
        {
            RasterToVectorFactory separateThreadFactory = new RasterToVectorFactory();
        }
    }
    class RasterToVectorFactory : Dictionary<int, Vector_Gen>
    {
        /*private float fgeoLeftUpX;
        private float fgeoLeftUpY;
        private float fgeoRightDownX;
        private float fgeoRightDownY;*/
        private Vector_Gen[][] vectorArray; //tablica z obektami wektorowymi
        RasterToVectorSettings settings;
        //przechowuje key to kolor w postaci int, value to grupa obektów wektorowych w tym kolorze
        public Dictionary<int, ColorGroupList> vectRectGroupsByColor{ get; set; }

        public void Init(RasterToVectorSettings aSettings)
        {
            settings = aSettings;
            settings.CalculateGeoPx();
        }

        
    }
}
