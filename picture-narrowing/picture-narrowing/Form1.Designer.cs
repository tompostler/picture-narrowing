namespace picture_narrowing
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.RemainingImages = new System.Windows.Forms.Label();
            this.KeepButton = new System.Windows.Forms.Button();
            this.TossButton = new System.Windows.Forms.Button();
            this.SkipButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(15, 15);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(594, 382);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // RemainingImages
            // 
            this.RemainingImages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RemainingImages.Location = new System.Drawing.Point(12, 407);
            this.RemainingImages.Name = "RemainingImages";
            this.RemainingImages.Size = new System.Drawing.Size(100, 25);
            this.RemainingImages.TabIndex = 1;
            this.RemainingImages.Text = "1000 Remaining...";
            this.RemainingImages.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // KeepButton
            // 
            this.KeepButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.KeepButton.Location = new System.Drawing.Point(118, 409);
            this.KeepButton.Name = "KeepButton";
            this.KeepButton.Size = new System.Drawing.Size(75, 23);
            this.KeepButton.TabIndex = 2;
            this.KeepButton.Text = "Keep";
            this.KeepButton.UseVisualStyleBackColor = true;
            // 
            // TossButton
            // 
            this.TossButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TossButton.Location = new System.Drawing.Point(199, 409);
            this.TossButton.Name = "TossButton";
            this.TossButton.Size = new System.Drawing.Size(75, 23);
            this.TossButton.TabIndex = 3;
            this.TossButton.Text = "Toss";
            this.TossButton.UseVisualStyleBackColor = true;
            // 
            // SkipButton
            // 
            this.SkipButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SkipButton.Location = new System.Drawing.Point(280, 409);
            this.SkipButton.Name = "SkipButton";
            this.SkipButton.Size = new System.Drawing.Size(75, 23);
            this.SkipButton.TabIndex = 4;
            this.SkipButton.Text = "Skip";
            this.SkipButton.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.SkipButton);
            this.Controls.Add(this.TossButton);
            this.Controls.Add(this.KeepButton);
            this.Controls.Add(this.RemainingImages);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "Form1";
            this.Text = "Picture Narrowing";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label RemainingImages;
        private System.Windows.Forms.Button KeepButton;
        private System.Windows.Forms.Button TossButton;
        private System.Windows.Forms.Button SkipButton;
    }
}

