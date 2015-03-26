using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace Migracja
{
    class ColorChanger
    {
        MainWindowSettings windowSettings;

        public ColorChanger(MainWindowSettings aWindowSettings)
        {
            windowSettings = aWindowSettings;
        }

        public void PosterizeBitmap(Bitmap aSrcBitmap, ref Bitmap aDstBitmap)
        {
            int i;
            int j;
            aDstBitmap = new Bitmap(aSrcBitmap);
            for (i = 0; i < aDstBitmap.Width; i++)
            {
                for (j = 0; j < aDstBitmap.Height; j++)
                {
                    aDstBitmap.SetPixel(i, j, GetNearestColor(aDstBitmap.GetPixel(i, j)));
                }
            }
        }

        private Color GetNearestColor(Color aColor)
        {
            /*int r;
            int g;
            int b;
            r = 255;
            g = 0;
            b = 0;*/
            float bestColor=9999; 
            PosterizedColorData nearestPostColData = null;
            float colorDist;

            PosterizedColorData postColData;
            for (int i = 0; i < windowSettings.dictColorData.Count; i++)
            {
                postColData = windowSettings.dictColorData[i];
                colorDist = postColData.GetColorDistance(aColor);
                if (nearestPostColData == null || colorDist < bestColor)
                {
                    bestColor = colorDist;
                    nearestPostColData = postColData;
                }
            }

            Debug.Assert(nearestPostColData != null, "Nie ustalono najbliższego odpowiadającego koloru.");
            return Color.FromArgb(nearestPostColData.garminColor.R, nearestPostColData.garminColor.G, nearestPostColData.garminColor.B);
        }

        

    }
}
