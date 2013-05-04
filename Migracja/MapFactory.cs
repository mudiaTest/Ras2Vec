using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace Ras2Vec
{

    class MapFactory : Dictionary<int, VectoredRectangleGroup>
    {
        public Vector_Rectangle[][] vectArr { get; set; }
        public int srcWidth { get; set; } //szerokość wczytanego (ReadFromImgIntoRectArray) obrazka
        public int srcHeight { get; set; } //wysokość wczytanego (ReadFromImgIntoRectArray) obrazka
        //wsp. geo. lewego górnego rogu
        public float geoLeftUpX { get; set; }
        public float geoLeftUpY { get; set; }
        //wsp. geo. prawego dolnego rogu
        public float geoRightDownX { get; set; }
        public float geoRightDownY { get; set; }
        public float XGeoPX { get; set; }
        public float YeGoPX { get; set; }
        public Dictionary<int, ColorGroupList> vectRectGroupsByColor { get; set; } //key - kolor; obj - lista grup w tym kolorze
        public ColorPx[][] colorArr{ get; set; } //kolor każdego pixela
        protected int inMod;
        public string stMessage;
        public string stTime;
        //dostęp do obiektów używając GetObjById i SetObjById
        //property vObj[index: integer]: TVectObj read GetObjByIdx write SetObjByIdx;

        private void UpdateInfoAction(string aStr) { }

        private void InfoTime(string aStr) { }

        //wypełnia vectArr obiektami TVectRectangle reprezentującymi poszczególne
        //pixele obrazka
        public void ReadFromImgIntoRectArray(Bitmap aImg)  
        {
            Debug.Assert(false, "Uzupełnić");
        }

        //oddaje bitmapę - wypełnia obraz "rectanglami"
        public Bitmap FillImgWithRect(Bitmap aImg, int aZoom, bool atestColor, bool aGrid, Color agridColor) 
        {
            Debug.Assert(false, "Uzupełnić");
            return null;
        }

        //oddaje bitmapę - wypełnia obraz "polygonami"
        public Bitmap FillImgWithPolygons(Bitmap aImg, int aZoom, bool atestColor, bool aGrid, Color agridColor) 
        {
            Debug.Assert(false, "Uzupełnić");
            return null;
        }

        //tworzy grupy obiektów TVectRectangle czyli przyszłe polygony
        public void GroupRect()
        {
     /*       var
  x, y: Integer;
  vectObj: TVectRectangle;
  perf: TTimeInterval;
  perf2, perf3: TTimeInterval;
  colorGroupList: TColorGroupList;*/

            /**perf = TTimeInterval.Create;
            perf2 = TTimeInterval.Create;
            perf3 = TTimeInterval.Create;*/
            Clear();
            int lpGroup = 0;
            ColorGroupList colorGroupList;
            Vector_Rectangle vectObj;
            Random random = new Random();
   
            for (int y=0; y<srcHeight; y++) 
            {
                //perf.Start;
                for (int x=0; x<srcWidth; x++)
                {
                    vectObj = vectArr[x][y];
                    //perf2.Start(false);
                    if (vectObj.parentVectorGroup == null)
                    {
                        vectObj.parentVectorGroup = new VectoredRectangleGroup();
                        vectObj.parentVectorGroup.parentMapFactory = this;
                        vectObj.parentVectorGroup.lpGroup = lpGroup;
                        lpGroup++;
                        vectObj.parentVectorGroup.testColor = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
                        vectObj.parentVectorGroup.sourceColor = vectObj.color;
                        vectObj.parentVectorGroup.Add(0, vectObj);
                        vectObj.parentVectorGroup.edgeList.Add(0, vectObj);
                        
                        int key = this.NextKey();
                        Add(key, vectObj.parentVectorGroup);
                        vectObj.parentVectorGroupId = key;

                        //dodanie grupy do listy kolorów
                        colorGroupList = vectRectGroupsByColor[vectObj.parentVectorGroup.sourceColor.ToArgb()];
                        if (colorGroupList == null)
                        {
                            colorGroupList = new ColorGroupList(vectRectGroupsByColor.Count);
                            vectRectGroupsByColor.Add(vectObj.parentVectorGroup.sourceColor.ToArgb(), colorGroupList);
                        }
                        colorGroupList.SetColorPx(vectObj.parentVectorGroup.sourceColor);
                        colorGroupList.Add(vectObj.parentVectorGroupId, vectObj.parentVectorGroup);
                    }
                    //perf2.Stop;
                    //perf3.Start(false);
                    if (x < srcWidth-1) 
                        Vector_Rectangle.Zintegruj(vectObj, vectArr[x+1][y], this);
                    if (y < srcHeight-1)
                        Vector_Rectangle.Zintegruj(vectObj, vectArr[x][y+1], this);
                    //perf3.Stop;
                }
                //perf.Stop;
                //UpdateInfoAction('Grupowanie pixeli - linia:' + IntToStr(y) + '/' + IntToStr(srcHeight-1));
                //InfoTime('Time: ' + perf.InterSt + ' / ' + perf2.InterSt + ' / ' + perf3.InterSt);
                //perf2.Reset;
                //perf3.Reset;
            }

        }

        //wypełania arrColor, czyli tablicę z informacją o kolorach piceli i ich sąsiadów
        public void FillColorArr(){}

        //uzupełnia arrColor i informacje o kierunkach dla px będącymi granicami zewnętrznymi
        public void UpdateColorArr(){}

        //dla wszystkich grup tworzone są krawędzie (mekeEdges)
        public void MakeEdgesForGroups(){}

        //wybudowanie granic wewnętrznych. Przechodzimy po tablicy colorArr i dla
        //kandydatów dodajemy do ich grup nowe granice wewnętrzne - dlo listy
        public void MakeInnerEdgesForGroups(){}

        public MapFactory()
        {
            vectRectGroupsByColor = new Dictionary<int, ColorGroupList>(256*4);
        }

        //Oblicze ile stopni zawiera jeden PX
        public void CalculateGeoPx(){}
    }
}
