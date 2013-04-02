using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            float dpShift = float.Parse(textBox1.Text);
            switch (picture)
            {
                case MovedPicture.source:
                    {

                        p.shiftX += (int)Math.Round((mouseDownSourcePBLeft - sourcePB.Left) / dpShift);
                        p.shiftY += (int)Math.Round((mouseDownSourcePBTop - sourcePB.Top) / dpShift);
                        break;
                    }
                case MovedPicture.desination:
                    {
                        p.shiftX += (int)Math.Round((mouseDownDesinationPBLeft - destinationPB.Left) / dpShift);
                        p.shiftY += (int)Math.Round((mouseDownDesinationPBTop - destinationPB.Top) / dpShift);
                        break;
                    }
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            dpScale = new float();
            srcImg = Image.FromFile("C:\\Users\\mudia\\Desktop\\kop1b.jpg");
            //DrawScaledImage(float.Parse(textBox1.Text));
            bmp = new Bitmap("C:\\Users\\mudia\\Desktop\\kop1b.jpg");
            p = new ImageCrooper(new Size(sourcePanel.Width, sourcePanel.Height));

            DrawCroppedScaledImage(float.Parse(textBox1.Text));
            ZoomInBtn.Enabled = true;
            ZoomOutBtn.Enabled = true;
        }

        private bool DrawCroppedScaledImage(float aDpScale, float? aDpScalePrev = null)
        {
            if (aDpScalePrev != null)
            {
                if (aDpScale > aDpScalePrev)
                {
                    p.shiftX = p.shiftX + (int)Math.Round( sourcePanel.Width *  (1 / Math.Pow(2, aDpScale)) );
                    p.shiftY = p.shiftY + (int)Math.Round( sourcePanel.Height * (1 / Math.Pow(2, aDpScale)) );
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
                
            }
            Bitmap croppedBmp = p.GetCroppedImage(bmp, aDpScale);
            int scaledShiftX = (int)Math.Round(p.shiftX * aDpScale);
            int scaledShiftY = (int)Math.Round(p.shiftY * aDpScale);
            UpdateInfoBox("bmp: " + croppedBmp.Width.ToString() + " x " + croppedBmp.Height.ToString());
            sourcePB.Height = croppedBmp.Height;
            sourcePB.Width = croppedBmp.Width;
            sourcePB.Image = croppedBmp;
            UpdateInfoBox("pb: " + sourcePB.Width.ToString() + " x " + sourcePB.Height.ToString() +
                          "L/T: " + sourcePB.Left.ToString() + " x " + sourcePB.Top.ToString());
            sourcePB.Left = -Math.Min(scaledShiftX, sourcePanel.Width);
            sourcePB.Top = -Math.Min(scaledShiftY, sourcePanel.Height);
            UpdateInfoBox("L/T: " + sourcePB.Left.ToString() + " x " + sourcePB.Top.ToString(), false);

            destinationPB.Height = croppedBmp.Height;
            destinationPB.Width = croppedBmp.Width;
            destinationPB.Image = croppedBmp;
            destinationPB.Left = -Math.Min(scaledShiftX, destinationPanel.Width);
            destinationPB.Top = -Math.Min(scaledShiftY, destinationPanel.Height);
            return true;
        }

        private bool DrawScaledImage(float adpScale)
        {
            int maxImgPx = 83000000;
            if (!checkBox1.Checked)
                adpScale = 1;

            if ((int)Math.Round(srcImg.Width * adpScale) * (int)Math.Round(srcImg.Height * adpScale) > maxImgPx)
            {
                return false;
            }
            //bmp = new Bitmap((int)Math.Round(srcImg.Width * adpScale), (int)Math.Round(srcImg.Height * adpScale));
            //graphics = Graphics.FromImage(bmp);
            //graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            //graphics.DrawImage(srcImg, 0, 0, srcImg.Width * adpScale, srcImg.Height * adpScale);
            //bmp = srcImg.GetThumbnailImage((int)Math.Round(srcImg.Width * adpScale), (int)Math.Round(srcImg.Height * adpScale), null, IntPtr.Zero);
            bmp = new Bitmap(srcImg, new Size((int)Math.Round(srcImg.Width * adpScale), (int)Math.Round(srcImg.Height * adpScale)));
            sourcePB.Height = bmp.Height;
            sourcePB.Width = bmp.Width;
            //sourcePB.Refresh();
            sourcePB.Image = bmp;

            destinationPB.Height = bmp.Height;
            destinationPB.Width = bmp.Width;
            //destinationPB.Refresh();
            destinationPB.Image = bmp;
            return true;
        }
    }
}
