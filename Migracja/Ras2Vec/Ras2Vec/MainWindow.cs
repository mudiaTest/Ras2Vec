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
        Bitmap bmp;
        private bool blMouseInMoveMode;
        int startingX;
        int startingY;
        ImageCrooper p;
        int horChange;
        int verChange;
        int mouseDownSourcePBLeft;
        int mouseDownSourcePBTop;
        int mouseDownDesinationPBLeft;
        int mouseDownDesinationPBTop;

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
                panel2.Width = 34;
                btnMenuHide.Text = ">>";
            }
            else
            {
                panel4.Visible = true;
                panel2.Width = 333;
                btnMenuHide.Text = "<<";
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void UpdateInfoBox(string atext = "", bool aBlNewLine = true){
            richTextBox1.Text = richTextBox1.Text + '\n';
            if (aBlNewLine) 
                richTextBox1.Text = richTextBox1.Text + '\n';
            richTextBox1.Text = richTextBox1.Text + atext;
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        private void sourcePB_MouseDown(object sender, MouseEventArgs e)
        {
            StartMovingPictures();
        }

        private void sourcePB_MouseUp(object sender, MouseEventArgs e)
        {
            StopMovingPictures(MovedPicture.source);
        }

        private void sourcePB_MouseMove(object sender, MouseEventArgs e)
        {
            MovePictures(e);
        }

        private void sourcePB_MouseLeave(object sender, EventArgs e)
        {
            blMouseInMoveMode = false;
        }

        private void destinationPB_MouseDown(object sender, MouseEventArgs e)
        {
            StartMovingPictures();
        }

        private void destinationPB_MouseLeave(object sender, EventArgs e)
        {
            blMouseInMoveMode = false;
        }

        private void destinationPB_MouseMove(object sender, MouseEventArgs e)
        {
            MovePictures(e);
        }

        private void destinationPB_MouseUp(object sender, MouseEventArgs e)
        {
            StopMovingPictures(MovedPicture.desination);
        }

        private void w(object sender, EventArgs e)
        {

        }

        private void ZoomInBtn_Click(object sender, EventArgs e)
        {
            if (int.Parse(textBox1.Text) <= 10)
            {

                if (DrawCroppedScaledImage(float.Parse(textBox1.Text) + 1, float.Parse(textBox1.Text)))
                    textBox1.Text = (int.Parse(textBox1.Text) + 1).ToString();
            }
        }

        private void ZoomOutBtn_Click(object sender, EventArgs e)
        {
            if (int.Parse(textBox1.Text) > 1)
            {
                if (DrawCroppedScaledImage(float.Parse(textBox1.Text) - 1, float.Parse(textBox1.Text)))
                    textBox1.Text = (int.Parse(textBox1.Text) - 1).ToString();
            }
        }

        private void panel7_SizeChanged(object sender, EventArgs e)
        {
            int panelSize = (int)Math.Round((panel7.Height - 8 - 10 - 8) / 2.0);
            sourcePanel.Height = panelSize;
            destinationPanel.Height = panelSize;
            p = new ImageCrooper(new Size(sourcePanel.Width, sourcePanel.Height), bmp);
            DrawCroppedScaledImage(float.Parse(textBox1.Text));
        }
    }
}
