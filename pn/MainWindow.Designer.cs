using CefSharp;
using CefSharp.WinForms;

namespace pn
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
            const int defaultWidth = 1366;
            const int defaultHeight = 768;

            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.RemainingImages = new Label();
            this.KeepButton = new Button();
            this.TossButton = new Button();
            this.SkipButton = new Button();
            this.FolderDialog = new FolderBrowserDialog();
            this.Filename = new Label();
            this.ChromeBrowser = new ChromiumWebBrowser(string.Empty);
            this.SuspendLayout();
            // 
            // RemainingImages
            // 
            this.RemainingImages.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.RemainingImages.Location = new Point(12, defaultHeight - 71);
            this.RemainingImages.Name = "RemainingImages";
            this.RemainingImages.Size = new Size(100, 23);
            this.RemainingImages.TabIndex = 1;
            this.RemainingImages.Text = "1000 Remaining...";
            this.RemainingImages.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // KeepButton
            // 
            this.KeepButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.KeepButton.Location = new Point(118, defaultHeight - 71);
            this.KeepButton.Name = "KeepButton";
            this.KeepButton.Size = new Size(75, 23);
            this.KeepButton.TabIndex = 2;
            this.KeepButton.Text = "Keep";
            this.KeepButton.UseVisualStyleBackColor = true;
            this.KeepButton.Click += this.KeepButton_Click;
            this.KeepButton.KeyPress += this.Button_KeyPress;
            // 
            // TossButton
            // 
            this.TossButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.TossButton.Location = new Point(199, defaultHeight - 71);
            this.TossButton.Name = "TossButton";
            this.TossButton.Size = new Size(75, 23);
            this.TossButton.TabIndex = 3;
            this.TossButton.Text = "Toss";
            this.TossButton.UseVisualStyleBackColor = true;
            this.TossButton.Click += this.TossButton_Click;
            this.TossButton.KeyPress += this.Button_KeyPress;
            // 
            // SkipButton
            // 
            this.SkipButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.SkipButton.Location = new Point(280, defaultHeight - 71);
            this.SkipButton.Name = "SkipButton";
            this.SkipButton.Size = new Size(75, 23);
            this.SkipButton.TabIndex = 4;
            this.SkipButton.Text = "Skip";
            this.SkipButton.UseVisualStyleBackColor = true;
            this.SkipButton.Click += this.SkipButton_Click;
            this.SkipButton.KeyPress += this.Button_KeyPress;
            // 
            // FolderDialog
            // 
            this.FolderDialog.Description = "Select a directory with images in it.";
            this.FolderDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            this.FolderDialog.ShowNewFolderButton = false;
            // 
            // Filename
            // 
            this.Filename.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.Filename.Location = new Point(361, defaultHeight - 71);
            this.Filename.Name = "Filename";
            this.Filename.Size = new Size(defaultWidth - 248, 23);
            this.Filename.TabIndex = 5;
            this.Filename.Text = "Filename";
            this.Filename.TextAlign = ContentAlignment.MiddleLeft;
            this.Filename.AutoEllipsis = true;
            //
            // ChromeBrowser
            //
            this.ChromeBrowser.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.ChromeBrowser.Location = new Point(15, 15);
            this.ChromeBrowser.Margin = new Padding(0);
            this.ChromeBrowser.Name = "ChromeBrowser";
            this.ChromeBrowser.Size = new Size(defaultWidth - 46, defaultHeight - 98);
            this.ChromeBrowser.TabIndex = 0;
            this.ChromeBrowser.TabStop = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(defaultWidth - 16, defaultHeight - 40);
            this.Controls.Add(this.Filename);
            this.Controls.Add(this.SkipButton);
            this.Controls.Add(this.TossButton);
            this.Controls.Add(this.KeepButton);
            this.Controls.Add(this.RemainingImages);
            this.Controls.Add(this.ChromeBrowser);
            this.Icon = (Icon)resources.GetObject("$this.Icon");
            this.MinimumSize = new Size(defaultWidth, defaultHeight);
            this.Name = "MainWindow";
            this.Text = "Picture Narrowing";
            this.FormClosing += this.MainWindow_FormClosing;
            this.Load += this.MainWindow_Load;
            this.ResumeLayout(false);
        }

        #endregion
        private Label RemainingImages;
        private Button KeepButton;
        private Button TossButton;
        private Button SkipButton;
        private FolderBrowserDialog FolderDialog;
        private Label Filename;
        private ChromiumWebBrowser ChromeBrowser;
    }
}