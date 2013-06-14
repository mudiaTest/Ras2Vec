using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Migracja
{
    //generyczny obiekt wektorowy
    class Vector_Gen
    {

        //lista wierzchołków obeiktu
        protected Dictionary<int, Point> points;
        //kolor wypełniający obiekt
        public Color color{get; set;}
        //odnośnik do grupy, która posiada dany obiekt vektorowy
        public VectoredRectangleGroup parentVectorGroup{get; set;}
        //numer grupy (w liście grup), która posiada dany obiekt vektorowy
        public int parentVectorGroupId { get; set; }



        public Vector_Gen()
        {
            points = new Dictionary<int, Point>();
        }

        //przepisanie (dołączenie) obiektów z innej grupy do tej, która posiada obiekt self
        public void DopiszGrupe(VectoredRectangleGroup aGroup)
        {
            foreach(KeyValuePair<int, Vector_Rectangle> vectObj in aGroup)
            {
                vectObj.Value.parentVectorGroup = parentVectorGroup;
                vectObj.Value.parentVectorGroupId = parentVectorGroupId;
                parentVectorGroup.Add(parentVectorGroup.Count, vectObj.Value);
            }
        }

        public Point GetP(int aIdx)
        {
            return points.ElementAt(aIdx).Value;
        }

        //podaje odległość punktu 0 obiektu(self) od linji wyznaczonej równaniem z parametrów
        public float Distance(int a, int c, float mian)
        {
            Point p = GetP(0);
            return Math.Abs(a * p.X - p.Y + c) / mian;
        }

        //czy odległość punktu 0 obiektu(self) od linji wyznaczonej równaniem z parametrów <= dst 
        public bool InDistance(int a, int c, float mian, float dst)
        {
            return Distance(a, c, mian) <= dst;
        }

    }
}
