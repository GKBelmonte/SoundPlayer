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
            this.label1 = new System.Windows.Forms.Label();
            this.trackBarSin = new System.Windows.Forms.TrackBar();
            this.trackBarSaw = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBarSqr = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBarAttack = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.labelDecay = new System.Windows.Forms.Label();
            this.trackBarDecay = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.labelKeyValue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSaw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSqr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAttack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDecay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sin:";
            // 
            // trackBarSin
            // 
            this.trackBarSin.Location = new System.Drawing.Point(42, 12);
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
            // trackBarAttack
            // 
            this.trackBarAttack.Location = new System.Drawing.Point(228, 12);
            this.trackBarAttack.Maximum = 100;
            this.trackBarAttack.Minimum = 1;
            this.trackBarAttack.Name = "trackBarAttack";
            this.trackBarAttack.Size = new System.Drawing.Size(133, 45);
            this.trackBarAttack.TabIndex = 7;
            this.trackBarAttack.Value = 10;
            this.trackBarAttack.Scroll += new System.EventHandler(this.trackBarAttack_Scroll);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(181, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Attack:";
            // 
            // labelDecay
            // 
            this.labelDecay.AutoSize = true;
            this.labelDecay.Location = new System.Drawing.Point(181, 72);
            this.labelDecay.Name = "labelDecay";
            this.labelDecay.Size = new System.Drawing.Size(41, 13);
            this.labelDecay.TabIndex = 10;
            this.labelDecay.Text = "Decay:";
            // 
            // trackBarDecay
            // 
            this.trackBarDecay.Location = new System.Drawing.Point(228, 63);
            this.trackBarDecay.Maximum = 100;
            this.trackBarDecay.Minimum = 5;
            this.trackBarDecay.Name = "trackBarDecay";
            this.trackBarDecay.Size = new System.Drawing.Size(133, 45);
            this.trackBarDecay.TabIndex = 9;
            this.trackBarDecay.Value = 35;
            this.trackBarDecay.Scroll += new System.EventHandler(this.trackBarDecay_Scroll);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(374, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Key:";
            // 
            // trackBar1
            // 
            this.trackBar1.LargeChange = 2;
            this.trackBar1.Location = new System.Drawing.Point(421, 12);
            this.trackBar1.Maximum = 12;
            this.trackBar1.Minimum = -12;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(133, 45);
            this.trackBar1.TabIndex = 11;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBarKey_Scroll);
            // 
            // labelKeyValue
            // 
            this.labelKeyValue.AutoSize = true;
            this.labelKeyValue.Location = new System.Drawing.Point(560, 21);
            this.labelKeyValue.Name = "labelKeyValue";
            this.labelKeyValue.Size = new System.Drawing.Size(14, 13);
            this.labelKeyValue.TabIndex = 13;
            this.labelKeyValue.Text = "C";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 163);
            this.Controls.Add(this.labelKeyValue);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.labelDecay);
            this.Controls.Add(this.trackBarDecay);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.trackBarAttack);
            this.Controls.Add(this.trackBarSqr);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.trackBarSaw);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.trackBarSin);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSaw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSqr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAttack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDecay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBarSin;
        private System.Windows.Forms.TrackBar trackBarSaw;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trackBarSqr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBarAttack;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelDecay;
        private System.Windows.Forms.TrackBar trackBarDecay;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label labelKeyValue;
    }
}

