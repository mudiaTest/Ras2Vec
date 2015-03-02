using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Migracja
{
    public class PosterizedColorData
    {
        public int lpRedMin = -1;
        public int lpRedMax = -1;
        public int lpGreenMin = -1;
        public int lpGreenMax = -1;
        public int lpBlueMin = -1;
        public int lpBlueMax = -1;
        public Color garminColor;

        public PosterizedColorData()
        {
            garminColor = new Color();
        }
    }
}
