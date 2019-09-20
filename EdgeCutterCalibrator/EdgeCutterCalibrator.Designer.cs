namespace WobblerCalibrator
{
    partial class EdgeCutterCalibrator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EdgeCutterCalibrator));
            this.UpNormal = new System.Windows.Forms.Button();
            this.NormalDown = new System.Windows.Forms.Button();
            this.NeutralButton = new System.Windows.Forms.Button();
            this.DownPhased = new System.Windows.Forms.Button();
            this.UpPhased = new System.Windows.Forms.Button();
            this.EepromSave = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.normalBox = new System.Windows.Forms.TextBox();
            this.phasedBox = new System.Windows.Forms.TextBox();
            this.RebootButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // UpNormal
            // 
            this.UpNormal.Location = new System.Drawing.Point(33, 206);
            this.UpNormal.Margin = new System.Windows.Forms.Padding(7);
            this.UpNormal.Name = "UpNormal";
            this.UpNormal.Size = new System.Drawing.Size(205, 129);
            this.UpNormal.TabIndex = 0;
            this.UpNormal.Text = "Up";
            this.UpNormal.UseVisualStyleBackColor = true;
            this.UpNormal.Click += new System.EventHandler(this.UpNormal_Click);
            // 
            // NormalDown
            // 
            this.NormalDown.Location = new System.Drawing.Point(33, 430);
            this.NormalDown.Margin = new System.Windows.Forms.Padding(7);
            this.NormalDown.Name = "NormalDown";
            this.NormalDown.Size = new System.Drawing.Size(205, 125);
            this.NormalDown.TabIndex = 1;
            this.NormalDown.Text = "Down";
            this.NormalDown.UseVisualStyleBackColor = true;
            this.NormalDown.Click += new System.EventHandler(this.NormalDown_Click);
            // 
            // NeutralButton
            // 
            this.NeutralButton.Location = new System.Drawing.Point(33, 34);
            this.NeutralButton.Margin = new System.Windows.Forms.Padding(7);
            this.NeutralButton.Name = "NeutralButton";
            this.NeutralButton.Size = new System.Drawing.Size(455, 156);
            this.NeutralButton.TabIndex = 2;
            this.NeutralButton.Text = "Write Neutral";
            this.NeutralButton.UseVisualStyleBackColor = true;
            this.NeutralButton.Click += new System.EventHandler(this.NeutralButton_Click);
            // 
            // DownPhased
            // 
            this.DownPhased.Location = new System.Drawing.Point(282, 430);
            this.DownPhased.Margin = new System.Windows.Forms.Padding(7);
            this.DownPhased.Name = "DownPhased";
            this.DownPhased.Size = new System.Drawing.Size(205, 125);
            this.DownPhased.TabIndex = 4;
            this.DownPhased.Text = "Down";
            this.DownPhased.UseVisualStyleBackColor = true;
            this.DownPhased.Click += new System.EventHandler(this.DownPhased_Click);
            // 
            // UpPhased
            // 
            this.UpPhased.Location = new System.Drawing.Point(282, 206);
            this.UpPhased.Margin = new System.Windows.Forms.Padding(7);
            this.UpPhased.Name = "UpPhased";
            this.UpPhased.Size = new System.Drawing.Size(205, 129);
            this.UpPhased.TabIndex = 3;
            this.UpPhased.Text = "Up";
            this.UpPhased.UseVisualStyleBackColor = true;
            this.UpPhased.Click += new System.EventHandler(this.UpPhased_Click);
            // 
            // EepromSave
            // 
            this.EepromSave.Location = new System.Drawing.Point(33, 572);
            this.EepromSave.Margin = new System.Windows.Forms.Padding(7);
            this.EepromSave.Name = "EepromSave";
            this.EepromSave.Size = new System.Drawing.Size(455, 73);
            this.EepromSave.TabIndex = 5;
            this.EepromSave.Text = "Save to EEPROM";
            this.EepromSave.UseVisualStyleBackColor = true;
            this.EepromSave.Click += new System.EventHandler(this.EepromSave_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 15;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // normalBox
            // 
            this.normalBox.Enabled = false;
            this.normalBox.Location = new System.Drawing.Point(37, 352);
            this.normalBox.Margin = new System.Windows.Forms.Padding(7);
            this.normalBox.Name = "normalBox";
            this.normalBox.Size = new System.Drawing.Size(195, 50);
            this.normalBox.TabIndex = 6;
            // 
            // phasedBox
            // 
            this.phasedBox.Enabled = false;
            this.phasedBox.Location = new System.Drawing.Point(282, 352);
            this.phasedBox.Margin = new System.Windows.Forms.Padding(7);
            this.phasedBox.Name = "phasedBox";
            this.phasedBox.Size = new System.Drawing.Size(195, 50);
            this.phasedBox.TabIndex = 7;
            // 
            // RebootButton
            // 
            this.RebootButton.Location = new System.Drawing.Point(32, 677);
            this.RebootButton.Margin = new System.Windows.Forms.Padding(7);
            this.RebootButton.Name = "RebootButton";
            this.RebootButton.Size = new System.Drawing.Size(455, 82);
            this.RebootButton.TabIndex = 8;
            this.RebootButton.Text = "REBOOT";
            this.RebootButton.UseVisualStyleBackColor = true;
            this.RebootButton.Click += new System.EventHandler(this.RebootButton_Click);
            // 
            // EdgeCutterCalibrator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(22F, 44F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 790);
            this.Controls.Add(this.RebootButton);
            this.Controls.Add(this.phasedBox);
            this.Controls.Add(this.normalBox);
            this.Controls.Add(this.EepromSave);
            this.Controls.Add(this.DownPhased);
            this.Controls.Add(this.UpPhased);
            this.Controls.Add(this.NeutralButton);
            this.Controls.Add(this.NormalDown);
            this.Controls.Add(this.UpNormal);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(7);
            this.Name = "EdgeCutterCalibrator";
            this.Text = "Edgecutter Calibrator";
            this.Load += new System.EventHandler(this.EdgeCutterCalibrator_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WobblerCalibrator_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button UpNormal;
        private System.Windows.Forms.Button NormalDown;
        private System.Windows.Forms.Button NeutralButton;
        private System.Windows.Forms.Button DownPhased;
        private System.Windows.Forms.Button UpPhased;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox normalBox;
        private System.Windows.Forms.TextBox phasedBox;
        private System.Windows.Forms.Button EepromSave;
        private System.Windows.Forms.Button RebootButton;
    }
}

