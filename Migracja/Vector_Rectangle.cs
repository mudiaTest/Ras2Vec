using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Migracja
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

        public static void Zintegruj(Vector_Rectangle aObj1, Vector_Rectangle aObj2, MapFactory aMapFactory)
        {
            Vector_Rectangle obj1;
            Vector_Rectangle obj2;
            int delId;
            int colorGroupListIdx;
            ColorGroupList colorGroupList;

            if ((aObj2.parentVectorGroup == null) || (aObj1.parentVectorGroup.lpGroup < aObj2.parentVectorGroup.lpGroup)) 
            {
                obj1 = aObj1;
                obj2 = aObj2;
            }
            else
            {
                obj1 = aObj2;
                obj2 = aObj1;
            };

            //jeśli sąsiad jest niezintegrowany i ma taki sam kolor
            if ((obj2.parentVectorGroup == null) && (obj2.color == obj1.color)) 
            //dodajemy do grupy obj na którym jesteśmy
            {
                obj2.parentVectorGroup = obj1.parentVectorGroup;
                obj2.parentVectorGroupId = obj1.parentVectorGroupId;
                obj2.parentVectorGroup.lpGroup = obj1.parentVectorGroup.lpGroup;
                obj1.parentVectorGroup.Add(obj1.parentVectorGroup.Count, obj2);
            //jeśli sąsiad jest ma grupę, ale ta grupa ma takisam kolor, to
            }
            else if ((obj2.parentVectorGroup != obj1.parentVectorGroup) && (obj2.color == obj1.color)) 
            {
                //jeśli nasza grupa jest liczniejsza, to dodajemy grupę sąsiada do naszej
                if (obj1.parentVectorGroup.Count > obj2.parentVectorGroup.Count) 
                {
                    delId = obj2.parentVectorGroupId;
                    obj1.DopiszGrupe(obj2.parentVectorGroup);
                    colorGroupListIdx = obj2.parentVectorGroup.sourceColor.ToArgb();
                }
                //jeśli grupa sąsiadaj est silniejsza, to dołączamy się do niej
                else
                {
                    delId = obj1.parentVectorGroupId;
                    obj2.DopiszGrupe(obj1.parentVectorGroup);
                    colorGroupListIdx = obj1.parentVectorGroup.sourceColor.ToArgb();
                };
                //usunięcie przepisanej grupy z listy grup
                //assert(aMapFactory.indexOf(delIdx) >== 0, 'Brak grupy do usunięcia: ' + intToStr(delIdx) + '.');
                aMapFactory.Remove(delId);

                colorGroupList = aMapFactory.vectRectGroupsByColor[colorGroupListIdx];
                colorGroupList.Remove(delId);
            };
        }

        // !! TO TYLKO PROTEZA KLONOWANIA !!
        internal Vector_Rectangle Clone()
        {
            Vector_Rectangle result = new Vector_Rectangle(color, new Point(p1.X, p1.Y), new Point(p2.X, p2.Y));
            result.parentVectorGroup = parentVectorGroup;
            result.parentVectorGroupId = parentVectorGroupId;
            return result;
        }
    }
}
