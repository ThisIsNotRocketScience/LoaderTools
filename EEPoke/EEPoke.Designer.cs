namespace EEPoke
{
    partial class EEPoke
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
            this.AddressLabel = new System.Windows.Forms.Label();
            this.AddressBox = new System.Windows.Forms.TextBox();
            this.ValueBox = new System.Windows.Forms.TextBox();
            this.ValueLabel = new System.Windows.Forms.Label();
            this.PokeButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // AddressLabel
            // 
            this.AddressLabel.AutoSize = true;
            this.AddressLabel.ForeColor = System.Drawing.Color.Coral;
            this.AddressLabel.Location = new System.Drawing.Point(183, 9);
            this.AddressLabel.Name = "AddressLabel";
            this.AddressLabel.Size = new System.Drawing.Size(16, 17);
            this.AddressLabel.TabIndex = 0;
            this.AddressLabel.Text = "0";
            this.AddressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AddressBox
            // 
            this.AddressBox.Location = new System.Drawing.Point(77, 4);
            this.AddressBox.Name = "AddressBox";
            this.AddressBox.Size = new System.Drawing.Size(100, 22);
            this.AddressBox.TabIndex = 1;
            this.AddressBox.TextChanged += new System.EventHandler(this.AddressBox_TextChanged);
            // 
            // ValueBox
            // 
            this.ValueBox.Location = new System.Drawing.Point(77, 32);
            this.ValueBox.Name = "ValueBox";
            this.ValueBox.Size = new System.Drawing.Size(100, 22);
            this.ValueBox.TabIndex = 3;
            this.ValueBox.TextChanged += new System.EventHandler(this.ValueBox_TextChanged);
            // 
            // ValueLabel
            // 
            this.ValueLabel.AutoSize = true;
            this.ValueLabel.ForeColor = System.Drawing.Color.Coral;
            this.ValueLabel.Location = new System.Drawing.Point(183, 35);
            this.ValueLabel.Name = "ValueLabel";
            this.ValueLabel.Size = new System.Drawing.Size(16, 17);
            this.ValueLabel.TabIndex = 2;
            this.ValueLabel.Text = "0";
            this.ValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PokeButton
            // 
            this.PokeButton.Location = new System.Drawing.Point(15, 60);
            this.PokeButton.Name = "PokeButton";
            this.PokeButton.Size = new System.Drawing.Size(214, 34);
            this.PokeButton.TabIndex = 4;
            this.PokeButton.Text = "Poke!";
            this.PokeButton.UseVisualStyleBackColor = true;
            this.PokeButton.Click += new System.EventHandler(this.PokeButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Coral;
            this.label3.Location = new System.Drawing.Point(28, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Value";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Coral;
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "Address";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Purple;
            this.ClientSize = new System.Drawing.Size(253, 104);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.PokeButton);
            this.Controls.Add(this.ValueBox);
            this.Controls.Add(this.ValueLabel);
            this.Controls.Add(this.AddressBox);
            this.Controls.Add(this.AddressLabel);
            this.Name = "Form1";
            this.Text = "TINRS EEPoke";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label AddressLabel;
        private System.Windows.Forms.TextBox AddressBox;
        private System.Windows.Forms.TextBox ValueBox;
        private System.Windows.Forms.Label ValueLabel;
        private System.Windows.Forms.Button PokeButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

