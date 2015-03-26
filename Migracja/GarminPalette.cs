using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Migracja
{
    public partial class GarminPalette : Form
    {
        public Color color;
        public GarminPalette(ref Color aColor)
        {
            InitializeComponent();
            flpTest.Controls.Add(new ColorField(0, 0, 0, Ok));
            flpTest.Controls.Add(new ColorField(255, 0, 0, Ok));
            flpTest.Controls.Add(new ColorField(0, 255, 0, Ok));
            flpTest.Controls.Add(new ColorField(0, 0, 255, Ok));
            flpTest.Controls.Add(new ColorField(255, 255, 255, Ok));

            flpVistaHCx.Controls.Add(new ColorField(0, 0, 0, Ok));
            flpVistaHCx.Controls.Add(new ColorField(0, 0, 95, Ok));
            flpVistaHCx.Controls.Add(new ColorField(0, 0, 135, Ok));
            flpVistaHCx.Controls.Add(new ColorField(0, 0, 175, Ok));
            flpVistaHCx.Controls.Add(new ColorField(0, 0, 215, Ok));
            flpVistaHCx.Controls.Add(new ColorField(0, 0, 255, Ok));
            flpVistaHCx.Controls.Add(new ColorField(0, 95, 0, Ok));
            flpVistaHCx.Controls.Add(new ColorField(0, 95, 95, Ok));
            flpVistaHCx.Controls.Add(new ColorField(0, 95, 135, Ok));
            flpVistaHCx.Controls.Add(new ColorField(0, 95, 175, Ok));
            flpVistaHCx.Controls.Add(new ColorField(0, 95, 215, Ok));
            flpVistaHCx.Controls.Add(new ColorField(0, 95, 255, Ok));
            flpVistaHCx.Controls.Add(new ColorField(0, 135, 0, Ok));
            flpVistaHCx.Controls.Add(new ColorField(0, 135, 95, Ok));
            flpVistaHCx.Controls.Add(new ColorField(0, 135, 135, Ok));
            flpVistaHCx.Controls.Add(new ColorField(0, 135, 175, Ok));
            flpVistaHCx.Controls.Add(new ColorField(0, 135, 215, Ok));
            flpVistaHCx.Controls.Add(new ColorField(0, 135, 255, Ok));
            flpVistaHCx.Controls.Add(new ColorField(0, 175, 0, Ok));
            flpVistaHCx.Controls.Add(new ColorField(0, 175, 95, Ok));
            flpVistaHCx.Controls.Add(new ColorField(0, 175, 135, Ok));
            flpVistaHCx.Controls.Add(new ColorField(0, 175, 175, Ok));
            flpVistaHCx.Controls.Add(new ColorField(0, 175, 215, Ok));
            flpVistaHCx.Controls.Add(new ColorField(0, 175, 255, Ok));
            flpVistaHCx.Controls.Add(new ColorField(0, 215, 0, Ok));
            flpVistaHCx.Controls.Add(new ColorField(0, 215, 95, Ok));
            flpVistaHCx.Controls.Add(new ColorField(0, 215, 135, Ok));
            flpVistaHCx.Controls.Add(new ColorField(0, 215, 175, Ok));
            flpVistaHCx.Controls.Add(new ColorField(0, 215, 215, Ok));
            flpVistaHCx.Controls.Add(new ColorField(0, 215, 255, Ok));
            flpVistaHCx.Controls.Add(new ColorField(0, 255, 0, Ok));
            flpVistaHCx.Controls.Add(new ColorField(0, 255, 95, Ok));
            flpVistaHCx.Controls.Add(new ColorField(0, 255, 135, Ok));
            flpVistaHCx.Controls.Add(new ColorField(0, 255, 175, Ok));
            flpVistaHCx.Controls.Add(new ColorField(0, 255, 215, Ok));
            flpVistaHCx.Controls.Add(new ColorField(0, 255, 255, Ok));
            flpVistaHCx.Controls.Add(new ColorField(95, 0, 0, Ok));
            flpVistaHCx.Controls.Add(new ColorField(95, 0, 95, Ok));
            flpVistaHCx.Controls.Add(new ColorField(95, 0, 135, Ok));
            flpVistaHCx.Controls.Add(new ColorField(95, 0, 175, Ok));
            flpVistaHCx.Controls.Add(new ColorField(95, 0, 215, Ok));
            flpVistaHCx.Controls.Add(new ColorField(95, 0, 255, Ok));
            flpVistaHCx.Controls.Add(new ColorField(95, 95, 0, Ok));
            flpVistaHCx.Controls.Add(new ColorField(95, 95, 95, Ok));
            flpVistaHCx.Controls.Add(new ColorField(95, 95, 135, Ok));
            flpVistaHCx.Controls.Add(new ColorField(95, 95, 175, Ok));
            flpVistaHCx.Controls.Add(new ColorField(95, 95, 215, Ok));
            flpVistaHCx.Controls.Add(new ColorField(95, 95, 255, Ok));
            flpVistaHCx.Controls.Add(new ColorField(95, 135, 0, Ok));
            flpVistaHCx.Controls.Add(new ColorField(95, 135, 95, Ok));
            flpVistaHCx.Controls.Add(new ColorField(95, 135, 135, Ok));
            flpVistaHCx.Controls.Add(new ColorField(95, 135, 175, Ok));
            flpVistaHCx.Controls.Add(new ColorField(95, 135, 215, Ok));
            flpVistaHCx.Controls.Add(new ColorField(95, 135, 255, Ok));
            flpVistaHCx.Controls.Add(new ColorField(95, 175, 0, Ok));
            flpVistaHCx.Controls.Add(new ColorField(95, 175, 95, Ok));
            flpVistaHCx.Controls.Add(new ColorField(95, 175, 135, Ok));
            flpVistaHCx.Controls.Add(new ColorField(95, 175, 175, Ok));
            flpVistaHCx.Controls.Add(new ColorField(95, 175, 215, Ok));
            flpVistaHCx.Controls.Add(new ColorField(95, 175, 255, Ok));
            flpVistaHCx.Controls.Add(new ColorField(95, 215, 0, Ok));
            flpVistaHCx.Controls.Add(new ColorField(95, 215, 95, Ok));
            flpVistaHCx.Controls.Add(new ColorField(95, 215, 135, Ok));
            flpVistaHCx.Controls.Add(new ColorField(95, 215, 175, Ok));
            flpVistaHCx.Controls.Add(new ColorField(95, 215, 215, Ok));
            flpVistaHCx.Controls.Add(new ColorField(95, 215, 255, Ok));
            flpVistaHCx.Controls.Add(new ColorField(95, 255, 0, Ok));
            flpVistaHCx.Controls.Add(new ColorField(95, 255, 95, Ok));
            flpVistaHCx.Controls.Add(new ColorField(95, 255, 135, Ok));
            flpVistaHCx.Controls.Add(new ColorField(95, 255, 175, Ok));
            flpVistaHCx.Controls.Add(new ColorField(95, 255, 215, Ok));
            flpVistaHCx.Controls.Add(new ColorField(95, 255, 255, Ok));
            flpVistaHCx.Controls.Add(new ColorField(135, 0, 0, Ok));
            flpVistaHCx.Controls.Add(new ColorField(135, 0, 95, Ok));
            flpVistaHCx.Controls.Add(new ColorField(135, 0, 135, Ok));
            flpVistaHCx.Controls.Add(new ColorField(135, 0, 175, Ok));
            flpVistaHCx.Controls.Add(new ColorField(135, 0, 215, Ok));
            flpVistaHCx.Controls.Add(new ColorField(135, 0, 255, Ok));
            flpVistaHCx.Controls.Add(new ColorField(135, 95, 0, Ok));
            flpVistaHCx.Controls.Add(new ColorField(135, 95, 95, Ok));
            flpVistaHCx.Controls.Add(new ColorField(135, 95, 135, Ok));
            flpVistaHCx.Controls.Add(new ColorField(135, 95, 175, Ok));
            flpVistaHCx.Controls.Add(new ColorField(135, 95, 215, Ok));
            flpVistaHCx.Controls.Add(new ColorField(135, 95, 255, Ok));
            flpVistaHCx.Controls.Add(new ColorField(135, 135, 0, Ok));
            flpVistaHCx.Controls.Add(new ColorField(135, 135, 95, Ok));
            flpVistaHCx.Controls.Add(new ColorField(135, 135, 135, Ok));
            flpVistaHCx.Controls.Add(new ColorField(135, 135, 175, Ok));
            flpVistaHCx.Controls.Add(new ColorField(135, 135, 215, Ok));
            flpVistaHCx.Controls.Add(new ColorField(135, 135, 255, Ok));
            flpVistaHCx.Controls.Add(new ColorField(135, 175, 0, Ok));
            flpVistaHCx.Controls.Add(new ColorField(135, 175, 95, Ok));
            flpVistaHCx.Controls.Add(new ColorField(135, 175, 135, Ok));
            flpVistaHCx.Controls.Add(new ColorField(135, 175, 175, Ok));
            flpVistaHCx.Controls.Add(new ColorField(135, 175, 215, Ok));
            flpVistaHCx.Controls.Add(new ColorField(135, 175, 255, Ok));
            flpVistaHCx.Controls.Add(new ColorField(135, 215, 0, Ok));
            flpVistaHCx.Controls.Add(new ColorField(135, 215, 95, Ok));
            flpVistaHCx.Controls.Add(new ColorField(135, 215, 135, Ok));
            flpVistaHCx.Controls.Add(new ColorField(135, 215, 175, Ok));
            flpVistaHCx.Controls.Add(new ColorField(135, 215, 215, Ok));
            flpVistaHCx.Controls.Add(new ColorField(135, 215, 255, Ok));
            flpVistaHCx.Controls.Add(new ColorField(135, 255, 0, Ok));
            flpVistaHCx.Controls.Add(new ColorField(135, 255, 95, Ok));
            flpVistaHCx.Controls.Add(new ColorField(135, 255, 135, Ok));
            flpVistaHCx.Controls.Add(new ColorField(135, 255, 175, Ok));
            flpVistaHCx.Controls.Add(new ColorField(135, 255, 215, Ok));
            flpVistaHCx.Controls.Add(new ColorField(135, 255, 255, Ok));
            flpVistaHCx.Controls.Add(new ColorField(175, 0, 0, Ok));
            flpVistaHCx.Controls.Add(new ColorField(175, 0, 95, Ok));
            flpVistaHCx.Controls.Add(new ColorField(175, 0, 135, Ok));
            flpVistaHCx.Controls.Add(new ColorField(175, 0, 175, Ok));
            flpVistaHCx.Controls.Add(new ColorField(175, 0, 215, Ok));
            flpVistaHCx.Controls.Add(new ColorField(175, 0, 255, Ok));
            flpVistaHCx.Controls.Add(new ColorField(175, 95, 0, Ok));
            flpVistaHCx.Controls.Add(new ColorField(175, 95, 95, Ok));
            flpVistaHCx.Controls.Add(new ColorField(175, 95, 135, Ok));
            flpVistaHCx.Controls.Add(new ColorField(175, 95, 175, Ok));
            flpVistaHCx.Controls.Add(new ColorField(175, 95, 215, Ok));
            flpVistaHCx.Controls.Add(new ColorField(175, 95, 255, Ok));
            flpVistaHCx.Controls.Add(new ColorField(175, 135, 0, Ok));
            flpVistaHCx.Controls.Add(new ColorField(175, 135, 95, Ok));
            flpVistaHCx.Controls.Add(new ColorField(175, 135, 135, Ok));
            flpVistaHCx.Controls.Add(new ColorField(175, 135, 175, Ok));
            flpVistaHCx.Controls.Add(new ColorField(175, 135, 215, Ok));
            flpVistaHCx.Controls.Add(new ColorField(175, 135, 255, Ok));
            flpVistaHCx.Controls.Add(new ColorField(175, 175, 0, Ok));
            flpVistaHCx.Controls.Add(new ColorField(175, 175, 95, Ok));
            flpVistaHCx.Controls.Add(new ColorField(175, 175, 135, Ok));
            flpVistaHCx.Controls.Add(new ColorField(175, 175, 175, Ok));
            flpVistaHCx.Controls.Add(new ColorField(175, 175, 215, Ok));
            flpVistaHCx.Controls.Add(new ColorField(175, 175, 255, Ok));
            flpVistaHCx.Controls.Add(new ColorField(175, 215, 0, Ok));
            flpVistaHCx.Controls.Add(new ColorField(175, 215, 95, Ok));
            flpVistaHCx.Controls.Add(new ColorField(175, 215, 135, Ok));
            flpVistaHCx.Controls.Add(new ColorField(175, 215, 175, Ok));
            flpVistaHCx.Controls.Add(new ColorField(175, 215, 215, Ok));
            flpVistaHCx.Controls.Add(new ColorField(175, 215, 255, Ok));
            flpVistaHCx.Controls.Add(new ColorField(175, 255, 0, Ok));
            flpVistaHCx.Controls.Add(new ColorField(175, 255, 95, Ok));
            flpVistaHCx.Controls.Add(new ColorField(175, 255, 135, Ok));
            flpVistaHCx.Controls.Add(new ColorField(175, 255, 175, Ok));
            flpVistaHCx.Controls.Add(new ColorField(175, 255, 215, Ok));
            flpVistaHCx.Controls.Add(new ColorField(175, 255, 255, Ok));
            flpVistaHCx.Controls.Add(new ColorField(215, 0, 0, Ok));
            flpVistaHCx.Controls.Add(new ColorField(215, 0, 95, Ok));
            flpVistaHCx.Controls.Add(new ColorField(215, 0, 135, Ok));
            flpVistaHCx.Controls.Add(new ColorField(215, 0, 175, Ok));
            flpVistaHCx.Controls.Add(new ColorField(215, 0, 215, Ok));
            flpVistaHCx.Controls.Add(new ColorField(215, 0, 255, Ok));
            flpVistaHCx.Controls.Add(new ColorField(215, 95, 0, Ok));
            flpVistaHCx.Controls.Add(new ColorField(215, 95, 95, Ok));
            flpVistaHCx.Controls.Add(new ColorField(215, 95, 135, Ok));
            flpVistaHCx.Controls.Add(new ColorField(215, 95, 175, Ok));
            flpVistaHCx.Controls.Add(new ColorField(215, 95, 215, Ok));
            flpVistaHCx.Controls.Add(new ColorField(215, 95, 255, Ok));
            flpVistaHCx.Controls.Add(new ColorField(215, 135, 0, Ok));
            flpVistaHCx.Controls.Add(new ColorField(215, 135, 95, Ok));
            flpVistaHCx.Controls.Add(new ColorField(215, 135, 135, Ok));
            flpVistaHCx.Controls.Add(new ColorField(215, 135, 175, Ok));
            flpVistaHCx.Controls.Add(new ColorField(215, 135, 215, Ok));
            flpVistaHCx.Controls.Add(new ColorField(215, 135, 255, Ok));
            flpVistaHCx.Controls.Add(new ColorField(215, 175, 0, Ok));
            flpVistaHCx.Controls.Add(new ColorField(215, 175, 95, Ok));
            flpVistaHCx.Controls.Add(new ColorField(215, 175, 135, Ok));
            flpVistaHCx.Controls.Add(new ColorField(215, 175, 175, Ok));
            flpVistaHCx.Controls.Add(new ColorField(215, 175, 215, Ok));
            flpVistaHCx.Controls.Add(new ColorField(215, 175, 255, Ok));
            flpVistaHCx.Controls.Add(new ColorField(215, 215, 0, Ok));
            flpVistaHCx.Controls.Add(new ColorField(215, 215, 95, Ok));
            flpVistaHCx.Controls.Add(new ColorField(215, 215, 135, Ok));
            flpVistaHCx.Controls.Add(new ColorField(215, 215, 175, Ok));
            flpVistaHCx.Controls.Add(new ColorField(215, 215, 215, Ok));
            flpVistaHCx.Controls.Add(new ColorField(215, 215, 255, Ok));
            flpVistaHCx.Controls.Add(new ColorField(215, 255, 0, Ok));
            flpVistaHCx.Controls.Add(new ColorField(215, 255, 95, Ok));
            flpVistaHCx.Controls.Add(new ColorField(215, 255, 135, Ok));
            flpVistaHCx.Controls.Add(new ColorField(215, 255, 175, Ok));
            flpVistaHCx.Controls.Add(new ColorField(215, 255, 215, Ok));
            flpVistaHCx.Controls.Add(new ColorField(215, 255, 255, Ok));
            flpVistaHCx.Controls.Add(new ColorField(255, 0, 0, Ok));
            flpVistaHCx.Controls.Add(new ColorField(255, 0, 95, Ok));
            flpVistaHCx.Controls.Add(new ColorField(255, 0, 135, Ok));
            flpVistaHCx.Controls.Add(new ColorField(255, 0, 175, Ok));
            flpVistaHCx.Controls.Add(new ColorField(255, 0, 215, Ok));
            flpVistaHCx.Controls.Add(new ColorField(255, 0, 255, Ok));
            flpVistaHCx.Controls.Add(new ColorField(255, 95, 0, Ok));
            flpVistaHCx.Controls.Add(new ColorField(255, 95, 95, Ok));
            flpVistaHCx.Controls.Add(new ColorField(255, 95, 135, Ok));
            flpVistaHCx.Controls.Add(new ColorField(255, 95, 175, Ok));
            flpVistaHCx.Controls.Add(new ColorField(255, 95, 215, Ok));
            flpVistaHCx.Controls.Add(new ColorField(255, 95, 255, Ok));
            flpVistaHCx.Controls.Add(new ColorField(255, 135, 0, Ok));
            flpVistaHCx.Controls.Add(new ColorField(255, 135, 95, Ok));
            flpVistaHCx.Controls.Add(new ColorField(255, 135, 135, Ok));
            flpVistaHCx.Controls.Add(new ColorField(255, 135, 175, Ok));
            flpVistaHCx.Controls.Add(new ColorField(255, 135, 215, Ok));
            flpVistaHCx.Controls.Add(new ColorField(255, 135, 255, Ok));
            flpVistaHCx.Controls.Add(new ColorField(255, 175, 0, Ok));
            flpVistaHCx.Controls.Add(new ColorField(255, 175, 95, Ok));
            flpVistaHCx.Controls.Add(new ColorField(255, 175, 135, Ok));
            flpVistaHCx.Controls.Add(new ColorField(255, 175, 175, Ok));
            flpVistaHCx.Controls.Add(new ColorField(255, 175, 215, Ok));
            flpVistaHCx.Controls.Add(new ColorField(255, 175, 255, Ok));
            flpVistaHCx.Controls.Add(new ColorField(255, 215, 0, Ok));
            flpVistaHCx.Controls.Add(new ColorField(255, 215, 95, Ok));
            flpVistaHCx.Controls.Add(new ColorField(255, 215, 135, Ok));
            flpVistaHCx.Controls.Add(new ColorField(255, 215, 175, Ok));
            flpVistaHCx.Controls.Add(new ColorField(255, 215, 215, Ok));
            flpVistaHCx.Controls.Add(new ColorField(255, 215, 255, Ok));
            flpVistaHCx.Controls.Add(new ColorField(255, 255, 0, Ok));
            flpVistaHCx.Controls.Add(new ColorField(255, 255, 95, Ok));
            flpVistaHCx.Controls.Add(new ColorField(255, 255, 135, Ok));
            flpVistaHCx.Controls.Add(new ColorField(255, 255, 175, Ok));
            flpVistaHCx.Controls.Add(new ColorField(255, 255, 215, Ok));
            flpVistaHCx.Controls.Add(new ColorField(255, 255, 255, Ok));
            flpVistaHCx.Controls.Add(new ColorField(8, 8, 8, Ok));
            flpVistaHCx.Controls.Add(new ColorField(18, 18, 18, Ok));
            flpVistaHCx.Controls.Add(new ColorField(28, 28, 28, Ok));
            flpVistaHCx.Controls.Add(new ColorField(38, 38, 38, Ok));
            flpVistaHCx.Controls.Add(new ColorField(48, 48, 48, Ok));
            flpVistaHCx.Controls.Add(new ColorField(58, 58, 58, Ok));
            flpVistaHCx.Controls.Add(new ColorField(68, 68, 68, Ok));
            flpVistaHCx.Controls.Add(new ColorField(78, 78, 78, Ok));
            flpVistaHCx.Controls.Add(new ColorField(88, 88, 88, Ok));
            flpVistaHCx.Controls.Add(new ColorField(98, 98, 98, Ok));
            flpVistaHCx.Controls.Add(new ColorField(108, 108, 108, Ok));
            flpVistaHCx.Controls.Add(new ColorField(118, 118, 118, Ok));
            flpVistaHCx.Controls.Add(new ColorField(128, 128, 128, Ok));
            flpVistaHCx.Controls.Add(new ColorField(138, 138, 138, Ok));
            flpVistaHCx.Controls.Add(new ColorField(148, 148, 148, Ok));
            flpVistaHCx.Controls.Add(new ColorField(158, 158, 158, Ok));
            flpVistaHCx.Controls.Add(new ColorField(168, 168, 168, Ok));
            flpVistaHCx.Controls.Add(new ColorField(178, 178, 178, Ok));
            flpVistaHCx.Controls.Add(new ColorField(188, 188, 188, Ok));
            flpVistaHCx.Controls.Add(new ColorField(198, 198, 198, Ok));
            flpVistaHCx.Controls.Add(new ColorField(208, 208, 208, Ok));
            flpVistaHCx.Controls.Add(new ColorField(218, 218, 218, Ok));
            flpVistaHCx.Controls.Add(new ColorField(228, 228, 228, Ok));
            flpVistaHCx.Controls.Add(new ColorField(238, 238, 238, Ok));
        }

        public Color Ok(int aLpRed, int aLpGreen, int aLpBlue)
        {
            Color result = Color.FromArgb(aLpRed, aLpGreen, aLpBlue);
            color = result;
            Close();
            return result;
        }

        private void tpTest_Click(object sender, EventArgs e)
        {
            /*flpTest.Controls.Add(new ColorField(0, 0, 0, Ok));
            flpTest.Controls.Add(new ColorField(255, 0, 0, Ok));
            flpTest.Controls.Add(new ColorField(0, 255, 0, Ok));
            flpTest.Controls.Add(new ColorField(0, 0, 255, Ok));
            flpTest.Controls.Add(new ColorField(255, 255, 255, Ok));*/
        }

        private void btnColorDialog_Click(object sender, EventArgs e)
        {
            if (winColorDialog.ShowDialog() == DialogResult.OK)
            {
                color = winColorDialog.Color;
                Close();
            }
        }

        private void tcPalettes_TabIndexChanged(object sender, EventArgs e)
        {
        }

        private void tcPalettes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcPalettes.SelectedIndex == 2)
            {
                this.Width = 300;
                this.Height = 300;
            }
            else
            if (tcPalettes.SelectedIndex == 1)
            {
                this.Width = 650;
                this.Height = 900;
            }
        }
    }
}
