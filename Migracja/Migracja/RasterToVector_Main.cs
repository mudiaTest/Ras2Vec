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
        public static MapFactory RunRasterToVectorMainThread(RasterToVectorSettings aSettings)
        {
            MapFactory singleThreadFactory = new MapFactory(aSettings);
            singleThreadFactory.PrzygotujMapFactory();
            singleThreadFactory.GroupRect();
            singleThreadFactory.FillColorArr();
            singleThreadFactory.MakeEdgesForGroups();
            singleThreadFactory.UpdateColorArr();
            singleThreadFactory.MakeInnerEdgesForGroups();
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
