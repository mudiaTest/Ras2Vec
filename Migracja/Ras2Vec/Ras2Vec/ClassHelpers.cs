using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ras2Vec
{
    static class ExtDictionary
    {
        public static int NextKey( this Dictionary<int, VectoredRectangleGroup> dict)
        {
            return dict.Keys.Max();
        }

        public static int NextKey( this Dictionary<int, Vector_Rectangle> dict)
        {
            return dict.Keys.Max();
        }
    }
}
