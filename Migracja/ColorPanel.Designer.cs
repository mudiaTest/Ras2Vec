namespace Migracja
{
    partial class ColorPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlColor = new System.Windows.Forms.Panel();
            this.txtRedMin = new System.Windows.Forms.MaskedTextBox();
            this.txtRedMax = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGreenMax = new System.Windows.Forms.MaskedTextBox();
            this.txtGreenMin = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBlueMax = new System.Windows.Forms.MaskedTextBox();
            this.txtBlueMin = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // pnlColor
            // 
            this.pnlColor.AccessibleDescription = "111";
            this.pnlColor.AccessibleName = "111";
            this.pnlColor.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlColor.Location = new System.Drawing.Point(6, 5);
            this.pnlColor.Name = "pnlColor";
            this.pnlColor.Padding = new System.Windows.Forms.Padding(2);
            this.pnlColor.Size = new System.Drawing.Size(35, 68);
            this.pnlColor.TabIndex = 0;
            this.pnlColor.Click += new System.EventHandler(this.pnlColor_Click);
            this.pnlColor.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlColor_Paint);
            // 
            // txtRedMin
            // 
            this.txtRedMin.Location = new System.Drawing.Point(60, 5);
            this.txtRedMin.Mask = "000";
            this.txtRedMin.Name = "txtRedMin";
            this.txtRedMin.Size = new System.Drawing.Size(25, 20);
            this.txtRedMin.TabIndex = 4;
            this.txtRedMin.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.maskedTextBox1_MaskInputRejected);
            this.txtRedMin.Leave += new System.EventHandler(this.txtRedMin_Leave);
            // 
            // txtRedMax
            // 
            this.txtRedMax.Location = new System.Drawing.Point(94, 5);
            this.txtRedMax.Mask = "000";
            this.txtRedMax.Name = "txtRedMax";
            this.txtRedMax.Size = new System.Drawing.Size(25, 20);
            this.txtRedMax.TabIndex = 5;
            this.txtRedMax.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.maskedTextBox2_MaskInputRejected);
            this.txtRedMax.Leave += new System.EventHandler(this.txtRedMax_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "R          -";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "G          -";
            // 
            // txtGreenMax
            // 
            this.txtGreenMax.Location = new System.Drawing.Point(94, 29);
            this.txtGreenMax.Mask = "000";
            this.txtGreenMax.Name = "txtGreenMax";
            this.txtGreenMax.Size = new System.Drawing.Size(25, 20);
            this.txtGreenMax.TabIndex = 8;
            this.txtGreenMax.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.maskedTextBox3_MaskInputRejected);
            this.txtGreenMax.Leave += new System.EventHandler(this.txtGreenMax_Leave);
            // 
            // txtGreenMin
            // 
            this.txtGreenMin.Location = new System.Drawing.Point(60, 29);
            this.txtGreenMin.Mask = "000";
            this.txtGreenMin.Name = "txtGreenMin";
            this.txtGreenMin.Size = new System.Drawing.Size(25, 20);
            this.txtGreenMin.TabIndex = 7;
            this.txtGreenMin.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.maskedTextBox4_MaskInputRejected);
            this.txtGreenMin.Leave += new System.EventHandler(this.txtGreenMin_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "B          -";
            // 
            // txtBlueMax
            // 
            this.txtBlueMax.Location = new System.Drawing.Point(94, 53);
            this.txtBlueMax.Mask = "000";
            this.txtBlueMax.Name = "txtBlueMax";
            this.txtBlueMax.Size = new System.Drawing.Size(25, 20);
            this.txtBlueMax.TabIndex = 11;
            this.txtBlueMax.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.maskedTextBox5_MaskInputRejected);
            this.txtBlueMax.Leave += new System.EventHandler(this.txtBlueMax_Leave);
            // 
            // txtBlueMin
            // 
            this.txtBlueMin.Location = new System.Drawing.Point(60, 53);
            this.txtBlueMin.Mask = "000";
            this.txtBlueMin.Name = "txtBlueMin";
            this.txtBlueMin.Size = new System.Drawing.Size(25, 20);
            this.txtBlueMin.TabIndex = 10;
            this.txtBlueMin.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.maskedTextBox6_MaskInputRejected);
            this.txtBlueMin.Leave += new System.EventHandler(this.txtBlueMin_Leave);
            // 
            // ColorPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.txtBlueMax);
            this.Controls.Add(this.txtBlueMin);
            this.Controls.Add(this.txtGreenMax);
            this.Controls.Add(this.txtGreenMin);
            this.Controls.Add(this.txtRedMax);
            this.Controls.Add(this.txtRedMin);
            this.Controls.Add(this.pnlColor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ColorPanel";
            this.Size = new System.Drawing.Size(126, 78);
            this.Load += new System.EventHandler(this.ColorPanel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlColor;
        private System.Windows.Forms.MaskedTextBox txtRedMin;
        private System.Windows.Forms.MaskedTextBox txtRedMax;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox txtGreenMax;
        private System.Windows.Forms.MaskedTextBox txtGreenMin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox txtBlueMax;
        private System.Windows.Forms.MaskedTextBox txtBlueMin;
    }
}
