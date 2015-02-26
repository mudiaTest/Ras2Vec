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
        enum MovedPicture { source, posterized, desination };

        private void StartMovingPictures()
        {
            blMouseInMoveMode = true;
            mouseDownSourcePBLeft = sourcePB.Left;
            mouseDownSourcePBTop = sourcePB.Top;
            mouseDownPosterizedPBLeft = posterizedPB.Left;
            mouseDownPosterizedPBTop = posterizedPB.Top;
            mouseDownDesinationPBLeft = destinationPB.Left;
            mouseDownDesinationPBTop = destinationPB.Top;
        }

        private void StopMovingPictures(MovedPicture picture)
        {
            float dpShift = float.Parse(txtScaleLvlVect.Text);
            switch (picture)
            {
                //centrum oglądanego obszaru przesówamy o przesunięcię myszą z uwzględnieniem sklai
                case MovedPicture.source:
                    {
                        sourceImageCropper.centerX += (int)Math.Round((mouseDownSourcePBLeft - sourcePB.Left) / dpShift);
                        sourceImageCropper.centerY += (int)Math.Round((mouseDownSourcePBTop - sourcePB.Top) / dpShift);
                        posterizedImageCropper.centerX = sourceImageCropper.centerX;
                        posterizedImageCropper.centerY = sourceImageCropper.centerY;
                        desinationImageCropper.centerX = sourceImageCropper.centerX;
                        desinationImageCropper.centerY = sourceImageCropper.centerY;
                        break;
                    }
                case MovedPicture.posterized:
                    {
                        posterizedImageCropper.centerX += (int)Math.Round((mouseDownPosterizedPBLeft - posterizedPB.Left) / dpShift);
                        posterizedImageCropper.centerY += (int)Math.Round((mouseDownPosterizedPBTop - posterizedPB.Top) / dpShift);
                        sourceImageCropper.centerX = posterizedImageCropper.centerX;
                        sourceImageCropper.centerY = posterizedImageCropper.centerY;
                        desinationImageCropper.centerX = posterizedImageCropper.centerX;
                        desinationImageCropper.centerY = posterizedImageCropper.centerY;
                        break;
                    }
                case MovedPicture.desination:
                    {
                        if (destinationPB.Image != null)
                        {
                            desinationImageCropper.centerX += (int)Math.Round((mouseDownDesinationPBLeft - destinationPB.Left) / dpShift);
                            desinationImageCropper.centerY += (int)Math.Round((mouseDownDesinationPBTop - destinationPB.Top) / dpShift);
                            sourceImageCropper.centerX = desinationImageCropper.centerX;
                            sourceImageCropper.centerY = desinationImageCropper.centerY;
                            posterizedImageCropper.centerX = desinationImageCropper.centerX;
                            posterizedImageCropper.centerY = desinationImageCropper.centerY;                           
                        }
                        break;
                    }
            }
            //poniżej bierzemy source, bo posterized i detination mają takie same wartośći
            windowSettings.centerXVect = sourceImageCropper.centerX;
            windowSettings.centerYVect = sourceImageCropper.centerY;
            DrawCroppedScaledImage(dpShift, UpdateInfoBoxTime);
            blMouseInMoveMode = false;
        }

        private void MovePictures(MouseEventArgs e)
        {
            if (blMouseInMoveMode)
            {
                horChange = e.X - startingX;
                verChange = e.Y - startingY;

                sourcePB.Left = Math.Min(0, Math.Max(sourcePB.Left + horChange, sourcePanel.Width - sourcePB.Image.Width));
                sourcePB.Top = Math.Min(0, Math.Max(sourcePB.Top + verChange, sourcePanel.Height - sourcePB.Image.Height));
                posterizedPB.Left = Math.Min(0, Math.Max(posterizedPB.Left + horChange, posterizedPanel.Width - posterizedPB.Image.Width));
                posterizedPB.Top = Math.Min(0, Math.Max(posterizedPB.Top + verChange, posterizedPanel.Height - posterizedPB.Image.Height));
                if (destinationPB.Image != null)
                {
                    destinationPB.Left = Math.Min(0, Math.Max(destinationPB.Left + horChange, destinationPanel.Width - destinationPB.Image.Width));
                    destinationPB.Top = Math.Min(0, Math.Max(destinationPB.Top + verChange, destinationPanel.Height - destinationPB.Image.Height));
                }
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
            posterizedBmp = new Bitmap(aPath);
            sourceImageCropper = new RaserImageCrooper(new Size(sourcePanel.Width, sourcePanel.Height), sourceBmp);
            posterizedImageCropper = new RaserImageCrooper(new Size(posterizedPanel.Width, posterizedPanel.Height), posterizedBmp);
            desinationImageCropper = new VectorImageCrooper(new Size(destinationPanel.Width, destinationPanel.Height), mapFactory,
                                                            posterizedImageCropper.centerX, posterizedImageCropper.centerY, windowSettings,
                                                            posterizedBmp);
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
            Bitmap posterizedSrcBmp = posterizedImageCropper.GetCroppedImage(aDpScale);

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
            Bitmap croppedDstBmp4Vect;
            if (desinationImageCropper.mapFactory == null)
            {
                croppedDstBmp4Vect = null;
                dtTimePrv = aFunct("GetCroppedImage 2 NULL:", false, dtTimePrv);
            }
            else
            {
                croppedDstBmp4Vect = desinationImageCropper.GetCroppedImage(aDpScale, aFunct);
                dtTimePrv = aFunct("GetCroppedImage 2:", false, dtTimePrv);
            }

            int scaledShiftX = (int)Math.Round(posterizedImageCropper.centerX * aDpScale);
            int scaledShiftY = (int)Math.Round(posterizedImageCropper.centerY * aDpScale);

            //UpdateInfoBox("bmp: " + croppedSrcBmp.Width.ToString() + " x " + croppedSrcBmp.Height.ToString());
            sourcePB.Height = croppedSrcBmp.Height;
            sourcePB.Width = croppedSrcBmp.Width;
            sourcePB.Image = croppedSrcBmp;
            //UpdateInfoBox("pb: " + sourcePB.Width.ToString() + " x " + sourcePB.Height.ToString() +
            //              "L/T: " + sourcePB.Left.ToString() + " x " + sourcePB.Top.ToString());
            sourcePB.Left = -sourcePanel.Width;
            sourcePB.Top = -sourcePanel.Height;
            //UpdateInfoBox("L/T: " + sourcePB.Left.ToString() + " x " + sourcePB.Top.ToString(), false);

            //UpdateInfoBox("bmp: " + croppedSrcBmp.Width.ToString() + " x " + croppedSrcBmp.Height.ToString());
            posterizedPB.Height = posterizedSrcBmp.Height;
            posterizedPB.Width = posterizedSrcBmp.Width;
            posterizedPB.Image = posterizedSrcBmp;
            //UpdateInfoBox("pb: " + sourcePB.Width.ToString() + " x " + sourcePB.Height.ToString() +
            //              "L/T: " + sourcePB.Left.ToString() + " x " + sourcePB.Top.ToString());
            posterizedPB.Left = -posterizedPanel.Width;
            posterizedPB.Top = -posterizedPanel.Height;
            //UpdateInfoBox("L/T: " + sourcePB.Left.ToString() + " x " + sourcePB.Top.ToString(), false);

            if (croppedDstBmp4Vect != null)
            {
                destinationPB.Height = croppedDstBmp4Vect.Height;
                destinationPB.Width = croppedDstBmp4Vect.Width;
                destinationPB.Image = croppedDstBmp4Vect;
                destinationPB.Left = -destinationPanel.Width;
                destinationPB.Top = -destinationPanel.Height;
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
