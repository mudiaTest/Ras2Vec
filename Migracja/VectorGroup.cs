﻿using System;
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
                    NewBorderGeoPoint(aGeoArr, aActPoint, aMultiX, aMultiY, aDisplaceX, aDisplaceY, null, aPointAdvArr, aColorArr, false);
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
                    NewBorderGeoPoint(aGeoArr, aActPoint, aMultiX, aMultiY, aDisplaceX, aDisplaceY, Cst.SE, aPointAdvArr, aColorArr, false);
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
                        NewBorderGeoPoint(aGeoArr, aActPoint, aMultiX, aMultiY, aDisplaceX, aDisplaceY, null, aPointAdvArr, aColorArr, aIsFirstEdgeVectRect, Cst.W, Cst.NW, Cst.N);  
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
                    NewBorderGeoPoint(aGeoArr, aActPoint, aMultiX, aMultiY, aDisplaceX, aDisplaceY, Cst.E, aPointAdvArr, aColorArr, false);
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
                    NewBorderGeoPoint(aGeoArr, aActPoint, aMultiX, aMultiY, aDisplaceX, aDisplaceY, Cst.S, aPointAdvArr, aColorArr, false);  
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
                                           int? pxToTypeCheck1 = null, //1 punkt zprawdzamy na możliwośc zmiany grupy w okolicy dodawanego punktu
                                           int? pxToTypeCheck2 = null, //2 punkt zprawdzamy na możliwośc zmiany grupy w okolicy dodawanego punktu
                                           int? pxToTypeCheck3 = null)  //3 punkt zprawdzamy na możliwośc zmiany grupy w okolicy dodawanego punktu
            {
                int kdPxType = 0; 
                if (pxToTypeCheck1 != null && pxToTypeCheck2 != null)
                {
                    if (aIsFirstEdgeVectRect)
                    {
                        kdPxType = Cst.c_geoPxStartEnd;
                    }
                    else if (pxToTypeCheck3 == null)
                    {
                        kdPxType = GetPxType(GetColorPx(aColorArr, aPoint, pxToTypeCheck1),
                                             GetColorPx(aColorArr, aPoint, pxToTypeCheck2),
                                             aColorArr[aPoint.X][aPoint.Y].group);
                    }
                    else
                    {
                        kdPxType = GetPxType(GetColorPx(aColorArr, aPoint, pxToTypeCheck1),
                                             GetColorPx(aColorArr, aPoint, pxToTypeCheck2),
                                             GetColorPx(aColorArr, aPoint, pxToTypeCheck2),
                                             GetColorPx(aColorArr, aPoint, pxToTypeCheck3),
                                             aColorArr[aPoint.X][aPoint.Y].group);
                    }
                }
                else
                {
                    kdPxType = Cst.c_geoPxSimple;
                }
                Point? tmpGeoPoint = GetDisplacedPoint(aPoint, aDisplaceForGeoArr);
                Debug.Assert(tmpGeoPoint != null, "newGeoPoint jest 'Point.Empty'");
                Point newGeoPoint = (Point)tmpGeoPoint; //newGeoPoint ma jeszcze zmienne X,Y odpowiadające px. Przekształcenie ma prawdzowe zmienne geograficzne następuje poniżej

                if (!(aGeoArr.Count > 0 && aGeoArr[aGeoArr.Count - 1].pictX == newGeoPoint.X && aGeoArr[aGeoArr.Count - 1].pictY == newGeoPoint.Y))
                { 
                    aGeoArr.Add(new GeoEdgePoint(newGeoPoint.X * aMultiX + aDisplaceX, newGeoPoint.Y * aMultiY + aDisplaceY, newGeoPoint.X, newGeoPoint.Y, kdPxType));
                    if (aPointAdvArr[newGeoPoint.X][newGeoPoint.Y] == null)
                        aPointAdvArr[newGeoPoint.X][newGeoPoint.Y] = new PointAdv((Point)newGeoPoint, kdPxType);
                }
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

            private bool IsThaSameGroup(ColorPx apx1, ColorPx apx2, int aEdgeGroup)
            {
                if (apx1 == null || apx2 == null)
                {
                    return true;
                }
                else {
                    return (apx1.group == apx2.group || apx1.group == aEdgeGroup || apx2.group == aEdgeGroup);
                }               
            }
            private int GetPxType(ColorPx apx1, ColorPx apx2, int aEdgeGroup)
            {
                if (IsThaSameGroup(apx1, apx2, aEdgeGroup))
                {
                    return Cst.c_geoPxSimple;
                }
                else
                {
                    return Cst.c_geoPxGroupBorder;
                }
            }
            private int GetPxType(ColorPx apx1, ColorPx apx2, ColorPx apx3, ColorPx apx4, int aEdgeGroup)
            {
                if (IsThaSameGroup(apx1, apx2, aEdgeGroup) && IsThaSameGroup(apx3, apx4, aEdgeGroup))
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
                                            PointAdv[][] aPointAdvArr,
                                            bool aBlOnlyFillColorArr)
        {
            if (!aBlOnlyFillColorArr)
            {
                /*aGeoArr.Add(new GeoEdgePoint((aPoint.X) * aDpMultiX + aDpDisplaceX, (aPoint.Y) * aDPMultiY + aDpDisplaceY, aPoint.X, aPoint.Y,
                                             GetPxType(GetColorPx(aColorArr, aPoint, Cst.W), 
                                                       GetColorPx(aColorArr, aPoint, Cst.NW),
                                                       GetColorPx(aColorArr, aPoint, Cst.NW),
                                                       GetColorPx(aColorArr, aPoint, Cst.N),
                                                       aColorArr[aPoint.X][aPoint.Y].group)));
                 * 
                aGeoArr.Add(new GeoEdgePoint((aPoint.X + 1) * aDpMultiX + aDpDisplaceX, aPoint.Y * aDPMultiY + aDpDisplaceY, aPoint.X, aPoint.Y,
                                             GetPxType(GetColorPx(aColorArr, aPoint, Cst.N), 
                                                       GetColorPx(aColorArr, aPoint, Cst.NE),
                                                       GetColorPx(aColorArr, aPoint, Cst.NE),
                                                       GetColorPx(aColorArr, aPoint, Cst.E),
                                                       aColorArr[aPoint.X][aPoint.Y].group)));

                aGeoArr.Add(new GeoEdgePoint((aPoint.X + 1) * aDpMultiX + aDpDisplaceX, (aPoint.Y + 1) * aDPMultiY + aDpDisplaceY, aPoint.X, aPoint.Y,
                                             GetPxType(GetColorPx(aColorArr, aPoint, Cst.E), 
                                                       GetColorPx(aColorArr, aPoint, Cst.SE),
                                                       GetColorPx(aColorArr, aPoint, Cst.SE),
                                                       GetColorPx(aColorArr, aPoint, Cst.S),
                                                       aColorArr[aPoint.X][aPoint.Y].group)));

                aGeoArr.Add(new GeoEdgePoint((aPoint.X) * aDpMultiX + aDpDisplaceX, (aPoint.Y + 1) * aDPMultiY + aDpDisplaceY, aPoint.X, aPoint.Y,
                                             GetPxType(GetColorPx(aColorArr, aPoint, Cst.S), 
                                                       GetColorPx(aColorArr, aPoint, Cst.SW),
                                                       GetColorPx(aColorArr, aPoint, Cst.SW),
                                                       GetColorPx(aColorArr, aPoint, Cst.W),
                                                       aColorArr[aPoint.X][aPoint.Y].group)));*/
                NewBorderGeoPoint(aGeoArr, aPoint, aDpMultiX, aDPMultiY, aDpDisplaceX, aDpDisplaceY, null, aPointAdvArr, aColorArr, true, Cst.W, Cst.NW, Cst.N);
                NewBorderGeoPoint(aGeoArr, aPoint, aDpMultiX, aDPMultiY, aDpDisplaceX, aDpDisplaceY, Cst.E, aPointAdvArr, aColorArr, false, Cst.N, Cst.NE, Cst.E);
                NewBorderGeoPoint(aGeoArr, aPoint, aDpMultiX, aDPMultiY, aDpDisplaceX, aDpDisplaceY, Cst.SE, aPointAdvArr, aColorArr, false, Cst.E, Cst.SE, Cst.S);
                NewBorderGeoPoint(aGeoArr, aPoint, aDpMultiX, aDPMultiY, aDpDisplaceX, aDpDisplaceY, Cst.S, aPointAdvArr, aColorArr, false, Cst.S, Cst.SW, Cst.W);
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

        /*
         *     A           B             C
         * |(y1-y2)*x0 + (x2-x1)*y0 + (y2x1-x2y1)|
         * -------------------------------------
         *            _________D__________
         *           V(y1-y2)2 + (x2-x1)2|
         * 
         */
        private float DistancePointToLine(Point p1/*(x1,y1)*/, Point p2/*(x2,y2)*/, Point p3/*(x0,y0)*/)
        {
            int A = p1.Y - p2.Y;
            int B = p2.X - p1.X;
            int C = p2.Y*p1.X - p2.X*p1.Y;
            float D = (float)Math.Sqrt(A * A + B * B);
            return Math.Abs(A*p3.X + B*p3.Y + C) / D;
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
                                        aColorArr, aPointAdvArr, aBlOnlyFillColorArr);
                counter = 4;
            }
            //Z powodu kolejności dodawania punktów w metodzie MakePartEdge, do listy jako pierwszy dodamy 
            //ostatni punkt, a potem dopiero pierwszy, drugi etc. Dlatego przesówamy całą listę o jedną pozycję.
            if (result.Count > 0)
            {
                result.Add(result[0]);
                result.RemoveAt(0);
            }
            return result;
        }

        internal void MakePointArrFromFullEdge(float aDpScale, float aDisplaceX, float aDisplaceY)
        {
            pointAdvMapFromFullEdge = MakePointArrFromEdge(edgeList, aDpScale, aDisplaceX, aDisplaceY);
        }

        internal void MakePointArrFromSimplifiedEdge(float aDpScale, float aDisplaceX, float aDisplaceY, bool asimplifyPhase1, bool asimplifyPhase3)
        {
            //pointAdvMapFromSimplifiedEdge = MakePointArrFromEdge(simplifiedEdgeList, aDpScale, aDisplaceX, aDisplaceY);
            //SimplifyPointAdvMapPhase1(pointAdvMapFromSimplifiedEdge);

            List<GeoEdgePoint> pxPointList = MakeVectorEdge(simplifiedEdgeList, GetColorArr(), GetPointAdvArr(), false, aDpScale, aDpScale, aDisplaceX, aDisplaceY);
            if (asimplifyPhase1)
                SimplifyPointAdvListPhase1(pxPointList);
            if (asimplifyPhase3)
                SimplifyPointAdvListPhase2(pxPointList);
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
                for (int i = 0; i < aGeoEdgePointList.Count - 1; i++)
                {
                    //startPoint = aGeoEdgePointList[i];
                    middlePoint = aGeoEdgePointList[i + 1];
                    if (i < aGeoEdgePointList.Count - 2)
                    {
                        endPoint = aGeoEdgePointList[i + 2];
                    }
                    else
                    {
                        endPoint = aGeoEdgePointList[0];
                    }

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
        
            private void SimplifyPointAdvListPhase2(List<GeoEdgePoint> aGeoEdgePointList)
            {
                GeoEdgePoint startPoint = aGeoEdgePointList[0];
                GeoEdgePoint middlePoint;
                GeoEdgePoint endPoint;
                //int startId = 0;
                int endId;
                List<GeoEdgePoint> lstPointsToDelete = new List<GeoEdgePoint>();
                List<GeoEdgePoint> lstPointsToCheck = new List<GeoEdgePoint>();
                List<int> lstIdStartEnd = new List<int>();
                lstIdStartEnd.Add(0);
                //int ileMiddlePoints = 0;
                float distance = (float)1;

                for (int i = 0; i < aGeoEdgePointList.Count - 1; i++)
                {
                    //startPoint = aGeoEdgePointList[i];
                    middlePoint = aGeoEdgePointList[i + 1];
                    lstPointsToCheck.Add(middlePoint);
                    if (i < aGeoEdgePointList.Count - 2)
                    {
                        endPoint = aGeoEdgePointList[i + 2];
                        endId = i + 2;
                    }
                    else
                    {
                        endPoint = aGeoEdgePointList[0];
                        endId = 0;
                    }

                    if (parentMapFactory.pointAdvArr[endPoint.pictX][endPoint.pictY].IsDelSimplified())
                    {
                        //właściwie nie powinniśmy nic robić, ale może się okazać, że middle
                        if (!parentMapFactory.pointAdvArr[middlePoint.pictX][middlePoint.pictY].CanBeDelSimplified())
                        {
                            lstPointsToCheck.RemoveAll(x => true);
                        }
                    }
                    //Jeśli ostatnio dodany punkt środkowy NIE MOŻE być usunięty - ta sytuacja może zajśc tylko gdy 
                    else if ((!parentMapFactory.pointAdvArr[middlePoint.pictX][middlePoint.pictY].CanBeDelSimplified() /*&& lstPointsToCheck.Count == 1*/) ||
                              //Funkcja sprawdzająca odległośc punktu middle od linii
                              DistancePointToLine(startPoint.ToPictPoint(), endPoint.ToPictPoint(), middlePoint.ToPictPoint()) >= distance ||
                              DistanceOfPoints(startPoint.ToPictPoint(), middlePoint.ToPictPoint()) > 4)
                    {
                        startPoint = middlePoint;
                        //startId = i + 1;
                        lstIdStartEnd.Add(i + 1);                        
                        //przepisujemy wszystkie punkty z listy Check do Delete, poza ostatnim, bo kończymy na punkcie middle
                        for (int j = 0; j < lstPointsToCheck.Count - 1; j++)
                        {
                            lstPointsToDelete.Add(lstPointsToCheck[j]);
                        }
                        lstPointsToCheck.RemoveAll(x => true);
                    }
                    else if (!parentMapFactory.pointAdvArr[endPoint.pictX][endPoint.pictY].CanBeDelSimplified() )
                    {
                        //ustawaimy na nowo startPoint na pozycję z endPoint
                        startPoint = endPoint;
                        //przepisujemy punkty z listy Check do Delete
                        foreach(GeoEdgePoint checkPoint in lstPointsToCheck)
                        {
                            lstPointsToDelete.Add(checkPoint);
                        }
                        lstPointsToCheck.RemoveAll(x => true);
                        i++;
                    }
                }
                DeletePoints(lstPointsToDelete, aGeoEdgePointList);
            }

                private float DistanceOfPoints(Point p1, Point p2)
                {
                    return (float)Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
                }

                private void DeletePoints(List<GeoEdgePoint> alstPointsToDelete, List<GeoEdgePoint> aGeoEdgePointList)
                {
                    foreach (GeoEdgePoint point in alstPointsToDelete)
                    {
                        parentMapFactory.pointAdvArr[point.pictX][point.pictY].DelSimplifyPhase2();
                    }
                    aGeoEdgePointList.RemoveAll(x => alstPointsToDelete.Contains(x));
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

        internal void MakeSimplifyVectorEdge(bool asimplifyPhase1)
        {
            simplifiedEdgeList = new VectorRectangeGroup();

            if (asimplifyPhase1)
            {
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
                        prevDiff = edgeList[sortedKeyList[1]].p1.Y - startRect.p1.Y;
                    }
                    else
                    {
                        prevDiff = edgeList[sortedKeyList[1]].p1.X - startRect.p1.X;
                    }

                    for (var i = 1; i < sortedKeyList.Count; i++)
                    {
                        middleRect = edgeList[sortedKeyList[i]];
                        if (i < sortedKeyList.Count - 1)
                        {
                            endRect = edgeList[sortedKeyList[i + 1]];
                        }
                        else
                        {
                            endRect = edgeList[sortedKeyList[0]];
                        }
                        if (InLineHorizontal(startRect, middleRect, endRect, prevDiff))
                        {
                            prevDiff = endRect.p1.X - middleRect.p1.X;
                        }
                        else if (InLineVertical(startRect, middleRect, endRect, prevDiff))
                        {
                            prevDiff = endRect.p1.Y - middleRect.p1.Y;
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
                }
                else
                {
                    for (var i = 0; i < sortedKeyList.Count; i++)
                    {
                        simplifiedEdgeList.Add(edgeList[sortedKeyList[i]]);
                    }
                }
            }
            else
            {
                foreach (KeyValuePair<int, Vector_Rectangle> pair in edgeList)
                {
                    simplifiedEdgeList.Add(pair.Value);
                }
            }
        }
            //obiekty "podążają" w jednym kierunku w linii poziomej
            internal bool InLineHorizontal(Vector_Rectangle aStartRect,
                                    Vector_Rectangle aMiddleRect,
                                    Vector_Rectangle aEndRect,
                                    int aPrevDiff)
            {
                return aStartRect.p1.Y == aMiddleRect.p1.Y && aStartRect.p1.Y == aEndRect.p1.Y &&
                       aPrevDiff == aEndRect.p1.X - aMiddleRect.p1.X;
            }

            //obiekty "podążają" w jednym kierunku w linii pionowej
            internal bool InLineVertical(Vector_Rectangle aStartRect,
                                    Vector_Rectangle aMiddleRect,
                                    Vector_Rectangle aEndRect,
                                    int aPrevDiff)
            {
                return aStartRect.p1.X == aMiddleRect.p1.X && aStartRect.p1.X == aEndRect.p1.X &&
                       aPrevDiff == aEndRect.p1.Y - aMiddleRect.p1.Y;
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
