namespace Blaze.SoundPlayer.TestWinForms
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBarSin = new System.Windows.Forms.TrackBar();
            this.trackBarSaw = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBarSqr = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSaw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSqr)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(306, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(251, 188);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sin:";
            // 
            // trackBarSin
            // 
            this.trackBarSin.Location = new System.Drawing.Point(42, 13);
            this.trackBarSin.Maximum = 100;
            this.trackBarSin.Name = "trackBarSin";
            this.trackBarSin.Size = new System.Drawing.Size(133, 45);
            this.trackBarSin.TabIndex = 2;
            this.trackBarSin.Value = 100;
            this.trackBarSin.Scroll += new System.EventHandler(this.trackBarSin_Scroll);
            // 
            // trackBarSaw
            // 
            this.trackBarSaw.Location = new System.Drawing.Point(42, 63);
            this.trackBarSaw.Maximum = 100;
            this.trackBarSaw.Name = "trackBarSaw";
            this.trackBarSaw.Size = new System.Drawing.Size(133, 45);
            this.trackBarSaw.TabIndex = 4;
            this.trackBarSaw.Value = 100;
            this.trackBarSaw.Scroll += new System.EventHandler(this.trackBarSaw_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Saw:";
            // 
            // trackBarSqr
            // 
            this.trackBarSqr.Location = new System.Drawing.Point(42, 113);
            this.trackBarSqr.Maximum = 100;
            this.trackBarSqr.Name = "trackBarSqr";
            this.trackBarSqr.Size = new System.Drawing.Size(133, 45);
            this.trackBarSqr.TabIndex = 6;
            this.trackBarSqr.Value = 100;
            this.trackBarSqr.Scroll += new System.EventHandler(this.trackBarSqr_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Sqr:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 219);
            this.Controls.Add(this.trackBarSqr);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.trackBarSaw);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.trackBarSin);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSaw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSqr)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBarSin;
        private System.Windows.Forms.TrackBar trackBarSaw;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trackBarSqr;
        private System.Windows.Forms.Label label3;
    }
}

