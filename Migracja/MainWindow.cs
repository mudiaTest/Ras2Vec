using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace Migracja
{
    public partial class MainWindow : Form
    {
        Bitmap sourceBmp;
        Bitmap posterizedBmp;
        private bool blMouseInMoveMode;
        int startingX;
        int startingY;
        RaserImageCrooper sourceImageCropper;
        RaserImageCrooper posterizedImageCropper;
        VectorImageCrooper desinationImageCropper;
        int horChange;
        int verChange;
        int mouseDownSourcePBLeft;
        int mouseDownSourcePBTop;
        int mouseDownPosterizedPBLeft;
        int mouseDownPosterizedPBTop;
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
            windowSettings.dpScaleVect = 1;
            ScaleRefresh();
            //tymczasowo
            LoadLastSave();
            ResizePanelPict();
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
            if (panelInputVect.Visible)
            {
                panelInputVect.Visible = false;
                panelMenuVect.Width = 34;
                btnMenuVectHide.Text = ">>";
            }
            else
            {
                panelInputVect.Visible = true;
                panelMenuVect.Width = 333;
                btnMenuVectHide.Text = "<<";
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

        private void posterizedPB_MouseDown(object sender, MouseEventArgs e)
        {
            StartMovingPictures();
        }

        private void posterizedPB_MouseUp(object sender, MouseEventArgs e)
        {
            StopMovingPictures(MovedPicture.posterized);
        }

        private void posterizedPB_MouseMove(object sender, MouseEventArgs e)
        {
            MovePictures(e);
        }

        private void posterizedPB_MouseLeave(object sender, EventArgs e)
        {
            blMouseInMoveMode = false;
        }

        private void destinationPB_MouseDown(object sender, MouseEventArgs e)
        {
            if (destinationPB.Image != null) 
                StartMovingPictures();
        }

        private void destinationPB_MouseLeave(object sender, EventArgs e)
        {
            blMouseInMoveMode = false;
        }

        private void destinationPB_MouseMove(object sender, MouseEventArgs e)
        {
            if (destinationPB.Image != null)
                MovePictures(e);
        }

        private void destinationPB_MouseUp(object sender, MouseEventArgs e)
        {
            if (destinationPB.Image != null)
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
            edtSliceHeight.Text = aSettings.sliceHeightVect.ToString();
            edtSliceWidth.Text = aSettings.sliceWidthVect.ToString();
            ScaleRefresh();
            if (posterizedImageCropper != null)
            {
                posterizedImageCropper.centerX = aSettings.centerXVect;
                posterizedImageCropper.centerY = aSettings.centerYVect;
                DrawCroppedScaledImage(windowSettings.dpScaleVect, UpdateInfoBoxTime);
                //Dodać kod, który odczytaną mapę odpoweirdnio ustawi i wyświetli przy pomocy destinationImageCropper 
            }
            /*if (sourceImage4ColCropper != null)
            {
                sourceImage4ColCropper.centerX = aSettings.centerXVect;
                sourceImage4ColCropper.centerY = aSettings.centerYVect;
                DrawCroppedScaledImage4Col(windowSettings.dpScaleVect, UpdateInfoBoxTime);
                //Dodać kod, który odczytaną mapę odpoweirdnio ustawi i wyświetli przy pomocy destinationImageCropper 
            }*/
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
            txtScaleLvlVect.Text = windowSettings.dpScaleVect.ToString();
            trScaleVect.Value = (int)windowSettings.dpScaleVect;
        }

        private void ZoomInBtn_Click(object sender, EventArgs e)
        {
            if (windowSettings.dpScaleVect < Cst.maxZoom)
            {

                if (DrawCroppedScaledImage(windowSettings.dpScaleVect + 1, UpdateInfoBoxTime, windowSettings.dpScaleVect))
                    windowSettings.dpScaleVect += 1;
                    ScaleRefresh();
            }
        }

        private void ZoomOutBtn_Click(object sender, EventArgs e)
        {
            if (windowSettings.dpScaleVect > 1)
            {
                if (DrawCroppedScaledImage(windowSettings.dpScaleVect - 1, UpdateInfoBoxTime, windowSettings.dpScaleVect))
                windowSettings.dpScaleVect -= 1;
                ScaleRefresh();
            }
        }

        private void panel7_SizeChanged(object sender, EventArgs e)
        {
            ResizePanelPict();
        }

        private void ResizePanelPict()
        {
            int imagePanelHeight = (int)Math.Round((flpPictures.Height - 3 - 3 - 3 - 3) / 3.0);
            int imagePanelWidth = flpPictures.Width - 3 - 3;
            sourcePanel.Height = imagePanelHeight;
            posterizedPanel.Height = imagePanelHeight;
            destinationPanel.Height = imagePanelHeight;
            sourcePanel.Width = imagePanelWidth;
            posterizedPanel.Width = imagePanelWidth;
            destinationPanel.Width = imagePanelWidth;
            //posterizedPanel.Top = 5 + imagePanelHeight + 5;
            //destinationPanel.Top = 5 + imagePanelHeight + 5 + imagePanelHeight + 5;
            posterizedImageCropper = new RaserImageCrooper(new Size(posterizedPanel.Width, posterizedPanel.Height),
                                                           sourceImageCropper.centerX, sourceImageCropper.centerY, posterizedBmp);
            desinationImageCropper = new VectorImageCrooper(new Size(destinationPanel.Width, destinationPanel.Height), mapFactory,
                                                            posterizedImageCropper.centerX, posterizedImageCropper.centerY,
                                                            windowSettings, posterizedBmp);
            DrawCroppedScaledImage(float.Parse(txtScaleLvlVect.Text), UpdateInfoBoxTime);
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
            windowSettings.dpScaleVect = float.Parse(((TextBox)sender).Text);
        }

        private void ScaleTb_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void ScaleTb_MouseUp(object sender, MouseEventArgs e)
        {
            if (trScaleVect.Value != windowSettings.dpScaleVect)
            {
                if (DrawCroppedScaledImage(trScaleVect.Value, UpdateInfoBoxTime, windowSettings.dpScaleVect))
                windowSettings.dpScaleVect = trScaleVect.Value;
                ScaleRefresh();
            }
        }

        private void ScaleTb_MouseMove(object sender, MouseEventArgs e)
        {
            txtScaleLvlVect.Text = trScaleVect.Value.ToString();
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
            Debug.Assert(posterizedBmp != null, "Nie wgrano obrazu źródłowego.");
            R2VSettings r2vSettings = new R2VSettings { sourceBmp = posterizedBmp };
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
            r2vSettings.sourceBmp = posterizedBmp;
            r2vSettings.CalculateGeoPx();
            r2vSettings.sliceWidth = windowSettings.sliceWidthVect;
            r2vSettings.sliceHeight = windowSettings.sliceHeightVect;
            r2vSettings.simplifyPhase1 = windowSettings.SimplifyPhase1();
            r2vSettings.simplifyPhase2 = windowSettings.SimplifyPhase2();
            r2vSettings.simplifyPhase3 = windowSettings.SimplifyPhase3();

            mapFactory = R2VRunner.RunR2VMainThread(r2vSettings, new UpdateInfoBoxTimeDelegate(UpdateInfoBoxTime));
            desinationImageCropper = new VectorImageCrooper(new Size(posterizedPanel.Width, posterizedPanel.Height), mapFactory,
                                                            posterizedImageCropper.centerX, posterizedImageCropper.centerY,
                                                            windowSettings, posterizedBmp);

            DrawCroppedScaledImage(float.Parse(txtScaleLvlVect.Text), UpdateInfoBoxTime);
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
            DrawCroppedScaledImage(windowSettings.dpScaleVect, UpdateInfoBoxTime, windowSettings.dpScaleVect);
        }

        private void edtSliceWidth_Leave(object sender, EventArgs e)
        {
            if (((MaskedTextBox)sender).Text == "")
                windowSettings.sliceWidthVect = 0;
            else
                windowSettings.sliceWidthVect = Int32.Parse(((MaskedTextBox)sender).Text);
        }


        private void edtSliceHeight_Leave(object sender, EventArgs e)
        {
            if (((MaskedTextBox)sender).Text == "")
                windowSettings.sliceHeightVect = 0;
            else
                windowSettings.sliceHeightVect = Int32.Parse(((MaskedTextBox)sender).Text);
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
            int x = (int)Math.Floor((((MouseEventArgs)e).X - resultLeftX) / windowSettings.dpScaleVect) + srcLeftX;
            int y = (int)Math.Floor((((MouseEventArgs)e).Y - resultLeftY) / windowSettings.dpScaleVect) + srcLeftY;

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

        private void btnMainThread_Click_1(object sender, EventArgs e)
        {

        }

        private void panelC2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel16_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            LoadImage();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ReloadImage();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*DrawCroppedScaledImage4Col(windowSettings.dpScaleVect, UpdateInfoBoxTime, windowSettings.dpScaleVect);*/
        }

        private void posterizeSrcImageBtn_Click(object sender, EventArgs e)
        {
            ColorChanger cc = new ColorChanger();
            cc.PosterizeBitmap(sourceBmp, ref posterizedBmp);
            posterizedImageCropper.srcBmp = posterizedBmp;
            /*DrawCroppedScaledImage4Col(windowSettings.dpScaleVect, UpdateInfoBoxTime, windowSettings.dpScaleVect);*/
            DrawCroppedScaledImage(windowSettings.dpScaleVect, UpdateInfoBoxTime, windowSettings.dpScaleVect);
        }
        
    }
}