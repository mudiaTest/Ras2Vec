using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Diagnostics;

namespace Migracja
{
    abstract class Crooper
    {
        public Size panelSize;
        private int fCenterX;
        private int fCenterY;
        public Bitmap srcBmp;
        protected int srcBmpWidth;
        protected int srcBmpHeight;
        public MapFactory mapFactory;
            public int centerX 
            {
                get { return fCenterX; }
                set 
                { 
                    fCenterX = (int)Math.Max(0, value);
                    fCenterX = (int)Math.Min(fCenterX, srcBmpWidth); 
                }
            }
            public int centerY
            {
                get { return fCenterY; }
                set
                {
                    fCenterY = (int)Math.Max(0, value);
                    fCenterY = (int)Math.Min(fCenterY, srcBmpHeight);
                }
            }
        public int left;
        public int top;

        public Crooper(Size aPanelSize, int aSrcBmpHeight, int aSrcBmpWidth)
        {
            panelSize = aPanelSize;
            centerX = (int)Math.Round(aPanelSize.Width / (float)2);
            centerY = (int)Math.Round(aPanelSize.Height / (float)2);
            srcBmpHeight = aSrcBmpHeight;
            srcBmpWidth = aSrcBmpWidth;
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

        //ustala jaki wycinek oryginalnego obrazu ma zostać pokazany
        protected Rectangle GetSourceRectangle(float aScale)
        {
            //aby x,y nie były mniejsze niż punkt (0,0) - ograniczenie o lewej strony. 
            //Od prawej strony nie wyjdą poza ramy obrazu, bo x zawsze będzie < centerX, 
            //które to ma założony warunek
            int x = Math.Max(0, GetLeftAbsoluteOryg(aScale));
            int y = Math.Max(0, GetTopAbsoluteOryg(aScale));

            //ustalanie rozmiaru, tak aby dla mocnego przesuniącia wycinka w lewo pobrał tylko część wycinka
            int rectWidth = Math.Min(srcBmpWidth, GetRightAbsoluteOryg(aScale)) - Math.Max(GetLeftAbsoluteOryg(aScale), x);
            int rectHeight = Math.Min(srcBmpHeight, GetBottomAbsoluteOryg(aScale)) - Math.Max(GetTopAbsoluteOryg(aScale), y);

            return new Rectangle(x, y, rectWidth, rectHeight);
        }

        //ustala jak ma być położony oryginalny wycinek w nowopokazywanym fragmencie. Możliwe jest, że wycinek będzie 
        //przesunięty pokazując wolną przestrzeń na jednym z brzegów fragmentu.
        protected Rectangle GetDestinationRectangle(float aScale, Rectangle srcRectangle)
        {
            int resultRectX;
            int resultRectY;
            //jeśli wycinek jest mniejszy i ma być odsunięty od boku obrazu (sam wycinek był przyciśnięty do boku, więc 
            //prawdopodobnie postła wolna przestrzeń po lewej stronie do wypełnienia. Prawa soraona "wypełni" 
            //się automatycznie, bo mając przerwę z lewej strony i wklejony obrazek, przerwa po prawej stworzy się samoczynnie)
            if ((srcRectangle.Width * aScale < 3 * panelSize.Width) && (srcRectangle.X == 0))
                resultRectX = (int)Math.Ceiling(-GetLeftAbsoluteOryg(aScale) * aScale);//GetAbsolute... oddaje wartości dla oryginalnej wielkości/skala, a my 
            //obliczamy teraz przesunięcie na rozciągniętym obrazie, więc trzeba 
            //pomnożyć to przez skalę
            else
                resultRectX = 0;
            if ((srcRectangle.Height * aScale < 3 * panelSize.Height) && (srcRectangle.Y == 0))
                resultRectY = (int)Math.Ceiling(-GetTopAbsoluteOryg(aScale) * aScale);
            else
                resultRectY = 0;

            return new Rectangle(resultRectX,
                                 resultRectY,
                                 (int)Math.Ceiling(srcRectangle.Width * aScale),
                                 (int)Math.Ceiling(srcRectangle.Height * aScale)
                                 );
        }

        public abstract Bitmap GetCroppedImage(float aScale, UpdateInfoBoxTimeDelegate aFunct = null);
   
    }

