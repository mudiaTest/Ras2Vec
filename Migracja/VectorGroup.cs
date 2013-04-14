using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Ras2Vec
{
    //DynamicGeoPointArray = List<GeoPoint>;
    //TDynamicPointArray = Vector_Gen[];
    //TDynamicPxColorPointArray = ColorPx[,];

    //Grupa rectangli tworzacych jedną płąszczyznę. Pozwala obliczyć swoją granicę
    class VectoredRectangleGroup : Dictionary<int, Vector_Gen>
    {
        //cztery punkty geograficzne określanące rogi obrazka
        //leftTopGeo, rightTopGeo, leftBottomGeo, rightBottomGeo: Double;
        //lista obiektów Vector_Rectangle tworzących krawędź (self, czyli grupy obiektów Vector_Rectangle). 
        //Kolejnośc wyznaczają klucze
        public VectorRectangeGroup edgeList{get;set;}
        //lista wewnętrznych krawędzi. Każda z nich ma konstrukcję jak edgePxList
        public Dictionary<int, VectorRectangeGroup> innerEdgesList{get;set;}
        //lista krawędzi punktów Double
        //fedgeGeoList: TIntList;
        //lista krawędzi punktów-pixeli (kolejnych), które zostały poddane uproszczaniu - jest to okrojona edgePxList
        public VectorRectangeGroup simplifiedEdgeList{get;set;}
        //lista uroszczonych wewnętrznych krawędzi. Każda z nich ma konstrukcję jak edgePxList
        public Dictionary<int, VectorRectangeGroup> simplifiedInnerEdgesList{get;set;}

        //niepotrzebe bo obiekt jedt grupą samą w sobie
        //lista 'kwadratów' należących do grupy
        //frectList: TIntList;
        //kolor testowy - tym kolorem wypełniana jest grupa gdy włączymy opcję testu
        public Color testColor{get;set;}
        //kolor oryginalny
        public Color sourceColor{get;set;}
        //lp utworzonej grupy. Później utworzona ma wyższy numer
        private int lpGroup{get;set;}
        //rodzic
        public MapFactory parentMapFactory{get;set;}

        //in: dwa KOLEJNE punkty poruszające się po liniach Hor i Ver
        //out: Cst.fromLeft, Cst.fromTop, Cst.fromRight, Cst.fromBottom
        private int Direction(Point p1, Point p2)
        {
            if (p1.X > p2.X)
                return Cst.fromRight;
            else if (p1.X < p2.X)
                return Cst.fromLeft;
            else if (p1.Y > p2.Y)
                return Cst.fromBottom;
            else
                return Cst.fromTop;
        }

        //tworzy krawędź z 3 kolejnych punktów
        //  multi = mnożnik: dla grafiki bedzie to zoom, dla geo będzie to szerokośc
        //          geograficzna jednego px
        private void MakePartEdge(  Point aPrvPoint, Point aActPoint, Point aNextPoint,
                                    ref int lpCounter, List<GeoPoint> aGeoArr,
                                    float aMultiX, float aMultiY, float dpDisplaceX, float dpDisplaceY,
                                    ColorPx[,] aColorArr,
                                    bool aBlOnlyFillColorArr)
        {
            ColorPx colorPx = aColorArr[aActPoint.X, aActPoint.Y] as ColorPx;
            
//-------------------

            if (Direction(aPrvPoint, aActPoint) == Cst.fromLeft) 
            {
                if (Direction(aActPoint, aNextPoint) == Cst.fromBottom) 
                {
                    //nic
                }
                else if (Direction(aActPoint, aNextPoint) == Cst.fromTop) 
                {
                    if (!aBlOnlyFillColorArr) 
                    {
                        aGeoArr[lpCounter] = new GeoPoint(aActPoint.X*aMultiX + aDisplaceX, aActPoint.Y*aMultiY + aDisplaceY);
                        aGeoArr[lpCounter+1] = new GeoPoint((aActPoint.X+1)*aMultiX + aDisplaceX, aActPoint.Y*aMultiY + aDisplaceY);
                        lpCounter = lpCounter + 2;
                    };
                    colorPx.borderWE = true;
                    colorPx.borderNS = true;
                }
                else if (Direction(aActPoint, aNextPoint) == Cst.fromLeft) 
                {
                    if (!aBlOnlyFillColorArr) 
                    {
                        aGeoArr[lpCounter] = new GeoPoint((aActPoint.X)*aMultiX + aDisplaceX, (aActPoint.Y)*aMultiY + aDisplaceY);
                        lpCounter = lpCounter + 1;
                    };
                    colorPx.borderWE = true;
                }
                else
                {
                    if (!aBlOnlyFillColorArr) 
                    {
                        aGeoArr[lpCounter] = new GeoPoint((aActPoint.X)*aMultiX + aDisplaceX, (aActPoint.Y)*aMultiY + aDisplaceY);
                        aGeoArr[lpCounter+1] = new GeoPoint((aActPoint.X+1)*aMultiX + aDisplaceX, aActPoint.Y*aMultiY + aDisplaceY);
                        aGeoArr[lpCounter+2] = new GeoPoint((aActPoint.X+1)*aMultiX + aDisplaceX, (aActPoint.Y+1)*aMultiY + aDisplaceY);
                        lpCounter = lpCounter + 3;
                    };
                    colorPx.borderWE = true;
                    colorPx.borderNS = true;
                    colorPx.borderEW = true;
                };
            }

            else if (Direction(aPrvPoint, aActPoint) == Cst.fromRight) 
            {
                if (Direction(aActPoint, aNextPoint) == Cst.fromBottom) 
                {
                    if (!aBlOnlyFillColorArr) 
                    {
                        aGeoArr[lpCounter] = new GeoPoint((aActPoint.X+1)*aMultiX + aDisplaceX, (aActPoint.Y+1)*aMultiY + aDisplaceY);
                        aGeoArr[lpCounter+1] = new GeoPoint(aActPoint.X*aMultiX + aDisplaceX, (aActPoint.Y+1)*aMultiY + aDisplaceY);
                        lpCounter = lpCounter + 2;
                    };
                    colorPx.borderEW = true;
                    colorPx.borderSN = true;
                }
                else if (Direction(aActPoint, aNextPoint) == Cst.fromTop) 
                {
                    //nic
                }
                else if (Direction(aActPoint, aNextPoint) == Cst.fromRight) 
                {
                    if (!aBlOnlyFillColorArr) 
                    {
                        aGeoArr[lpCounter] = new GeoPoint((aActPoint.X + 1)*aMultiX + aDisplaceX, (aActPoint.Y + 1)*aMultiY + aDisplaceY);
                        lpCounter = lpCounter + 1;
                    };
                    colorPx.borderEW = true;
                }
                else
                {
                    if (!aBlOnlyFillColorArr) 
                    {
                        aGeoArr[lpCounter] = new GeoPoint((aActPoint.X+1)*aMultiX + aDisplaceX, (aActPoint.Y+1)*aMultiY + aDisplaceY);
                        aGeoArr[lpCounter+1] = new GeoPoint(aActPoint.X*aMultiX + aDisplaceX, (aActPoint.Y+1)*aMultiY + aDisplaceY);
                        aGeoArr[lpCounter+2] = new GeoPoint(aActPoint.X*aMultiX + aDisplaceX, (aActPoint.Y)*aMultiY + aDisplaceY);
                        lpCounter = lpCounter + 3;
                    };
                    colorPx.borderEW = true;
                    colorPx.borderNS = true;
                    colorPx.borderWE = true;
                };
            }

            else if (Direction(aPrvPoint, aActPoint) == Cst.fromTop) 
            {
                if (Direction(aActPoint, aNextPoint) == Cst.fromLeft) 
                {
                    //nic
                }
                else if (Direction(aActPoint, aNextPoint) == Cst.fromRight) 
                {
                    if (!aBlOnlyFillColorArr) 
                    {
                        aGeoArr[lpCounter] = new GeoPoint((aActPoint.X+1)*aMultiX + aDisplaceX, aActPoint.Y*aMultiY + aDisplaceY);
                        aGeoArr[lpCounter+1] = new GeoPoint((aActPoint.X+1)*aMultiX + aDisplaceX, (aActPoint.Y+1)*aMultiY + aDisplaceY);
                        lpCounter = lpCounter + 2;
                    };
                    colorPx.borderNS = true;
                    colorPx.borderEW = true;
                }
                else if (Direction(aActPoint, aNextPoint) == Cst.fromTop) 
                {
                    if (!aBlOnlyFillColorArr) 
                    {
                        aGeoArr[lpCounter] = new GeoPoint((aActPoint.X+1)*aMultiX + aDisplaceX, aActPoint.Y*aMultiY + aDisplaceY);
                        lpCounter = lpCounter + 1;
                    };
                    colorPx.borderNS = true;
                }
                else
                {
                    if (!aBlOnlyFillColorArr) 
                    {
                        aGeoArr[lpCounter] = new GeoPoint((aActPoint.X+1)*aMultiX + aDisplaceX, aActPoint.Y*aMultiY + aDisplaceY);
                        aGeoArr[lpCounter+1] = new GeoPoint((aActPoint.X+1)*aMultiX + aDisplaceX, (aActPoint.Y+1)*aMultiY + aDisplaceY);
                        aGeoArr[lpCounter+2] = new GeoPoint(aActPoint.X*aMultiX + aDisplaceX, (aActPoint.Y+1)*aMultiY + aDisplaceY);
                        lpCounter = lpCounter + 3;
                    };
                    colorPx.borderNS = true;
                    colorPx.borderEW = true;
                    colorPx.borderSN = true;
                };
            }

            else //from bottom
            {
                if (Direction(aActPoint, aNextPoint) == Cst.fromLeft) 
                {
                    if (!aBlOnlyFillColorArr) 
                    {
                        aGeoArr[lpCounter] = new GeoPoint(aActPoint.X*aMultiX + aDisplaceX, (aActPoint.Y+1)*aMultiY + aDisplaceY);
                        aGeoArr[lpCounter+1] = new GeoPoint(aActPoint.X*aMultiX + aDisplaceX, aActPoint.Y*aMultiY + aDisplaceY);
                        lpCounter = lpCounter + 2;
                    };
                    colorPx.borderSN = true;
                    colorPx.borderWE = true;
                }
                else if (Direction(aActPoint, aNextPoint) == Cst.fromRight) 
                {
                    //nic
                }
                else if (Direction(aActPoint, aNextPoint) == Cst.fromBottom) 
                {
                    if (!aBlOnlyFillColorArr) 
                    {
                        aGeoArr[lpCounter] = new GeoPoint(aActPoint.X*aMultiX + aDisplaceX, (aActPoint.Y+1)*aMultiY + aDisplaceY);
                        lpCounter = lpCounter + 1;
                    };
                    colorPx.borderSN = true;
                }
                else
                {
                    if (!aBlOnlyFillColorArr) 
                    {
                        aGeoArr[lpCounter] = new GeoPoint(aActPoint.X*aMultiX + aDisplaceX, (aActPoint.Y+1)*aMultiY + aDisplaceY);
                        aGeoArr[lpCounter+1] = new GeoPoint(aActPoint.X*aMultiX + aDisplaceX, aActPoint.Y*aMultiY + aDisplaceY);
                        aGeoArr[lpCounter+2] = new GeoPoint((aActPoint.X+1)*aMultiX + aDisplaceX, aActPoint.Y*aMultiY + aDisplaceY);
                        lpCounter = lpCounter + 3;
                    };
                    colorPx.borderSN = true;
                    colorPx.borderWE = true;
                    colorPx.borderNS = true;
            };
        }

//-------------------
        }

        //tworzy krawędzie dla pojedynczego punktu
        private void MakePartEdgeForOnePoint( Point aPoint, List<GeoPoint> aGeoArr,
                                            float aDpMultiX, float aDPMultiY, float aDpDisplaceX, float aDpDisplaceY,
                                            ColorPx[,] aColorArr,
                                            bool aBlOnlyFillColorArr)
        {
            if (!aBlOnlyFillColorArr)
            {
                aGeoArr[0] = new GeoPoint((aPoint.X)*aDpMultiX + aDpDisplaceX, (aPoint.Y)*aDPMultiY + aDpDisplaceY);
                aGeoArr[1] = new GeoPoint((aPoint.X+1)*aDpMultiX + aDpDisplaceX, aPoint.Y*aDPMultiY + aDpDisplaceY);
                aGeoArr[2] = new GeoPoint((aPoint.X+1)*aDpMultiX + aDpDisplaceX, (aPoint.Y+1)*aDPMultiY + aDpDisplaceY);
                aGeoArr[3] = new GeoPoint((aPoint.X)*aDpMultiX + aDpDisplaceX, (aPoint.Y+1)*aDPMultiY + aDpDisplaceY);
            }
            ColorPx colorPx = aColorArr[aPoint.X, aPoint.Y];
            colorPx.borderNS = true;
            colorPx.borderSN = true;
            colorPx.borderEW = true;
            colorPx.borderWE = true;            
        }

        private void GetLine(Point p1, Point p2, ref float A, ref float C, ref float mian)
        {
            var GetLineA = new Func<float>
            (
                () => (p2.Y - p1.Y) / (p2.X - p1.X)
            );
            var GetLineC = new Func<float, float>
            (
                (innerA) => p1.Y - innerA*p1.X
            );
            var GetMianownik = new Func<float, float>
            (
                (innerA) => (float)Math.Sqrt(Math.Pow((double)innerA,(double)2) + 1)
            );
            
            A = GetLineA();
            C = GetLineC(A);
            mian = GetMianownik(A);            
        }

        private int SrcHeight()
        {
            return parentMapFactory.srcHeight;
        }

        private int  SrcWidth()
        {
            return parentMapFactory.srcWidth;
        }

        private Vector_Gen[,] GetVectorArr()
        {
            return parentMapFactory.vectArr;
        }

        private ColorPx[,] GetColorArr()
        {
            return parentMapFactory.colorArr;
        }

        private GeoPoint PxPointToGeoPoint(Vector_Rectangle aPxPoint)
        {
            //P1 i P2 będą takie same, bo aPxPoint reprezentuje pojedynczy pixel, więc wartośći x i y możemy wziąć z p1
            return new GeoPoint(aPxPoint.p1.X, aPxPoint.p1.Y);
        }
      
        //public VectoredRectangleGroup();

        //tworzy tablicę punktów z ponktów zawartych w edgePxList
        public List<GeoPoint> MakeVectorEdge(VectorRectangeGroup aEdgePxList,
                                             ColorPx[,] aColorArr,
                                             bool aBlOnlyFillColorArr,
                                             float aMultiX = 0, float aMultiY = 0,
                                             float aDisplaceX = 0, float aDisplaceY = 0)
        {
            List<GeoPoint> result = new List<GeoPoint>(0);
            int counter = 0;
            //if (!aBlOnlyFillColorArr)
            //    SetLength(result, aEdgePxList.Count*3);
            if (aEdgePxList.Count > 1)
            {
                //SetLength(result, self.rectList.Count+30);
                Point o1 = aEdgePxList[aEdgePxList.Count-1].GetP(0);
                Point o2 = aEdgePxList[0].GetP(0);
                Point o3 = aEdgePxList[1].GetP(0);
                MakePartEdge(o1, o2, o3, ref counter, result, aMultiX, aMultiY, aDisplaceX, aDisplaceY, aColorArr, aBlOnlyFillColorArr);

                for (int i=1; i<aEdgePxList.Count-1; i++)
                {
                    o1 = aEdgePxList[i-1].GetP(0);
                    o2 = aEdgePxList[i].GetP(0);
                    o3 = aEdgePxList[i+1].GetP(0);
                    MakePartEdge(o1, o2, o3, ref counter, result, aMultiX, aMultiY, aDisplaceX, aDisplaceY, aColorArr, aBlOnlyFillColorArr);
                };

                o1 = aEdgePxList[aEdgePxList.Count-2].GetP(0);
                o2 = aEdgePxList[aEdgePxList.Count-1].GetP(0);
                o3 = aEdgePxList[0].GetP(0);
                MakePartEdge(o1, o2, o3, ref counter, result, aMultiX, aMultiY, aDisplaceX, aDisplaceY, aColorArr, aBlOnlyFillColorArr);
                //SetLength(result, Min(counter,10));
                //if (!aBlOnlyFillColorArr) then
                //   SetLength(result, counter);
            } 
            else
            {
                //if (!aBlOnlyFillColorArr) 
                //    SetLength(result, 4);
                MakePartEdgeForOnePoint(aEdgePxList[0].GetP(0),
                                        result, aMultiX, aMultiY, aDisplaceX, aDisplaceY,
                                        aColorArr, aBlOnlyFillColorArr);
                counter = 4;
            }
            return result;
        }

        public List<GeoPoint> SimplifyVectorEdge(List<GeoPoint> aArr)
        {
            //to do
            return new List<GeoPoint>();
        }

        public Point[] GeoArray2PxArray(GeoPoint[] aGeoArr)
        {
            Point[] result = new Point[aGeoArr.Length];
            for (int i=0; i<aGeoArr.Length; i++)
                result[i] = aGeoArr[i].ToPoint();
            return result;
        }

        //buduje krawędź
        public void MakeEdges( VectorRectangeGroup aEdgePxList, bool aBlInnerBorder = false, int aOuterGroup = 0)
        {
            Func<int, int> NextDirection = delegate(int aDirection)
            {
                long dummy;
                aDirection++;
                return (int)Math.DivRem((long)aDirection, (long)4, out dummy);
            };//<<NextDirection

            Func<Vector_Rectangle, bool> CheckBottomPX = delegate(Vector_Rectangle aStartEdgePoint)
            {
                bool result = false;
                if (aStartEdgePoint.p1.Y < SrcHeight() - 1)
                {
                    Vector_Gen bottomVectorRectangle = GetVectorArr()[aStartEdgePoint.p1.X, aStartEdgePoint.p1.Y + 1];
                    result = (bottomVectorRectangle != null) & (bottomVectorRectangle.parentVectorGroupId == aStartEdgePoint.parentVectorGroupId);
                }
                return result;
            };//<<CheckBottomPX

            Func<Vector_Rectangle, int, Vector_Rectangle> GetNextEdge = delegate(Vector_Rectangle aPrevEdge, ref int aArrDir)
            {
                Func<Vector_Rectangle, int, Vector_Rectangle> CheckNextEdge = delegate(Vector_Rectangle aPrevEdge2, int aArrDir)
                {
                    Func<Vector_Rectangle, Vector_Rectangle> CheckTop = delegate(Vector_Rectangle aPrevEdge3)
                    {
                        if (aPrevEdge3.p1.Y > 0)
                        {
                            Vector_Rectangle Result3 = GetVectObjArr[aPrevEdge3.p1.X, aPrevEdge3.p1.Y-1];
                            if ((!aBlInnerBorder & (aPrevEdge3.parentVectorGroupId == Result3.parentVectorGroupId)) |
                                (aBlInnerBorder & ( Result3.parentVectorGroupId != aOuterGroup))) 
                                return Result3;
                        }
                        return null;         
                    };//<<CheckTop

                    Func<Vector_Rectangle, Vector_Rectangle> CheckRight = delegate(Vector_Rectangle aPrevEdge3)
                    {
                        if (aPrevEdge3.p1.X < srcWidth-1) 
                        {
                            Vector_Rectangle Result3 = VectObjArr[aPrevEdge3.p1.X+1, aPrevEdge3.p1.Y];
                            if ((!aBlInnerBorder & (aPrevEdge3.parentVectorGroupId == Result3.parentVectorGroupId)) |
                                (aBlInnerBorder & (Result3.parentVectorGroupId != aOuterGroup)))
                                return Result3;
                        }
                        return null;
                    };//<<CheckRight

                    Func<Vector_Rectangle, Vector_Rectangle> CheckBottom = delegate(Vector_Rectangle aPrevEdge3)
                    {
                        if (aPrevEdge3.p1.Y < srcHeight-1)
                        {
                            Vector_Rectangle Result3 = VectObjArr[aPrevEdge3.p1.X, aPrevEdge3.p1.Y+1];
                            if ((!aBlInnerBorder & (aPrevEdge3.parentVectorGroupId == Result3.parentVectorGroupId)) |
                                (aBlInnerBorder & (Result3.parentVectorGroupId != aOuterGroup)))
                                return Result3;
                        }
                        return null;
                    };//<<CheckBottom

                    Func<Vector_Rectangle, Vector_Rectangle> CheckLeft = delegate(Vector_Rectangle aPrevEdge3)
                    {
                        if (aPrevEdge3.p1.X > 0)
                        {
                            Vector_Rectangle Result3 = VectObjArr[aPrevEdge3.p1.X-1, aPrevEdge3.p1.Y];
                            if ((!aBlInnerBorder & (aPrevEdge3.parentVectorGroupId == Result3.parentVectorGroupId)) |
                                (aBlInnerBorder & (Result3.parentVectorGroupId != aOuterGroup)))
                                return Result3;
                        }
                        return null;
                    };//<<CheckLeft

                    Vector_Rectangle Result2;
                    if (aArrDir == Cst.goTop)
                        Result2 = CheckTop(aPrevEdge2);
                    else if (aArrDir == Cst.goRight)
                        Result2 = CheckRight(aPrevEdge2);
                    else if (aArrDir == Cst.goBottom)
                        Result2 = CheckBottom(aPrevEdge2);
                    else if (aArrDir == Cst.goLeft)
                        Result2 = CheckLeft(aPrevEdge2);
                    else
                    {
                        Result2 = null;
                        //Assert(False, 'checkNextEdge');
                    };
                };//<<CheckNextEdge

                Vector_Rectangle Result;
                int j = 0;
                for (int i=0; i<4; i++)
                {
                    if (aArrDir + j == 4) 
                    {
                        j = -aArrDir;
                        Result = CheckNextEdge(aPrevEdge, aArrDir+j);
                        if (Result != null)
                        {
                            if (aArrDir + j == Cst.fromLeft)
                                aArrDir = Cst.fromBottom;
                            else if (aArrDir + j == Cst.fromBottom)
                                aArrDir = Cst.fromRight;
                            else if (aArrDir + j == Cst.fromTop)
                                aArrDir = Cst.fromLeft;
                            else if (aArrDir + j == Cst.fromRight)
                                aArrDir = Cst.fromTop;
                            break;
                        };
                        j++;
                    };
                    if ((Result != null) &
                        //przeszliśmy z prawa na lewo
                        (aArrDir == Cst.fromRight)) 
                        GetColorArr[Result.p1.x, Result.p1.y]).borderEW = true;
                };
            };//<<GetNextEdge

            Action MakeUsed = delegate()
            {
            if (aBlInnerBorder) 
                for (int i=0; i<aEdgePxList.Count; i++) 
                {
                    Vector_Rectangle point = aEdgePxList[i];
                    GetColorArr[point.p1.X, point.p1.Y-1].used = true;
                };
            };//<<MakeUsed

            //var
            //startEdgePoint, nextEdgePoint, prevEdgePoint: TVectRectangle;
            //arrivDir: integer;
            //dummyArr: TDynamicGeoPointArray;

            
            aEdgePxList.Clear;
            //startEdgePoint to pierwszy punkt na liście, bo idziemy ol lewej strony
            //w najwyższym wierszu
            Vector_Rectangle startEdgePoint = rectList.Objects[0];
            Vector_Rectangle prevEdgePoint = startEdgePoint;
            int arrivDir = Cst.goRight; //zaczynamy od max lewego ponktu na górnej linji
            //Każemy zacząć szukanie od prawej
            Vector_Rectangle nextEdgePoint = null;
            //1-pixelowy obiekt traktujemy inaczej
            if (rectList.count != 1)
            //kończymy jeśli trafiamy na początek, lub na 1-pixelowy obiekt
            //while (nextEdgePoint != startEdgePoint) & (prevEdgePoint != nil) do
            while (true) do
            {
                if (nextEdgePoint == startEdgePoint)
                {
                    if (CheckBottomPX(startEdgePoint) & (arrivDir == Cst.fromRight))
                    {
                        arrivDir = Cst.goBottom;
                    };
                    else
                    {
                        //arrivDir = -1;
                        break;
                    };
                };
                //try
                nextEdgePoint = GetNextEdge(prevEdgePoint, arrivDir);
                //powstanie gdy nie możemy oddać następnej krawędzi, ale wyjątkikem jest gdy jest to pojedynczy pixel
                if ((nextEdgePoint == nil) & (aEdgePxList.Count != 0))
                    Assert(false, 'Oddany edge jest nil (' + IntToStr(prevEdgePoint.p1.X) +
                           ',' + IntToStr(prevEdgePoint.p1.Y) + '), liczba znalezionych kreawędzi:' +
                           aEdgePxList.Count.ToString);

                aEdgePxList.AddObject(aEdgePxList.NextKey, nextEdgePoint);
                //MakeUsed(nextEdgePoint);
                prevEdgePoint = nextEdgePoint;

                //except
                //  on E: Exception do
                //    raise;
                //end;

            };
            //dla obiektu 1-pixelowego
            else
            {
                //dodajemy punkty graniczne do listy
                aEdgePxList.AddObject(aEdgePxList.NextKey, startEdgePoint);
                // MakeUsed(startEdgePoint);
            };
            List<GeoPoint> dummyArr = MakeVectorEdge(aEdgePxList, GetColorArr, true);
            MakeUsed();
            SetLength(dummyArr, 0);
            //PxListToGeoList;
            
        }


            
    }
}
