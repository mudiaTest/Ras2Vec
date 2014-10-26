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

    class  EdgeList: Dictionary<int, VectorRectangeGroup>
    {
        private int maxKey;
        internal int NextKey()
        {
            return maxKey++;
        }
        public EdgeList()
        {
            maxKey = 0;
        }
    }

    //Grupa rectangli tworzacych jedną płąszczyznę. Pozwala obliczyć swoją granicę
    partial class VectoredRectangleGroup : Dictionary<int, Vector_Rectangle>
    {
        //cztery punkty geograficzne określanące rogi obrazka
        //leftTopGeo, rightTopGeo, leftBottomGeo, rightBottomGeo: Double;

        //lista obiektów Vector_Rectangle tworzących krawędź (self, czyli grupy obiektów Vector_Rectangle). 
        //Kolejnośc wyznaczają klucze
        public VectorRectangeGroup edgeList{get;set;}
        //lista wewnętrznych krawędzi. Każda z nich ma konstrukcję jak edgePxList
        public EdgeList innerEdgesList { get; set; }
        //lista krawędzi punktów Double
        public Dictionary<int, GeoEdgePoint> edgeGeoList { get; set; }
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

        internal PointAdv[] pointAdvMapFromFullEdge = null;
        internal PointAdv[] pointAdvMapFromSimplifiedEdge = null;

        Point[] GetEdgeListAsArray(float aScale)
        {
            Debug.Assert(aScale >= 1, "GetEdgeListAsArray nie może utworzyć polygonu dla skali <= 1.");

            Point[] result = new Point[edgeList.Count];
            int i = 0;
            foreach(int key in edgeList.GetSortedKeyList())
            {
                result[i] = edgeList[key].p1;
                i++;
            }
           // jkl;
            //
            return result;
        }

        //in: dwa KOLEJNE punkty poruszające się po liniach Hor i Ver
        //out: Cst.fromLeft, Cst.fromTop, Cst.fromRight, Cst.fromBottom
        private int Direction(Point p1, Point p2)
        {
            if (p1.X > p2.X)
                return Cst.fromRight;
            else if (p1.X < p2.X)
                return Cst.fromLeft;//►
            else if (p1.Y > p2.Y)
                return Cst.fromBottom;//▼
            else
                return Cst.fromTop;//▲
        }

        //tworzy krawędź z 3 kolejnych punktów
        //  multi = mnożnik: dla grafiki bedzie to zoom, dla geo będzie to szerokośc
        //          geograficzna jednego px
        private void MakePartEdge(  Point aPrvPoint, Point aActPoint, Point aNextPoint,
                                    ref int lpCounter, List<GeoEdgePoint> aGeoArr,
                                    float aMultiX, float aMultiY, float aDisplaceX, float aDisplaceY,
                                    ColorPx[][] aColorArr,
                                    PointAdv[][] aPointAdvArr,
                                    bool aBlOnlyFillColorArr,
                                    bool aIsFirstEdgeVectRect = false)
        {
            ColorPx colorPx = GetColorPx(aColorArr, aActPoint, null) as ColorPx;
            
//-------------------

            //►
            if (Direction(aPrvPoint, aActPoint) == Cst.fromLeft) 
            {
                //►▲
                if (Direction(aActPoint, aNextPoint) == Cst.fromBottom) 
                {
                    //nic
                }
                //►▼
                else if (Direction(aActPoint, aNextPoint) == Cst.fromTop) 
                {
                    if (!aBlOnlyFillColorArr) 
                    {
                        NewBorderGeoPoint(aGeoArr, aActPoint, aMultiX, aMultiY, aDisplaceX, aDisplaceY, null, aPointAdvArr, aColorArr, false, Cst.NW, Cst.N);          
                        //aGeoArr.Add(new GeoEdgePoint(aActPoint.X * aMultiX + aDisplaceX, aActPoint.Y * aMultiY + aDisplaceY, 
                        //                             GetPxType(GetColorPx(aColorArr, aActPoint.X - 1, aActPoint.Y - 1), GetColorPx(aColorArr, aActPoint.X, aActPoint.Y - 1))));

                        NewBorderGeoPoint(aGeoArr, aActPoint, aMultiX, aMultiY, aDisplaceX, aDisplaceY, Cst.E, aPointAdvArr, aColorArr, false, Cst.N, Cst.NE, Cst.E);
                        //aGeoArr.Add(new GeoEdgePoint((aActPoint.X + 1) * aMultiX + aDisplaceX, aActPoint.Y * aMultiY + aDisplaceY,
                        //                             GetPxType(GetColorPx(aColorArr, aActPoint.X, aActPoint.Y - 1), GetColorPx(aColorArr, aActPoint.X + 1, aActPoint.Y - 1),
                        //                                       GetColorPx(aColorArr, aActPoint.X + 1, aActPoint.Y - 1), GetColorPx(aColorArr, aActPoint.X + 1, aActPoint.Y))));
                        lpCounter = lpCounter + 2;
                    };
                    colorPx.borderWE = true;
                    colorPx.borderNS = true;
                }
                //►►
                else if (Direction(aActPoint, aNextPoint) == Cst.fromLeft) 
                {
                    if (!aBlOnlyFillColorArr) 
                    {
                        NewBorderGeoPoint(aGeoArr, aActPoint, aMultiX, aMultiY, aDisplaceX, aDisplaceY, null, aPointAdvArr, aColorArr, false, Cst.NW, Cst.N);  
                        //aGeoArr.Add(new GeoEdgePoint((aActPoint.X) * aMultiX + aDisplaceX, (aActPoint.Y) * aMultiY + aDisplaceY,
                        //                             GetPxType(GetColorPx(aColorArr, aActPoint.X - 1, aActPoint.Y - 1), GetColorPx(aColorArr, aActPoint.X, aActPoint.Y - 1))));
                        lpCounter = lpCounter + 1;
                    };
                    colorPx.borderWE = true;
                }
                //►◄
                else
                {
                    if (!aBlOnlyFillColorArr) 
                    {
                        NewBorderGeoPoint(aGeoArr, aActPoint, aMultiX, aMultiY, aDisplaceX, aDisplaceY, null, aPointAdvArr, aColorArr, false, Cst.NW, Cst.N);  
                        //aGeoArr.Add(new GeoEdgePoint((aActPoint.X) * aMultiX + aDisplaceX, (aActPoint.Y) * aMultiY + aDisplaceY,
                        //                             GetPxType(GetColorPx(aColorArr, aActPoint.X - 1, aActPoint.Y - 1), GetColorPx(aColorArr, aActPoint.X, aActPoint.Y - 1))));
                        NewBorderGeoPoint(aGeoArr, aActPoint, aMultiX, aMultiY, aDisplaceX, aDisplaceY, Cst.E, aPointAdvArr, aColorArr, false, Cst.N, Cst.NE, Cst.E);
                        //aGeoArr.Add(new GeoEdgePoint((aActPoint.X + 1) * aMultiX + aDisplaceX, aActPoint.Y * aMultiY + aDisplaceY,
                        //                             GetPxType(GetColorPx(aColorArr, aActPoint.X, aActPoint.Y - 1), GetColorPx(aColorArr, aActPoint.X + 1, aActPoint.Y - 1),
                        //                                       GetColorPx(aColorArr, aActPoint.X + 1, aActPoint.Y - 1), GetColorPx(aColorArr, aActPoint.X + 1, aActPoint.Y))));
                        NewBorderGeoPoint(aGeoArr, aActPoint, aMultiX, aMultiY, aDisplaceX, aDisplaceY, Cst.SE, aPointAdvArr, aColorArr, false, Cst.E, Cst.SE, Cst.S);
                        //aGeoArr.Add(new GeoEdgePoint((aActPoint.X + 1) * aMultiX + aDisplaceX, (aActPoint.Y + 1) * aMultiY + aDisplaceY,
                        //                             GetPxType(GetColorPx(aColorArr, aActPoint.X + 1, aActPoint.Y), GetColorPx(aColorArr, aActPoint.X + 1, aActPoint.Y - 1),
                        //                                       GetColorPx(aColorArr, aActPoint.X + 1, aActPoint.Y - 1), GetColorPx(aColorArr, aActPoint.X, aActPoint.Y - 1))));
                        lpCounter = lpCounter + 3;
                    };
                    colorPx.borderWE = true;
                    colorPx.borderNS = true;
                    colorPx.borderEW = true;
                };
            }

            //◄
            else if (Direction(aPrvPoint, aActPoint) == Cst.fromRight) 
            {
                //◄▲
                if (Direction(aActPoint, aNextPoint) == Cst.fromBottom) 
                {
                    if (!aBlOnlyFillColorArr) 
                    {
                        NewBorderGeoPoint(aGeoArr, aActPoint, aMultiX, aMultiY, aDisplaceX, aDisplaceY, Cst.SE, aPointAdvArr, aColorArr, false, Cst.SE, Cst.S);  
                        //aGeoArr.Add(new GeoEdgePoint((aActPoint.X + 1) * aMultiX + aDisplaceX, (aActPoint.Y + 1) * aMultiY + aDisplaceY,
                        //                             GetPxType(GetColorPx(aColorArr, aActPoint.X + 1, aActPoint.Y + 1), GetColorPx(aColorArr, aActPoint.X, aActPoint.Y + 1))));
                        NewBorderGeoPoint(aGeoArr, aActPoint, aMultiX, aMultiY, aDisplaceX, aDisplaceY, Cst.S, aPointAdvArr, aColorArr, false, Cst.S, Cst.SW, Cst.W);  
                        //aGeoArr.Add(new GeoEdgePoint(aActPoint.X * aMultiX + aDisplaceX, (aActPoint.Y + 1) * aMultiY + aDisplaceY,
                        //                             GetPxType(GetColorPx(aColorArr, aActPoint.X, aActPoint.Y + 1), GetColorPx(aColorArr, aActPoint.X - 1, aActPoint.Y + 1),
                        //                                       GetColorPx(aColorArr, aActPoint.X - 1, aActPoint.Y + 1), GetColorPx(aColorArr, aActPoint.X - 1, aActPoint.Y))));
                        lpCounter = lpCounter + 2;
                    };
                    colorPx.borderEW = true;
                    colorPx.borderSN = true;
                }
                //◄▼
                else if (Direction(aActPoint, aNextPoint) == Cst.fromTop) 
                {
                    //nic
                }
                //◄◄
                else if (Direction(aActPoint, aNextPoint) == Cst.fromRight) 
                {
                    if (!aBlOnlyFillColorArr) 
                    {
                        NewBorderGeoPoint(aGeoArr, aActPoint, aMultiX, aMultiY, aDisplaceX, aDisplaceY, Cst.SE, aPointAdvArr, aColorArr, false, Cst.SE, Cst.S);  
                        //aGeoArr.Add(new GeoEdgePoint((aActPoint.X + 1) * aMultiX + aDisplaceX, (aActPoint.Y + 1) * aMultiY + aDisplaceY,
                        //                             GetPxType(GetColorPx(aColorArr, aActPoint.X + 1, aActPoint.Y + 1), GetColorPx(aColorArr, aActPoint.X, aActPoint.Y + 1))));
                        lpCounter = lpCounter + 1;
                    };
                    colorPx.borderEW = true;
                }
                //◄►
                else
                {
                    if (!aBlOnlyFillColorArr) 
                    {
                        NewBorderGeoPoint(aGeoArr, aActPoint, aMultiX, aMultiY, aDisplaceX, aDisplaceY, Cst.SE, aPointAdvArr, aColorArr, false, Cst.SE, Cst.S);  
                        //aGeoArr.Add(new GeoEdgePoint((aActPoint.X + 1) * aMultiX + aDisplaceX, (aActPoint.Y + 1) * aMultiY + aDisplaceY,
                        //                             GetPxType(GetColorPx(aColorArr, aActPoint.X + 1, aActPoint.Y + 1), GetColorPx(aColorArr, aActPoint.X, aActPoint.Y + 1))));
                        NewBorderGeoPoint(aGeoArr, aActPoint, aMultiX, aMultiY, aDisplaceX, aDisplaceY, Cst.S, aPointAdvArr, aColorArr, false, Cst.S, Cst.SW, Cst.W);  
                        //aGeoArr.Add(new GeoEdgePoint(aActPoint.X * aMultiX + aDisplaceX, (aActPoint.Y + 1) * aMultiY + aDisplaceY,
                        //                             GetPxType(GetColorPx(aColorArr, aActPoint.X, aActPoint.Y + 1), GetColorPx(aColorArr, aActPoint.X - 1, aActPoint.Y + 1),
                        //                                       GetColorPx(aColorArr, aActPoint.X - 1, aActPoint.Y + 1), GetColorPx(aColorArr, aActPoint.X - 1, aActPoint.Y))));
                        NewBorderGeoPoint(aGeoArr, aActPoint, aMultiX, aMultiY, aDisplaceX, aDisplaceY, null, aPointAdvArr, aColorArr, false, Cst.W, Cst.NW, Cst.N);  
                        //aGeoArr.Add(new GeoEdgePoint(aActPoint.X * aMultiX + aDisplaceX, (aActPoint.Y) * aMultiY + aDisplaceY,
                        //                             GetPxType(GetColorPx(aColorArr, aActPoint.X - 1, aActPoint.Y), GetColorPx(aColorArr, aActPoint.X - 1, aActPoint.Y - 1),
                        //                                       GetColorPx(aColorArr, aActPoint.X - 1, aActPoint.Y - 1), GetColorPx(aColorArr, aActPoint.X, aActPoint.Y - 1))));
                        lpCounter = lpCounter + 3;
                    };
                    colorPx.borderEW = true;
                    colorPx.borderNS = true;
                    colorPx.borderWE = true;
                };
            }
            //▼
            else if (Direction(aPrvPoint, aActPoint) == Cst.fromTop) 
            {
                //▼►
                if (Direction(aActPoint, aNextPoint) == Cst.fromLeft) 
                {
                    //nic
                }
                //▼◄
                else if (Direction(aActPoint, aNextPoint) == Cst.fromRight) 
                {
                    if (!aBlOnlyFillColorArr) 
                    {
                        NewBorderGeoPoint(aGeoArr, aActPoint, aMultiX, aMultiY, aDisplaceX, aDisplaceY, Cst.E, aPointAdvArr, aColorArr, false, Cst.NE, Cst.E);  
                        //aGeoArr.Add(new GeoEdgePoint((aActPoint.X + 1) * aMultiX + aDisplaceX, aActPoint.Y * aMultiY + aDisplaceY,
                        //                             GetPxType(GetColorPx(aColorArr, aActPoint.X + 1, aActPoint.Y - 1), GetColorPx(aColorArr, aActPoint.X + 1, aActPoint.Y))));
                        NewBorderGeoPoint(aGeoArr, aActPoint, aMultiX, aMultiY, aDisplaceX, aDisplaceY, Cst.SE, aPointAdvArr, aColorArr, false, Cst.E, Cst.SE, Cst.S);  
                        //aGeoArr.Add(new GeoEdgePoint((aActPoint.X + 1) * aMultiX + aDisplaceX, (aActPoint.Y + 1) * aMultiY + aDisplaceY,
                        //                             GetPxType(GetColorPx(aColorArr, aActPoint.X + 1, aActPoint.Y), GetColorPx(aColorArr, aActPoint.X + 1, aActPoint.Y + 1),
                        //                                       GetColorPx(aColorArr, aActPoint.X + 1, aActPoint.Y + 1), GetColorPx(aColorArr, aActPoint.X, aActPoint.Y + 1))));
                        lpCounter = lpCounter + 2;
                    };
                    colorPx.borderNS = true;
                    colorPx.borderEW = true;
                }
                //▼▼
                else if (Direction(aActPoint, aNextPoint) == Cst.fromTop) 
                {
                    if (!aBlOnlyFillColorArr) 
                    {
                        NewBorderGeoPoint(aGeoArr, aActPoint, aMultiX, aMultiY, aDisplaceX, aDisplaceY, Cst.E, aPointAdvArr, aColorArr, false, Cst.NE, Cst.E);  
                        //aGeoArr.Add(new GeoEdgePoint((aActPoint.X + 1) * aMultiX + aDisplaceX, aActPoint.Y * aMultiY + aDisplaceY,
                        //                             GetPxType(GetColorPx(aColorArr, aActPoint.X + 1, aActPoint.Y - 1), GetColorPx(aColorArr, aActPoint.X + 1, aActPoint.Y))));
                        lpCounter = lpCounter + 1;
                    };
                    colorPx.borderNS = true;
                }
                //▼▲
                else
                {
                    if (!aBlOnlyFillColorArr) 
                    {
                        NewBorderGeoPoint(aGeoArr, aActPoint, aMultiX, aMultiY, aDisplaceX, aDisplaceY, Cst.E, aPointAdvArr, aColorArr, false, Cst.NE, Cst.E);  
                        //aGeoArr.Add(new GeoEdgePoint((aActPoint.X + 1) * aMultiX + aDisplaceX, aActPoint.Y * aMultiY + aDisplaceY,
                        //                             GetPxType(GetColorPx(aColorArr, aActPoint.X + 1, aActPoint.Y - 1), GetColorPx(aColorArr, aActPoint.X + 1, aActPoint.Y))));
                        NewBorderGeoPoint(aGeoArr, aActPoint, aMultiX, aMultiY, aDisplaceX, aDisplaceY, Cst.SE, aPointAdvArr, aColorArr, false, Cst.E, Cst.SE, Cst.S);  
                        //aGeoArr.Add(new GeoEdgePoint((aActPoint.X + 1) * aMultiX + aDisplaceX, (aActPoint.Y + 1) * aMultiY + aDisplaceY,
                        //                             GetPxType(GetColorPx(aColorArr, aActPoint.X + 1, aActPoint.Y), GetColorPx(aColorArr, aActPoint.X + 1, aActPoint.Y + 1),
                        //                                       GetColorPx(aColorArr, aActPoint.X + 1, aActPoint.Y + 1), GetColorPx(aColorArr, aActPoint.X, aActPoint.Y + 1))));
                        NewBorderGeoPoint(aGeoArr, aActPoint, aMultiX, aMultiY, aDisplaceX, aDisplaceY, Cst.S, aPointAdvArr, aColorArr, false, Cst.S, Cst.SW, Cst.W);  
                        //aGeoArr.Add(new GeoEdgePoint(aActPoint.X * aMultiX + aDisplaceX, (aActPoint.Y + 1) * aMultiY + aDisplaceY,
                        //                             GetPxType(GetColorPx(aColorArr, aActPoint.X, aActPoint.Y + 1), GetColorPx(aColorArr, aActPoint.X - 1, aActPoint.Y + 1),
                        //                                       GetColorPx(aColorArr, aActPoint.X - 1, aActPoint.Y + 1), GetColorPx(aColorArr, aActPoint.X - 1, aActPoint.Y))));
                        lpCounter = lpCounter + 3;
                    };
                    colorPx.borderNS = true;
                    colorPx.borderEW = true;
                    colorPx.borderSN = true;
                };
            }

            //▲
            else //from bottom
            {
                //▲►
                if (Direction(aActPoint, aNextPoint) == Cst.fromLeft) 
                {
                    if (!aBlOnlyFillColorArr) 
                    {
                        NewBorderGeoPoint(aGeoArr, aActPoint, aMultiX, aMultiY, aDisplaceX, aDisplaceY, Cst.S, aPointAdvArr, aColorArr, false, Cst.SW, Cst.W);  
                        //aGeoArr.Add(new GeoEdgePoint(aActPoint.X * aMultiX + aDisplaceX, (aActPoint.Y + 1) * aMultiY + aDisplaceY,
                        //                             GetPxType(GetColorPx(aColorArr, aActPoint.X - 1, aActPoint.Y + 1), GetColorPx(aColorArr, aActPoint.X - 1, aActPoint.Y))));
                        NewBorderGeoPoint(aGeoArr, aActPoint, aMultiX, aMultiY, aDisplaceX, aDisplaceY, null, aPointAdvArr, aColorArr, aIsFirstEdgeVectRect, Cst.W, Cst.NW, Cst.N);  
                        //aGeoArr.Add(new GeoEdgePoint(aActPoint.X * aMultiX + aDisplaceX, aActPoint.Y * aMultiY + aDisplaceY,
                        //                             GetPxType(GetColorPx(aColorArr, aActPoint.X - 1, aActPoint.Y), GetColorPx(aColorArr, aActPoint.X - 1, aActPoint.Y - 1),
                        //                                       GetColorPx(aColorArr, aActPoint.X - 1, aActPoint.Y - 1), GetColorPx(aColorArr, aActPoint.X, aActPoint.Y - 1))));
                        lpCounter = lpCounter + 2;
                    };
                    colorPx.borderSN = true;
                    colorPx.borderWE = true;
                }
                //▲◄
                else if (Direction(aActPoint, aNextPoint) == Cst.fromRight) 
                {
                    //nic
                }
                //▲▲
                else if (Direction(aActPoint, aNextPoint) == Cst.fromBottom) 
                {
                    if (!aBlOnlyFillColorArr) 
                    {
                        NewBorderGeoPoint(aGeoArr, aActPoint, aMultiX, aMultiY, aDisplaceX, aDisplaceY, Cst.S, aPointAdvArr, aColorArr, false, Cst.SW, Cst.W);  
                        //aGeoArr.Add(new GeoEdgePoint(aActPoint.X * aMultiX + aDisplaceX, (aActPoint.Y + 1) * aMultiY + aDisplaceY,
                        //                             GetPxType(GetColorPx(aColorArr, aActPoint.X - 1, aActPoint.Y + 1), GetColorPx(aColorArr, aActPoint.X - 1, aActPoint.Y))));
                        lpCounter = lpCounter + 1;
                    };
                    colorPx.borderSN = true;
                }
                //▲▼
                else
                {
                    if (!aBlOnlyFillColorArr) 
                    {
                        NewBorderGeoPoint(aGeoArr, aActPoint, aMultiX, aMultiY, aDisplaceX, aDisplaceY, Cst.S, aPointAdvArr, aColorArr, false, Cst.SW, Cst.W);  
                        //aGeoArr.Add(new GeoEdgePoint(aActPoint.X * aMultiX + aDisplaceX, (aActPoint.Y + 1) * aMultiY + aDisplaceY,
                        //                             GetPxType(GetColorPx(aColorArr, aActPoint.X - 1, aActPoint.Y + 1), GetColorPx(aColorArr, aActPoint.X - 1, aActPoint.Y))));
                        NewBorderGeoPoint(aGeoArr, aActPoint, aMultiX, aMultiY, aDisplaceX, aDisplaceY, null, aPointAdvArr, aColorArr, aIsFirstEdgeVectRect, Cst.W, Cst.NW, Cst.N);  
                        //aGeoArr.Add(new GeoEdgePoint(aActPoint.X * aMultiX + aDisplaceX, aActPoint.Y * aMultiY + aDisplaceY,
                        //                             GetPxType(GetColorPx(aColorArr, aActPoint.X - 1, aActPoint.Y), GetColorPx(aColorArr, aActPoint.X - 1, aActPoint.Y - 1),
                        //                                       GetColorPx(aColorArr, aActPoint.X - 1, aActPoint.Y - 1), GetColorPx(aColorArr, aActPoint.X, aActPoint.Y - 1))));
                        NewBorderGeoPoint(aGeoArr, aActPoint, aMultiX, aMultiY, aDisplaceX, aDisplaceY, Cst.E, aPointAdvArr, aColorArr, false, Cst.N, Cst.NE, Cst.E);  
                        //aGeoArr.Add(new GeoEdgePoint((aActPoint.X + 1) * aMultiX + aDisplaceX, aActPoint.Y * aMultiY + aDisplaceY,
                        //                             GetPxType(GetColorPx(aColorArr, aActPoint.X, aActPoint.Y - 1), GetColorPx(aColorArr, aActPoint.X + 1, aActPoint.Y - 1),
                        //                                       GetColorPx(aColorArr, aActPoint.X + 1, aActPoint.Y - 1), GetColorPx(aColorArr, aActPoint.X + 1, aActPoint.Y))));
                        lpCounter = lpCounter + 3;
                    };
                    colorPx.borderSN = true;
                    colorPx.borderWE = true;
                    colorPx.borderNS = true;
            };
        }

//-------------------
        }

            private void NewBorderGeoPoint(List<GeoEdgePoint> aGeoArr, //Lista GeoPx tworzących krawędź
                                           Point aPoint, //Aktualnie opracowywany vectRect będacy cześcią krawędzi 
                                           float aMultiX, //mnożnik dla GeoPx
                                           float aMultiY, //mnożnik dla GeoPx
                                           float aDisplaceX, //przesunięcie dla GeoPx
                                           float aDisplaceY, //przesunięcie dla GeoPx 
                                           int? aDisplaceForGeoArr, //pozycja punktu dodawanego do aGeoArr względem aPoint. Null oznaza aPoint
                                           PointAdv[][] aPointAdvArr, //mapa dla PointAdv
                                           ColorPx[][] aColorArr, //mapa dla ColorPx
                                           bool aIsFirstEdgeVectRect, //Czy jest pierwszym punktem krawędzi
                                           //Sprawdzamy na możliwośc zmiany grupy w okolicy dodawanego punktu pary punktów 1,2 i 2,3( o ile 3 != null) 
                                           int pxToTypeCheck1, //1 punkt zprawdzamy na możliwośc zmiany grupy w okolicy dodawanego punktu
                                           int pxToTypeCheck2, //2 punkt zprawdzamy na możliwośc zmiany grupy w okolicy dodawanego punktu
                                           int? pxToTypeCheck3 = null)  //3 punkt zprawdzamy na możliwośc zmiany grupy w okolicy dodawanego punktu
            {
                int X = aPoint.X;
                int Y = aPoint.Y;
                int kdPxType = 0;
                if (aIsFirstEdgeVectRect)
                {
                    kdPxType = Cst.c_geoPxStartEnd;
                }
                else if (pxToTypeCheck3 == null)
                {
                    kdPxType = GetPxType(GetColorPx(aColorArr, aPoint, pxToTypeCheck1), GetColorPx(aColorArr, aPoint, pxToTypeCheck2));
                }
                else
                {
                    kdPxType = GetPxType(GetColorPx(aColorArr, aPoint, pxToTypeCheck1), GetColorPx(aColorArr, aPoint, pxToTypeCheck2),
                                                            GetColorPx(aColorArr, aPoint, pxToTypeCheck2), GetColorPx(aColorArr, aPoint, pxToTypeCheck3));
                }
                Point? tmpGeoPoint = GetDisplacedPoint(aPoint, aDisplaceForGeoArr);
                Debug.Assert(tmpGeoPoint != null, "newGeoPoint jest 'Point.Empty'");
                Point newGeoPoint = (Point)tmpGeoPoint; //newGeoPoint ma jeszcze zmienne X,Y odpowiadające px. Przekształcenie ma prawdzowe zmienne geograficzne następuje poniżej
                aGeoArr.Add(new GeoEdgePoint(newGeoPoint.X * aMultiX + aDisplaceX, newGeoPoint.Y * aMultiY + aDisplaceY, newGeoPoint.X, newGeoPoint.Y, kdPxType));
                if (aPointAdvArr[newGeoPoint.X][newGeoPoint.Y] == null)
                    aPointAdvArr[newGeoPoint.X][newGeoPoint.Y] = new PointAdv((Point)newGeoPoint, kdPxType);
            }

            private ColorPx GetColorPx(ColorPx[][] aColorArr, Point aPoint, int? aDisplace)//int X, int Y)
            {
                Point? dPoint = GetDisplacedPoint(aPoint, aDisplace, aColorArr.GetLength(0)-1, aColorArr[0].GetLength(0)-1);
                if (dPoint != null)
                {
                    return aColorArr[((Point)dPoint).X][((Point)dPoint).Y];                    
                }
                else{
                    return null;
                }
            }

            private Point? GetDisplacedPoint(Point aPoint, int? aDisplace, int? maxX = null, int? maxY = null)
            {
                int dX = 0;
                int dY = 0;
                if (aDisplace == null)
                {
                    dX = 0;
                    dY = 0;
                }
                else if (aDisplace == Cst.NW)
                {
                    dX = -1;
                    dY = -1;
                }
                else if (aDisplace == Cst.N)
                {
                    dX = 0;
                    dY = -1;
                } 
                else if (aDisplace == Cst.NE)
                {
                    dX = 1;
                    dY = -1;
                }  
                else if (aDisplace == Cst.E)
                {
                    dX = 1;
                    dY = 0;
                }  
                else if (aDisplace == Cst.SE)
                {
                    dX = 1;
                    dY = 1;
                }  
                else if (aDisplace == Cst.S)
                {
                    dX = 0;
                    dY = 1;
                }  
                else if (aDisplace == Cst.SW)
                {
                    dX = -1;
                    dY = 1;
                }  
                else if (aDisplace == Cst.W)
                {
                    dX = -1;
                    dY = 0;
                } 
                else
                    Debug.Assert(false, String.Format("Nieobsługowana wartość zmiennej aDisplace: {0}", aDisplace));
                if (aPoint.X + dX>=0 && aPoint.Y + dY>=0)
                {
                    if ((maxX == null && maxY == null) || (aPoint.X + dX <= maxX && aPoint.Y + dY <= maxY))
                        return new Point(aPoint.X + dX, aPoint.Y + dY);
                    else
                        return null;
                }
                else
                    return null;
            }

            private bool IsThaSameGroup(ColorPx apx1, ColorPx apx2)
            {
                if (apx1 == null || apx2 == null)
                {
                    return true;
                }
                else { 
                    return (apx1.group == apx2.group);
                }               
            }
            private int GetPxType(ColorPx apx1, ColorPx apx2)
            {
                if (IsThaSameGroup(apx1, apx2))
                {
                    return Cst.c_geoPxSimple;
                }
                else
                {
                    return Cst.c_geoPxGroupBorder;
                }
            }
            private int GetPxType(ColorPx apx1, ColorPx apx2, ColorPx apx3, ColorPx apx4)
            {
                if (IsThaSameGroup(apx1, apx2) && IsThaSameGroup(apx3, apx4))
                {
                    return Cst.c_geoPxSimple;
                }
                else
                {
                    return Cst.c_geoPxGroupBorder;
                }
            }

        //tworzy krawędzie dla pojedynczego punktu
        private void MakePartEdgeForOnePoint(Point aPoint, List<GeoEdgePoint> aGeoArr,
                                            float aDpMultiX, float aDPMultiY, float aDpDisplaceX, float aDpDisplaceY,
                                            ColorPx[][] aColorArr,
                                            bool aBlOnlyFillColorArr)
        {
            if (!aBlOnlyFillColorArr)
            {
                aGeoArr.Add(new GeoEdgePoint((aPoint.X) * aDpMultiX + aDpDisplaceX, (aPoint.Y) * aDPMultiY + aDpDisplaceY, aPoint.X, aPoint.Y,
                                             GetPxType(aColorArr[aPoint.X - 1][aPoint.Y], aColorArr[aPoint.X - 1][aPoint.Y - 1],
                                                       aColorArr[aPoint.X - 1][aPoint.Y - 1], aColorArr[aPoint.X][aPoint.Y - 1])));
                aGeoArr.Add(new GeoEdgePoint((aPoint.X + 1) * aDpMultiX + aDpDisplaceX, aPoint.Y * aDPMultiY + aDpDisplaceY, aPoint.X, aPoint.Y,
                                             GetPxType(aColorArr[aPoint.X][aPoint.Y - 1], aColorArr[aPoint.X + 1][aPoint.Y - 1],
                                                       aColorArr[aPoint.X + 1][aPoint.Y - 1], aColorArr[aPoint.X + 1][aPoint.Y])));
                aGeoArr.Add(new GeoEdgePoint((aPoint.X + 1) * aDpMultiX + aDpDisplaceX, (aPoint.Y + 1) * aDPMultiY + aDpDisplaceY, aPoint.X, aPoint.Y,
                                             GetPxType(aColorArr[aPoint.X + 1][aPoint.Y], aColorArr[aPoint.X + 1][aPoint.Y + 1],
                                                       aColorArr[aPoint.X + 1][aPoint.Y + 1], aColorArr[aPoint.X][aPoint.Y + 1])));
                aGeoArr.Add(new GeoEdgePoint((aPoint.X) * aDpMultiX + aDpDisplaceX, (aPoint.Y + 1) * aDPMultiY + aDpDisplaceY, aPoint.X, aPoint.Y,
                                             GetPxType(aColorArr[aPoint.X][aPoint.Y + 1], aColorArr[aPoint.X - 1][aPoint.Y + 1],
                                                       aColorArr[aPoint.X - 1][aPoint.Y + 1], aColorArr[aPoint.X - 1][aPoint.Y])));
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

        private PointAdv[][] GetPointAdvArr()
        {
            return parentMapFactory.pointAdvArr;
        }

        private GeoEdgePoint PxPointToGeoPoint(Vector_Rectangle aPxPoint)
        {
            //P1 i P2 będą takie same, bo aPxPoint reprezentuje pojedynczy pixel, więc wartośći x i y możemy wziąć z p1
            return new GeoEdgePoint(aPxPoint.p1.X, aPxPoint.p1.Y, aPxPoint.p1.X, aPxPoint.p1.Y, Cst.c_geoPxSimple);
        }

        public VectoredRectangleGroup()
        {
            edgeList = new VectorRectangeGroup();
            innerEdgesList = new EdgeList();
        }

        //tworzy tablicę punktów z obiektów rectangli tworzących granicę (zawartych w edgePxList)
        public List<GeoEdgePoint> MakeVectorEdge(VectorRectangeGroup aEdgePxList,
                                                 ColorPx[][] aColorArr,
                                                 PointAdv[][] aPointAdvArr,
                                                 bool aBlOnlyFillColorArr,
                                                 float aMultiX = 1, float aMultiY = 1,
                                                 float aDisplaceX = 0, float aDisplaceY = 0)
        {
            List<GeoEdgePoint> result = new List<GeoEdgePoint>(aEdgePxList.Count * 3);
            int counter = 0;
            //if (!aBlOnlyFillColorArr)
            //    SetLength(result, aEdgePxList.Count*3);
            if (aEdgePxList.Count > 1)
            {
                //SetLength(result, self.rectList.Count+30);
                //dla pierwszej iteracji poprzednim rect jest ostatni rect tworzący granicę
                Point o1 = aEdgePxList[aEdgePxList.Count-1].GetP(0);
                Point o2 = aEdgePxList[0].GetP(0);
                Point o3 = aEdgePxList[1].GetP(0);
                MakePartEdge(o1, o2, o3, ref counter, result, aMultiX, aMultiY, aDisplaceX, aDisplaceY, aColorArr, aPointAdvArr, aBlOnlyFillColorArr, true);

                //jest tyle iteracji, ile rect tworzących granicę. Pierwsza i ostatnia iteracja są szczególne, więc pozostało n-2 iteracji
                for (int i=1; i<aEdgePxList.Count-1; i++)
                {
                    o1 = aEdgePxList[i-1].GetP(0);
                    o2 = aEdgePxList[i].GetP(0);
                    o3 = aEdgePxList[i+1].GetP(0);
                    MakePartEdge(o1, o2, o3, ref counter, result, aMultiX, aMultiY, aDisplaceX, aDisplaceY, aColorArr, aPointAdvArr, aBlOnlyFillColorArr);
                };

                //dla ostatniej iteracji trzecim rect jest pierwszy rect tworzący granicę
                o1 = aEdgePxList[aEdgePxList.Count-2].GetP(0);
                o2 = aEdgePxList[aEdgePxList.Count-1].GetP(0);
                o3 = aEdgePxList[0].GetP(0);
                MakePartEdge(o1, o2, o3, ref counter, result, aMultiX, aMultiY, aDisplaceX, aDisplaceY, aColorArr, aPointAdvArr, aBlOnlyFillColorArr);
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

        internal void MakePointArrFromFullEdge(float aDpScale, float aDisplaceX, float aDisplaceY)
        {
            pointAdvMapFromFullEdge = MakePointArrFromEdge(edgeList, aDpScale, aDisplaceX, aDisplaceY);
        }

        internal void MakePointArrFromSimplifiedEdge(float aDpScale, float aDisplaceX, float aDisplaceY)
        {
            //pointAdvMapFromSimplifiedEdge = MakePointArrFromEdge(simplifiedEdgeList, aDpScale, aDisplaceX, aDisplaceY);
            //SimplifyPointAdvMapPhase1(pointAdvMapFromSimplifiedEdge);

            List<GeoEdgePoint> pxPointList = MakeVectorEdge(simplifiedEdgeList, GetColorArr(), GetPointAdvArr(), false, aDpScale, aDpScale, aDisplaceX, aDisplaceY);
            SimplifyPointAdvListPhase1(pxPointList);
            pointAdvMapFromSimplifiedEdge = PointList2PxArray(pxPointList);
        }

            private PointAdv[] MakePointArrFromEdge(VectorRectangeGroup aEdgeList, float aDpScale, float aDisplaceX, float aDisplaceY)
            {
                //Point[] result = new Point[aEdgePxList.Count * 3];
                List<GeoEdgePoint> pxPointList = MakeVectorEdge(aEdgeList, GetColorArr(), GetPointAdvArr(), false, aDpScale, aDpScale, aDisplaceX, aDisplaceY);
                return PointList2PxArray(pxPointList);
            }

            private void SimplifyPointAdvListPhase1(List<GeoEdgePoint> aGeoEdgePointList)
            {
                GeoEdgePoint startPoint = aGeoEdgePointList[0];
                GeoEdgePoint middlePoint;
                GeoEdgePoint endPoint;
                //int ileMiddlePoints = 0;
                for (int i = 0; i < aGeoEdgePointList.Count - 2; i++)
                {
                    //startPoint = aGeoEdgePointList[i];
                    middlePoint = aGeoEdgePointList[i + 1];
                    endPoint = aGeoEdgePointList[i + 2];

                    //Jeśli punkt środkowy NIE MOŻE być usunięty
                    if (!parentMapFactory.pointAdvArr[middlePoint.pictX][middlePoint.pictY].CanBeDelSimplified())
                    {
                        startPoint = aGeoEdgePointList[i + 1];
                    }
                    //Jeśli 3 punkty sa w jednej lini to mozna usunąć środkowy
                    else if (ArePointsInVector(startPoint.ToPictPoint(), middlePoint.ToPictPoint(), endPoint.ToPictPoint()))
                    {
                        //uproszczenie punktu poprzez jego usunięcie
                        if (!parentMapFactory.pointAdvArr[middlePoint.pictX][middlePoint.pictY].IsDelSimplified())
                        {
                            parentMapFactory.pointAdvArr[middlePoint.pictX][middlePoint.pictY].DelSimplifyPhase1();
                        }
                        aGeoEdgePointList[i + 1] = null;                     
                    }
                    //Jeśli punkty nie są w jednej lini to dla kolejnego sprawdzenia punktem startu będzie punkt z narożnika    
                    else
                    {
                        startPoint = aGeoEdgePointList[i + 1];
                    }
                }
                aGeoEdgePointList.RemoveAll(x => x == null);
    
            }

            private void SimplifyPointAdvMapPhase1(PointAdv[] pointAdvMap)
            {
                PointAdv startPoint = pointAdvMap[0];
                PointAdv middlePoint;
                PointAdv endPoint;
                int ileMiddlePoints = 0;
                for (int i = 0; i < pointAdvMap.Length - 2; i++)
                {
                    //startPoint = pointAdvMap[i];
                    middlePoint = pointAdvMap[i + 1];
                    endPoint = pointAdvMap[i + 2];
                    
                    //Jeśli punkt środkowy NIE MOŻE być usunięty
                    if (!parentMapFactory.pointAdvArr[middlePoint.X][middlePoint.Y].CanBeDelSimplified())
                    {
                        startPoint = pointAdvMap[i + 1];
                    }
                    //Jeśli 3 punkty sa w jednej lini to mozna usunąć środkowy
                    else if (ArePointsInVector(startPoint.GetPoint(), middlePoint.GetPoint(), endPoint.GetPoint()))
                    {
                        //uproszczenie punktu poprzez jego usunięcie
                        if (!parentMapFactory.pointAdvArr[middlePoint.X][middlePoint.Y].IsDelSimplified())
                        {
                            parentMapFactory.pointAdvArr[middlePoint.X][middlePoint.Y].DelSimplifyPhase1();
                        }
                        pointAdvMap[i + 1] = null;
                    }
                    //Jeśli punkty nie są w jednej lini to dla kolejnego sprawdzenia punktem startu będzie punkt z narożnika    
                    else
                    {
                        startPoint = pointAdvMap[i + 1];
                    }
                }





                /*int prevDirection = Cst.fromLeft;
                foreach(KeyValuePair<int, PointAdv> pair in pointAdvList)
                {
                    PointAdv currentPoint = pair.Value;
                    if (startPoint != currentPoint && )  
                }*/
            }

                private bool ArePointsInVector(Point aPoint1, Point aPoint2, Point aPoint3)
                {
                    return (aPoint1.X < aPoint2.X && aPoint2.X < aPoint3.X && aPoint1.Y == aPoint2.Y && aPoint2.Y == aPoint3.Y) ||
                           (aPoint1.X > aPoint2.X && aPoint2.X > aPoint3.X && aPoint1.Y == aPoint2.Y && aPoint2.Y == aPoint3.Y) ||
                           (aPoint1.X == aPoint2.X && aPoint2.X == aPoint3.X && aPoint1.Y < aPoint2.Y && aPoint2.Y < aPoint3.Y) ||
                           (aPoint1.X == aPoint2.X && aPoint2.X == aPoint3.X && aPoint1.Y > aPoint2.Y && aPoint2.Y > aPoint3.Y);
                }
        
                private bool IsVerticalVector(Point aPoint1, Point aPoint2, int aDirection)
                {
                    return (aDirection == Cst.fromLeft && aPoint1.X < aPoint2.X && aPoint1.Y == aPoint2.Y) ||
                           (aDirection == Cst.fromRight && aPoint1.X > aPoint2.X && aPoint1.Y == aPoint2.Y);
                }

                private bool IsHorizontalVector(Point aPoint1, Point aPoint2, int aDirection)
                {
                    return (aDirection == Cst.fromTop && aPoint1.X == aPoint2.X && aPoint1.Y > aPoint2.Y) ||
                           (aDirection == Cst.fromBottom && aPoint1.X == aPoint2.X && aPoint1.Y < aPoint2.Y);
                }

                private bool IsStraightVector(Point aPoint1, Point aPoint2, int aDirection)
                {
                    return IsVerticalVector(aPoint1, aPoint2, aDirection) || IsHorizontalVector(aPoint1, aPoint2, aDirection);
                }


        internal PointAdv[] GetScaledPointArrFromFullEdge(float adpScale, float aDisplaceX, float aDisplaceY)
        {
            return GetScaledPointArrFromEdge(adpScale, aDisplaceX, aDisplaceY, pointAdvMapFromFullEdge);
        }

        internal PointAdv[] GetScaledPointArrFromSimplifiedEdge(float adpScale, float aDisplaceX, float aDisplaceY)
        {
            return GetScaledPointArrFromEdge(adpScale, aDisplaceX, aDisplaceY, pointAdvMapFromSimplifiedEdge);
        }

        private PointAdv[] GetScaledPointArrFromEdge(float adpScale, float aDisplaceX, float aDisplaceY, PointAdv[] aPointArr)
        {
            PointAdv[] result = new PointAdv[aPointArr.Length];
            for(int i=0; i< aPointArr.Length; i++)
            {
                result[i] = new PointAdv((int)Math.Round(aPointArr[i].X * adpScale / Cst.maxZoom + aDisplaceX), 
                                            (int)Math.Round(aPointArr[i].Y * adpScale / Cst.maxZoom + aDisplaceY),
                                            aPointArr[i].GetKdPointType());
            }
            return result;
        }

        internal void MakeSimplifyVectorEdge()
        {
            //to do
            simplifiedEdgeList = new VectorRectangeGroup();
            List<int> sortedKeyList = edgeList.GetSortedKeyList();
            Vector_Rectangle startRect = edgeList[sortedKeyList[0]]; // punkt, wobec którego sprawdzamy położenie kolejnych
            Vector_Rectangle middleRect = null;
            Vector_Rectangle endRect = null;
            int lastKey;

            int prevDiff;
            //usunięcie punktów z linii poziomych i pionowych
            if (sortedKeyList.Count >= 3)
            {
                lastKey = simplifiedEdgeList.NextKey();
                simplifiedEdgeList.Add(lastKey, startRect);
                if (edgeList[sortedKeyList[0]].p1.X == edgeList[sortedKeyList[1]].p1.X)
                {
                    prevDiff = edgeList[sortedKeyList[1]].p1.X - startRect.p1.X;
                }
                else /*if (edgeList[sortedKeyList[0]].p1.Y == edgeList[sortedKeyList[1]].p1.Y)*/
                {
                    prevDiff = edgeList[sortedKeyList[1]].p1.Y - startRect.p1.Y;
                }

                for (var i = 1; i < sortedKeyList.Count - 1; i++)
                {                      
                    middleRect = edgeList[sortedKeyList[i]];
                    endRect = edgeList[sortedKeyList[i + 1]];
                    if (InLineHorizontal(startRect, middleRect, endRect, prevDiff)) 
                    {
                        prevDiff = endRect.p1.Y - middleRect.p1.Y;
                    }
                    else if (InLineVertical(startRect, middleRect, endRect, prevDiff)) 
                    {
                        prevDiff = endRect.p1.X - middleRect.p1.X;
                    }
                    else
                    {
                        lastKey = simplifiedEdgeList.NextKey();
                        simplifiedEdgeList.Add(lastKey, middleRect);
                        startRect = middleRect;
                        //jeśli krawędź zakręciła w kierunku poziomym, to prevDiff też w tym kierunku obliczamy
                        if (middleRect.p1.X != endRect.p1.X)
                        {
                            prevDiff = endRect.p1.X - middleRect.p1.X;
                        }
                        else
                        {
                            prevDiff = endRect.p1.Y - middleRect.p1.Y;
                        }
                    }                    
                }


                startRect = simplifiedEdgeList[lastKey];
                middleRect = edgeList[sortedKeyList[sortedKeyList.Count - 1]];
                endRect = edgeList[sortedKeyList[0]];
                if (edgeList[sortedKeyList[0]].p1.X == edgeList[sortedKeyList[1]].p1.X)
                {
                    prevDiff = edgeList[sortedKeyList[1]].p1.X - startRect.p1.X;
                }
                else /*if (edgeList[sortedKeyList[0]].p1.Y == edgeList[sortedKeyList[1]].p1.Y)*/
                {
                    prevDiff = edgeList[sortedKeyList[1]].p1.Y - startRect.p1.Y;
                }

                simplifiedEdgeList.Add(edgeList[sortedKeyList[sortedKeyList.Count - 1]]);
            }
            else
            {//hjkhjkgjk
                for (var i = 0; i < sortedKeyList.Count; i++)
                {
                    simplifiedEdgeList.Add(edgeList[sortedKeyList[i]]);
                }
            }
        }
            //obiekty "podążają" w jednym kierunku w linii poziomej
            internal bool InLineHorizontal(Vector_Rectangle aStartRect,
                                    Vector_Rectangle aMiddleRect,
                                    Vector_Rectangle aEndRect,
                                    int aPrevDiff)
            {
                return aStartRect.p1.X == aMiddleRect.p1.X && aStartRect.p1.X == aEndRect.p1.X &&
                       aPrevDiff == aEndRect.p1.Y - aMiddleRect.p1.Y;
            }

            //obiekty "podążają" w jednym kierunku w linii pionowej
            internal bool InLineVertical(Vector_Rectangle aStartRect,
                                    Vector_Rectangle aMiddleRect,
                                    Vector_Rectangle aEndRect,
                                    int aPrevDiff)
            {
                return aStartRect.p1.Y == aMiddleRect.p1.Y && aStartRect.p1.Y == aEndRect.p1.Y &&
                       aPrevDiff == aEndRect.p1.X - aMiddleRect.p1.X;
            }

        public PointAdv[] PointList2PxArray(List<GeoEdgePoint> aGeoList)
        {
            PointAdv[] result = new PointAdv[aGeoList.Count];
            for (int i = 0; i < aGeoList.Count; i++)
               result[i] = aGeoList[i].PointToPointAdv();
            return result;
        }

        public PointAdv[] PictPointList2PxArray(List<GeoEdgePoint> aGeoList)
        {
            PointAdv[] result = new PointAdv[aGeoList.Count];
            for (int i = 0; i < aGeoList.Count; i++)
                result[i] = aGeoList[i].PictPointToPointAdv();
            return result;
        }

                    
    }
}
