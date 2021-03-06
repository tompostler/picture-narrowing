﻿using CefSharp;
using CefSharp.WinForms;

namespace picture_narrowing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.RemainingImages = new System.Windows.Forms.Label();
            this.KeepButton = new System.Windows.Forms.Button();
            this.TossButton = new System.Windows.Forms.Button();
            this.SkipButton = new System.Windows.Forms.Button();
            this.FolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.Filename = new System.Windows.Forms.Label();
            this.ChromeBrowser = new ChromiumWebBrowser(string.Empty);
            this.SuspendLayout();
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
            this.KeepButton.Click += new System.EventHandler(this.KeepButton_Click);
            this.KeepButton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Button_KeyPress);
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
            this.TossButton.Click += new System.EventHandler(this.TossButton_Click);
            this.TossButton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Button_KeyPress);
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
            this.SkipButton.Click += new System.EventHandler(this.SkipButton_Click);
            this.SkipButton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Button_KeyPress);
            // 
            // FolderDialog
            // 
            this.FolderDialog.Description = "Select a directory with images in it.";
            this.FolderDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.FolderDialog.ShowNewFolderButton = false;
            // 
            // Filename
            // 
            this.Filename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Filename.Location = new System.Drawing.Point(361, 407);
            this.Filename.Name = "Filename";
            this.Filename.Size = new System.Drawing.Size(248, 25);
            this.Filename.TabIndex = 5;
            this.Filename.Text = "Filename";
            this.Filename.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // ChromeBrowser
            //
            this.ChromeBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ChromeBrowser.Location = new System.Drawing.Point(15, 15);
            this.ChromeBrowser.Margin = new System.Windows.Forms.Padding(0);
            this.ChromeBrowser.Name = "ChromeBrowser";
            this.ChromeBrowser.Size = new System.Drawing.Size(594, 382);
            this.ChromeBrowser.TabIndex = 0;
            this.ChromeBrowser.TabStop = false;
            this.ChromeBrowser.BrowserSettings.FileAccessFromFileUrls = CefState.Enabled;
            this.ChromeBrowser.BrowserSettings.UniversalAccessFromFileUrls = CefState.Enabled;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.Filename);
            this.Controls.Add(this.SkipButton);
            this.Controls.Add(this.TossButton);
            this.Controls.Add(this.KeepButton);
            this.Controls.Add(this.RemainingImages);
            this.Controls.Add(this.ChromeBrowser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "MainWindow";
            this.Text = "Picture Narrowing";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.Resize += MainWindow_Resize;
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label RemainingImages;
        private System.Windows.Forms.Button KeepButton;
        private System.Windows.Forms.Button TossButton;
        private System.Windows.Forms.Button SkipButton;
        private System.Windows.Forms.FolderBrowserDialog FolderDialog;
        private System.Windows.Forms.Label Filename;
        private ChromiumWebBrowser ChromeBrowser;
    }
}

