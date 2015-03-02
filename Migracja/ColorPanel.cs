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
            pnlColor.BackColor = BackColor;
            postColorObj = aPostColorObj;
            Obj2Scr();
        }

        public void Scr2Obj()
        {
            postColorObj.lpRedMin = Int32.Parse(txtRedMin.Text);
            postColorObj.lpRedMax = Int32.Parse(txtRedMax.Text);
            postColorObj.lpGreenMin = Int32.Parse(txtGreenMin.Text);
            postColorObj.lpGreenMax = Int32.Parse(txtGreenMax.Text);
            postColorObj.lpBlueMin = Int32.Parse(txtBlueMin.Text);
            postColorObj.lpBlueMax = Int32.Parse(txtBlueMax.Text);
        }

        public void Obj2Scr()
        {
            txtRedMin.Text = postColorObj.lpRedMin.ToString();
            txtRedMax.Text = postColorObj.lpRedMax.ToString();
            txtGreenMin.Text = postColorObj.lpGreenMin.ToString();
            txtGreenMax.Text = postColorObj.lpGreenMax.ToString();
            txtBlueMin.Text = postColorObj.lpBlueMin.ToString();
            txtBlueMax.Text = postColorObj.lpBlueMax.ToString();
        }

        private void ColorPanel_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
