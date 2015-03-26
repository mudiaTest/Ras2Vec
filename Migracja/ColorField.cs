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
        Func<int, int, int, Color> ReturnFunct;

        public ColorField(int aLpRed = -1, int aLpGreen = -1, int aLpBlue = -1, Func<int, int, int, Color> aReturnFunct = null)
        {
            InitializeComponent();
            lpRed = aLpRed;
            lpGreen = aLpGreen;
            lpBlue = aLpBlue;
            ObjToScreen();     
            ReturnFunct = aReturnFunct/*(aLpRed, aLpGreen, aLpBlue)*/;
        }

        private void ObjToScreen()
        {
            lblRed.Text = lpRed.ToString();
            lblGreen.Text = lpGreen.ToString();
            lblBlue.Text = lpBlue.ToString();
            pnlColor.BackColor = Color.FromArgb(lpRed, lpGreen, lpBlue);
        }

        private void ColorField_Click(object sender, EventArgs e)
        {
            ReturnFunct(lpRed, lpGreen, lpBlue);
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

        private void pnlColor_Click(object sender, EventArgs e)
        {
            ReturnFunct(lpRed, lpGreen, lpBlue);
        }
    }
}
