using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace Migracja
{
    public partial class MainWindow : Form
    {
        Bitmap sourceBmp;
        Bitmap destinationBmp;
        private bool blMouseInMoveMode;
        int startingX;
        int startingY;
        RaserImageCrooper sourceImageCropper;
        VectorImageCrooper desinationImageCrooper;
        int horChange;
        int verChange;
        int mouseDownSourcePBLeft;
        int mouseDownSourcePBTop;
        int mouseDownDesinationPBLeft;
        int mouseDownDesinationPBTop;
        MainWindowSettings windowSettings;
        private string lastSavePath;
        MapFactory mapFactory;
        int srcLeftX;
        int srcLeftY;
        int resultLeftX;
        float resultLeftY;

        public MainWindow()
        {
            InitializeComponent();
            //pobieranie informacji o ostatnim savie
            SettingsRegister reg = new SettingsRegister();
            lastSavePath = reg.GetLastSaveInfo();
            RefreshLastSaveButton();

            windowSettings = new MainWindowSettings();
            windowSettings.dpScale = 1;
            ScaleRefresh();
            //tymczasowo
            LoadLastSave();
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
            Debug.Assert(false, "Akcja nie została zimplementowana.");
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

        DateTime UpdateInfoBoxTime(string aText = "", bool aBlNewLine = true, DateTime? aDatePrv = null)
         {
            infoBox.Text = infoBox.Text + '\n';
            if (aBlNewLine) 
                infoBox.Text = infoBox.Text + '\n';
            if (aDatePrv != null)
            {
                TimeSpan span = (DateTime.Now - aDatePrv.Value);
                infoBox.Text = infoBox.Text + "[T]:" + span.ToString(@"hh\:mm\:ss\:fff") + " - ";
            }
            infoBox.Text = infoBox.Text + aText;
            infoBox.SelectionStart = infoBox.Text.Length;
            infoBox.ScrollToCaret();
            return DateTime.Now;
         }

        void UpdateInfoBox(string aText = "", bool aBlNewLine = true)
        {
            UpdateInfoBoxTime(aText, aBlNewLine);
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

        private void SettingsToScr(MainWindowSettings aSettings)
        {
            maskedTextBox1.Text = aSettings.leftXCoord;
            maskedTextBox2.Text = aSettings.leftYCoord;
            maskedTextBox3.Text = aSettings.rightXCoord;
            maskedTextBox4.Text = aSettings.rightYCoord;
            edtSliceHeight.Text = aSettings.sliceHeight.ToString();
            edtSliceWidth.Text = aSettings.sliceWidth.ToString();
            ScaleRefresh();
            if (sourceImageCropper != null)
            {
                sourceImageCropper.centerX = aSettings.centerX;
                sourceImageCropper.centerY = aSettings.centerY;
                DrawCroppedScaledImage(windowSettings.dpScale, UpdateInfoBoxTime);
                //Dodać kod, który odczytaną mapę odpoweirdnio ustawi i wyświetli przy pomocy destinationImageCropper 
            }
            SetScaleControlEnable(true);

            chkBoxTestOptions.ClearSelected();
            if (aSettings.testOptions != "")
                foreach (string stIdx in aSettings.GetCheckegTestOptionsList())
                {
                    chkBoxTestOptions.SetItemCheckState( int.Parse(stIdx), CheckState.Checked);
                }

            string result = "";
            foreach (int i in chkBoxTestOptions.CheckedIndices)
            {
                if (result != "")
                    result += ",";
                result += i.ToString();
            }
            windowSettings.testOptions = result;

            //center
        }

        private void ScaleRefresh(){
            ScaleTB.Text = windowSettings.dpScale.ToString();
            ScaleTrB.Value = (int)windowSettings.dpScale;
        }

        private void ZoomInBtn_Click(object sender, EventArgs e)
        {
            if (windowSettings.dpScale < Cst.maxZoom)
            {

                if (DrawCroppedScaledImage(windowSettings.dpScale + 1, UpdateInfoBoxTime, windowSettings.dpScale))
                    windowSettings.dpScale += 1;
                    ScaleRefresh();
            }
        }

        private void ZoomOutBtn_Click(object sender, EventArgs e)
        {
            if (windowSettings.dpScale > 1)
            {
                if (DrawCroppedScaledImage(windowSettings.dpScale - 1, UpdateInfoBoxTime, windowSettings.dpScale))
                windowSettings.dpScale -= 1;
                ScaleRefresh();
            }
        }

        private void panel7_SizeChanged(object sender, EventArgs e)
        {
            int panelSize = (int)Math.Round((panel7.Height - 8 - 10 - 8) / 2.0);
            sourcePanel.Height = panelSize;
            destinationPanel.Height = panelSize;
            sourceImageCropper = new RaserImageCrooper(new Size(sourcePanel.Width, sourcePanel.Height), sourceBmp);
            desinationImageCrooper = new VectorImageCrooper(new Size(sourcePanel.Width, sourcePanel.Height), mapFactory,
                                                            sourceImageCropper.centerX, sourceImageCropper.centerY,
                                                            windowSettings, sourceBmp);
            DrawCroppedScaledImage(float.Parse(ScaleTB.Text), UpdateInfoBoxTime);
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox1_DragLeave(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_Leave(object sender, EventArgs e)
        {
            windowSettings.leftXCoord = ((MaskedTextBox)sender).Text;
        }

        private void maskedTextBox2_Leave(object sender, EventArgs e)
        {
            windowSettings.leftYCoord = ((MaskedTextBox)sender).Text;
        }

        private void maskedTextBox3_Leave(object sender, EventArgs e)
        {
            windowSettings.rightXCoord = ((MaskedTextBox)sender).Text;
        }

        private void maskedTextBox4_Leave(object sender, EventArgs e)
        {
            windowSettings.rightYCoord = ((MaskedTextBox)sender).Text;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            windowSettings.dpScale = float.Parse(((TextBox)sender).Text);
        }

        private void ScaleTb_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void ScaleTb_MouseUp(object sender, MouseEventArgs e)
        {
            if (ScaleTrB.Value != windowSettings.dpScale)
            {
                if (DrawCroppedScaledImage(ScaleTrB.Value, UpdateInfoBoxTime, windowSettings.dpScale))
                windowSettings.dpScale = ScaleTrB.Value;
                ScaleRefresh();
            }
        }

        private void ScaleTb_MouseMove(object sender, MouseEventArgs e)
        {
            ScaleTB.Text = ScaleTrB.Value.ToString();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuSaveAs(sender, e);
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuLoad(sender, e);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuSave(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadImage();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReloadImage();
        }

        private void loadLastSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadLastSave();
        }

        private void chkBoxTestOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            string result = "";
            foreach (int i in chkBoxTestOptions.CheckedIndices)
            {
                if (result != "") 
                    result += ",";
                result += i.ToString();
            }
            windowSettings.testOptions = result;
        }

        private void btnStartR2V_Click_1(object sender, EventArgs e)
        {
            Debug.Assert(sourceBmp != null, "Nie wgrano obrazu źródłowego.");
            R2VSettings r2vSettings = new R2VSettings{ sourceBmp = sourceBmp };
            r2vSettings.ReadGeoCorners(windowSettings.leftXCoord, windowSettings.leftYCoord, windowSettings.rightXCoord, windowSettings.rightYCoord);
            if (rbMainThread.Checked)
            {
                mapFactory = R2VRunner.RunR2VMainThread(r2vSettings, new UpdateInfoBoxTimeDelegate(UpdateInfoBoxTime));
               // Bitmap res = mapFactory.getBitmap(new Rectangle(0, 0, sourceBmp.Width, sourceBmp.Height));
            }
            else if (rbSeparateThread.Checked)
            {
                R2VRunner.RunRasterToVectorSeparateThread();
            }
            else
            {
                Debug.Assert(false, "Oba: Main i Separate = false");
            }
        }

        private void btnMainThread_Click(object sender, EventArgs e)
        {
            R2VSettings r2vSettings = new R2VSettings();
            r2vSettings.ReadGeoCorners(windowSettings.leftXCoord, 
                                       windowSettings.leftYCoord, 
                                       windowSettings.rightXCoord, 
                                       windowSettings.rightYCoord);
            r2vSettings.sourceBmp = sourceBmp;
            r2vSettings.CalculateGeoPx();
            r2vSettings.sliceWidth = windowSettings.sliceWidth;
            r2vSettings.sliceHeight = windowSettings.sliceHeight;
            r2vSettings.simplifyPhase1 = windowSettings.SimplifyPhase1();
            r2vSettings.simplifyPhase2 = windowSettings.SimplifyPhase2();
            r2vSettings.simplifyPhase3 = windowSettings.SimplifyPhase3();

            mapFactory = R2VRunner.RunR2VMainThread(r2vSettings, new UpdateInfoBoxTimeDelegate(UpdateInfoBoxTime));
            desinationImageCrooper = new VectorImageCrooper(new Size(sourcePanel.Width, sourcePanel.Height), mapFactory,
                                                            sourceImageCropper.centerX, sourceImageCropper.centerY,
                                                            windowSettings, sourceBmp);

            DrawCroppedScaledImage(float.Parse(ScaleTB.Text), UpdateInfoBoxTime);
        }

        private void btnSeparateThread_Click(object sender, EventArgs e)
        {
           R2VRunner. RunRasterToVectorSeparateThread();
        }

        private void btnStopR2V_Click(object sender, EventArgs e)
        {
            Debug.Assert(false, "Akcja nie została zimplementowana.");
        }

        private void btnRefreshResultImg_Click(object sender, EventArgs e)
        {
            DrawCroppedScaledImage(windowSettings.dpScale, UpdateInfoBoxTime, windowSettings.dpScale);
        }

        private void edtSliceWidth_Leave(object sender, EventArgs e)
        {
            if (((MaskedTextBox)sender).Text == "")
                windowSettings.sliceWidth = 0;
            else
                windowSettings.sliceWidth = Int32.Parse(((MaskedTextBox)sender).Text);
        }


        private void edtSliceHeight_Leave(object sender, EventArgs e)
        {
            if (((MaskedTextBox)sender).Text == "")
                windowSettings.sliceHeight = 0;
            else
                windowSettings.sliceHeight = Int32.Parse(((MaskedTextBox)sender).Text);
        }

        private void edtSliceHeight_Validated(object sender, EventArgs e)
        {
            int r = 0;
        }

        private void menuMain_MouseClick(object sender, MouseEventArgs e)
        {
            Focus();
        }

        private void destinationPB_Click(object sender, EventArgs e)
        {
            Focus();
            /*windowSettings.dpScale;
            windowSettings.leftXCoord;
            windowSettings.centerX;
            srcLeftX = srcRect.X;
            srcLeftY = srcRect.Y;
            resultLeftX = resultRect.X;
            resultLeftY = resultRect.Y;*/
            int x = (int)Math.Floor((((MouseEventArgs)e).X - resultLeftX) / windowSettings.dpScale) + srcLeftX;
            int y = (int)Math.Floor((((MouseEventArgs)e).Y - resultLeftY) / windowSettings.dpScale) + srcLeftY;

            string info = "";
            info += string.Format("Point({0},{1}):", x, y);
            Vector_Rectangle vr = null;
            if (x >= 0 && x < mapFactory.vectArr.Length && y >= 0 && y < mapFactory.vectArr[0].Length)
            {
                vr = mapFactory.vectArr[x][y];
            }
            if (vr != null)
                info += '\n' + string.Format("color={0}", vr.color);
            PointAdv pointAdv = null;
            if (x >= 0 && x <= mapFactory.vectArr.Length && y >= 0 && y <= mapFactory.vectArr[0].Length)
            {
                pointAdv = mapFactory.pointAdvArr[x][y];
            }
            if (pointAdv != null)
            {
                info += '\n' + string.Format("type={0}", mapFactory.pointAdvArr[x][y].GetKdPointType());
            }
            else
                info += '\n' + string.Format("Punkt poza obszarem mapy."); 

            //Granica całkowita
            /*if (vr != null)
            {
                VectoredRectangleGroup group = mapFactory[mapFactory.colorArr[x][y].group];
                info += '\n' + group.edgeList.GetPointsStr();
            }*/

            //Fragmenty granic
            if (pointAdv != null)
            {
                foreach (GeoEdgePart geoEdgePart in pointAdv.geoEdgePartList)
                    info += '\n' + string.Format("PartEdge: {0}", geoEdgePart.GeoEdgePointPictToString());
            }
            
            UpdateInfoBoxTime( info );
          //  UpdateInfoBoxTime( mapFactory.colorArr[x][y]..ToString );
        }



    }
}