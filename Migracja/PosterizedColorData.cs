using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Migracja
{
    public class PosterizedColorData
    {
        public double gravity = 1;
        public Color garminColor;
        public Color rasterColor;

        public PosterizedColorData()
        {
            garminColor = new Color();
            rasterColor = new Color();
        }
    }
}
