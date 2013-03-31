using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace Ras2Vec
{
    public class ImageProjector
    {
        private Size panelSize;
        public int shiftX;
        public int shiftY;

        public ImageProjector(Size aPanelSize)
        {
            panelSize = aPanelSize;
            shiftX = 0;
            shiftY = 0;
        }

        public Bitmap GetCroppedImage(Bitmap aSrcBmp, float aScale)
        {
            int x = Math.Max(0, shiftX - (int)Math.Ceiling(panelSize.Width / aScale));
            int y = Math.Max(0, shiftY - (int)Math.Ceiling(panelSize.Height / aScale));
            x = Math.Min(x, aSrcBmp.Width - (int)Math.Ceiling(3 * panelSize.Width / aScale));
            y = Math.Min(y, aSrcBmp.Height - (int)Math.Ceiling(3 * panelSize.Height / aScale));

            //Bitmap result = new Bitmap(1,1);
            Rectangle rect = new Rectangle(x, y,
                                           (int)Math.Ceiling(3 * panelSize.Width / aScale),
                                           (int)Math.Ceiling(3 * panelSize.Height / aScale));
            Bitmap result = (Bitmap)aSrcBmp.Clone(rect, aSrcBmp.PixelFormat);
            return result;
        }
    }
}