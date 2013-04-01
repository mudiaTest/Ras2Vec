using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Ras2Vec
{
    public class ImageCrooper
    {
        private Size panelSize;
        public int shiftX;
        public int shiftY;

        public ImageCrooper(Size aPanelSize)
        {
            panelSize = aPanelSize;
            shiftX = 0;
            shiftY = 0;
        }

        public Bitmap GetCroppedImage(Bitmap aSrcBmp, float aScale)
        {
            //aby x,y nie były mniejsze niż punkt (0,0) - ograniczenie o lewej strony
            int x = Math.Max(0, shiftX - (int)Math.Ceiling(panelSize.Width / aScale));
            int y = Math.Max(0, shiftY - (int)Math.Ceiling(panelSize.Height / aScale));

            //aby x,y nie były większe niż odległość 2-krotnej szerokości (pokazywanego obrazu) 
            //od rozmiaru oryginalnego obrazu - ograniczenie od prawej strony obrazu
            x = Math.Min(x, aSrcBmp.Width - (int)Math.Ceiling(2 * (panelSize.Width / aScale)));
            y = Math.Min(y, aSrcBmp.Height - (int)Math.Ceiling(2 * (panelSize.Height / aScale)));

            //ustalanie rozmiaru, tak aby dla mocnego prrzesuniącia wycinka w prawo nie pobierał 
            //wycinka z poza oryginalnego obrazu, co spowoduje wyjątek
            int rectWidth = Math.Min((int)Math.Ceiling(3 * panelSize.Width / aScale), 
                                     aSrcBmp.Width - x);
            int rectHeight = Math.Min((int)Math.Ceiling(3 * panelSize.Height / aScale), 
                                      aSrcBmp.Height - y);

            Rectangle rect = new Rectangle(x, y, rectWidth, rectHeight);
            //utworzenie przyciętej bitmapy
            Bitmap result = (Bitmap)aSrcBmp.Clone(rect, aSrcBmp.PixelFormat);
            //rozciągnięie przycientej bitmapy zgodnie ze skalą
            if (aScale!= 1)
                result = new Bitmap(result, (int)Math.Round(result.Width * aScale), (int)Math.Round(result.Height * aScale));
            return result;
        }
    }
}