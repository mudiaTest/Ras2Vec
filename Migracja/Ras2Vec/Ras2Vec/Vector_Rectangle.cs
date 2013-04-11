using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Ras2Vec
{
    //obiekt wektorowego kwadratu
    class Vector_Rectangle: Vector_Gen
    {
        //lewy górny róg kwadratu
        public Point p1{ get{return points.ElementAt(0).Value;}
                         set{points.Add(0, value);}
        }
        //prawy dolny róg kwadratu
        public Point p2{ get{return points.ElementAt(3).Value;}
                         set{points.Add(3, value);}
        }

        public Vector_Rectangle(Color aColor, Point aP1, Point aP2)
        {
            color = aColor;
            p1 = aP1;
            p2 = aP2;
        }

           
        static void Zintegruj(Vector_Rectangle aObj1, Vector_Rectangle aObj2, TMapFactory aMapFactory)
        {
            
        }
    }
}
