using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Migracja
{
    public partial class ColorPanel : UserControl
    {
        PosterizedColorData postColorObj = null;
        public ColorPanel(PosterizedColorData aPostColorObj)
        {
            InitializeComponent();
            pnlGarminColor.BackColor = BackColor;
            postColorObj = aPostColorObj;
            Obj2Scr();
        }

        public void Scr2Obj()
        {
            postColorObj.gravity = float.Parse(txtGravity.Text);
            postColorObj.garminColor = pnlGarminColor.BackColor;
            postColorObj.rasterColor = pnlRasterColor.BackColor;
        }

        public void Obj2Scr()
        {
            txtGravity.Text = String.Format("{0:0.000}", postColorObj.gravity);
            pnlGarminColor.BackColor = postColorObj.garminColor;
            pnlRasterColor.BackColor = postColorObj.rasterColor;
        }

        private void ColorPanel_Load(object sender, EventArgs e)
        {

        }

        private void maskedTextBox5_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox6_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox3_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox4_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void pnlColor_Click(object sender, EventArgs e)
        {
            DoPnlColor_Click(sender, e);
        }

        private void DoPnlColor_Click(object sender, EventArgs e)
        {
            GarminPalette palette = new GarminPalette(ref postColorObj.garminColor);
            palette.ShowDialog(this);
            postColorObj.garminColor = palette.color;
            Obj2Scr();
            palette.Dispose();
        }

        private void pnlColor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtRedMin_Leave(object sender, EventArgs e)
        {
            Scr2Obj();
        }

        private void txtRedMax_Leave(object sender, EventArgs e)
        {
            Scr2Obj();
        }

        private void txtGreenMin_Leave(object sender, EventArgs e)
        {
            Scr2Obj();
        }

        private void txtGreenMax_Leave(object sender, EventArgs e)
        {
            Scr2Obj();
        }

        private void txtBlueMin_Leave(object sender, EventArgs e)
        {
            Scr2Obj();
        }

        private void txtBlueMax_Leave(object sender, EventArgs e)
        {
            Scr2Obj();
        }

        private void pnlRasterColor_Click(object sender, EventArgs e)
        {
            DoPnlRasterColor_Click(sender, e);
        }

        private void DoPnlRasterColor_Click(object sender, EventArgs e)
        {
            GarminPalette palette = new GarminPalette(ref postColorObj.rasterColor);
            palette.ShowDialog(this);
            postColorObj.rasterColor = palette.color;
            Obj2Scr();
            palette.Dispose();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            DoPnlColor_Click(null, e);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            DoPnlRasterColor_Click(null, e);
        }
    }
}
