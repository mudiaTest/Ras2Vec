using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Migracja
{
    class VectorRectangeGroup : Dictionary<int, Vector_Rectangle>
    {
        private int maxKey;

        internal int NextKey()
        {
            return maxKey++;
        }

        public List<int> GetSortedKeyList()
        {
            List<int> result = Keys.ToList();
            result.Sort();
            return result;
        }

        public VectorRectangeGroup()
        {
            maxKey = 0;
        }

        public void ClearReset()
        {
            Clear();
            maxKey = 0;
        }
    }

    partial class VectoredRectangleGroup : Dictionary<int, Vector_Rectangle>
    {
        private int NextDirection(int aDirection)
        {
            long dummy;
            aDirection++;
            return (int)Math.DivRem((long)aDirection, (long)4, out dummy);
        }

        private bool CheckBottomPX(Vector_Rectangle aStartEdgePoint)
        {
            bool result = false;
            if (aStartEdgePoint.p1.Y < SrcHeight() - 1)
            {
                Vector_Gen bottomVectorRectangle = GetVectorArr()[aStartEdgePoint.p1.X][aStartEdgePoint.p1.Y + 1];
                result = (bottomVectorRectangle != null) && (bottomVectorRectangle.parentVectorGroupId == aStartEdgePoint.parentVectorGroupId);
            }
            return result;
        }

        private Vector_Rectangle CheckTop(Vector_Rectangle aPrevEdge3, bool aBlInnerBorder, int aOuterGroup)
        {
            if (aPrevEdge3.p1.Y > 0)
            {
                Vector_Rectangle Result3 = GetVectorArr()[aPrevEdge3.p1.X][aPrevEdge3.p1.Y - 1];
                if ((!aBlInnerBorder && (aPrevEdge3.parentVectorGroupId == Result3.parentVectorGroupId)) ||
                    (aBlInnerBorder && (Result3.parentVectorGroupId != aOuterGroup)))
                    return Result3;
            }
            return null;
        }

        private Vector_Rectangle CheckRight(Vector_Rectangle aPrevEdge3, bool aBlInnerBorder, int aOuterGroup)
        {
            if (aPrevEdge3.p1.X < SrcWidth() - 1)
            {
                Vector_Rectangle Result3 = GetVectorArr()[aPrevEdge3.p1.X + 1][aPrevEdge3.p1.Y];
                if ((!aBlInnerBorder && (aPrevEdge3.parentVectorGroupId == Result3.parentVectorGroupId)) ||
                    (aBlInnerBorder && (Result3.parentVectorGroupId != aOuterGroup)))
                    return Result3;
            }
            return null;
        }

        private Vector_Rectangle CheckBottom(Vector_Rectangle aPrevEdge3, bool aBlInnerBorder, int aOuterGroup)
        {
            if (aPrevEdge3.p1.Y < SrcHeight() - 1)
            {
                Vector_Rectangle Result3 = GetVectorArr()[aPrevEdge3.p1.X][aPrevEdge3.p1.Y + 1];
                if ((!aBlInnerBorder && (aPrevEdge3.parentVectorGroupId == Result3.parentVectorGroupId)) ||
                    (aBlInnerBorder && (Result3.parentVectorGroupId != aOuterGroup)))
                    return Result3;
            }
            return null;
        }

        private Vector_Rectangle CheckLeft(Vector_Rectangle aPrevEdge3, bool aBlInnerBorder, int aOuterGroup)
        {
            if (aPrevEdge3.p1.X > 0)
            {
                Vector_Rectangle Result3 = GetVectorArr()[aPrevEdge3.p1.X - 1][aPrevEdge3.p1.Y];
                if ((!aBlInnerBorder && (aPrevEdge3.parentVectorGroupId == Result3.parentVectorGroupId)) ||
                    (aBlInnerBorder && (Result3.parentVectorGroupId != aOuterGroup)))
                    return Result3;
            }
            return null;
        }

        private Vector_Rectangle GetNextEdge(Vector_Rectangle aPrevEdge, ref int aArrDir, bool aBlInnerBorder, int aOuterGroup)
        {
            Vector_Rectangle Result = null;
            int j = 0;
            for (int i = 0; i < 4; i++)
            {
                if (aArrDir + j == 4)
                    j = -aArrDir;
                Result = CheckNextEdge(aPrevEdge, aArrDir + j, aBlInnerBorder, aOuterGroup);
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
            if ((Result != null) &&
                //przeszliśmy z prawa na lewo
                (aArrDir == Cst.fromRight))
                GetColorArr()[Result.p1.X][Result.p1.Y].borderEW = true;
            return Result;
        }

        private void MakeUsed(VectorRectangeGroup aEdgePxList, bool aBlInnerBorder)
        {
            if (aBlInnerBorder)
                for (int i = 0; i < aEdgePxList.Count; i++)
                {
                    Vector_Rectangle point = aEdgePxList[i];
                    if (point.p1.Y >= 1)
                        GetColorArr()[point.p1.X][point.p1.Y - 1].used = true;
                };
        }

        private Vector_Rectangle CheckNextEdge(Vector_Rectangle aPrevEdge2, int aArrDir, bool aBlInnerBorder, int aOuterGroup)
        {
            Vector_Rectangle Result;
            if (aArrDir == Cst.goTop)
                Result = CheckTop(aPrevEdge2, aBlInnerBorder, aOuterGroup);
            else if (aArrDir == Cst.goRight)
                Result = CheckRight(aPrevEdge2, aBlInnerBorder, aOuterGroup);
            else if (aArrDir == Cst.goBottom)
                Result = CheckBottom(aPrevEdge2, aBlInnerBorder, aOuterGroup);
            else if (aArrDir == Cst.goLeft)
                Result = CheckLeft(aPrevEdge2, aBlInnerBorder, aOuterGroup);
            else
            {
                Result = null;
                //Assert(False, 'checkNextEdge');
            };
            return Result;
        }


        //buduje krawędź
        //VectorRectangeGroup to mapa (kluczem jest int - kolejne wartości wyznaczają kolejność) obiektów Vector_Rectangle 
        public void MakeEdges(VectorRectangeGroup aEdgePxList,  bool aBlInnerBorder = false, int aOuterGroup = 0)
        {
            //var
            //startEdgePoint, nextEdgePoint, prevEdgePoint: TVectRectangle;
            //arrivDir: integer;
            //dummyArr: TDynamicGeoPointArray;

            
            aEdgePxList.ClearReset();
            //startEdgePoint to pierwszy punkt na liście, bo idziemy ol lewej strony
            //w najwyższym wierszu
            Vector_Rectangle startEdgePoint = this[0];
            Vector_Rectangle prevEdgePoint = startEdgePoint;
            int arrivDir = Cst.goRight; //zaczynamy od max lewego ponktu na górnej linji
            //Każemy zacząć szukanie od prawej
            Vector_Rectangle nextEdgePoint = null;
            //1-pixelowy obiekt traktujemy inaczej
            if (Count != 1)
            {
            //kończymy jeśli trafiamy na początek, lub na 1-pixelowy obiekt
                Debug.Assert(aEdgePxList.Count == 0, "Dictionary aEdgePxList nie jest pusty.");
                aEdgePxList.Add(aEdgePxList.NextKey(), null);
                while (
                        ((nextEdgePoint != startEdgePoint) && (prevEdgePoint != null)) ||
                        (CheckBottomPX(startEdgePoint) && (arrivDir == Cst.fromRight)) //przypadek gdy wracamy się do punktu startu, ale mamy do prawdzenia to co jest pod nim
                        )
                {
                    if (nextEdgePoint == startEdgePoint)
                    {
                        if (CheckBottomPX(startEdgePoint) && (arrivDir == Cst.fromRight))
                        {
                            arrivDir = Cst.goBottom;
                        }
                        else
                        {
                            //arrivDir = -1;
                            break;
                        };
                    };
                    nextEdgePoint = GetNextEdge(prevEdgePoint, ref arrivDir, aBlInnerBorder, aOuterGroup);
                    //powstanie gdy nie możemy oddać następnej krawędzi, ale wyjątkikem jest gdy jest to pojedynczy pixel
                    if ((nextEdgePoint == null) && (aEdgePxList.Count != 0))
                        Debug.Assert(false, "Oddany edge jest nil (" + prevEdgePoint.p1.X.ToString() +
                               "," + prevEdgePoint.p1.Y.ToString() + "), liczba znalezionych kreawędzi:" +
                               aEdgePxList.Count.ToString());

                    aEdgePxList.Add(aEdgePxList.NextKey(), nextEdgePoint);
                    //MakeUsed(nextEdgePoint aBlInnerBorder);
                    prevEdgePoint = nextEdgePoint;
                }
                aEdgePxList[0] = aEdgePxList[aEdgePxList.Keys.Max()];
                aEdgePxList.Remove(aEdgePxList.Keys.Max());
            }
            //dla obiektu 1-pixelowego
            else
            {
                //dodajemy punkty graniczne do listy
                aEdgePxList.Add(aEdgePxList.NextKey(), startEdgePoint);
                // MakeUsed(startEdgePoint aBlInnerBorder);
            };
            List<GeoPoint> geoPointList = MakeVectorEdge(aEdgePxList, GetColorArr(), true);
            MakeUsed(aEdgePxList, aBlInnerBorder);
            geoPointList.Clear();
            //PxListToGeoList;
            
        }

        //VectorRectangeGroup to mapa (kluczem jest int - kolejne wartości wyznaczają kolejność) obiektów Vector_Rectangle 
        private void PxListToGeoList(VectorRectangeGroup aEdgePxList)
        {
            Vector_Rectangle edgePoint;
            GeoPoint geoPoint;
            for (int i=0; i < aEdgePxList.Count; i++) 
            {
                edgePoint = aEdgePxList[i];
                geoPoint = PxPointToGeoPoint(edgePoint);
                edgeGeoList.Add(i, geoPoint);//przepisujemy klucz z edgePxList
            }
        }
        
    }
}
