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
    public partial class ColorField : UserControl
    {
        int lpRed;
        int lpGreen;
        int lpBlue;
        Func<string, int> ReturnFunct;

        public ColorField(int aLpRed = -1, int aLpGreen = -1, int aLpBlue = -1, Func<string, int> aReturnFunct = null)
        {
            InitializeComponent();
            lpRed = aLpRed;
            lpGreen = aLpGreen;
            lpBlue = aLpBlue;
            pnlColor.BackColor = new Color();
            pnlColor.BackColor.R = lpRed;
            pnlColor.BackColor.G = lpGreen;
            pnlColor.BackColor.B = lpBlue;
            ReturnFunct = aReturnFunct(aLpRed, aLpGreen, aLpBlue);
        }

        private void ColorField_Click(object sender, EventArgs e)
        {
            ReturnFunct(aLpRed, aLpGreen, aLpBlue);
        }

        public void ShowDetails()
        {
            lblRed.Visible = true;
            lblGreen.Visible = true;
            lblBlue.Visible = true;
            pnlColor.Width = 16;
        }

        public void HideDetails()
        {
            lblRed.Visible = false;
            lblGreen.Visible = false;
            lblBlue.Visible = false;
            pnlColor.Width = 16;
        }
    }
}
