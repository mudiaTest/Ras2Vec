using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Migracja
{
    class PosterizedColorData
    {
        int lpRedMin = -1;
        int lpRedMax = -1;
        int lpGreenMin = -1;
        int lpGreenMax = -1;
        int lpBlueMin = -1;
        int lpBlueMax = -1;
        Color garminColor;

        public PosterizedColorData()
        {
            garminColor = new Color();
        }
    }
}
