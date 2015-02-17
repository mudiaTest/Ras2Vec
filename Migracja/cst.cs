using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Migracja
{
    class Cst
    {
        internal const int fromLeft = 0;
        internal const int fromTop = 1;
        internal const int fromRight = 2;
        internal const int fromBottom = 3;

        internal const int goTop = 0;
        internal const int goRight = 1;
        internal const int goBottom = 2;
        internal const int goLeft = 3;

        internal const string progName = "Ras2Vec";

        internal const int maxZoom = 10;

        //typ geoPunktu
        internal const int c_geoPxSimple = 1; /*punkt "zwykły" zero obostrzeń. Z początku może być ich wiele. 
                                               *Podczas pierwszego upraszczania granicy usunięte zostaną wszystkie punkty 
                                               *zwykłe, które nie są rogami granicy
                                               */
        internal const int c_geoPxStartEnd = 2; //punkt będacy startem/końcem granicy
        internal const int c_geoPxDontDelete = 3; //punkt w najbliższej okolicy którego zachodzi zmiana grup
        internal const int c_geoPxDonePhase2 = 4; //punkt był już poddany próbie upraszczania podczas fazy 2

        internal const int N = 1;
        internal const int S = 2;
        internal const int W = 3;
        internal const int E = 4;
        internal const int NW = 5;
        internal const int NE = 6;
        internal const int SW = 7;
        internal const int SE = 8;
    }
}
