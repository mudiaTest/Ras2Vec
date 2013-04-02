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
            public int centerX 
            {
                get { return fCenterX; }
                set 
                { 
                    fCenterX = (int)Math.Max(0, value);
                    fCenterX = (int)Math.Min(fCenterX, panelSize.Width); 
                }
            }
            public int centerY
            {
                get { return fCenterY; }
                set
                {
                    fCenterY = (int)Math.Max(0, value);
                    fCenterY = (int)Math.Min(fCenterY, panelSize.Height);
                }
            }
        public int left;
        public int top;

        public ImageCrooper(Size aPanelSize)
        {
            panelSize = aPanelSize;
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


        public Bitmap GetCroppedImage(Bitmap aSrcBmp, float aScale)
        {
            /*//aby x,y nie były mniejsze niż punkt (0,0) - ograniczenie o lewej strony
            int x = Math.Max(0, centerX - (int)Math.Ceiling(panelSize.Width / aScale));
            int y = Math.Max(0, centerY - (int)Math.Ceiling(panelSize.Height / aScale));

            //aby x,y nie były większe niż odległość 2-krotnej szerokości (pokazywanego obrazu) 
            //od rozmiaru oryginalnego obrazu - ograniczenie od prawej strony obrazu
            x = Math.Min(x, aSrcBmp.Width - (int)Math.Ceiling(2 * (panelSize.Width / aScale)));
            y = Math.Min(y, aSrcBmp.Height - (int)Math.Ceiling(2 * (panelSize.Height / aScale)));

            //ustalanie rozmiaru, tak aby dla mocnego przesuniącia wycinka w prawo nie pobierał 
            //wycinka z poza oryginalnego obrazu (co spowoduje wyjątek)
            int rectWidth = Math.Min((int)Math.Ceiling(3 * panelSize.Width / aScale), 
                                     aSrcBmp.Width - x);
            int rectHeight = Math.Min((int)Math.Ceiling(3 * panelSize.Height / aScale), 
                                      aSrcBmp.Height - y);*/

            //aby x,y nie były mniejsze niż punkt (0,0) - ograniczenie o lewej strony
            int x = Math.Max(0, GetLeftAbsoluteOryg(aScale));
            int y = Math.Max(0, GetTopAbsoluteOryg(aScale));

            //aby x,y nie były większe niż odległość 2-krotnej szerokości (pokazywanego obrazu) 
            //od rozmiaru oryginalnego obrazu - ograniczenie od prawej strony obrazu
            x = Math.Min(x, aSrcBmp.Width - (int)Math.Ceiling(0.5 * panelSize.Width / aScale));
            y = Math.Min(y, aSrcBmp.Height - (int)Math.Ceiling(0.5 * panelSize.Height / aScale));

            //ustalanie rozmiaru, tak aby dla mocnego przesuniącia wycinka w lewo pobrał tylko część wycinka
            int rectWidth = Math.Min(GetRightAbsoluteOryg(aScale), (int)Math.Ceiling(3 * panelSize.Width / aScale));
            int rectHeight = Math.Min(GetBottomAbsoluteOryg(aScale), (int)Math.Ceiling(3 * panelSize.Height / aScale));

            //ustalanie rozmiaru, tak aby dla mocnego przesuniącia wycinka w prawo pobrał tylko część wycinka
            rectWidth = Math.Min(aSrcBmp.Width - GetLeftAbsoluteOryg(aScale), (int)Math.Ceiling(3 * panelSize.Width / aScale));
            rectHeight = Math.Min(aSrcBmp.Height - GetTopAbsoluteOryg(aScale), (int)Math.Ceiling(3 * panelSize.Height / aScale));


            //sHiftX = centerX - (int)Math.Round(1.5 * rectWidth);
            //sHiftY = centerY - (int)Math.Round(1.5 * rectHeight);


            Rectangle rect = new Rectangle(x, y, rectWidth, rectHeight);
 
            Bitmap result;
            /*if (aScale == 1)
            {
                //utworzenie przyciętej bitmapy z oryginalnej bitmapy
                result = (Bitmap)aSrcBmp.Clone(rect, aSrcBmp.PixelFormat);
            }
            else *//*{if (aScale > 1)*/
            {
                int resultRectX;
                int resultRectY;
                if ((rectWidth * aScale * 3 < 3 * panelSize.Width) & (x == 0))
                {
                    resultRectX = (int)Math.Ceiling(3 * panelSize.Width - rectWidth * aScale * 3);
                }
                else
                {
                    resultRectX = 0;
                    left = 0;
                }


                if ((rectHeight * aScale * 3 < 3 * panelSize.Height) & (y == 0))
                {
                    resultRectY = (int)Math.Ceiling(3 * panelSize.Height - rectHeight * aScale * 3);
                }
                else
                {
                    resultRectY = 0;
                    top = 0;
                }

                Rectangle resultRect = new Rectangle(resultRectX,
                                                     resultRectY,
                                                     3 * panelSize.Width,
                                                     3 * panelSize.Height
                                                     );

                //finalna bitmapa o odpowiednim rozmiarze
                result = new Bitmap((int)Math.Round(rectWidth * aScale), (int)Math.Round(rectHeight * aScale));
                //ustawia, że result będzie płutnem graphics
                Graphics graphics = Graphics.FromImage(result);
                //ustawia sposób zmiękczania przy powiększaniy - NN da brak zmiękczania
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                //przepisze źródło na cel odpowiednio skalująć. W tym przypadku źódło i cel to result
                //nowy obszar jest większy od startego więc nastąpi rozciągnięcie pixeli zgodnie z InterpolationMode
                graphics.DrawImage(aSrcBmp,
                                   resultRect, /*nowy obszar*/
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