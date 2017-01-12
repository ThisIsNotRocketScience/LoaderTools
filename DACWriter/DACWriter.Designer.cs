namespace DACWriter
{
    partial class DACWriter
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
            this.PlayButton = new System.Windows.Forms.Button();
            this.val1box = new System.Windows.Forms.NumericUpDown();
            this.min1 = new System.Windows.Forms.Button();
            this.max1 = new System.Windows.Forms.Button();
            this.max2 = new System.Windows.Forms.Button();
            this.min2 = new System.Windows.Forms.Button();
            this.val2box = new System.Windows.Forms.NumericUpDown();
            this.Mid = new System.Windows.Forms.Button();
            this.mid2 = new System.Windows.Forms.Button();
            this.vminBox = new System.Windows.Forms.NumericUpDown();
            this.vmaxbox = new System.Windows.Forms.NumericUpDown();
            this.daccenter = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.val1box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.val2box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vminBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vmaxbox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.daccenter)).BeginInit();
            this.SuspendLayout();
            // 
            // PlayButton
            // 
            this.PlayButton.Location = new System.Drawing.Point(82, 175);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(120, 66);
            this.PlayButton.TabIndex = 0;
            this.PlayButton.Text = "Play!";
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // val1box
            // 
            this.val1box.Location = new System.Drawing.Point(82, 54);
            this.val1box.Maximum = new decimal(new int[] {
            4095,
            0,
            0,
            0});
            this.val1box.Name = "val1box";
            this.val1box.Size = new System.Drawing.Size(120, 22);
            this.val1box.TabIndex = 1;
            // 
            // min1
            // 
            this.min1.Location = new System.Drawing.Point(4, 49);
            this.min1.Name = "min1";
            this.min1.Size = new System.Drawing.Size(72, 31);
            this.min1.TabIndex = 2;
            this.min1.Text = "Min";
            this.min1.UseVisualStyleBackColor = true;
            this.min1.Click += new System.EventHandler(this.min1_Click);
            // 
            // max1
            // 
            this.max1.Location = new System.Drawing.Point(208, 49);
            this.max1.Name = "max1";
            this.max1.Size = new System.Drawing.Size(72, 31);
            this.max1.TabIndex = 3;
            this.max1.Text = "Max";
            this.max1.UseVisualStyleBackColor = true;
            this.max1.Click += new System.EventHandler(this.max1_Click);
            // 
            // max2
            // 
            this.max2.Location = new System.Drawing.Point(208, 91);
            this.max2.Name = "max2";
            this.max2.Size = new System.Drawing.Size(72, 31);
            this.max2.TabIndex = 6;
            this.max2.Text = "Max";
            this.max2.UseVisualStyleBackColor = true;
            this.max2.Click += new System.EventHandler(this.max2_Click);
            // 
            // min2
            // 
            this.min2.Location = new System.Drawing.Point(4, 91);
            this.min2.Name = "min2";
            this.min2.Size = new System.Drawing.Size(72, 31);
            this.min2.TabIndex = 5;
            this.min2.Text = "Min";
            this.min2.UseVisualStyleBackColor = true;
            this.min2.Click += new System.EventHandler(this.min2_Click);
            // 
            // val2box
            // 
            this.val2box.Location = new System.Drawing.Point(82, 96);
            this.val2box.Maximum = new decimal(new int[] {
            4095,
            0,
            0,
            0});
            this.val2box.Name = "val2box";
            this.val2box.Size = new System.Drawing.Size(120, 22);
            this.val2box.TabIndex = 4;
            // 
            // Mid
            // 
            this.Mid.Location = new System.Drawing.Point(82, 12);
            this.Mid.Name = "Mid";
            this.Mid.Size = new System.Drawing.Size(120, 31);
            this.Mid.TabIndex = 7;
            this.Mid.Text = "Mid";
            this.Mid.UseVisualStyleBackColor = true;
            this.Mid.Click += new System.EventHandler(this.Mid_Click);
            // 
            // mid2
            // 
            this.mid2.Location = new System.Drawing.Point(82, 124);
            this.mid2.Name = "mid2";
            this.mid2.Size = new System.Drawing.Size(120, 31);
            this.mid2.TabIndex = 8;
            this.mid2.Text = "Mid";
            this.mid2.UseVisualStyleBackColor = true;
            this.mid2.Click += new System.EventHandler(this.mid2_Click);
            // 
            // vminBox
            // 
            this.vminBox.DecimalPlaces = 3;
            this.vminBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.vminBox.Location = new System.Drawing.Point(363, 54);
            this.vminBox.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.vminBox.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            -2147483648});
            this.vminBox.Name = "vminBox";
            this.vminBox.Size = new System.Drawing.Size(120, 22);
            this.vminBox.TabIndex = 9;
            this.vminBox.ValueChanged += new System.EventHandler(this.vminBox_ValueChanged);
            // 
            // vmaxbox
            // 
            this.vmaxbox.DecimalPlaces = 3;
            this.vmaxbox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.vmaxbox.Location = new System.Drawing.Point(363, 96);
            this.vmaxbox.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.vmaxbox.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            -2147483648});
            this.vmaxbox.Name = "vmaxbox";
            this.vmaxbox.Size = new System.Drawing.Size(120, 22);
            this.vmaxbox.TabIndex = 10;
            this.vmaxbox.ValueChanged += new System.EventHandler(this.vmaxbox_ValueChanged);
            // 
            // daccenter
            // 
            this.daccenter.Location = new System.Drawing.Point(363, 150);
            this.daccenter.Maximum = new decimal(new int[] {
            4095,
            0,
            0,
            0});
            this.daccenter.Name = "daccenter";
            this.daccenter.Size = new System.Drawing.Size(120, 22);
            this.daccenter.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(489, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "Vmin";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(489, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 17);
            this.label2.TabIndex = 13;
            this.label2.Text = "Vmax";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(489, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 17);
            this.label3.TabIndex = 14;
            this.label3.Text = "Suggested 0level";
            // 
            // DACWriter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OliveDrab;
            this.ClientSize = new System.Drawing.Size(707, 253);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.daccenter);
            this.Controls.Add(this.vmaxbox);
            this.Controls.Add(this.vminBox);
            this.Controls.Add(this.mid2);
            this.Controls.Add(this.Mid);
            this.Controls.Add(this.max2);
            this.Controls.Add(this.min2);
            this.Controls.Add(this.val2box);
            this.Controls.Add(this.max1);
            this.Controls.Add(this.min1);
            this.Controls.Add(this.val1box);
            this.Controls.Add(this.PlayButton);
            this.Name = "DACWriter";
            this.Text = "TINRS DACWriter";
            ((System.ComponentModel.ISupportInitialize)(this.val1box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.val2box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vminBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vmaxbox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.daccenter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button PlayButton;
        private System.Windows.Forms.NumericUpDown val1box;
        private System.Windows.Forms.Button min1;
        private System.Windows.Forms.Button max1;
        private System.Windows.Forms.Button max2;
        private System.Windows.Forms.Button min2;
        private System.Windows.Forms.NumericUpDown val2box;
        private System.Windows.Forms.Button Mid;
        private System.Windows.Forms.Button mid2;
        private System.Windows.Forms.NumericUpDown vminBox;
        private System.Windows.Forms.NumericUpDown vmaxbox;
        private System.Windows.Forms.NumericUpDown daccenter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

