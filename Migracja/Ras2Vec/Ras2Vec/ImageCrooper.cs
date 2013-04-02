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
        public Size panelSize;
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

            //ustalanie rozmiaru, tak aby dla mocnego przesuniącia wycinka w prawo nie pobierał 
            //wycinka z poza oryginalnego obrazu (co spowoduje wyjątek)
            int rectWidth = Math.Min((int)Math.Ceiling(3 * panelSize.Width / aScale), 
                                     aSrcBmp.Width - x);
            int rectHeight = Math.Min((int)Math.Ceiling(3 * panelSize.Height / aScale), 
                                      aSrcBmp.Height - y);
            Rectangle rect = new Rectangle(x, y, rectWidth, rectHeight); 
            Bitmap result;
            if (aScale == 1)
            {
                //utworzenie przyciętej bitmapy z oryginalnej bitmapy
                result = (Bitmap)aSrcBmp.Clone(rect, aSrcBmp.PixelFormat);
            }
            else /*{if (aScale > 1)*/
            {
                //finalna bitmapa o odpowiednim rozmiarze
                result = new Bitmap((int)Math.Round(rectWidth * aScale), (int)Math.Round(rectHeight * aScale));
                //ustawia, że result będzie płutnem graphics
                Graphics graphics = Graphics.FromImage(result);
                //ustawia sposób zmiękczania przy powiększaniy - NN da brak zmiękczania
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                //przepisze źródło na cel odpowiednio skalująć. W tym przypadku źódło i cel to result
                //nowy obszar jest większy od startego więc nastąpi rozciągnięcie pixeli zgodnie z InterpolationMode
                graphics.DrawImage(aSrcBmp, 
                                   new Rectangle(0, 0, (int)Math.Round(rectWidth * aScale), (int)Math.Round(rectHeight * aScale)), /*nowy obszar*/
                                   rect, /*originalny obszar*/
                                   System.Drawing.GraphicsUnit.Pixel);
            }
            /*else
            {
                result = null;
            }*/
            return result;
        }
    }
}