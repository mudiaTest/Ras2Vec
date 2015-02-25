using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Migracja
{
    public partial class MainWindow : Form
    {
        enum MovedPicture { source, desination };

        private void StartMovingPictures()
        {
            blMouseInMoveMode = true;
            mouseDownSourcePBLeft = sourceVectPB.Left;
            mouseDownSourcePBTop = sourceVectPB.Top;
            mouseDownDesinationPBLeft = destinationVectPB.Left;
            mouseDownDesinationPBTop = destinationVectPB.Top;
        }

        private void StopMovingPictures(MovedPicture picture)
        {
            float dpShift = float.Parse(txtScaleLvlVect.Text);
            switch (picture)
            {
                //centrum oglądanego obszaru przesówamy o przesunięcię myszą z uwzględnieniem sklai
                case MovedPicture.source:
                    {
                        sourceImage4VectCrooper.centerX += (int)Math.Round((mouseDownSourcePBLeft - sourceVectPB.Left) / dpShift);
                        sourceImage4VectCrooper.centerY += (int)Math.Round((mouseDownSourcePBTop - sourceVectPB.Top) / dpShift);
                        desinationImage4VectCrooper.centerX = sourceImage4VectCrooper.centerX;
                        desinationImage4VectCrooper.centerY = sourceImage4VectCrooper.centerY;
                        break;
                    }
                case MovedPicture.desination:
                    {
                        desinationImage4VectCrooper.centerX += (int)Math.Round((mouseDownDesinationPBLeft - destinationVectPB.Left) / dpShift);
                        desinationImage4VectCrooper.centerY += (int)Math.Round((mouseDownDesinationPBTop - destinationVectPB.Top) / dpShift);
                        sourceImage4VectCrooper.centerX = desinationImage4VectCrooper.centerX;
                        sourceImage4VectCrooper.centerY = desinationImage4VectCrooper.centerY;
                        break;
                    }
            }
            windowSettings.centerX = sourceImage4VectCrooper.centerX;
            windowSettings.centerY = sourceImage4VectCrooper.centerY;
            DrawCroppedScaledImage4Vect(dpShift, UpdateInfoBoxTime);
            blMouseInMoveMode = false;
        }

        private void MovePictures(MouseEventArgs e)
        {
            if (blMouseInMoveMode)
            {
                horChange = e.X - startingX;
                verChange = e.Y - startingY;

                sourceVectPB.Left = Math.Min(0, Math.Max(sourceVectPB.Left + horChange, sourceVectPanel.Width - sourceVectPB.Image.Width));
                sourceVectPB.Top = Math.Min(0, Math.Max(sourceVectPB.Top + verChange, sourceVectPanel.Height - sourceVectPB.Image.Height));
                destinationVectPB.Left = Math.Min(0, Math.Max(destinationVectPB.Left + horChange, destinationVectPanel.Width - destinationVectPB.Image.Width));
                destinationVectPB.Top = Math.Min(0, Math.Max(destinationVectPB.Top + verChange, destinationVectPanel.Height - destinationVectPB.Image.Height));
                UpdateInfoBox("startingX: " + startingX.ToString() + "\n" +
                              "startingY: " + startingY.ToString() + "\n" +
                              "horChange: " + horChange.ToString() + "\n" +
                              "verChange" + verChange.ToString() + "\n" +
                              "e.X" + e.X.ToString() + "\n" +
                              "e.Y" + e.Y.ToString());
            }
            else
            {
                startingX = e.X;
                startingY = e.Y;
            }

        }

        private void SetScaleControlEnable(bool aEnabled){
            btnZoomInVect.Enabled = aEnabled;
            btnZoomOutVect.Enabled = aEnabled;
            trScaleVect.Enabled = aEnabled;
        }

        //metoda po wczytaniu pliku save inicjalizuje obraz źródłowy i czyści docelowy. 
        //Buduje na nowo też obiekty sourceImageCropper i destinationImageCropper
        private void PrepareSourceImage(String aPath)
        {
            sourceBmp4Col = new Bitmap(aPath);
            destinationBmp = null;
            sourceImage4ColCropper = new RaserImageCrooper(new Size(sourceColPanel.Width, sourceColPanel.Height), sourceBmp4Col);
            destinationImage4ColCrooper = new RaserImageCrooper(new Size(destinationColPanel.Width, destinationColPanel.Height), sourceBmp4Col);
            /*sourceImage4VectCropper = new RaserImageCrooper(new Size(sourceVectPanel.Width, sourceVectPanel.Height), sourceBmp);*/
           /* desinationImage4VectCrooper = new VectorImageCrooper(new Size(destinationVectPanel.Width, destinationVectPanel.Height), mapFactory,
                                                            sourceImage4VectCrooper.centerX, sourceImage4VectCrooper.centerY, windowSettings,
                                                            sourceBmp4Col);*/
        }

        private bool LoadImage(String aPath)
        {
            if (File.Exists(aPath))
            {
                PrepareSourceImage(aPath);
                DrawCroppedScaledImage4Col(float.Parse(txtScaleLvlVect.Text), UpdateInfoBoxTime);
                /*DrawCroppedScaledImage4Vect(float.Parse(txtScaleLvlVect.Text), UpdateInfoBoxTime);*/
                SetScaleControlEnable(true);
                return true;
            }
            return false;
        }

        private bool DrawCroppedScaledImage4Col(float aDpScale, UpdateInfoBoxTimeDelegate aFunct = null, float? aDpScalePrev = null)
        {
            DateTime dtTimePrv = DateTime.Now;
            Bitmap croppedSrcBmp4Col = sourceImage4ColCropper.GetCroppedImage(aDpScale);

            //pozyskiwanie danych potrzebnych do odczytania, na który px kliknęliśmy
            Rectangle srcRect = sourceImage4ColCropper.GetSourceRectangle(aDpScale);
            Rectangle resultRect = sourceImage4ColCropper.GetDestinationRectangle(aDpScale, srcRect);
            srcLeftX = srcRect.X;
            srcLeftY = srcRect.Y;
            resultLeftX = resultRect.X;
            resultLeftY = resultRect.Y;

            dtTimePrv = aFunct("GetCroppedImage 1:", true, dtTimePrv);
            //tymczasowo przypisuję ten sam obraz
            //Bitmap croppedDstBmp = croppedSrcBmp;
            Bitmap croppedDstBmp;
            if (destinationImage4ColCrooper.mapFactory == null)
            {
                croppedDstBmp = null;
                dtTimePrv = aFunct("GetCroppedImage 2 NULL:", false, dtTimePrv);
            }
            else
            {
                croppedDstBmp = destinationImage4ColCrooper.GetCroppedImage(aDpScale, aFunct);
                dtTimePrv = aFunct("GetCroppedImage 2:", false, dtTimePrv);
            }

            /*int scaledShiftX = sourcePanel.Width;
            int scaledShiftY = sourcePanel.Height;*/
            int scaledShiftX = (int)Math.Round(sourceImage4ColCropper.centerX * aDpScale);
            int scaledShiftY = (int)Math.Round(sourceImage4ColCropper.centerY * aDpScale);

            //UpdateInfoBox("bmp: " + croppedSrcBmp.Width.ToString() + " x " + croppedSrcBmp.Height.ToString());
            sourceColPB.Height = croppedSrcBmp4Col.Height;
            sourceColPB.Width = croppedSrcBmp4Col.Width;
            sourceColPB.Image = croppedSrcBmp4Col;
            //UpdateInfoBox("pb: " + sourcePB.Width.ToString() + " x " + sourcePB.Height.ToString() +
            //              "L/T: " + sourcePB.Left.ToString() + " x " + sourcePB.Top.ToString());
            /*sourcePB.Left = -Math.Min(scaledShiftX, sourcePanel.Width);
            sourcePB.Top = -Math.Min(scaledShiftY, sourcePanel.Height);*/
            sourceColPB.Left = -sourceVectPanel.Width;
            sourceColPB.Top = -sourceVectPanel.Height;
            //UpdateInfoBox("L/T: " + sourcePB.Left.ToString() + " x " + sourcePB.Top.ToString(), false);

            if (croppedDstBmp != null)
            {
                destinationColPB.Height = croppedDstBmp.Height;
                destinationColPB.Width = croppedDstBmp.Width;
                destinationColPB.Image = croppedDstBmp;
                /*destinationColPB.Left = -Math.Min(scaledShiftX, destinationPanel.Width);
                destinationColPB.Top = -Math.Min(scaledShiftY, destinationPanel.Height);*/
                destinationColPB.Left = -destinationVectPanel.Width;
                destinationColPB.Top = -destinationVectPanel.Height;
            }
            aFunct("Pozostałe:", false, dtTimePrv);
            return true;
        }

        private bool DrawCroppedScaledImage4Vect(float aDpScale,  UpdateInfoBoxTimeDelegate aFunct = null, float? aDpScalePrev = null)
        {
            DateTime dtTimePrv = DateTime.Now;
            Bitmap croppedSrcBmp4Vect = sourceImage4VectCrooper.GetCroppedImage(aDpScale);

            //pozyskiwanie danych potrzebnych do odczytania, na który px kliknęliśmy
            Rectangle srcRect = sourceImage4VectCrooper.GetSourceRectangle(aDpScale);
            Rectangle resultRect = sourceImage4VectCrooper.GetDestinationRectangle(aDpScale, srcRect);
            srcLeftX = srcRect.X;
            srcLeftY = srcRect.Y;
            resultLeftX = resultRect.X;
            resultLeftY = resultRect.Y;

            dtTimePrv = aFunct("GetCroppedImage 1:", true, dtTimePrv);
            //tymczasowo przypisuję ten sam obraz
            //Bitmap croppedDstBmp = croppedSrcBmp;
            Bitmap croppedDstBmp;
            if (desinationImage4VectCrooper.mapFactory == null)
            {
                croppedDstBmp = null;
                dtTimePrv = aFunct("GetCroppedImage 2 NULL:", false, dtTimePrv);
            }
            else
            {
                croppedDstBmp = desinationImage4VectCrooper.GetCroppedImage(aDpScale, aFunct);
                dtTimePrv = aFunct("GetCroppedImage 2:", false, dtTimePrv);
            }
            
            /*int scaledShiftX = sourcePanel.Width;
            int scaledShiftY = sourcePanel.Height;*/
            int scaledShiftX = (int)Math.Round(sourceImage4VectCrooper.centerX * aDpScale);
            int scaledShiftY = (int)Math.Round(sourceImage4VectCrooper.centerY * aDpScale);

            //UpdateInfoBox("bmp: " + croppedSrcBmp.Width.ToString() + " x " + croppedSrcBmp.Height.ToString());
            sourceVectPB.Height = croppedSrcBmp4Vect.Height;
            sourceVectPB.Width = croppedSrcBmp4Vect.Width;
            sourceVectPB.Image = croppedSrcBmp4Vect;
            //UpdateInfoBox("pb: " + sourcePB.Width.ToString() + " x " + sourcePB.Height.ToString() +
            //              "L/T: " + sourcePB.Left.ToString() + " x " + sourcePB.Top.ToString());
            /*sourcePB.Left = -Math.Min(scaledShiftX, sourcePanel.Width);
            sourcePB.Top = -Math.Min(scaledShiftY, sourcePanel.Height);*/
            sourceVectPB.Left = -sourceVectPanel.Width;
            sourceVectPB.Top = -sourceVectPanel.Height;
            //UpdateInfoBox("L/T: " + sourcePB.Left.ToString() + " x " + sourcePB.Top.ToString(), false);

            if (croppedDstBmp != null)
            {
                destinationVectPB.Height = croppedDstBmp.Height;
                destinationVectPB.Width = croppedDstBmp.Width;
                destinationVectPB.Image = croppedDstBmp;
                /*destinationPB.Left = -Math.Min(scaledShiftX, destinationPanel.Width);
                destinationPB.Top = -Math.Min(scaledShiftY, destinationPanel.Height);*/
                destinationVectPB.Left = -destinationVectPanel.Width;
                destinationVectPB.Top = -destinationVectPanel.Height;
            }
            aFunct("Pozostałe:", false, dtTimePrv);
            return true;
        }

        private void LoadImage()
        {
            if (loadDialog.ShowDialog() == DialogResult.OK)
            {
                LoadImage(loadDialog.FileName);
                windowSettings.stSourceImagePath = loadDialog.FileName;
            }
        }

        private void ReloadImage(){
            if (windowSettings.stSourceImagePath != "")
            {
                if (File.Exists(windowSettings.stSourceImagePath))
                    LoadImage(windowSettings.stSourceImagePath);
                else
                {
                    Debug.Assert(false, "Nie powiadła się próba wgrania pliku źródłowego mapy: '" + windowSettings.stSourceImagePath + "'");
                }
            }
            else
            {
                Debug.Assert(false, "Pusta zmianna ze ściżką do pliku źródłowego mapy.");
            }
        }
    }
}
