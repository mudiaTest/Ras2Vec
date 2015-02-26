using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Migracja
{
    class ColorChanger
    {
        public void PosterizeBitmap(Bitmap aSrcBitmap, ref Bitmap aDstBitmap)
        {
            int i;
            int j;
            aDstBitmap = new Bitmap(aSrcBitmap);
            for (i = 0; i < aDstBitmap.Width; i++)
            {
                for (j = 0; j < aDstBitmap.Height; j++)
                {
                    aDstBitmap.SetPixel(i, j, Color.FromArgb(255, 0, 0));
                }
            }
        }
    }
}
