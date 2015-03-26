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
            this.pnlGarminColor = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtGravity = new System.Windows.Forms.MaskedTextBox();
            this.pnlRasterColor = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlGarminColor.SuspendLayout();
            this.pnlRasterColor.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlGarminColor
            // 
            this.pnlGarminColor.AccessibleDescription = "111";
            this.pnlGarminColor.AccessibleName = "111";
            this.pnlGarminColor.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlGarminColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGarminColor.Controls.Add(this.label1);
            this.pnlGarminColor.Location = new System.Drawing.Point(47, 6);
            this.pnlGarminColor.Name = "pnlGarminColor";
            this.pnlGarminColor.Padding = new System.Windows.Forms.Padding(2);
            this.pnlGarminColor.Size = new System.Drawing.Size(35, 42);
            this.pnlGarminColor.TabIndex = 0;
            this.pnlGarminColor.Click += new System.EventHandler(this.pnlColor_Click);
            this.pnlGarminColor.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlColor_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(4, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 25);
            this.label1.TabIndex = 12;
            this.label1.Text = "G";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Gravity";
            // 
            // txtGravity
            // 
            this.txtGravity.Location = new System.Drawing.Point(49, 53);
            this.txtGravity.Mask = "0.000";
            this.txtGravity.Name = "txtGravity";
            this.txtGravity.Size = new System.Drawing.Size(32, 20);
            this.txtGravity.TabIndex = 10;
            this.txtGravity.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.maskedTextBox6_MaskInputRejected);
            this.txtGravity.Leave += new System.EventHandler(this.txtBlueMin_Leave);
            // 
            // pnlRasterColor
            // 
            this.pnlRasterColor.AccessibleDescription = "111";
            this.pnlRasterColor.AccessibleName = "111";
            this.pnlRasterColor.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlRasterColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRasterColor.Controls.Add(this.label2);
            this.pnlRasterColor.Location = new System.Drawing.Point(6, 5);
            this.pnlRasterColor.Name = "pnlRasterColor";
            this.pnlRasterColor.Padding = new System.Windows.Forms.Padding(2);
            this.pnlRasterColor.Size = new System.Drawing.Size(34, 42);
            this.pnlRasterColor.TabIndex = 11;
            this.pnlRasterColor.Click += new System.EventHandler(this.pnlRasterColor_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(3, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 25);
            this.label2.TabIndex = 13;
            this.label2.Text = "R";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // ColorPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.pnlRasterColor);
            this.Controls.Add(this.txtGravity);
            this.Controls.Add(this.pnlGarminColor);
            this.Controls.Add(this.label3);
            this.Name = "ColorPanel";
            this.Size = new System.Drawing.Size(88, 78);
            this.Load += new System.EventHandler(this.ColorPanel_Load);
            this.pnlGarminColor.ResumeLayout(false);
            this.pnlGarminColor.PerformLayout();
            this.pnlRasterColor.ResumeLayout(false);
            this.pnlRasterColor.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlGarminColor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox txtGravity;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlRasterColor;
        private System.Windows.Forms.Label label2;
    }
}
