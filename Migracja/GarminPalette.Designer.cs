namespace Migracja
{
    partial class GarminPalette
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
            this.tcPalettes = new System.Windows.Forms.TabControl();
            this.tpTest = new System.Windows.Forms.TabPage();
            this.flpTest = new System.Windows.Forms.FlowLayoutPanel();
            this.tpVistaHCx = new System.Windows.Forms.TabPage();
            this.flpVistaHCx = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.winColorDialog = new System.Windows.Forms.ColorDialog();
            this.btnColorDialog = new System.Windows.Forms.Button();
            this.tcPalettes.SuspendLayout();
            this.tpTest.SuspendLayout();
            this.tpVistaHCx.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcPalettes
            // 
            this.tcPalettes.Controls.Add(this.tpTest);
            this.tcPalettes.Controls.Add(this.tpVistaHCx);
            this.tcPalettes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcPalettes.Location = new System.Drawing.Point(0, 0);
            this.tcPalettes.Name = "tcPalettes";
            this.tcPalettes.SelectedIndex = 0;
            this.tcPalettes.Size = new System.Drawing.Size(284, 229);
            this.tcPalettes.TabIndex = 1;
            this.tcPalettes.SelectedIndexChanged += new System.EventHandler(this.tcPalettes_SelectedIndexChanged);
            this.tcPalettes.TabIndexChanged += new System.EventHandler(this.tcPalettes_TabIndexChanged);
            // 
            // tpTest
            // 
            this.tpTest.Controls.Add(this.btnColorDialog);
            this.tpTest.Controls.Add(this.flpTest);
            this.tpTest.Location = new System.Drawing.Point(4, 22);
            this.tpTest.Name = "tpTest";
            this.tpTest.Size = new System.Drawing.Size(276, 203);
            this.tpTest.TabIndex = 1;
            this.tpTest.Text = "Test";
            this.tpTest.UseVisualStyleBackColor = true;
            this.tpTest.Click += new System.EventHandler(this.tpTest_Click);
            // 
            // flpTest
            // 
            this.flpTest.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flpTest.Location = new System.Drawing.Point(0, 36);
            this.flpTest.Margin = new System.Windows.Forms.Padding(0);
            this.flpTest.Name = "flpTest";
            this.flpTest.Size = new System.Drawing.Size(276, 167);
            this.flpTest.TabIndex = 0;
            // 
            // tpVistaHCx
            // 
            this.tpVistaHCx.Controls.Add(this.flpVistaHCx);
            this.tpVistaHCx.Location = new System.Drawing.Point(4, 22);
            this.tpVistaHCx.Name = "tpVistaHCx";
            this.tpVistaHCx.Size = new System.Drawing.Size(276, 203);
            this.tpVistaHCx.TabIndex = 0;
            this.tpVistaHCx.Text = "Vista HCx";
            this.tpVistaHCx.UseVisualStyleBackColor = true;
            // 
            // flpVistaHCx
            // 
            this.flpVistaHCx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpVistaHCx.Location = new System.Drawing.Point(0, 0);
            this.flpVistaHCx.Margin = new System.Windows.Forms.Padding(0);
            this.flpVistaHCx.Name = "flpVistaHCx";
            this.flpVistaHCx.Size = new System.Drawing.Size(276, 203);
            this.flpVistaHCx.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 229);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(284, 33);
            this.panel1.TabIndex = 1;
            // 
            // btnColorDialog
            // 
            this.btnColorDialog.Location = new System.Drawing.Point(5, 6);
            this.btnColorDialog.Name = "btnColorDialog";
            this.btnColorDialog.Size = new System.Drawing.Size(102, 23);
            this.btnColorDialog.TabIndex = 0;
            this.btnColorDialog.Text = "Wybór koloru";
            this.btnColorDialog.UseVisualStyleBackColor = true;
            this.btnColorDialog.Click += new System.EventHandler(this.btnColorDialog_Click);
            // 
            // GarminPalette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.tcPalettes);
            this.Controls.Add(this.panel1);
            this.Name = "GarminPalette";
            this.Text = "Form1";
            this.tcPalettes.ResumeLayout(false);
            this.tpTest.ResumeLayout(false);
            this.tpVistaHCx.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcPalettes;
        private System.Windows.Forms.TabPage tpVistaHCx;
        private System.Windows.Forms.TabPage tpTest;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flpTest;
        private System.Windows.Forms.FlowLayoutPanel flpVistaHCx;
        private System.Windows.Forms.ColorDialog winColorDialog;
        private System.Windows.Forms.Button btnColorDialog;
    }
}