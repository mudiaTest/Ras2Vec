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
            switch (picture)
            {
                case MovedPicture.source:
                    {

                        p.shiftX += mouseDownSourcePBLeft - sourcePB.Left;
                        p.shiftY += mouseDownSourcePBTop - sourcePB.Top;
                        break;
                    }
                case MovedPicture.desination:
                    {
                        p.shiftX += mouseDownDesinationPBLeft - destinationPB.Left;
                        p.shiftY += mouseDownDesinationPBTop - destinationPB.Top;
                        break;
                    }
            }
            DrawCroppedScaledImage(float.Parse(textBox1.Text));
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

        private void ZoomInBtn_Click(object sender, EventArgs e)
        {
            if (int.Parse(textBox1.Text) < 5)
            {

                if (DrawCroppedScaledImage(float.Parse(textBox1.Text) + 1))
                    textBox1.Text = (int.Parse(textBox1.Text) + 1).ToString();
            }
        }

        private void ZoomOutBtn_Click(object sender, EventArgs e)
        {
            if (int.Parse(textBox1.Text) > 1)
            {
                if (DrawCroppedScaledImage(float.Parse(textBox1.Text) - 1))
                    textBox1.Text = (int.Parse(textBox1.Text) - 1).ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dpScale = new float();
            //srcImg = Image.FromFile("C:\\Users\\mudia\\Desktop\\R2VImg\\Untitled-1.bmp");
            srcImg = Image.FromFile("C:\\Users\\mudia\\Desktop\\kop1b.jpg");
            //DrawScaledImage(float.Parse(textBox1.Text));
            bmp = new Bitmap("C:\\Users\\mudia\\Desktop\\kop1b.jpg");
            p = new ImageCrooper(new Size(sourcePanel.Width, sourcePanel.Height));

            DrawCroppedScaledImage(float.Parse(textBox1.Text));
            ZoomInBtn.Enabled = true;
            ZoomOutBtn.Enabled = true;
        }

        private bool DrawCroppedScaledImage(float aDpScale)
        {
            Bitmap croppedBmp = p.GetCroppedImage(bmp, aDpScale);
            UpdateInfoBox("bmp: " + croppedBmp.Width.ToString() + " x " + croppedBmp.Height.ToString());
            sourcePB.Height = croppedBmp.Height;
            sourcePB.Width = croppedBmp.Width;
            sourcePB.Image = croppedBmp;
            UpdateInfoBox("pb: " + sourcePB.Width.ToString() + " x " + sourcePB.Height.ToString() +
                          "L/T: " + sourcePB.Left.ToString() + " x " + sourcePB.Top.ToString());
            sourcePB.Left = -Math.Min(p.shiftX, sourcePanel.Width);
            sourcePB.Top = -Math.Min(p.shiftY, sourcePanel.Height);
            UpdateInfoBox("L/T: " + sourcePB.Left.ToString() + " x " + sourcePB.Top.ToString(), false);

            destinationPB.Height = croppedBmp.Height;
            destinationPB.Width = croppedBmp.Width;
            destinationPB.Image = croppedBmp;
            destinationPB.Left = -Math.Min(p.shiftX, destinationPanel.Width);
            destinationPB.Top = -Math.Min(p.shiftY, destinationPanel.Height);
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
