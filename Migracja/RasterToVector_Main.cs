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
            aSettings.sliceDisplacementX = 0;
            aSettings.sliceDisplacementY = 0;
            MapFactory singleThreadFactory = new MapFactory(aSettings) { infoBoxUpdateFunct = aFunct };
            DateTime datePrv = DateTime.Now;
            singleThreadFactory.PrzygotujMapFactory();
            datePrv = aFunct("'Przygotuj singleThreadFactory'", false, datePrv);

            singleThreadFactory.GroupRect();
            datePrv = aFunct("'  GroupRect'", false, datePrv);
           /* Bitmap srcBmp = aSettings.sourceBmp;
            for (int y = 0; y < srcBmp.Height; y += aSettings.sliceHeight)
            {
                for (int x = 0; x < srcBmp.Width; y += aSettings.sliceWidth)
                {
                    Bitmap sliceSrcBmp = new Bitmap(aSettings.sliceWidth, aSettings.sliceHeight);
                    Graphics sliceSrcGraphics = Graphics.FromImage(sliceSrcBmp);
                    sliceSrcGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    sliceSrcGraphics.DrawImage(srcBmp,
                                               new Rectangle(0, 
                                                             0,
                                                             Math.Min(aSettings.sliceWidth,
                                                                      srcBmp.Width - x * aSettings.sliceWidth),
                                                             Math.Min(aSettings.sliceHeight,
                                                                      srcBmp.Height -
                                                                      y*aSettings.sliceHeight)),
                                               new Rectangle(aSettings.sliceWidth*x, 
                                                             aSettings.sliceHeight*y,
                                                             Math.Min(aSettings.sliceWidth,
                                                                      srcBmp.Width - x * aSettings.sliceWidth),
                                                             Math.Min(aSettings.sliceHeight,
                                                                      srcBmp.Height -
                                                                      y*aSettings.sliceHeight)),
                                               System.Drawing.GraphicsUnit.Pixel);
                    aSettings.sourceBmp = sliceSrcBmp;

                    //przygotowanie do vektorowanie - np. zczytanie obrazka to tablicy rectangli
                    MapFactory sliceThreadFactory = new MapFactory(aSettings) { infoBoxUpdateFunct  = aFunct };
                    sliceThreadFactory.PrzygotujMapFactory();
                    datePrv = aFunct( "x:'" + x.ToString() + " y:" + y.ToString() + '\n' +"'  Przygotuj sliceMapFactory" , false, datePrv);

                    //grupowanie rectangli
                    sliceThreadFactory.GroupRect();
                    datePrv = aFunct("'  GroupRect'", false, datePrv);

                    //przepisywanie grup rectangli do singleThreadFactory
                    singleThreadFactory.GetGroupRectDisplacement(sliceThreadFactory, x * aSettings.sliceWidth, y * aSettings.sliceHeight);
                    datePrv = aFunct("'  GetGroupRectDisplacement'", false, datePrv);

                }
            }
            aSettings.sourceBmp = srcBmp;*/

            //wypełnianie informacją o kolorze
            singleThreadFactory.FillColorArr();
            datePrv = aFunct("'FillColorArr'", false, datePrv);

            // połączenie granicznych grup rect

            //budowanie granic
            singleThreadFactory.MakeEdgesForGroups();
            datePrv = aFunct("'MakeEdgesForGroups'", false, datePrv);

            //
            singleThreadFactory.UpdateColorArr();
            datePrv = aFunct("'UpdateColorArr'", false, datePrv);

            //budowanie granic wewnętrznych
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
