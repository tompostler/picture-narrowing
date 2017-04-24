namespace picture_narrowing
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Windows.Forms;

    public partial class MainWindow : Form
    {
        private Manager manager;
        private FileInfo image;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void KeepButton_Click(object sender, EventArgs e)
        {
            manager.RemoveImage(image, true);
            nextImage();
        }

        private void TossButton_Click(object sender, EventArgs e)
        {
            manager.RemoveImage(image, false);
            nextImage();
        }

        private void SkipButton_Click(object sender, EventArgs e)
        {
            nextImage();
        }

        private void Button_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'w':
                    KeepButton_Click(null, null);
                    break;
                case 's':
                    TossButton_Click(null, null);
                    break;
                case 'd':
                    SkipButton_Click(null, null);
                    break;
            }
            e.Handled = true;
        }

        /// <summary>
        /// When the main window is loaded:
        ///     Force the user to choose a directory,
        ///     Dumbly try to create the keep/toss directories,
        ///     Expensively check if every file is an image, and
        ///     Load the first image into the viewer.
        /// This is all done syncronously and expensively.
        /// TODO: Refactor to be less stupid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Load(object sender, EventArgs e)
        {
            DirectoryInfo directory = new DirectoryInfo(chooseDirectory());

            manager = new Manager(directory);
            if (manager.Pass == "Done")
            {
                Close();
                return;
            }

            nextImage();
        }

        /// <summary>
        /// Forces the user to choose a directory until they've selected one and clicked OK.
        /// </summary>
        /// <returns>Directory path.</returns>
        private string chooseDirectory()
        {
            while (FolderDialog.ShowDialog() != DialogResult.OK) ;
            return FolderDialog.SelectedPath;
        }

        /// <summary>
        /// Remove the current image from the manager and serve up the next one with updated text.
        /// </summary>
        private void nextImage()
        {
            // Check for end
            if (manager.ImagesRemaining == 0)
            {
                Close();
            }
            else
            {
                // Show the next image
                image = manager.RandomImage();
                //ImageViewer.Load(image.FullName);
                //https://www.microsoft.com/en-in/download/details.aspx?id=40784
                this.DisplayHtml($@"
<!DOCTYPE html> 
<html> 
<body> 

<video autoplay controls loop src=""file:///{image.FullName}"">
  Your browser does not support HTML5 video.
</video>

</body>
</html>
");
                updateImageLabels();
            }
        }

        /// <summary>
        /// Updates the remaining images text and current filename text.
        /// </summary>
        private void updateImageLabels()
        {
            RemainingImages.Text = $"{manager.ImagesRemaining - 1} Remaining...";
            Filename.Text = $"{manager.Pass} pass: {image.Name} ";// ({ImageViewer.Image.Width}x{ImageViewer.Image.Height})";
        }

        /// <summary>
        /// After the form has closed, delete all the files that are scheduled for deletion.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            ImageViewer.Dispose();
            Thread.Sleep(250); // Make sure the image viewer is disposed.
        }
        
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }

        //private void DisplayHtml(string html)
        //{
        //    webBrowser1.Navigate("about:blank");
        //    try
        //    {
        //        if (webBrowser1.Document != null)
        //        {
        //            webBrowser1.Document.Write(string.Empty);
        //        }
        //    }
        //    catch (Exception e)
        //    { } // do nothing with this
        //    webBrowser1.DocumentText = html;
        //}
    }
}
