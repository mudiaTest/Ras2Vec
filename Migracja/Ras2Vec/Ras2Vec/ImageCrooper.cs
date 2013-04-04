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
        private int fCenterX;
        private int fCenterY;
        public Bitmap srcBmp;
            public int centerX 
            {
                get { return fCenterX; }
                set 
                { 
                    fCenterX = (int)Math.Max(0, value);
                    fCenterX = (int)Math.Min(fCenterX, srcBmp.Width); 
                }
            }
            public int centerY
            {
                get { return fCenterY; }
                set
                {
                    fCenterY = (int)Math.Max(0, value);
                    fCenterY = (int)Math.Min(fCenterY, srcBmp.Height);
                }
            }
        public int left;
        public int top;

        public ImageCrooper(Size aPanelSize,  Bitmap aSrcBmp )
        {
            panelSize = aPanelSize;
            srcBmp = aSrcBmp;
            centerX = (int)Math.Round(aPanelSize.Width / (float)2);
            centerY = (int)Math.Round(aPanelSize.Height / (float)2);
        }

        public int GetLeftAbsoluteOryg(float aScale)
        {
            return centerX - (int)Math.Ceiling(1.5 * panelSize.Width / aScale);
        }

        public int GetTopAbsoluteOryg(float aScale)
        {
            return centerY - (int)Math.Ceiling(1.5 * panelSize.Height / aScale);
        }

        public int GetRightAbsoluteOryg(float aScale)
        {
            return GetLeftAbsoluteOryg(aScale) + (int)Math.Ceiling(3 * panelSize.Width / aScale);
        }

        public int GetBottomAbsoluteOryg(float aScale)
        {
            return GetTopAbsoluteOryg(aScale) + (int)Math.Ceiling(3 * panelSize.Height / aScale);
        }


        public Bitmap GetCroppedImage(float aScale)
        {
            //aby x,y nie były mniejsze niż punkt (0,0) - ograniczenie o lewej strony. 
            //Od prawej strony nie wyjdą poza ramy obrazu, bo x zawsze będzie < centerX, 
            //które to ma założony warunek
            int x = Math.Max(0, GetLeftAbsoluteOryg(aScale));
            int y = Math.Max(0, GetTopAbsoluteOryg(aScale));

            //ustalanie rozmiaru, tak aby dla mocnego przesuniącia wycinka w lewo pobrał tylko część wycinka
            int rectWidth =  Math.Min(srcBmp.Width, GetRightAbsoluteOryg(aScale)) - Math.Max(GetLeftAbsoluteOryg(aScale), x);
            int rectHeight = Math.Min(srcBmp.Height, GetBottomAbsoluteOryg(aScale)) - Math.Max(GetTopAbsoluteOryg(aScale), y);

            Rectangle rect = new Rectangle(x, y, rectWidth, rectHeight);
            Bitmap result;
            int resultRectX;
            int resultRectY;
            //jeśli wycinek jest mniejszy i ma być odsunięty od boku obrazu (sam wycinek był przyciśnięty do boku, więc 
            //prawdopodobnie postła wolna przestrzeń po lewej stronie do wypełnienia. Prawa soraona "wypełni" 
            //się automatycznie, bo mając przerwę z lewej strony i wklejony obrazek, przerwa po prawej stworzy się samoczynnie)
            if ((rectWidth * aScale < 3 * panelSize.Width) & (x == 0))
                resultRectX = (int)Math.Ceiling(-GetLeftAbsoluteOryg(aScale) * aScale);//GetAbsolute... oddaje wartości dla oryginalnej wielkości/skala, a my 
                                                                                       //obliczamy teraz przesunięcie na rozciągniętym obrazie, więc trzeba 
                                                                                       //pomnożyć to przez skalę
            else
                resultRectX = 0;
            if ((rectHeight * aScale < 3 * panelSize.Height) & (y == 0))
                resultRectY = (int)Math.Ceiling(-GetTopAbsoluteOryg(aScale) * aScale);
            else
                resultRectY = 0;

            Rectangle resultRect = new Rectangle(resultRectX,
                                                    resultRectY,
                                                    (int)Math.Ceiling(rectWidth * aScale),
                                                    (int)Math.Ceiling( rectHeight * aScale)
                                                    );

            //finalna bitmapa o odpowiednim rozmiarze
            result = new Bitmap(3 * panelSize.Width, 3 * panelSize.Height);
            //ustawia, że result będzie płutnem graphics
            Graphics graphics = Graphics.FromImage(result);
            //ustawia sposób zmiękczania przy powiększaniy - NN da brak zmiękczania
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            //przepisze źródło na cel odpowiednio skalująć. W tym przypadku źódło i cel to result
            //nowy obszar jest większy od startego więc nastąpi rozciągnięcie pixeli zgodnie z InterpolationMode
            graphics.DrawImage(srcBmp,
                                resultRect, /*nowy obszar*/
                                rect, /*originalny obszar*/
                                System.Drawing.GraphicsUnit.Pixel);

            return result;
        }
    }
}