    class RaserImageCrooper: Crooper
    {
        public RaserImageCrooper(Size aPanelSize, Bitmap aSrcBmp)
            : base(aPanelSize, aSrcBmp.Height, aSrcBmp.Width)
        { 
            srcBmp = aSrcBmp; 
        }

        public override Bitmap GetCroppedImage(float aScale, UpdateInfoBoxTimeDelegate aFunct = null)
        {
            Rectangle rect = GetSourceRectangle(aScale);
            Bitmap result;
            Rectangle resultRect = GetDestinationRectangle(aScale, rect);

            //finalna bitmapa o odpowiednim rozmiarze
            result = new Bitmap(3 * panelSize.Width, 3 * panelSize.Height);
            //ustawia, że result będzie płótnem graphics
            Graphics graphics = Graphics.FromImage(result);
            //ustawia sposób zmiękczania przy powiększaniu - NN da brak zmiękczania
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            //przepisze źródło na cel odpowiednio skalująć. W tym przypadku źódło i cel to result
            //nowy obszar jest większy od startego więc nastąpi rozciągnięcie pixeli zgodnie z InterpolationMode
            graphics.DrawImage(srcBmp,
                                resultRect, //nowy obszar
                                rect, //originalny obszar
                                System.Drawing.GraphicsUnit.Pixel);

            return result;
        }
    }

    class VectorImageCrooper : Crooper
    {
        MainWindowSettings settings;
        public VectorImageCrooper(Size aPanelSize, MapFactory aMapFactory, int aCenterX, int aCenterY, MainWindowSettings aSettings, Bitmap aSrcBmp)
            : base(aPanelSize, aSrcBmp.Height, aSrcBmp.Width)
        {
            mapFactory = aMapFactory;
            settings = aSettings;
            if (aMapFactory != null)
            {
                srcBmpWidth = aMapFactory.vectArr.Length;
                Debug.Assert(aMapFactory.vectArr[0] != null, "aMapFactory.vectArr[0] jest null");
                srcBmpHeight = aMapFactory.vectArr[0].Length;
            }
            centerX = aCenterX;
            centerY = aCenterY;
        }

