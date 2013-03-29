using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        Image srcImg;
        Graphics graphics;
        Bitmap bmp;
        float dpScale;
        private bool blMouseInMoveMode;
        int startingX;
        int startingY;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnStartR2V_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnMenuHide_Click(object sender, EventArgs e)
        {
            if (panel4.Visible)
            {
                panel4.Visible = false;
                panel2.Width = 314;
                btnMenuHide.Text = ">>";
            }
            else
            {
                panel4.Visible = true;
                panel2.Width = 333;
                btnMenuHide.Text = "<<";
            }

        }

        private void DrawScaledImage()
        {
            if (checkBox1.Checked)
                dpScale = float.Parse(textBox1.Text);
            else
                dpScale = 1;
            bmp = new Bitmap((int)Math.Round(srcImg.Width * dpScale), (int)Math.Round(srcImg.Height * dpScale));
            graphics = Graphics.FromImage(bmp);
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            graphics.DrawImage(srcImg, 0, 0, srcImg.Width * dpScale, srcImg.Height * dpScale);
            Image myImg2 = srcImg.GetThumbnailImage((int)Math.Round(srcImg.Width * dpScale), (int)Math.Round(srcImg.Height * dpScale), null, IntPtr.Zero);
            //Bitmap b = new System.Drawing.Bitmap(myImg, new Size((int)Math.Round(myImg.Width * scale), (int)Math.Round(myImg.Height * scale)));
            pictureBoxSrc.Height = myImg2.Height;
            pictureBoxSrc.Width = myImg2.Width;
            pictureBoxSrc.Refresh();
            pictureBoxSrc.Image = bmp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dpScale = new float();
            srcImg = Image.FromFile("C:\\Users\\mudia\\Desktop\\R2VImg\\Untitled-1.bmp");
            DrawScaledImage();
            ZoomInBtn.Enabled = true;
            ZoomOutBtn.Enabled = true;
  
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ZoomInBtn_Click(object sender, EventArgs e)
        {
            if (int.Parse(textBox1.Text) < 5) 
            { 
                textBox1.Text = (int.Parse(textBox1.Text) + 1).ToString();
                DrawScaledImage();
            }
        }

        private void ZoomOutBtn_Click(object sender, EventArgs e)
        {
            if (int.Parse(textBox1.Text) > 1)
            {
                textBox1.Text = (int.Parse(textBox1.Text) - 1).ToString();
                DrawScaledImage();
            }
        }

        private void pictureBoxSrc_MouseDown(object sender, MouseEventArgs e)
        {
            blMouseInMoveMode = true;
        }

        private void pictureBoxSrc_MouseUp(object sender, MouseEventArgs e)
        {
            blMouseInMoveMode = false;
        }

        private void pictureBoxSrc_MouseMove(object sender, MouseEventArgs e)
        {
            MovePictures(e);
        }

        private void MovePictures(MouseEventArgs e)
        {
            int horChange ;
            int verChange ;
            if (blMouseInMoveMode)
            {
                horChange = e.X - startingX;
                verChange = e.Y - startingY;
                pictureBoxSrc.Left = Math.Min(0, Math.Max(pictureBoxSrc.Left + horChange, panel6.Width-pictureBoxSrc.Image.Width));
                pictureBoxSrc.Top = Math.Min(0, Math.Max(pictureBoxSrc.Top + verChange, panel6.Height - pictureBoxSrc.Image.Height)); ;
                UpdateInfoBox("horChange: " + horChange.ToString() + "\n" + "verChange" + verChange.ToString());
            }
            else
            {
                startingX = e.X;
                startingY = e.Y;
                UpdateInfoBox();
            }
            
        }

        private void pictureBoxSrc_MouseLeave(object sender, EventArgs e)
        {
            //blMouseInMoveMode = false;
        }

        private void UpdateInfoBox(string atext = ""){
            richTextBox1.Text = "startingX: "+startingX.ToString()+"\n"+
                                "startingY: "+startingY.ToString()+"\n"+
                                atext;
        }
    }
}
