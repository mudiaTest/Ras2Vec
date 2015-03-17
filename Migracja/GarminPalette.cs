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
    }
}
