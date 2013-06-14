using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace Migracja
{
    //w ciele znajdują się grupy rectangli (VectoredRectangleGroup), czyli zbiory sąsiadujących rectangli o tym samym kolorze
    class MapFactory : Dictionary<int, VectoredRectangleGroup>
    {
        public Vector_Rectangle[][] vectArr;// { get; set; } //2 wymiarowa tablica odzwiercieglająca rastra przy pomocy obiektów Vector_Rectangle
        public int srcWidth { get; set; } //szerokość wczytanego (ReadFromImgIntoRectArray) obrazka
        public int srcHeight { get; set; } //wysokość wczytanego (ReadFromImgIntoRectArray) obrazka
        //wsp. geo. lewego górnego rogu
        public float geoLeftUpX { get; set; }
        public float geoLeftUpY { get; set; }
        //wsp. geo. prawego dolnego rogu
        public float geoRightDownX { get; set; }
        public float geoRightDownY { get; set; }
        public float XGeoPX { get; set; } //ile stopni zawiera zmiana o jeden px pionowo
        public float YeGoPX { get; set; } //ile stopni zawiera zmiana o jeden px poziomo
        public Dictionary<int, ColorGroupList> vectRectGroupsByColor { get; set; } //key - kolor; obj - lista grup w tym kolorze
        public ColorPx[][] colorArr{ get; set; } //kolor każdego pixela
        protected int inMod;
        public string stMessage;
        public string stTime;
        private RasterToVectorSettings settings;
        internal UpdateInfoBoxTimeDelegate infoBoxUpdateFunct;

        internal int maxKey;
        internal void ClearReset()
        {
            Clear();
            maxKey = 0;
        }

        internal TimeSpan ts;

        //dostęp do obiektów używając GetObjById i SetObjById
        //property vObj[index: integer]: TVectObj read GetObjByIdx write SetObjByIdx;

        private void UpdateInfoAction(string aStr) { }

        private void InfoTime(string aStr) { }

        //wypełnia vectArr obiektami TVectRectangle reprezentującymi poszczególne
        //pixele obrazka
        public void ReadFromImgIntoRectArray(Bitmap aImg)  
        {
            Vector_Rectangle rec;
            srcWidth = aImg.Width;
            srcHeight = aImg.Height;
            vectArr = new Vector_Rectangle[srcWidth][];
            for (int i = 0; i < srcWidth; i++)
                vectArr[i] = new Vector_Rectangle[srcHeight];
            colorArr = new ColorPx[srcWidth][];
            for (int i = 0; i < srcWidth; i++)
                colorArr[i] = new ColorPx[srcHeight];
            
            for (int y=0; y<srcHeight; y++) 
                for (int x=0; x<srcWidth; x++) 
                    vectArr[x][y] = new Vector_Rectangle(aImg.GetPixel(x, y),
                                                         new Point(x, y),
                                                         new Point(x, y)
                                                         );
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
            ClearReset();
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
                        DateTime d1 = DateTime.Now;
                        int key = this.NextKey();
                        ts += (DateTime.Now - d1);
                        Add(key, vectObj.parentVectorGroup);
                        vectObj.parentVectorGroupId = key;
                        //dodanie grupy do listy kolorów
                        if (vectRectGroupsByColor.ContainsKey(vectObj.parentVectorGroup.sourceColor.ToArgb()))
                            colorGroupList = vectRectGroupsByColor[vectObj.parentVectorGroup.sourceColor.ToArgb()];
                        else
                            colorGroupList = null;
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

        //DateTime d1 = DateTime.Now;

        //metoda pomocnicza dla FillColorArr
        ColorPx dajColorPx(int x, int y)
        {
            ColorPx Result = new ColorPx();
            Result.color = vectArr[x][y].color.ToArgb();
            Result.group = vectArr[x][y].parentVectorGroupId;
            if (y > 0)
            {
                Result.colorN = vectArr[x][y - 1].color.ToArgb();
                Result.groupN = vectArr[x][ y - 1].parentVectorGroupId;
            }
            else
            {
                Result.colorN = null;
                Result.groupN = null;
            };
            if (y < srcHeight - 1)
            {
                Result.colorS = vectArr[x][y + 1].color.ToArgb();
                Result.groupS = vectArr[x][y + 1].parentVectorGroupId;
            }
            else
            {
                Result.colorS = null;
                Result.groupS = null;
            };
            if (x < srcWidth - 1)
            {
                Result.colorE = vectArr[x + 1][y].color.ToArgb();
                Result.groupE = vectArr[x + 1][y].parentVectorGroupId;
            }
            else
            {
                Result.colorE = null;
                Result.groupE = null;
            };
            if (x > 0)
            {
                Result.colorW = vectArr[x - 1][y].color.ToArgb();
                Result.groupW = vectArr[x - 1][y].parentVectorGroupId;
            }
            else
            {
                Result.colorW = null;
                Result.groupW = null;
            };
            return Result;
        }

        //wypełania arrColor, czyli tablicę z informacją o kolorach pixeli i ich sąsiadów
        public void FillColorArr()
        {
            ColorPx colorPx;
            ColorPx colorPx2;

            // perf = TTimeInterval.Create;
            // perf2 = TTimeInterval.Create;
            // perf3 = TTimeInterval.Create;
            // Clear;

            // srodek
            for (int y = 0; y < srcHeight; y++)
            {
                // perf.Start;
                for (int x = 0; x < srcWidth; x++)
                {
                    colorArr[x][y] = dajColorPx(x, y);
                }
            }

            for (int y = 0; y < srcHeight - 1; y++)
            {
                // perf.Start;
                for (int x = 0; x < srcWidth; x++)
                {
                    colorPx = colorArr[x][y];
                    colorPx2 = colorArr[x][y + 1];
                    if ((colorPx.group != colorPx2.group) && !colorPx.borderEW)
                        colorPx.candidate = true;
                }
            }
        }

        //uzupełnia arrColor i informacje o kierunkach dla px będącymi granicami zewnętrznymi
        public void UpdateColorArr()
        {
            // lpGrupa: integer;
            // perf: TTimeInterval;
            // perf2, perf3: TTimeInterval;
            // colorGroupList: TColorGroupList;
            ColorPx colorPx;
            ColorPx colorPx2;
        
            // perf := TTimeInterval.Create;
            // perf2 := TTimeInterval.Create;
            // perf3 := TTimeInterval.Create;
            // Clear;
            // lpGrupa := 0;
            // srodek

            for (int y = 0; y < srcHeight - 1; y++)
            {
                // perf.Start;
                for (int x = 0; x < srcWidth; x++)
                {
                    colorPx = colorArr[x][y];
                    colorPx2 = colorArr[x][y + 1];
                    if ((colorPx.group != colorPx2.group) && !colorPx.borderEW)
                        colorPx.candidate = true;
                    else
                        colorPx.candidate = false;
                }
            }
        }

        //dla wszystkich grup tworzone są krawędzie (mekeEdges)
        public void MakeEdgesForGroups()
        {
            VectoredRectangleGroup vectGroup;
            long dummy;
            int i = 0;

            //for (int i = 0; i < Count; i++)
            foreach(KeyValuePair<int, VectoredRectangleGroup> pair in this)
            {
                vectGroup = pair.Value;                
                if ((int)Math.DivRem((long)i, (long)inMod, out dummy) == 0)
                    UpdateInfoAction("Tworzenie granicy dla grupy " + i.ToString() + "/" + (Count - 1).ToString());
                i++;
                vectGroup.MakeEdges(vectGroup.edgeList);
            };            
        }

        internal void MakeSimplifiedEdges()
        {
            VectoredRectangleGroup vectGroup;
            foreach (KeyValuePair<int, VectoredRectangleGroup> pair in this)
            {
                vectGroup = pair.Value;
                vectGroup.MakeSimplifyVectorEdge();
            }
        }

        //wybudowanie granic wewnętrznych. Przechodzimy po tablicy colorArr i dla
        //kandydatów dodajemy do ich grup nowe granice wewnętrzne - dlo listy
        public void MakeInnerEdgesForGroups()
        {
            ColorPx colorPx;
            ColorPx colorPxDown;
            VectoredRectangleGroup vectGroup;
            VectoredRectangleGroup vectGroupDown;
            VectorRectangeGroup list;
            
            for (int y = 0; y < srcHeight - 1; y++)
                for (int x = 0; x < srcWidth; x++)
                {
                    colorPx = colorArr[x][y];
                    colorPxDown = colorArr[x][y + 1];
                    if (colorPx.candidate && !colorPx.used)
                    {
                        // wybuduj i dodaj wewnętrzną granicę

                        // budujemy listę punktów dla nowej granicy wewnętrznej
                        list = new VectorRectangeGroup();
                        // vectGroup grupa główna
                        vectGroup = this[colorPx.group];
                        // vectGroupDown - grupa px będącego poniżej. Z niego zaczniemy budować  granicę wewnętrzną
                        vectGroupDown = this[colorPxDown.group];
                        // MakeEdges wybuduje granicę i wszystkie px powyżej punktów granicy ustawi used=true
                        vectGroupDown.MakeEdges(list, true, colorPx.group);
                        vectGroup.innerEdgesList.Add(vectGroup.innerEdgesList.NextKey(), list);
                    }
                }
        }

        internal void MakePointArrFromFullEdgeForGroups()
        {
            foreach (KeyValuePair<int, VectoredRectangleGroup> pair in this)
            {
                pair.Value.MakePointArrFromFullEdge(Cst.maxZoom, 
                                                    0,
                                                    0);
            }
        }

        internal void MakePointArrFromSimplifiedEdgeForGroups()
        {
            foreach (KeyValuePair<int, VectoredRectangleGroup> pair in this)
            {
                pair.Value.MakePointArrFromSimplifiedEdge(Cst.maxZoom,
                                                          0,
                                                          0);
            }
        }

        public MapFactory(RasterToVectorSettings aSettings)
        {
            settings = aSettings;
            vectRectGroupsByColor = new Dictionary<int, ColorGroupList>(256*4);
            inMod = 1;
            maxKey = 0;
            ts = new TimeSpan();
        }

        internal int NextKey()
        {
            return maxKey++;
        }

        public void PrzygotujMapFactory()
        {
            ClearReset();
            ReadFromImgIntoRectArray(settings.sourceBmp);
            geoLeftUpX = settings.geoLeftUpX;
            geoLeftUpY = settings.geoLeftUpY;
            geoRightDownX = settings.geoRightDownX;
            geoRightDownY = settings.geoRightDownY;
        }

        public VectoredRectangleGroup GetGroupByXY(int x, int y, Boolean[][] aUsedArray)
        {
            

            if (!aUsedArray[x][y])
            {
                VectoredRectangleGroup result = vectArr[x][y].parentVectorGroup;
                foreach (KeyValuePair<int, Vector_Rectangle> pair in result)
                {
                    if (aUsedArray.Length > pair.Value.p1.X)
                        if(aUsedArray[pair.Value.p1.X].Length > pair.Value.p1.Y)
                            aUsedArray[pair.Value.p1.X][pair.Value.p1.Y] = true;
                }
                return result;
            }
            else
            {
                return null;
            }
            
           /* if (!aList.Contains(result.lpGroup))
            {
                aList.Add(result.lpGroup);
                return result;
            }
            else
            {
                return null;
            }*/
        }

        //public Bitmap getBitmap(Rectangle rect)
        //{
        //    Bitmap result = new Bitmap(rect.Width, rect.Height);
        //    ...
        //    return result;
        //}
    }
}
