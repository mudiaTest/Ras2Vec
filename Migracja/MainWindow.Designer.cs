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
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.edtSliceHeight = new System.Windows.Forms.MaskedTextBox();
            this.edtSliceWidth = new System.Windows.Forms.MaskedTextBox();
            this.reloadSrcMapBtn = new System.Windows.Forms.Button();
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
            this.loadSrcMapBtn = new System.Windows.Forms.Button();
            this.chkBoxTestOptions = new System.Windows.Forms.CheckedListBox();
            this.btnRefreshResultImg = new System.Windows.Forms.Button();
            this.btnMainThread = new System.Windows.Forms.Button();
            this.btnStopR2V = new System.Windows.Forms.Button();
            this.gbThreadChoice = new System.Windows.Forms.GroupBox();
            this.rbSeparateThread = new System.Windows.Forms.RadioButton();
            this.rbMainThread = new System.Windows.Forms.RadioButton();
            this.btnStartR2V = new System.Windows.Forms.Button();
            this.btnSeparateThread = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnMenuHide = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.ScaleTrB = new System.Windows.Forms.TrackBar();
            this.ZoomOutBtn = new System.Windows.Forms.Button();
            this.ScaleTB = new System.Windows.Forms.TextBox();
            this.ZoomInBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.destinationPanel = new System.Windows.Forms.Panel();
            this.destinationPB = new System.Windows.Forms.PictureBox();
            this.sourcePanel = new System.Windows.Forms.Panel();
            this.sourcePB = new System.Windows.Forms.PictureBox();
            this.saveDialog = new System.Windows.Forms.SaveFileDialog();
            this.panel9 = new System.Windows.Forms.Panel();
            this.menuMain.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel5.SuspendLayout();
            this.gbThreadChoice.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScaleTrB)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.destinationPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.destinationPB)).BeginInit();
            this.sourcePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sourcePB)).BeginInit();
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
            this.menuMain.Size = new System.Drawing.Size(880, 24);
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
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(349, 517);
            this.panel2.TabIndex = 12;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.panel9);
            this.panel4.Controls.Add(this.panel8);
            this.panel4.Controls.Add(this.reloadSrcMapBtn);
            this.panel4.Controls.Add(this.infoBox);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.loadSrcMapBtn);
            this.panel4.Controls.Add(this.chkBoxTestOptions);
            this.panel4.Controls.Add(this.btnRefreshResultImg);
            this.panel4.Controls.Add(this.btnMainThread);
            this.panel4.Controls.Add(this.gbThreadChoice);
            this.panel4.Controls.Add(this.btnSeparateThread);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(34, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(315, 517);
            this.panel4.TabIndex = 26;
            this.panel4.Click += new System.EventHandler(this.w);
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
            // reloadSrcMapBtn
            // 
            this.reloadSrcMapBtn.Location = new System.Drawing.Point(193, 293);
            this.reloadSrcMapBtn.Name = "reloadSrcMapBtn";
            this.reloadSrcMapBtn.Size = new System.Drawing.Size(112, 23);
            this.reloadSrcMapBtn.TabIndex = 26;
            this.reloadSrcMapBtn.Text = "Reload source map";
            this.reloadSrcMapBtn.UseVisualStyleBackColor = true;
            this.reloadSrcMapBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // infoBox
            // 
            this.infoBox.Location = new System.Drawing.Point(5, 326);
            this.infoBox.Name = "infoBox";
            this.infoBox.Size = new System.Drawing.Size(295, 178);
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
            // loadSrcMapBtn
            // 
            this.loadSrcMapBtn.Location = new System.Drawing.Point(89, 293);
            this.loadSrcMapBtn.Name = "loadSrcMapBtn";
            this.loadSrcMapBtn.Size = new System.Drawing.Size(98, 23);
            this.loadSrcMapBtn.TabIndex = 14;
            this.loadSrcMapBtn.Text = "Load source map";
            this.loadSrcMapBtn.UseVisualStyleBackColor = true;
            this.loadSrcMapBtn.Click += new System.EventHandler(this.button1_Click);
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
            "Uproszczona krawędź"});
            this.chkBoxTestOptions.Location = new System.Drawing.Point(8, 225);
            this.chkBoxTestOptions.Name = "chkBoxTestOptions";
            this.chkBoxTestOptions.Size = new System.Drawing.Size(297, 64);
            this.chkBoxTestOptions.TabIndex = 10;
            this.chkBoxTestOptions.SelectedIndexChanged += new System.EventHandler(this.chkBoxTestOptions_SelectedIndexChanged);
            // 
            // btnRefreshResultImg
            // 
            this.btnRefreshResultImg.Location = new System.Drawing.Point(8, 293);
            this.btnRefreshResultImg.Name = "btnRefreshResultImg";
            this.btnRefreshResultImg.Size = new System.Drawing.Size(75, 23);
            this.btnRefreshResultImg.TabIndex = 16;
            this.btnRefreshResultImg.Text = "Refresh result";
            this.btnRefreshResultImg.UseVisualStyleBackColor = true;
            this.btnRefreshResultImg.Click += new System.EventHandler(this.btnRefreshResultImg_Click);
            // 
            // btnMainThread
            // 
            this.btnMainThread.Location = new System.Drawing.Point(207, 164);
            this.btnMainThread.Name = "btnMainThread";
            this.btnMainThread.Size = new System.Drawing.Size(99, 23);
            this.btnMainThread.TabIndex = 11;
            this.btnMainThread.Text = "MainThread";
            this.btnMainThread.UseVisualStyleBackColor = true;
            this.btnMainThread.Click += new System.EventHandler(this.btnMainThread_Click);
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
            // gbThreadChoice
            // 
            this.gbThreadChoice.Controls.Add(this.rbSeparateThread);
            this.gbThreadChoice.Controls.Add(this.rbMainThread);
            this.gbThreadChoice.Controls.Add(this.btnStopR2V);
            this.gbThreadChoice.Controls.Add(this.btnStartR2V);
            this.gbThreadChoice.Location = new System.Drawing.Point(8, 154);
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
            this.btnSeparateThread.Location = new System.Drawing.Point(207, 192);
            this.btnSeparateThread.Name = "btnSeparateThread";
            this.btnSeparateThread.Size = new System.Drawing.Size(99, 23);
            this.btnSeparateThread.TabIndex = 13;
            this.btnSeparateThread.Text = "SeparateThread";
            this.btnSeparateThread.UseVisualStyleBackColor = true;
            this.btnSeparateThread.Click += new System.EventHandler(this.btnSeparateThread_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnMenuHide);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(34, 517);
            this.panel3.TabIndex = 25;
            // 
            // btnMenuHide
            // 
            this.btnMenuHide.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnMenuHide.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnMenuHide.Location = new System.Drawing.Point(0, 0);
            this.btnMenuHide.Margin = new System.Windows.Forms.Padding(0);
            this.btnMenuHide.Name = "btnMenuHide";
            this.btnMenuHide.Size = new System.Drawing.Size(30, 517);
            this.btnMenuHide.TabIndex = 0;
            this.btnMenuHide.Text = "<<";
            this.btnMenuHide.UseVisualStyleBackColor = true;
            this.btnMenuHide.Click += new System.EventHandler(this.btnMenuHide_Click);
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.ScaleTrB);
            this.panel6.Controls.Add(this.ZoomOutBtn);
            this.panel6.Controls.Add(this.ScaleTB);
            this.panel6.Controls.Add(this.ZoomInBtn);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(349, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(531, 38);
            this.panel6.TabIndex = 13;
            // 
            // ScaleTrB
            // 
            this.ScaleTrB.Enabled = false;
            this.ScaleTrB.Location = new System.Drawing.Point(120, 3);
            this.ScaleTrB.Minimum = 1;
            this.ScaleTrB.Name = "ScaleTrB";
            this.ScaleTrB.Size = new System.Drawing.Size(175, 45);
            this.ScaleTrB.TabIndex = 26;
            this.ScaleTrB.Value = 1;
            this.ScaleTrB.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ScaleTb_MouseMove);
            this.ScaleTrB.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ScaleTb_MouseUp);
            // 
            // ZoomOutBtn
            // 
            this.ZoomOutBtn.Enabled = false;
            this.ZoomOutBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ZoomOutBtn.Location = new System.Drawing.Point(86, 4);
            this.ZoomOutBtn.Name = "ZoomOutBtn";
            this.ZoomOutBtn.Size = new System.Drawing.Size(28, 26);
            this.ZoomOutBtn.TabIndex = 23;
            this.ZoomOutBtn.Text = "-";
            this.ZoomOutBtn.UseVisualStyleBackColor = true;
            this.ZoomOutBtn.Click += new System.EventHandler(this.ZoomOutBtn_Click);
            // 
            // ScaleTB
            // 
            this.ScaleTB.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ScaleTB.Location = new System.Drawing.Point(301, 7);
            this.ScaleTB.Name = "ScaleTB";
            this.ScaleTB.ReadOnly = true;
            this.ScaleTB.Size = new System.Drawing.Size(27, 20);
            this.ScaleTB.TabIndex = 22;
            this.ScaleTB.Text = "1";
            this.ScaleTB.Leave += new System.EventHandler(this.textBox1_Leave);
            // 
            // ZoomInBtn
            // 
            this.ZoomInBtn.Enabled = false;
            this.ZoomInBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ZoomInBtn.Location = new System.Drawing.Point(52, 4);
            this.ZoomInBtn.Name = "ZoomInBtn";
            this.ZoomInBtn.Size = new System.Drawing.Size(28, 26);
            this.ZoomInBtn.TabIndex = 22;
            this.ZoomInBtn.Text = "+";
            this.ZoomInBtn.UseVisualStyleBackColor = true;
            this.ZoomInBtn.Click += new System.EventHandler(this.ZoomInBtn_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(880, 517);
            this.panel1.TabIndex = 11;
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.destinationPanel);
            this.panel7.Controls.Add(this.sourcePanel);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(349, 38);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(531, 479);
            this.panel7.TabIndex = 14;
            this.panel7.SizeChanged += new System.EventHandler(this.panel7_SizeChanged);
            // 
            // destinationPanel
            // 
            this.destinationPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.destinationPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.destinationPanel.Controls.Add(this.destinationPB);
            this.destinationPanel.Location = new System.Drawing.Point(6, 241);
            this.destinationPanel.Name = "destinationPanel";
            this.destinationPanel.Padding = new System.Windows.Forms.Padding(5);
            this.destinationPanel.Size = new System.Drawing.Size(517, 210);
            this.destinationPanel.TabIndex = 22;
            // 
            // destinationPB
            // 
            this.destinationPB.Location = new System.Drawing.Point(0, 0);
            this.destinationPB.Name = "destinationPB";
            this.destinationPB.Size = new System.Drawing.Size(152, 98);
            this.destinationPB.TabIndex = 14;
            this.destinationPB.TabStop = false;
            this.destinationPB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.destinationPB_MouseDown);
            this.destinationPB.MouseMove += new System.Windows.Forms.MouseEventHandler(this.destinationPB_MouseMove);
            this.destinationPB.MouseUp += new System.Windows.Forms.MouseEventHandler(this.destinationPB_MouseUp);
            // 
            // sourcePanel
            // 
            this.sourcePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sourcePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sourcePanel.Controls.Add(this.sourcePB);
            this.sourcePanel.Location = new System.Drawing.Point(6, 8);
            this.sourcePanel.Name = "sourcePanel";
            this.sourcePanel.Size = new System.Drawing.Size(517, 210);
            this.sourcePanel.TabIndex = 19;
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
            // panel9
            // 
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel9.Location = new System.Drawing.Point(156, 43);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(152, 81);
            this.panel9.TabIndex = 30;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 541);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuMain);
            this.MainMenuStrip = this.menuMain;
            this.Name = "MainWindow";
            this.Text = "Form1";
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.gbThreadChoice.ResumeLayout(false);
            this.gbThreadChoice.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScaleTrB)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.destinationPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.destinationPB)).EndInit();
            this.sourcePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sourcePB)).EndInit();
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
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
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
        private System.Windows.Forms.Button loadSrcMapBtn;
        private System.Windows.Forms.CheckedListBox chkBoxTestOptions;
        private System.Windows.Forms.Button btnRefreshResultImg;
        private System.Windows.Forms.Button btnMainThread;
        private System.Windows.Forms.Button btnStopR2V;
        private System.Windows.Forms.GroupBox gbThreadChoice;
        private System.Windows.Forms.RadioButton rbSeparateThread;
        private System.Windows.Forms.RadioButton rbMainThread;
        private System.Windows.Forms.Button btnStartR2V;
        private System.Windows.Forms.Button btnSeparateThread;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnMenuHide;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TrackBar ScaleTrB;
        private System.Windows.Forms.Button ZoomOutBtn;
        private System.Windows.Forms.TextBox ScaleTB;
        private System.Windows.Forms.Button ZoomInBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel destinationPanel;
        private System.Windows.Forms.PictureBox destinationPB;
        private System.Windows.Forms.Panel sourcePanel;
        private System.Windows.Forms.PictureBox sourcePB;
        private System.Windows.Forms.SaveFileDialog saveDialog;
        private System.Windows.Forms.Button reloadSrcMapBtn;
        private System.Windows.Forms.ToolStripMenuItem loadLastSaveToolStripMenuItem;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox edtSliceHeight;
        private System.Windows.Forms.MaskedTextBox edtSliceWidth;
        private System.Windows.Forms.Panel panel9;
    }
}