        public override Bitmap GetCroppedImage(float aScale, UpdateInfoBoxTimeDelegate aFunct = null)
        {
            DateTime dtStart = DateTime.Now;
            Rectangle rect = GetSourceRectangle(aScale);
            Bitmap result;
            Rectangle resultRect = GetDestinationRectangle(aScale, rect);
            //Rectangle resultRect = new Rectangle(0, 0, (int)Math.Round(srcBmpWidth * aScale), (int)Math.Round(srcBmpHeight * aScale));

            //finalna bitmapa o odpowiednim rozmiarze
            result = new Bitmap(3 * panelSize.Width, 3 * panelSize.Height);
            Graphics graphics = Graphics.FromImage(result);
            //Brush h = new Brush();
            //Pen localPen = new Pen(new Brush() );

            Vector_Rectangle[][] vectArr = mapFactory.vectArr;
            aFunct("  Faza1", false, dtStart);
            //rysowanie rectangli odzwierciedlających pixele wzorcowego obrazka
            if (!settings.Polygons() || aScale == 1)
            {
                //będziemy poruszać się po ustalonym wycinku wzorcowego obrazu
                for (int x = 0; x < rect.Width; x++)
                {
                    for (int y = 0; y < rect.Height; y++)
                    {
                        int sourceX = x + rect.X;
                        int sourceY = y + rect.Y;
                        graphics.FillRectangle(new SolidBrush(vectArr[sourceX][sourceY].color), (x) * aScale + resultRect.X, (y) * aScale + resultRect.Y, aScale, aScale);
                        if (settings.Edges() && aScale >= 4)
                            graphics.DrawRectangle(new Pen(Color.Red), (x) * aScale + resultRect.X, (y) * aScale + resultRect.Y, aScale, aScale);
                        //graphics.DrawRectangle(localPen, x/**aScale*/, y/**aScale*/, /*aScale, aScale*/ 100, 100);
                        //result.SetPixel(x + resultRect.X, y + resultRect.Y, vectArr[x][y].color);
                        /*for (int xScale = 0; xScale < aScale; xScale++)
                        {
                            for (int yScale = 0; yScale < aScale; yScale++)
                            {
                                result.SetPixel(x + resultRect.X, y + resultRect.Y, vectArr[x][y].color);
                            }
                        }   */
                    }
                }
            }
            //rysowanie polygonów
            else
            {
                Color tmpColor = new Color();
                int tmpLineR;
                int tmpLineG;
                int tmpLineB;
                int tmpPointR;
                int tmpPointG;
                int tmpPointB;

                Boolean[][] usedArray = new Boolean[rect.Width + rect.X][];
                for (int i = 0; i < rect.Width + rect.X; i++)
                {
                    usedArray[i] = new Boolean[rect.Height + rect.Y];
                    for (int j = 0; j < rect.Height; j++)
                    {
                        usedArray[i][j] = false;
                    }

                }
                
                //będziemy poruszać się po ustalonym wycinku wzorcowego obrazu
                for (int x = 0; x < rect.Width; x++)
                {
                    for (int y = 0; y < rect.Height; y++)
                    {
                        int sourceX = x + rect.X;
                        int sourceY = y + rect.Y;

                        VectoredRectangleGroup group = mapFactory.GetGroupByXY(sourceX, sourceY, usedArray);

                        //foreach (VectoredRectangleGroup group in mapFactory.Values)
                        if(group != null)
                        {
                            Point[] granica;
                            if (settings.ShowSimplifiedEdge())
                            {
                                Debug.Assert(group.pointArrFromSimplifiedEdge != null,
                                             "Tablica pointArrFromSimplifiedEdge nie została zainicjalizowana.");
                                Debug.Assert(group.pointArrFromSimplifiedEdge.Length != 0,
                                             "Tablica pointArrFromSimplifiedEdge jest pusta.");
                                granica = group.GetScaledPointArrFromSimplifiedEdge(aScale,
                                                                                            resultRect.X - rect.X * aScale,
                                                                                            resultRect.Y - rect.Y * aScale);
                            }
                            else
                            {
                                Debug.Assert(group.pointArrFromFullEdge != null,
                                             "Tablica pointArrFromFullEdge nie została zainicjalizowana.");
                                Debug.Assert(group.pointArrFromFullEdge.Length != 0,
                                             "Tablica pointArrFromFullEdge jest pusta.");
                                granica = group.GetScaledPointArrFromFullEdge(aScale,
                                                                                      resultRect.X - rect.X * aScale,
                                                                                      resultRect.Y - rect.Y * aScale);
                                                                                            
                            }


                            graphics.FillPolygon(new SolidBrush(group.sourceColor), granica);
                            if (settings.Edges())
                            {
                                if (!settings.TestColor())
                                {
                                    Math.DivRem(group.sourceColor.R + 100, 256, out tmpLineR);
                                    Math.DivRem(group.sourceColor.G + 200, 256, out tmpLineG);
                                    Math.DivRem(group.sourceColor.B + 10, 256, out tmpLineB);

                                    Math.DivRem(group.sourceColor.R + 200, 256, out tmpPointR);
                                    Math.DivRem(group.sourceColor.G + 10, 256, out tmpPointG);
                                    Math.DivRem(group.sourceColor.B + 100, 256, out tmpPointB);
                                }
                                else
                                {
                                    tmpLineR = 255;
                                    tmpLineG = 0;
                                    tmpLineB = 0;
                                    tmpPointR = 100;
                                    tmpPointG = 255;
                                    tmpPointB = 255;
                                }

                                graphics.DrawPolygon(new Pen(Color.FromArgb(tmpLineR, tmpLineG, tmpLineB)), granica);
                                foreach (Point p in granica)
                                {
                                    //graphics.DrawLine(new Pen(Color.Blue), p, p);
                                    if (p.X < result.Width && p.Y < result.Height && p.X > 0 && p.Y > 0)
                                        result.SetPixel(p.X, p.Y, Color.FromArgb(tmpPointR, tmpPointG, tmpPointB));
                                }
                            }
                            //group.edgeList
                        }
                    }
                }
            }



                /*//ustawia, że result będzie płótnem graphics
                Graphics graphics = Graphics.FromImage(result);
                //ustawia sposób zmiękczania przy powiększaniu - NN da brak zmiękczania
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                //przepisze źródło na cel odpowiednio skalująć. W tym przypadku źódło i cel to result
                //nowy obszar jest większy od startego więc nastąpi rozciągnięcie pixeli zgodnie z InterpolationMode

                graphics.DrawImage(srcBmp,
                                    resultRect, //nowy obszar
                                    rect, //originalny obszar
                                    System.Drawing.GraphicsUnit.Pixel);*/

                return result;

        }
    }
}