using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace Migracja
{
    //DynamicGeoPointArray = List<GeoPoint>;
    //TDynamicPointArray = Vector_Gen[];
    //TDynamicPxColorPointArray = ColorPx[,];

    //Grupa rectangli tworzacych jedną płąszczyznę. Pozwala obliczyć swoją granicę
    partial class VectoredRectangleGroup : Dictionary<int, Vector_Rectangle>
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
        public int lpGroup{get;set;}
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
                                    float aMultiX, float aMultiY, float aDisplaceX, float aDisplaceY,
                                    ColorPx[][] aColorArr,
                                    bool aBlOnlyFillColorArr)
        {
            ColorPx colorPx = aColorArr[aActPoint.X][aActPoint.Y] as ColorPx;
            
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
                                            ColorPx[][] aColorArr,
                                            bool aBlOnlyFillColorArr)
        {
            if (!aBlOnlyFillColorArr)
            {
                aGeoArr[0] = new GeoPoint((aPoint.X)*aDpMultiX + aDpDisplaceX, (aPoint.Y)*aDPMultiY + aDpDisplaceY);
                aGeoArr[1] = new GeoPoint((aPoint.X+1)*aDpMultiX + aDpDisplaceX, aPoint.Y*aDPMultiY + aDpDisplaceY);
                aGeoArr[2] = new GeoPoint((aPoint.X+1)*aDpMultiX + aDpDisplaceX, (aPoint.Y+1)*aDPMultiY + aDpDisplaceY);
                aGeoArr[3] = new GeoPoint((aPoint.X)*aDpMultiX + aDpDisplaceX, (aPoint.Y+1)*aDPMultiY + aDpDisplaceY);
            }
            ColorPx colorPx = aColorArr[aPoint.X][aPoint.Y];
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

        private Vector_Rectangle[][] GetVectorArr()
        {
            return parentMapFactory.vectArr;
        }

        private ColorPx[][] GetColorArr()
        {
            return parentMapFactory.colorArr;
        }

        private GeoPoint PxPointToGeoPoint(Vector_Rectangle aPxPoint)
        {
            //P1 i P2 będą takie same, bo aPxPoint reprezentuje pojedynczy pixel, więc wartośći x i y możemy wziąć z p1
            return new GeoPoint(aPxPoint.p1.X, aPxPoint.p1.Y);
        }

        public VectoredRectangleGroup()
        {
            edgeList = new VectorRectangeGroup();
            innerEdgesList = new Dictionary<int, VectorRectangeGroup>();
        }

        //tworzy tablicę punktów z ponktów zawartych w edgePxList
        public List<GeoPoint> MakeVectorEdge(VectorRectangeGroup aEdgePxList,
                                             ColorPx[][] aColorArr,
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

                    
    }
}
