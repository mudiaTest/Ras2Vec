using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;


namespace Migracja
{

    static class ExtDictionary
    {
        public static int NextKey( this Dictionary<int, VectoredRectangleGroup> dict)
        {//warunek jest konieczny, bo w przypadku pustego zbioru rzucany jest wyjątek
            if (dict.Keys.Count == 0)
                return 0;
            else
                return dict.Keys.Max()+1;
        }

        public static int NextKey( this Dictionary<int, Vector_Rectangle> dict)
        {
            if (dict.Keys.Count == 0)
                return 0;
            else
                return dict.Keys.Max()+1;
        }

        public static int NextKey(this Dictionary<int, VectorRectangeGroup> dict)
        {
            if (dict.Keys.Count == 0)
                return 0;
            else
                return dict.Keys.Max() + 1;
        }

        public static int NextKey(this Dictionary<int, PosterizedColorData> dict)
        {
            if (dict.Keys.Count == 0)
                return 0;
            else
                return dict.Keys.Max() + 1;
        }
    }

    public class DictSerializeItem
    {
        [XmlAttribute]
        public int id;
        [XmlAttribute]
        public int R;
        [XmlAttribute]
        public int G;
        [XmlAttribute]
        public int B;
        [XmlAttribute]
        public int RMin;
        [XmlAttribute]
        public int RMax;
        [XmlAttribute]
        public int GMin;
        [XmlAttribute]
        public int GMax;
        [XmlAttribute]
        public int BMin;
        [XmlAttribute]
        public int BMax;
    }
}
