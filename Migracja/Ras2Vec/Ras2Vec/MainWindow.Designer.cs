namespace Ras2Vec
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
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.destinationPanel = new System.Windows.Forms.Panel();
            this.destinationPB = new System.Windows.Forms.PictureBox();
            this.ZoomOutBtn = new System.Windows.Forms.Button();
            this.ZoomInBtn = new System.Windows.Forms.Button();
            this.sourcePanel = new System.Windows.Forms.Panel();
            this.sourcePB = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblY2 = new System.Windows.Forms.Label();
            this.lblX2 = new System.Windows.Forms.Label();
            this.maskedTextBox4 = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox3 = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox2 = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.button1 = new System.Windows.Forms.Button();
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
            this.menuMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.destinationPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.destinationPB)).BeginInit();
            this.sourcePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sourcePB)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.gbThreadChoice.SuspendLayout();
            this.panel3.SuspendLayout();
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
            this.menuMain.Size = new System.Drawing.Size(797, 24);
            this.menuMain.TabIndex = 0;
            // 
            // mainToolStripMenuItem
            // 
            this.mainToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.toolStripSeparator1,
            this.loadToolStripMenuItem,
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
            this.newToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.newToolStripMenuItem.Text = "New";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(123, 6);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.loadToolStripMenuItem.Text = "Load";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.saveAsToolStripMenuItem.Text = "Save As ...";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(123, 6);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(126, 22);
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
            // panel1
            // 
            this.panel1.Controls.Add(this.destinationPanel);
            this.panel1.Controls.Add(this.ZoomOutBtn);
            this.panel1.Controls.Add(this.ZoomInBtn);
            this.panel1.Controls.Add(this.sourcePanel);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(797, 506);
            this.panel1.TabIndex = 11;
            // 
            // destinationPanel
            // 
            this.destinationPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.destinationPanel.Controls.Add(this.destinationPB);
            this.destinationPanel.Location = new System.Drawing.Point(367, 280);
            this.destinationPanel.Name = "destinationPanel";
            this.destinationPanel.Size = new System.Drawing.Size(380, 214);
            this.destinationPanel.TabIndex = 18;
            // 
            // destinationPB
            // 
            this.destinationPB.Location = new System.Drawing.Point(0, 0);
            this.destinationPB.Name = "destinationPB";
            this.destinationPB.Size = new System.Drawing.Size(152, 98);
            this.destinationPB.TabIndex = 14;
            this.destinationPB.TabStop = false;
            this.destinationPB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.destinationPB_MouseDown);
            this.destinationPB.MouseLeave += new System.EventHandler(this.destinationPB_MouseLeave);
            this.destinationPB.MouseMove += new System.Windows.Forms.MouseEventHandler(this.destinationPB_MouseMove);
            this.destinationPB.MouseUp += new System.Windows.Forms.MouseEventHandler(this.destinationPB_MouseUp);
            // 
            // ZoomOutBtn
            // 
            this.ZoomOutBtn.Enabled = false;
            this.ZoomOutBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ZoomOutBtn.Location = new System.Drawing.Point(492, 8);
            this.ZoomOutBtn.Name = "ZoomOutBtn";
            this.ZoomOutBtn.Size = new System.Drawing.Size(28, 32);
            this.ZoomOutBtn.TabIndex = 17;
            this.ZoomOutBtn.Text = "-";
            this.ZoomOutBtn.UseVisualStyleBackColor = true;
            this.ZoomOutBtn.Click += new System.EventHandler(this.ZoomOutBtn_Click);
            // 
            // ZoomInBtn
            // 
            this.ZoomInBtn.Enabled = false;
            this.ZoomInBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ZoomInBtn.Location = new System.Drawing.Point(458, 8);
            this.ZoomInBtn.Name = "ZoomInBtn";
            this.ZoomInBtn.Size = new System.Drawing.Size(28, 32);
            this.ZoomInBtn.TabIndex = 16;
            this.ZoomInBtn.Text = "+";
            this.ZoomInBtn.UseVisualStyleBackColor = true;
            this.ZoomInBtn.Click += new System.EventHandler(this.ZoomInBtn_Click);
            // 
            // sourcePanel
            // 
            this.sourcePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sourcePanel.Controls.Add(this.sourcePB);
            this.sourcePanel.Location = new System.Drawing.Point(367, 48);
            this.sourcePanel.Name = "sourcePanel";
            this.sourcePanel.Size = new System.Drawing.Size(380, 214);
            this.sourcePanel.TabIndex = 15;
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
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(333, 506);
            this.panel2.TabIndex = 12;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.richTextBox1);
            this.panel4.Controls.Add(this.checkBox1);
            this.panel4.Controls.Add(this.textBox1);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.button1);
            this.panel4.Controls.Add(this.chkBoxTestOptions);
            this.panel4.Controls.Add(this.btnRefreshResultImg);
            this.panel4.Controls.Add(this.btnMainThread);
            this.panel4.Controls.Add(this.btnStopR2V);
            this.panel4.Controls.Add(this.gbThreadChoice);
            this.panel4.Controls.Add(this.btnStartR2V);
            this.panel4.Controls.Add(this.btnSeparateThread);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(34, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(299, 506);
            this.panel4.TabIndex = 26;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(112, 297);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(179, 196);
            this.richTextBox1.TabIndex = 25;
            this.richTextBox1.Text = "";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(11, 392);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(80, 17);
            this.checkBox1.TabIndex = 24;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(11, 366);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(79, 20);
            this.textBox1.TabIndex = 22;
            this.textBox1.Text = "1";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label1);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Controls.Add(this.lblY2);
            this.panel5.Controls.Add(this.lblX2);
            this.panel5.Controls.Add(this.maskedTextBox4);
            this.panel5.Controls.Add(this.maskedTextBox3);
            this.panel5.Controls.Add(this.maskedTextBox2);
            this.panel5.Controls.Add(this.maskedTextBox1);
            this.panel5.Location = new System.Drawing.Point(28, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(238, 109);
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
            this.lblY2.Location = new System.Drawing.Point(153, 87);
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
            this.lblX2.Location = new System.Drawing.Point(153, 61);
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
            this.maskedTextBox4.Location = new System.Drawing.Point(167, 85);
            this.maskedTextBox4.Mask = "00,00,00,00";
            this.maskedTextBox4.Name = "maskedTextBox4";
            this.maskedTextBox4.Size = new System.Drawing.Size(68, 20);
            this.maskedTextBox4.TabIndex = 24;
            // 
            // maskedTextBox3
            // 
            this.maskedTextBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.maskedTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.maskedTextBox3.Location = new System.Drawing.Point(167, 59);
            this.maskedTextBox3.Mask = "00,00,00,00";
            this.maskedTextBox3.Name = "maskedTextBox3";
            this.maskedTextBox3.Size = new System.Drawing.Size(68, 20);
            this.maskedTextBox3.TabIndex = 23;
            // 
            // maskedTextBox2
            // 
            this.maskedTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.maskedTextBox2.Location = new System.Drawing.Point(15, 29);
            this.maskedTextBox2.Mask = "00,00,00,00";
            this.maskedTextBox2.Name = "maskedTextBox2";
            this.maskedTextBox2.Size = new System.Drawing.Size(68, 20);
            this.maskedTextBox2.TabIndex = 22;
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.maskedTextBox1.Location = new System.Drawing.Point(15, 3);
            this.maskedTextBox1.Mask = "00,00,00,00";
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(68, 20);
            this.maskedTextBox1.TabIndex = 21;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 415);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "loadImage";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chkBoxTestOptions
            // 
            this.chkBoxTestOptions.BackColor = System.Drawing.SystemColors.Control;
            this.chkBoxTestOptions.CheckOnClick = true;
            this.chkBoxTestOptions.FormattingEnabled = true;
            this.chkBoxTestOptions.Items.AddRange(new object[] {
            "Polygons - po R2V (yes) / Rectangles - z Rastra (no)",
            "Edge (polygons) / Grid (rectangles)",
            "Kolor testowy"});
            this.chkBoxTestOptions.Location = new System.Drawing.Point(4, 242);
            this.chkBoxTestOptions.Name = "chkBoxTestOptions";
            this.chkBoxTestOptions.Size = new System.Drawing.Size(290, 49);
            this.chkBoxTestOptions.TabIndex = 10;
            // 
            // btnRefreshResultImg
            // 
            this.btnRefreshResultImg.Location = new System.Drawing.Point(4, 297);
            this.btnRefreshResultImg.Name = "btnRefreshResultImg";
            this.btnRefreshResultImg.Size = new System.Drawing.Size(75, 23);
            this.btnRefreshResultImg.TabIndex = 16;
            this.btnRefreshResultImg.Text = "Refresh result";
            this.btnRefreshResultImg.UseVisualStyleBackColor = true;
            // 
            // btnMainThread
            // 
            this.btnMainThread.Location = new System.Drawing.Point(8, 149);
            this.btnMainThread.Name = "btnMainThread";
            this.btnMainThread.Size = new System.Drawing.Size(75, 23);
            this.btnMainThread.TabIndex = 11;
            this.btnMainThread.Text = "MainThread";
            this.btnMainThread.UseVisualStyleBackColor = true;
            // 
            // btnStopR2V
            // 
            this.btnStopR2V.Location = new System.Drawing.Point(112, 178);
            this.btnStopR2V.Name = "btnStopR2V";
            this.btnStopR2V.Size = new System.Drawing.Size(99, 23);
            this.btnStopR2V.TabIndex = 15;
            this.btnStopR2V.Text = "Stop R2V";
            this.btnStopR2V.UseVisualStyleBackColor = true;
            // 
            // gbThreadChoice
            // 
            this.gbThreadChoice.Controls.Add(this.rbSeparateThread);
            this.gbThreadChoice.Controls.Add(this.rbMainThread);
            this.gbThreadChoice.Location = new System.Drawing.Point(8, 110);
            this.gbThreadChoice.Margin = new System.Windows.Forms.Padding(0);
            this.gbThreadChoice.Name = "gbThreadChoice";
            this.gbThreadChoice.Padding = new System.Windows.Forms.Padding(0);
            this.gbThreadChoice.Size = new System.Drawing.Size(203, 36);
            this.gbThreadChoice.TabIndex = 12;
            this.gbThreadChoice.TabStop = false;
            // 
            // rbSeparateThread
            // 
            this.rbSeparateThread.AutoSize = true;
            this.rbSeparateThread.Location = new System.Drawing.Point(91, 7);
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
            this.rbMainThread.Location = new System.Drawing.Point(3, 7);
            this.rbMainThread.Name = "rbMainThread";
            this.rbMainThread.Size = new System.Drawing.Size(82, 17);
            this.rbMainThread.TabIndex = 5;
            this.rbMainThread.TabStop = true;
            this.rbMainThread.Text = "MainThread";
            this.rbMainThread.UseVisualStyleBackColor = true;
            // 
            // btnStartR2V
            // 
            this.btnStartR2V.Location = new System.Drawing.Point(8, 178);
            this.btnStartR2V.Name = "btnStartR2V";
            this.btnStartR2V.Size = new System.Drawing.Size(75, 23);
            this.btnStartR2V.TabIndex = 14;
            this.btnStartR2V.Text = "Start R2V";
            this.btnStartR2V.UseVisualStyleBackColor = true;
            // 
            // btnSeparateThread
            // 
            this.btnSeparateThread.Location = new System.Drawing.Point(112, 149);
            this.btnSeparateThread.Name = "btnSeparateThread";
            this.btnSeparateThread.Size = new System.Drawing.Size(99, 23);
            this.btnSeparateThread.TabIndex = 13;
            this.btnSeparateThread.Text = "SeparateThread";
            this.btnSeparateThread.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnMenuHide);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(34, 506);
            this.panel3.TabIndex = 25;
            // 
            // btnMenuHide
            // 
            this.btnMenuHide.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnMenuHide.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnMenuHide.Location = new System.Drawing.Point(0, 0);
            this.btnMenuHide.Margin = new System.Windows.Forms.Padding(0);
            this.btnMenuHide.Name = "btnMenuHide";
            this.btnMenuHide.Size = new System.Drawing.Size(30, 506);
            this.btnMenuHide.TabIndex = 0;
            this.btnMenuHide.Text = "<<";
            this.btnMenuHide.UseVisualStyleBackColor = true;
            this.btnMenuHide.Click += new System.EventHandler(this.btnMenuHide_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 530);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuMain);
            this.MainMenuStrip = this.menuMain;
            this.Name = "MainWindow";
            this.Text = "Form1";
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.destinationPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.destinationPB)).EndInit();
            this.sourcePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sourcePB)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.gbThreadChoice.ResumeLayout(false);
            this.gbThreadChoice.PerformLayout();
            this.panel3.ResumeLayout(false);
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnMenuHide;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblY2;
        private System.Windows.Forms.Label lblX2;
        private System.Windows.Forms.MaskedTextBox maskedTextBox4;
        private System.Windows.Forms.MaskedTextBox maskedTextBox3;
        private System.Windows.Forms.MaskedTextBox maskedTextBox2;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.CheckedListBox chkBoxTestOptions;
        private System.Windows.Forms.Button btnRefreshResultImg;
        private System.Windows.Forms.Button btnMainThread;
        private System.Windows.Forms.Button btnStopR2V;
        private System.Windows.Forms.GroupBox gbThreadChoice;
        private System.Windows.Forms.RadioButton rbSeparateThread;
        private System.Windows.Forms.RadioButton rbMainThread;
        private System.Windows.Forms.Button btnStartR2V;
        private System.Windows.Forms.Button btnSeparateThread;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Panel sourcePanel;
        private System.Windows.Forms.PictureBox sourcePB;
        private System.Windows.Forms.Button ZoomOutBtn;
        private System.Windows.Forms.Button ZoomInBtn;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Panel destinationPanel;
        private System.Windows.Forms.PictureBox destinationPB;
    }
}

