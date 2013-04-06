using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Ras2Vec
{
    public partial class MainWindow : Form
    {
        enum MovedPicture { source, desination };

        private void StartMovingPictures()
        {
            blMouseInMoveMode = true;
            mouseDownSourcePBLeft = sourcePB.Left;
            mouseDownSourcePBTop = sourcePB.Top;
            mouseDownDesinationPBLeft = destinationPB.Left;
            mouseDownDesinationPBTop = destinationPB.Top;
        }

        private void StopMovingPictures(MovedPicture picture)
        {
            float dpShift = float.Parse(ScaleTB.Text);
            switch (picture)
            {
                //centrum oglądanego obszaru przesówamy o przesunięcię myszą z uwzględnieniem sklai
                case MovedPicture.source:
                    {
                        p.centerX += (int)Math.Round((mouseDownSourcePBLeft - sourcePB.Left) / dpShift);
                        p.centerY += (int)Math.Round((mouseDownSourcePBTop - sourcePB.Top) / dpShift);
                        break;
                    }
                case MovedPicture.desination:
                    {
                        p.centerX += (int)Math.Round((mouseDownDesinationPBLeft - destinationPB.Left) / dpShift);
                        p.centerY += (int)Math.Round((mouseDownDesinationPBTop - destinationPB.Top) / dpShift);
                        break;
                    }
            }
            windowSettings.centerX = p.centerX;
            windowSettings.centerY = p.centerY;
            DrawCroppedScaledImage(dpShift);
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
                destinationPB.Left = Math.Min(0, Math.Max(destinationPB.Left + horChange, destinationPanel.Width - destinationPB.Image.Width));
                destinationPB.Top = Math.Min(0, Math.Max(destinationPB.Top + verChange, destinationPanel.Height - destinationPB.Image.Height));
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
            ZoomInBtn.Enabled = aEnabled;
            ZoomOutBtn.Enabled = aEnabled;
            ScaleTrB.Enabled = aEnabled;
        }

        private void PrepareSourceImage(String aPath)
        {
            bmp = new Bitmap(aPath); //"C:\\Users\\mudia\\Desktop\\kop1b.jpg
            p = new ImageCrooper(new Size(sourcePanel.Width, sourcePanel.Height), bmp);
        }

        private bool LoadImage(String aPath)
        {
            if (File.Exists(aPath))
            {
                PrepareSourceImage(aPath);
                DrawCroppedScaledImage(float.Parse(ScaleTB.Text));
                SetScaleControlEnable(true);
                return true;
            }
            return false;
        }

        private bool DrawCroppedScaledImage(float aDpScale, float? aDpScalePrev = null)
        {
            /*if (aDpScalePrev != null)
            {
                if (aDpScale > aDpScalePrev)
                {
                    p.centerX = p.centerX + (int)Math.Round(sourcePanel.Width * (1 / Math.Pow(2, aDpScale)));
                    p.centerY = p.centerY + (int)Math.Round(sourcePanel.Height * (1 / Math.Pow(2, aDpScale)));
                }
                else
                {
                    p.shiftX = p.shiftX - (int)Math.Round(sourcePanel.Width * (1 / Math.Pow(2, (float)aDpScalePrev)));
                    p.shiftY = p.shiftY - (int)Math.Round(sourcePanel.Height * (1 / Math.Pow(2, (float)aDpScalePrev)));
                    p.shiftX = Math.Max(0, p.shiftX);
                    p.shiftY = Math.Max(0, p.shiftY);
                    p.shiftX = (int)Math.Round(Math.Min(bmp.Width - (sourcePanel.Width / aDpScale), p.shiftX));
                    p.shiftY =  (int)Math.Round(Math.Min(bmp.Height - (sourcePanel.Height / aDpScale), p.shiftY));
                }
                
            }*/
            Bitmap croppedBmp = p.GetCroppedImage(aDpScale);
            
            /*int scaledShiftX = sourcePanel.Width;
            int scaledShiftY = sourcePanel.Height;*/
            int scaledShiftX = (int)Math.Round(p.centerX * aDpScale);
            int scaledShiftY = (int)Math.Round(p.centerY * aDpScale);

            UpdateInfoBox("bmp: " + croppedBmp.Width.ToString() + " x " + croppedBmp.Height.ToString());
            sourcePB.Height = croppedBmp.Height;
            sourcePB.Width = croppedBmp.Width;
            sourcePB.Image = croppedBmp;
            UpdateInfoBox("pb: " + sourcePB.Width.ToString() + " x " + sourcePB.Height.ToString() +
                          "L/T: " + sourcePB.Left.ToString() + " x " + sourcePB.Top.ToString());
            /*sourcePB.Left = -Math.Min(scaledShiftX, sourcePanel.Width);
            sourcePB.Top = -Math.Min(scaledShiftY, sourcePanel.Height);*/
            sourcePB.Left = -sourcePanel.Width;
            sourcePB.Top = -sourcePanel.Height;
            UpdateInfoBox("L/T: " + sourcePB.Left.ToString() + " x " + sourcePB.Top.ToString(), false);

            destinationPB.Height = croppedBmp.Height;
            destinationPB.Width = croppedBmp.Width;
            destinationPB.Image = croppedBmp;
            /*destinationPB.Left = -Math.Min(scaledShiftX, destinationPanel.Width);
            destinationPB.Top = -Math.Min(scaledShiftY, destinationPanel.Height);*/
            destinationPB.Left = -destinationPanel.Width;
            destinationPB.Top = -destinationPanel.Height;
            return true;
        }
    }
}
