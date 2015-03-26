namespace Migracja
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.mainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadLastSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.loadDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveDialog = new System.Windows.Forms.SaveFileDialog();
            this.mainTab = new System.Windows.Forms.TabControl();
            this.tabColor = new System.Windows.Forms.TabPage();
            this.flpColors = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnPosterize = new System.Windows.Forms.Button();
            this.btnAddColorPanel = new System.Windows.Forms.Button();
            this.tabVect = new System.Windows.Forms.TabPage();
            this.panelMainVect = new System.Windows.Forms.Panel();
            this.panelMenuVect = new System.Windows.Forms.Panel();
            this.panelInputVect = new System.Windows.Forms.Panel();
            this.posterizeSrcImageBtn = new System.Windows.Forms.Button();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.edtSliceHeight = new System.Windows.Forms.MaskedTextBox();
            this.edtSliceWidth = new System.Windows.Forms.MaskedTextBox();
            this.reloadSrcMapVectBtn = new System.Windows.Forms.Button();
            this.infoBox = new System.Windows.Forms.RichTextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblY2 = new System.Windows.Forms.Label();
            this.lblX2 = new System.Windows.Forms.Label();
            this.maskedTextBox4 = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox3 = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox2 = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.loadSrcMapVectBtn = new System.Windows.Forms.Button();
            this.chkBoxTestOptions = new System.Windows.Forms.CheckedListBox();
            this.btnRefreshResultVectImg = new System.Windows.Forms.Button();
            this.btnMainThread = new System.Windows.Forms.Button();
            this.gbThreadChoice = new System.Windows.Forms.GroupBox();
            this.rbSeparateThread = new System.Windows.Forms.RadioButton();
            this.rbMainThread = new System.Windows.Forms.RadioButton();
            this.btnStopR2V = new System.Windows.Forms.Button();
            this.btnStartR2V = new System.Windows.Forms.Button();
            this.btnSeparateThread = new System.Windows.Forms.Button();
            this.panelMenuVectHide = new System.Windows.Forms.Panel();
            this.btnMenuVectHide = new System.Windows.Forms.Button();
            this.panelPict = new System.Windows.Forms.Panel();
            this.flpPictures = new System.Windows.Forms.FlowLayoutPanel();
            this.sourcePanel = new System.Windows.Forms.Panel();
            this.sourcePB = new System.Windows.Forms.PictureBox();
            this.posterizedPanel = new System.Windows.Forms.Panel();
            this.posterizedPB = new System.Windows.Forms.PictureBox();
            this.destinationPanel = new System.Windows.Forms.Panel();
            this.destinationPB = new System.Windows.Forms.PictureBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnZoomInVect = new System.Windows.Forms.Button();
            this.btnZoomOutVect = new System.Windows.Forms.Button();
            this.txtScaleLvlVect = new System.Windows.Forms.TextBox();
            this.trScaleVect = new System.Windows.Forms.TrackBar();
            this.menuMain.SuspendLayout();
            this.mainTab.SuspendLayout();
            this.tabColor.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabVect.SuspendLayout();
            this.panelMainVect.SuspendLayout();
            this.panelMenuVect.SuspendLayout();
            this.panelInputVect.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel5.SuspendLayout();
            this.gbThreadChoice.SuspendLayout();
            this.panelMenuVectHide.SuspendLayout();
            this.panelPict.SuspendLayout();
            this.flpPictures.SuspendLayout();
            this.sourcePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sourcePB)).BeginInit();
            this.posterizedPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.posterizedPB)).BeginInit();
            this.destinationPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.destinationPB)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trScaleVect)).BeginInit();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainToolStripMenuItem,
            this.actionsToolStripMenuItem,
            this.testToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuMain.Size = new System.Drawing.Size(853, 24);
            this.menuMain.TabIndex = 0;
            this.menuMain.MouseClick += new System.Windows.Forms.MouseEventHandler(this.menuMain_MouseClick);
            // 
            // mainToolStripMenuItem
            // 
            this.mainToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.toolStripSeparator1,
            this.loadToolStripMenuItem,
            this.loadLastSaveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem1});
            this.mainToolStripMenuItem.Name = "mainToolStripMenuItem";
            this.mainToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.mainToolStripMenuItem.Text = "Main";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.newToolStripMenuItem.Text = "New";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(142, 6);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // loadLastSaveToolStripMenuItem
            // 
            this.loadLastSaveToolStripMenuItem.Name = "loadLastSaveToolStripMenuItem";
            this.loadLastSaveToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.loadLastSaveToolStripMenuItem.Text = "Load lastSave";
            this.loadLastSaveToolStripMenuItem.Visible = false;
            this.loadLastSaveToolStripMenuItem.Click += new System.EventHandler(this.loadLastSaveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.saveAsToolStripMenuItem.Text = "Save As ...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(142, 6);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(145, 22);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.actionsToolStripMenuItem.Text = "Actions";
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.testToolStripMenuItem.Text = "Test";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // loadDialog
            // 
            this.loadDialog.FileName = "openFileDialog1";
            // 
            // mainTab
            // 
            this.mainTab.Controls.Add(this.tabColor);
            this.mainTab.Controls.Add(this.tabVect);
            this.mainTab.Dock = System.Windows.Forms.DockStyle.Left;
            this.mainTab.Location = new System.Drawing.Point(0, 24);
            this.mainTab.Name = "mainTab";
            this.mainTab.SelectedIndex = 0;
            this.mainTab.Size = new System.Drawing.Size(365, 513);
            this.mainTab.TabIndex = 1;
            this.mainTab.Tag = "";
            // 
            // tabColor
            // 
            this.tabColor.Controls.Add(this.flpColors);
            this.tabColor.Controls.Add(this.panel2);
            this.tabColor.Location = new System.Drawing.Point(4, 22);
            this.tabColor.Name = "tabColor";
            this.tabColor.Padding = new System.Windows.Forms.Padding(3);
            this.tabColor.Size = new System.Drawing.Size(357, 487);
            this.tabColor.TabIndex = 2;
            this.tabColor.Text = "Kolor";
            this.tabColor.UseVisualStyleBackColor = true;
            // 
            // flpColors
            // 
            this.flpColors.AutoScroll = true;
            this.flpColors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpColors.Location = new System.Drawing.Point(3, 3);
            this.flpColors.Margin = new System.Windows.Forms.Padding(0);
            this.flpColors.Name = "flpColors";
            this.flpColors.Size = new System.Drawing.Size(351, 441);
            this.flpColors.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnPosterize);
            this.panel2.Controls.Add(this.btnAddColorPanel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 444);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(351, 40);
            this.panel2.TabIndex = 1;
            // 
            // btnPosterize
            // 
            this.btnPosterize.Location = new System.Drawing.Point(48, 8);
            this.btnPosterize.Name = "btnPosterize";
            this.btnPosterize.Size = new System.Drawing.Size(63, 24);
            this.btnPosterize.TabIndex = 1;
            this.btnPosterize.Text = "Posterize";
            this.btnPosterize.UseVisualStyleBackColor = true;
            this.btnPosterize.Click += new System.EventHandler(this.btnPosterize_Click);
            // 
            // btnAddColorPanel
            // 
            this.btnAddColorPanel.Location = new System.Drawing.Point(7, 8);
            this.btnAddColorPanel.Name = "btnAddColorPanel";
            this.btnAddColorPanel.Size = new System.Drawing.Size(35, 24);
            this.btnAddColorPanel.TabIndex = 0;
            this.btnAddColorPanel.Text = "Add";
            this.btnAddColorPanel.UseVisualStyleBackColor = true;
            this.btnAddColorPanel.Click += new System.EventHandler(this.btnAddColorPanel_Click);
            // 
            // tabVect
            // 
            this.tabVect.Controls.Add(this.panelMainVect);
            this.tabVect.Location = new System.Drawing.Point(4, 22);
            this.tabVect.Name = "tabVect";
            this.tabVect.Padding = new System.Windows.Forms.Padding(3);
            this.tabVect.Size = new System.Drawing.Size(357, 487);
            this.tabVect.TabIndex = 1;
            this.tabVect.Text = "Wektoryzacja";
            this.tabVect.UseVisualStyleBackColor = true;
            // 
            // panelMainVect
            // 
            this.panelMainVect.Controls.Add(this.panelMenuVect);
            this.panelMainVect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainVect.Location = new System.Drawing.Point(3, 3);
            this.panelMainVect.Name = "panelMainVect";
            this.panelMainVect.Size = new System.Drawing.Size(351, 481);
            this.panelMainVect.TabIndex = 12;
            // 
            // panelMenuVect
            // 
            this.panelMenuVect.Controls.Add(this.panelInputVect);
            this.panelMenuVect.Controls.Add(this.panelMenuVectHide);
            this.panelMenuVect.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenuVect.Location = new System.Drawing.Point(0, 0);
            this.panelMenuVect.Name = "panelMenuVect";
            this.panelMenuVect.Size = new System.Drawing.Size(349, 481);
            this.panelMenuVect.TabIndex = 12;
            // 
            // panelInputVect
            // 
            this.panelInputVect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelInputVect.Controls.Add(this.posterizeSrcImageBtn);
            this.panelInputVect.Controls.Add(this.panel9);
            this.panelInputVect.Controls.Add(this.panel8);
            this.panelInputVect.Controls.Add(this.reloadSrcMapVectBtn);
            this.panelInputVect.Controls.Add(this.infoBox);
            this.panelInputVect.Controls.Add(this.panel5);
            this.panelInputVect.Controls.Add(this.loadSrcMapVectBtn);
            this.panelInputVect.Controls.Add(this.chkBoxTestOptions);
            this.panelInputVect.Controls.Add(this.btnRefreshResultVectImg);
            this.panelInputVect.Controls.Add(this.btnMainThread);
            this.panelInputVect.Controls.Add(this.gbThreadChoice);
            this.panelInputVect.Controls.Add(this.btnSeparateThread);
            this.panelInputVect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelInputVect.Location = new System.Drawing.Point(34, 0);
            this.panelInputVect.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.panelInputVect.Name = "panelInputVect";
            this.panelInputVect.Size = new System.Drawing.Size(315, 481);
            this.panelInputVect.TabIndex = 26;
            this.panelInputVect.Click += new System.EventHandler(this.w);
            // 
            // posterizeSrcImageBtn
            // 
            this.posterizeSrcImageBtn.Location = new System.Drawing.Point(206, 133);
            this.posterizeSrcImageBtn.Name = "posterizeSrcImageBtn";
            this.posterizeSrcImageBtn.Size = new System.Drawing.Size(99, 23);
            this.posterizeSrcImageBtn.TabIndex = 31;
            this.posterizeSrcImageBtn.Text = "Posterize";
            this.posterizeSrcImageBtn.UseVisualStyleBackColor = true;
            this.posterizeSrcImageBtn.Click += new System.EventHandler(this.posterizeSrcImageBtn_Click);
            // 
            // panel9
            // 
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel9.Location = new System.Drawing.Point(156, 43);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(152, 81);
            this.panel9.TabIndex = 30;
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.label3);
            this.panel8.Controls.Add(this.label4);
            this.panel8.Controls.Add(this.edtSliceHeight);
            this.panel8.Controls.Add(this.edtSliceWidth);
            this.panel8.Enabled = false;
            this.panel8.Location = new System.Drawing.Point(156, 4);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(152, 33);
            this.panel8.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(84, 5);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "Y";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 5);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "X";
            // 
            // edtSliceHeight
            // 
            this.edtSliceHeight.BackColor = System.Drawing.SystemColors.Info;
            this.edtSliceHeight.Location = new System.Drawing.Point(101, 3);
            this.edtSliceHeight.Mask = "000000";
            this.edtSliceHeight.Name = "edtSliceHeight";
            this.edtSliceHeight.ReadOnly = true;
            this.edtSliceHeight.Size = new System.Drawing.Size(44, 20);
            this.edtSliceHeight.TabIndex = 30;
            this.edtSliceHeight.Leave += new System.EventHandler(this.edtSliceHeight_Leave);
            // 
            // edtSliceWidth
            // 
            this.edtSliceWidth.BackColor = System.Drawing.SystemColors.Info;
            this.edtSliceWidth.Location = new System.Drawing.Point(21, 3);
            this.edtSliceWidth.Mask = "000000";
            this.edtSliceWidth.Name = "edtSliceWidth";
            this.edtSliceWidth.ReadOnly = true;
            this.edtSliceWidth.Size = new System.Drawing.Size(44, 20);
            this.edtSliceWidth.TabIndex = 29;
            this.edtSliceWidth.Leave += new System.EventHandler(this.edtSliceWidth_Leave);
            // 
            // reloadSrcMapVectBtn
            // 
            this.reloadSrcMapVectBtn.Enabled = false;
            this.reloadSrcMapVectBtn.Location = new System.Drawing.Point(193, 335);
            this.reloadSrcMapVectBtn.Name = "reloadSrcMapVectBtn";
            this.reloadSrcMapVectBtn.Size = new System.Drawing.Size(112, 23);
            this.reloadSrcMapVectBtn.TabIndex = 26;
            this.reloadSrcMapVectBtn.Text = "Reload source map";
            this.reloadSrcMapVectBtn.UseVisualStyleBackColor = true;
            this.reloadSrcMapVectBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // infoBox
            // 
            this.infoBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.infoBox.Location = new System.Drawing.Point(8, 366);
            this.infoBox.Name = "infoBox";
            this.infoBox.Size = new System.Drawing.Size(295, 102);
            this.infoBox.TabIndex = 25;
            this.infoBox.Text = "";
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.label1);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Controls.Add(this.lblY2);
            this.panel5.Controls.Add(this.lblX2);
            this.panel5.Controls.Add(this.maskedTextBox4);
            this.panel5.Controls.Add(this.maskedTextBox3);
            this.panel5.Controls.Add(this.maskedTextBox2);
            this.panel5.Controls.Add(this.maskedTextBox1);
            this.panel5.Location = new System.Drawing.Point(8, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(142, 120);
            this.panel5.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Y";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 5);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "X";
            // 
            // lblY2
            // 
            this.lblY2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblY2.AutoSize = true;
            this.lblY2.Location = new System.Drawing.Point(55, 96);
            this.lblY2.Margin = new System.Windows.Forms.Padding(0);
            this.lblY2.Name = "lblY2";
            this.lblY2.Size = new System.Drawing.Size(14, 13);
            this.lblY2.TabIndex = 26;
            this.lblY2.Text = "Y";
            // 
            // lblX2
            // 
            this.lblX2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblX2.AutoSize = true;
            this.lblX2.Location = new System.Drawing.Point(55, 70);
            this.lblX2.Margin = new System.Windows.Forms.Padding(0);
            this.lblX2.Name = "lblX2";
            this.lblX2.Size = new System.Drawing.Size(14, 13);
            this.lblX2.TabIndex = 25;
            this.lblX2.Text = "X";
            // 
            // maskedTextBox4
            // 
            this.maskedTextBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.maskedTextBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.maskedTextBox4.Location = new System.Drawing.Point(69, 94);
            this.maskedTextBox4.Mask = "00.00.00.00";
            this.maskedTextBox4.Name = "maskedTextBox4";
            this.maskedTextBox4.Size = new System.Drawing.Size(68, 20);
            this.maskedTextBox4.TabIndex = 24;
            this.maskedTextBox4.Leave += new System.EventHandler(this.maskedTextBox4_Leave);
            // 
            // maskedTextBox3
            // 
            this.maskedTextBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.maskedTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.maskedTextBox3.Location = new System.Drawing.Point(69, 68);
            this.maskedTextBox3.Mask = "00.00.00.00";
            this.maskedTextBox3.Name = "maskedTextBox3";
            this.maskedTextBox3.Size = new System.Drawing.Size(68, 20);
            this.maskedTextBox3.TabIndex = 23;
            this.maskedTextBox3.Leave += new System.EventHandler(this.maskedTextBox3_Leave);
            // 
            // maskedTextBox2
            // 
            this.maskedTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.maskedTextBox2.Location = new System.Drawing.Point(15, 29);
            this.maskedTextBox2.Mask = "00.00.00.00";
            this.maskedTextBox2.Name = "maskedTextBox2";
            this.maskedTextBox2.Size = new System.Drawing.Size(68, 20);
            this.maskedTextBox2.TabIndex = 22;
            this.maskedTextBox2.Leave += new System.EventHandler(this.maskedTextBox2_Leave);
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.maskedTextBox1.Location = new System.Drawing.Point(15, 3);
            this.maskedTextBox1.Mask = "00.00.00.00";
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(68, 20);
            this.maskedTextBox1.TabIndex = 21;
            this.maskedTextBox1.Leave += new System.EventHandler(this.maskedTextBox1_Leave);
            // 
            // loadSrcMapVectBtn
            // 
            this.loadSrcMapVectBtn.Location = new System.Drawing.Point(89, 335);
            this.loadSrcMapVectBtn.Name = "loadSrcMapVectBtn";
            this.loadSrcMapVectBtn.Size = new System.Drawing.Size(98, 23);
            this.loadSrcMapVectBtn.TabIndex = 14;
            this.loadSrcMapVectBtn.Text = "Load source map";
            this.loadSrcMapVectBtn.UseVisualStyleBackColor = true;
            this.loadSrcMapVectBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // chkBoxTestOptions
            // 
            this.chkBoxTestOptions.BackColor = System.Drawing.SystemColors.Control;
            this.chkBoxTestOptions.CheckOnClick = true;
            this.chkBoxTestOptions.FormattingEnabled = true;
            this.chkBoxTestOptions.Items.AddRange(new object[] {
            "Polygons - po R2V (yes) / Rectangles - z Rastra (no)",
            "Edge (polygons) / Grid (rectangles)",
            "Kolor testowy",
            "Uproszczona krawędź",
            "Faza 1 upraszczania (rectangle)",
            "Faza 2 upraszczania (punkty w liniach Vert. i Hor.)",
            "Faza 3 upraszczania (punkty w okilicach linii prostych)"});
            this.chkBoxTestOptions.Location = new System.Drawing.Point(8, 217);
            this.chkBoxTestOptions.Name = "chkBoxTestOptions";
            this.chkBoxTestOptions.Size = new System.Drawing.Size(297, 109);
            this.chkBoxTestOptions.TabIndex = 10;
            this.chkBoxTestOptions.SelectedIndexChanged += new System.EventHandler(this.chkBoxTestOptions_SelectedIndexChanged);
            // 
            // btnRefreshResultVectImg
            // 
            this.btnRefreshResultVectImg.Location = new System.Drawing.Point(8, 335);
            this.btnRefreshResultVectImg.Name = "btnRefreshResultVectImg";
            this.btnRefreshResultVectImg.Size = new System.Drawing.Size(75, 23);
            this.btnRefreshResultVectImg.TabIndex = 16;
            this.btnRefreshResultVectImg.Text = "Refresh result";
            this.btnRefreshResultVectImg.UseVisualStyleBackColor = true;
            this.btnRefreshResultVectImg.Click += new System.EventHandler(this.btnRefreshResultImg_Click);
            // 
            // btnMainThread
            // 
            this.btnMainThread.Location = new System.Drawing.Point(206, 159);
            this.btnMainThread.Name = "btnMainThread";
            this.btnMainThread.Size = new System.Drawing.Size(99, 23);
            this.btnMainThread.TabIndex = 11;
            this.btnMainThread.Text = "MainThread";
            this.btnMainThread.UseVisualStyleBackColor = true;
            this.btnMainThread.Click += new System.EventHandler(this.btnMainThread_Click);
            // 
            // gbThreadChoice
            // 
            this.gbThreadChoice.Controls.Add(this.rbSeparateThread);
            this.gbThreadChoice.Controls.Add(this.rbMainThread);
            this.gbThreadChoice.Controls.Add(this.btnStopR2V);
            this.gbThreadChoice.Controls.Add(this.btnStartR2V);
            this.gbThreadChoice.Location = new System.Drawing.Point(7, 149);
            this.gbThreadChoice.Margin = new System.Windows.Forms.Padding(0);
            this.gbThreadChoice.Name = "gbThreadChoice";
            this.gbThreadChoice.Padding = new System.Windows.Forms.Padding(0);
            this.gbThreadChoice.Size = new System.Drawing.Size(193, 65);
            this.gbThreadChoice.TabIndex = 12;
            this.gbThreadChoice.TabStop = false;
            // 
            // rbSeparateThread
            // 
            this.rbSeparateThread.AutoSize = true;
            this.rbSeparateThread.Location = new System.Drawing.Point(6, 39);
            this.rbSeparateThread.Name = "rbSeparateThread";
            this.rbSeparateThread.Size = new System.Drawing.Size(102, 17);
            this.rbSeparateThread.TabIndex = 6;
            this.rbSeparateThread.Text = "SeparateThread";
            this.rbSeparateThread.UseVisualStyleBackColor = true;
            // 
            // rbMainThread
            // 
            this.rbMainThread.AutoSize = true;
            this.rbMainThread.Checked = true;
            this.rbMainThread.Location = new System.Drawing.Point(6, 13);
            this.rbMainThread.Name = "rbMainThread";
            this.rbMainThread.Size = new System.Drawing.Size(82, 17);
            this.rbMainThread.TabIndex = 5;
            this.rbMainThread.TabStop = true;
            this.rbMainThread.Text = "MainThread";
            this.rbMainThread.UseVisualStyleBackColor = true;
            // 
            // btnStopR2V
            // 
            this.btnStopR2V.Location = new System.Drawing.Point(111, 39);
            this.btnStopR2V.Name = "btnStopR2V";
            this.btnStopR2V.Size = new System.Drawing.Size(75, 23);
            this.btnStopR2V.TabIndex = 15;
            this.btnStopR2V.Text = "Stop R2V";
            this.btnStopR2V.UseVisualStyleBackColor = true;
            this.btnStopR2V.Click += new System.EventHandler(this.btnStopR2V_Click);
            // 
            // btnStartR2V
            // 
            this.btnStartR2V.Location = new System.Drawing.Point(111, 10);
            this.btnStartR2V.Name = "btnStartR2V";
            this.btnStartR2V.Size = new System.Drawing.Size(75, 23);
            this.btnStartR2V.TabIndex = 14;
            this.btnStartR2V.Text = "Start R2V";
            this.btnStartR2V.UseVisualStyleBackColor = true;
            this.btnStartR2V.Click += new System.EventHandler(this.btnStartR2V_Click);
            // 
            // btnSeparateThread
            // 
            this.btnSeparateThread.Location = new System.Drawing.Point(206, 187);
            this.btnSeparateThread.Name = "btnSeparateThread";
            this.btnSeparateThread.Size = new System.Drawing.Size(99, 23);
            this.btnSeparateThread.TabIndex = 13;
            this.btnSeparateThread.Text = "SeparateThread";
            this.btnSeparateThread.UseVisualStyleBackColor = true;
            this.btnSeparateThread.Click += new System.EventHandler(this.btnSeparateThread_Click);
            // 
            // panelMenuVectHide
            // 
            this.panelMenuVectHide.Controls.Add(this.btnMenuVectHide);
            this.panelMenuVectHide.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenuVectHide.Location = new System.Drawing.Point(0, 0);
            this.panelMenuVectHide.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.panelMenuVectHide.Name = "panelMenuVectHide";
            this.panelMenuVectHide.Size = new System.Drawing.Size(34, 481);
            this.panelMenuVectHide.TabIndex = 25;
            // 
            // btnMenuVectHide
            // 
            this.btnMenuVectHide.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnMenuVectHide.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnMenuVectHide.Location = new System.Drawing.Point(0, 0);
            this.btnMenuVectHide.Margin = new System.Windows.Forms.Padding(0);
            this.btnMenuVectHide.Name = "btnMenuVectHide";
            this.btnMenuVectHide.Size = new System.Drawing.Size(30, 481);
            this.btnMenuVectHide.TabIndex = 0;
            this.btnMenuVectHide.Text = "<<";
            this.btnMenuVectHide.UseVisualStyleBackColor = true;
            this.btnMenuVectHide.Click += new System.EventHandler(this.btnMenuHide_Click);
            // 
            // panelPict
            // 
            this.panelPict.AutoSize = true;
            this.panelPict.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPict.Controls.Add(this.flpPictures);
            this.panelPict.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPict.Location = new System.Drawing.Point(0, 40);
            this.panelPict.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.panelPict.Name = "panelPict";
            this.panelPict.Size = new System.Drawing.Size(489, 471);
            this.panelPict.TabIndex = 14;
            this.panelPict.SizeChanged += new System.EventHandler(this.panel7_SizeChanged);
            // 
            // flpPictures
            // 
            this.flpPictures.Controls.Add(this.sourcePanel);
            this.flpPictures.Controls.Add(this.posterizedPanel);
            this.flpPictures.Controls.Add(this.destinationPanel);
            this.flpPictures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpPictures.Location = new System.Drawing.Point(0, 0);
            this.flpPictures.Name = "flpPictures";
            this.flpPictures.Size = new System.Drawing.Size(487, 469);
            this.flpPictures.TabIndex = 25;
            // 
            // sourcePanel
            // 
            this.sourcePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sourcePanel.Controls.Add(this.sourcePB);
            this.sourcePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.sourcePanel.Location = new System.Drawing.Point(3, 3);
            this.sourcePanel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.sourcePanel.Name = "sourcePanel";
            this.sourcePanel.Size = new System.Drawing.Size(480, 108);
            this.sourcePanel.TabIndex = 24;
            // 
            // sourcePB
            // 
            this.sourcePB.Location = new System.Drawing.Point(0, 0);
            this.sourcePB.Name = "sourcePB";
            this.sourcePB.Size = new System.Drawing.Size(152, 98);
            this.sourcePB.TabIndex = 14;
            this.sourcePB.TabStop = false;
            this.sourcePB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.sourcePB_MouseDown);
            this.sourcePB.MouseLeave += new System.EventHandler(this.sourcePB_MouseLeave);
            this.sourcePB.MouseMove += new System.Windows.Forms.MouseEventHandler(this.sourcePB_MouseMove);
            this.sourcePB.MouseUp += new System.Windows.Forms.MouseEventHandler(this.sourcePB_MouseUp);
            // 
            // posterizedPanel
            // 
            this.posterizedPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.posterizedPanel.Controls.Add(this.posterizedPB);
            this.posterizedPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.posterizedPanel.Location = new System.Drawing.Point(3, 114);
            this.posterizedPanel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.posterizedPanel.Name = "posterizedPanel";
            this.posterizedPanel.Padding = new System.Windows.Forms.Padding(5);
            this.posterizedPanel.Size = new System.Drawing.Size(480, 119);
            this.posterizedPanel.TabIndex = 19;
            // 
            // posterizedPB
            // 
            this.posterizedPB.Location = new System.Drawing.Point(0, 0);
            this.posterizedPB.Name = "posterizedPB";
            this.posterizedPB.Size = new System.Drawing.Size(152, 98);
            this.posterizedPB.TabIndex = 14;
            this.posterizedPB.TabStop = false;
            this.posterizedPB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.posterizedPB_MouseDown);
            this.posterizedPB.MouseLeave += new System.EventHandler(this.posterizedPB_MouseLeave);
            this.posterizedPB.MouseMove += new System.Windows.Forms.MouseEventHandler(this.posterizedPB_MouseMove);
            this.posterizedPB.MouseUp += new System.Windows.Forms.MouseEventHandler(this.posterizedPB_MouseUp);
            // 
            // destinationPanel
            // 
            this.destinationPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.destinationPanel.Controls.Add(this.destinationPB);
            this.destinationPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.destinationPanel.Location = new System.Drawing.Point(3, 236);
            this.destinationPanel.Name = "destinationPanel";
            this.destinationPanel.Padding = new System.Windows.Forms.Padding(5);
            this.destinationPanel.Size = new System.Drawing.Size(480, 105);
            this.destinationPanel.TabIndex = 22;
            // 
            // destinationPB
            // 
            this.destinationPB.Location = new System.Drawing.Point(0, 0);
            this.destinationPB.Name = "destinationPB";
            this.destinationPB.Size = new System.Drawing.Size(152, 98);
            this.destinationPB.TabIndex = 14;
            this.destinationPB.TabStop = false;
            this.destinationPB.Click += new System.EventHandler(this.destinationPB_Click);
            this.destinationPB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.destinationPB_MouseDown);
            this.destinationPB.MouseMove += new System.Windows.Forms.MouseEventHandler(this.destinationPB_MouseMove);
            this.destinationPB.MouseUp += new System.Windows.Forms.MouseEventHandler(this.destinationPB_MouseUp);
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.panelPict);
            this.panel6.Controls.Add(this.panel1);
            this.panel6.Location = new System.Drawing.Point(362, 24);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(491, 513);
            this.panel6.TabIndex = 13;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnZoomInVect);
            this.panel1.Controls.Add(this.btnZoomOutVect);
            this.panel1.Controls.Add(this.txtScaleLvlVect);
            this.panel1.Controls.Add(this.trScaleVect);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(489, 40);
            this.panel1.TabIndex = 27;
            // 
            // btnZoomInVect
            // 
            this.btnZoomInVect.Enabled = false;
            this.btnZoomInVect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnZoomInVect.Location = new System.Drawing.Point(13, 8);
            this.btnZoomInVect.Name = "btnZoomInVect";
            this.btnZoomInVect.Size = new System.Drawing.Size(28, 26);
            this.btnZoomInVect.TabIndex = 22;
            this.btnZoomInVect.Text = "+";
            this.btnZoomInVect.UseVisualStyleBackColor = true;
            this.btnZoomInVect.Click += new System.EventHandler(this.ZoomInBtn_Click);
            // 
            // btnZoomOutVect
            // 
            this.btnZoomOutVect.Enabled = false;
            this.btnZoomOutVect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnZoomOutVect.Location = new System.Drawing.Point(47, 8);
            this.btnZoomOutVect.Name = "btnZoomOutVect";
            this.btnZoomOutVect.Size = new System.Drawing.Size(28, 26);
            this.btnZoomOutVect.TabIndex = 23;
            this.btnZoomOutVect.Text = "-";
            this.btnZoomOutVect.UseVisualStyleBackColor = true;
            this.btnZoomOutVect.Click += new System.EventHandler(this.ZoomOutBtn_Click);
            // 
            // txtScaleLvlVect
            // 
            this.txtScaleLvlVect.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtScaleLvlVect.Location = new System.Drawing.Point(262, 13);
            this.txtScaleLvlVect.Name = "txtScaleLvlVect";
            this.txtScaleLvlVect.ReadOnly = true;
            this.txtScaleLvlVect.Size = new System.Drawing.Size(27, 20);
            this.txtScaleLvlVect.TabIndex = 22;
            this.txtScaleLvlVect.Text = "1";
            this.txtScaleLvlVect.Leave += new System.EventHandler(this.textBox1_Leave);
            // 
            // trScaleVect
            // 
            this.trScaleVect.Enabled = false;
            this.trScaleVect.Location = new System.Drawing.Point(81, 3);
            this.trScaleVect.Minimum = 1;
            this.trScaleVect.Name = "trScaleVect";
            this.trScaleVect.Size = new System.Drawing.Size(175, 45);
            this.trScaleVect.TabIndex = 26;
            this.trScaleVect.Value = 1;
            this.trScaleVect.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ScaleTb_MouseMove);
            this.trScaleVect.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ScaleTb_MouseUp);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 537);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.mainTab);
            this.Controls.Add(this.menuMain);
            this.MainMenuStrip = this.menuMain;
            this.Name = "MainWindow";
            this.Text = "Form1";
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.mainTab.ResumeLayout(false);
            this.tabColor.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tabVect.ResumeLayout(false);
            this.panelMainVect.ResumeLayout(false);
            this.panelMenuVect.ResumeLayout(false);
            this.panelInputVect.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.gbThreadChoice.ResumeLayout(false);
            this.gbThreadChoice.PerformLayout();
            this.panelMenuVectHide.ResumeLayout(false);
            this.panelPict.ResumeLayout(false);
            this.flpPictures.ResumeLayout(false);
            this.sourcePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sourcePB)).EndInit();
            this.posterizedPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.posterizedPB)).EndInit();
            this.destinationPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.destinationPB)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trScaleVect)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem mainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.OpenFileDialog loadDialog;
        private System.Windows.Forms.SaveFileDialog saveDialog;
        private System.Windows.Forms.ToolStripMenuItem loadLastSaveToolStripMenuItem;
        private System.Windows.Forms.TabControl mainTab;
        private System.Windows.Forms.TabPage tabVect;
        private System.Windows.Forms.Panel panelMainVect;
        private System.Windows.Forms.Panel panelPict;
        private System.Windows.Forms.Panel destinationPanel;
        private System.Windows.Forms.PictureBox destinationPB;
        private System.Windows.Forms.Panel posterizedPanel;
        private System.Windows.Forms.PictureBox posterizedPB;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TrackBar trScaleVect;
        private System.Windows.Forms.Button btnZoomOutVect;
        private System.Windows.Forms.TextBox txtScaleLvlVect;
        private System.Windows.Forms.Button btnZoomInVect;
        private System.Windows.Forms.Panel panelMenuVect;
        private System.Windows.Forms.Panel panelInputVect;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox edtSliceHeight;
        private System.Windows.Forms.MaskedTextBox edtSliceWidth;
        private System.Windows.Forms.Button reloadSrcMapVectBtn;
        private System.Windows.Forms.RichTextBox infoBox;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblY2;
        private System.Windows.Forms.Label lblX2;
        private System.Windows.Forms.MaskedTextBox maskedTextBox4;
        private System.Windows.Forms.MaskedTextBox maskedTextBox3;
        private System.Windows.Forms.MaskedTextBox maskedTextBox2;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.Button loadSrcMapVectBtn;
        private System.Windows.Forms.CheckedListBox chkBoxTestOptions;
        private System.Windows.Forms.Button btnRefreshResultVectImg;
        private System.Windows.Forms.Button btnMainThread;
        private System.Windows.Forms.GroupBox gbThreadChoice;
        private System.Windows.Forms.RadioButton rbSeparateThread;
        private System.Windows.Forms.RadioButton rbMainThread;
        private System.Windows.Forms.Button btnStopR2V;
        private System.Windows.Forms.Button btnStartR2V;
        private System.Windows.Forms.Button btnSeparateThread;
        private System.Windows.Forms.Panel panelMenuVectHide;
        private System.Windows.Forms.Button btnMenuVectHide;
        private System.Windows.Forms.Panel sourcePanel;
        private System.Windows.Forms.PictureBox sourcePB;
        private System.Windows.Forms.Button posterizeSrcImageBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flpPictures;
        private System.Windows.Forms.TabPage tabColor;
        private System.Windows.Forms.FlowLayoutPanel flpColors;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnAddColorPanel;
        private System.Windows.Forms.Button btnPosterize;
    }
}

