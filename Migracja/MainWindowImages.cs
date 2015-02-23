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
                        sourceImageCropper.centerX += (int)Math.Round((mouseDownSourcePBLeft - sourceVectPB.Left) / dpShift);
                        sourceImageCropper.centerY += (int)Math.Round((mouseDownSourcePBTop - sourceVectPB.Top) / dpShift);
                        desinationImageCrooper.centerX = sourceImageCropper.centerX;
                        desinationImageCrooper.centerY = sourceImageCropper.centerY;
                        break;
                    }
                case MovedPicture.desination:
                    {
                        desinationImageCrooper.centerX += (int)Math.Round((mouseDownDesinationPBLeft - destinationVectPB.Left) / dpShift);
                        desinationImageCrooper.centerY += (int)Math.Round((mouseDownDesinationPBTop - destinationVectPB.Top) / dpShift);
                        sourceImageCropper.centerX = desinationImageCrooper.centerX;
                        sourceImageCropper.centerY = desinationImageCrooper.centerY;
                        break;
                    }
            }
            windowSettings.centerX = sourceImageCropper.centerX;
            windowSettings.centerY = sourceImageCropper.centerY;
            DrawCroppedScaledImage(dpShift, UpdateInfoBoxTime);
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
            sourceBmp = new Bitmap(aPath);
            destinationBmp = null;
            sourceImageCropper = new RaserImageCrooper(new Size(sourceVectPanel.Width, sourceVectPanel.Height), sourceBmp);
            desinationImageCrooper = new VectorImageCrooper(new Size(destinationVectPanel.Width, destinationVectPanel.Height), mapFactory,
                                                            sourceImageCropper.centerX, sourceImageCropper.centerY, windowSettings,
                                                            sourceBmp);
        }

        private bool LoadImage(String aPath)
        {
            if (File.Exists(aPath))
            {
                PrepareSourceImage(aPath);
                DrawCroppedScaledImage(float.Parse(txtScaleLvlVect.Text), UpdateInfoBoxTime);
                SetScaleControlEnable(true);
                return true;
            }
            return false;
        }

        private bool DrawCroppedScaledImage(float aDpScale,  UpdateInfoBoxTimeDelegate aFunct = null, float? aDpScalePrev = null)
        {
            DateTime dtTimePrv = DateTime.Now;
            Bitmap croppedSrcBmp = sourceImageCropper.GetCroppedImage(aDpScale);

            //pozyskiwanie danych potrzebnych do odczytania, na który px kliknęliśmy
            Rectangle srcRect = sourceImageCropper.GetSourceRectangle(aDpScale);
            Rectangle resultRect = sourceImageCropper.GetDestinationRectangle(aDpScale, srcRect);
            srcLeftX = srcRect.X;
            srcLeftY = srcRect.Y;
            resultLeftX = resultRect.X;
            resultLeftY = resultRect.Y;

            dtTimePrv = aFunct("GetCroppedImage 1:", true, dtTimePrv);
            //tymczasowo przypisuję ten sam obraz
            //Bitmap croppedDstBmp = croppedSrcBmp;
            Bitmap croppedDstBmp;
            if (desinationImageCrooper.mapFactory == null)
            {
                croppedDstBmp = null;
                dtTimePrv = aFunct("GetCroppedImage 2 NULL:", false, dtTimePrv);
            }
            else
            {
                croppedDstBmp = desinationImageCrooper.GetCroppedImage(aDpScale, aFunct);
                dtTimePrv = aFunct("GetCroppedImage 2:", false, dtTimePrv);
            }
            
            /*int scaledShiftX = sourcePanel.Width;
            int scaledShiftY = sourcePanel.Height;*/
            int scaledShiftX = (int)Math.Round(sourceImageCropper.centerX * aDpScale);
            int scaledShiftY = (int)Math.Round(sourceImageCropper.centerY * aDpScale);

            //UpdateInfoBox("bmp: " + croppedSrcBmp.Width.ToString() + " x " + croppedSrcBmp.Height.ToString());
            sourceVectPB.Height = croppedSrcBmp.Height;
            sourceVectPB.Width = croppedSrcBmp.Width;
            sourceVectPB.Image = croppedSrcBmp;
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
                    Debug.Assert(false, "Nie powiadła się próba wgrania pliku źrógłowego mapy: '" + windowSettings.stSourceImagePath + "'");
                }
            }
            else
            {
                Debug.Assert(false, "Pusta zmianna ze ściżką do pliku źródłowego mapy.");
            }
        }
    }
}
