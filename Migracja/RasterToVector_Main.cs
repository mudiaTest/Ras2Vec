using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;

namespace Migracja
{
    class R2VRunner
    {
        public static MapFactory RunR2VMainThread(R2VSettings aSettings, UpdateInfoBoxTimeDelegate aFunct)
        {
            aSettings.sliceDisplacementX = 0;
            aSettings.sliceDisplacementY = 0;
            MapFactory singleThreadFactory = new MapFactory(aSettings) { infoBoxUpdateFunct = aFunct };
            DateTime datePrv = DateTime.Now;
            singleThreadFactory.PrzygotujMapFactory();
            datePrv = aFunct("'Przygotuj singleThreadFactory'", true, datePrv);

            singleThreadFactory.GroupRect();
            datePrv = aFunct("'  GroupRect'", false, datePrv);

            //wypełnianie informacją o kolorze
            singleThreadFactory.FillColorArr();
            datePrv = aFunct("'FillColorArr'", false, datePrv);

            // połączenie granicznych grup rect

            //budowanie krawędzi VectoredRectangleGroup.edgeList (lista kolejnych obiektów VectoredRectangle)
            singleThreadFactory.MakeEdgesForGroups();
            datePrv = aFunct("'MakeEdgesForGroups'", false, datePrv);

            //budowanie uproszczonej krawędzi na podstawie VectoredRectangleGroup.edgeList
            singleThreadFactory.MakeSimplifiedEdges();
            datePrv = aFunct("MakeSimplifiedEdges", false, datePrv);

            //
            singleThreadFactory.UpdateColorArr();
            datePrv = aFunct("'UpdateColorArr'", false, datePrv);

            //budowanie granic wewnętrznych
            singleThreadFactory.MakeInnerEdgesForGroups();
            aFunct("'MakeInnerEdgesForGroups'", false, datePrv);

            //budowanie list punktów dla rysowania polygonów - dla NIEUPROSZCZONEJ krawędzi. 
            //Jako mnożnika używamy maxymalnego dozwolonego powiększenia
            //Wynik zapisywany jest do VectoredRectangleGroup.pointArrFromFullEdge
            singleThreadFactory.MakePointArrFromFullEdgeForGroups();
            aFunct("'MakePointArrFromFullEdgeForGroups'", false, datePrv);

            //budowanie list punktów dla rysowania polygonów - dla UPROSZCZONEJ krawędzi. 
            //Jako mnożnika używamy maxymalnego dozwolonego powiększenia
            //Wynik zapisywany jest do VectoredRectangleGroup.pointArrFromSimplifiedEdge
            singleThreadFactory.MakePointArrFromSimplifiedEdgeForGroups();
            aFunct("'MakePointArrFromSimplifiedEdgeForGroups'", false, datePrv);

            //singleThreadFactory.Init(aSettings);
            return singleThreadFactory;
        }

        public static void RunRasterToVectorSeparateThread()
        {
            RasterToVectorFactory separateThreadFactory = new RasterToVectorFactory();            
            Debug.Assert(false, "Akcja nie została zimplementowana.");
        }
    }

    class RasterToVectorFactory : Dictionary<int, Vector_Gen>
    {
        /*private float fgeoLeftUpX;
        private float fgeoLeftUpY;
        private float fgeoRightDownX;
        private float fgeoRightDownY;*/
        private Vector_Gen[][] vectorArray; //tablica z obektami wektorowymi
        R2VSettings settings;
        //przechowuje key to kolor w postaci int, value to grupa obektów wektorowych w tym kolorze
        public Dictionary<int, ColorGroupList> vectRectGroupsByColor{ get; set; }

        public void Init(R2VSettings aSettings)
        {
            settings = aSettings;
            settings.CalculateGeoPx();
        }

        
    }
}
