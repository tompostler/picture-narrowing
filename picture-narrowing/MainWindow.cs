namespace picture_narrowing
{
    using CefSharp;
    using System;
    using System.Drawing;
    using System.IO;
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
        private void nextImage(bool actuallyProgressForward = true)
        {
            // Check for end
            if (manager.ImagesRemaining == 0)
            {
                Close();
            }
            else
            {
                // Show the next image
                if (actuallyProgressForward)
                    image = manager.RandomImage();
                if (Manager.SupportedHtml5VideoFormats.Contains(image.Extension))
                    this.updateBrowserVideo();
                else
                {
                    ChromeBrowser.Load($"file:///{image.FullName}");
                    updateImageLabels(true);
                }
            }
        }

        /// <summary>
        /// Updates the remaining images text and current filename text.
        /// </summary>
        private void updateImageLabels(bool isImage = false)
        {
            RemainingImages.Text = $"{manager.ImagesRemaining - 1} Remaining...";

            if (!isImage)
                Filename.Text = $"{manager.Pass} pass: {image.Name}";
            else
            {
                var img = Image.FromFile(image.FullName);
                Filename.Text = $"{manager.Pass} pass: {image.Name} ({img.Width}x{img.Height})";
                img.Dispose();
            }
        }

        /// <summary>
        /// Resizes the HTML5 video content (when applicable)
        /// </summary>
        private void updateBrowserVideo()
        {
            if (Manager.SupportedHtml5VideoFormats.Contains(image.Extension))
                ChromeBrowser.LoadHtml($@"
<!DOCTYPE html><html><body>

<video width=""{ChromeBrowser.Width - 25}"" height=""{ChromeBrowser.Height - 25}"" autoplay controls loop src=""file:///{image.FullName}"">
    Your browser does not support HTML5 video.
</video>

</body></html>", $"file:///{image.Directory.FullName}");
            this.updateImageLabels();
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }

        private void MainWindow_Resize(object sender, System.EventArgs e)
        {
            this.nextImage(actuallyProgressForward: false);
        }
    }
}
