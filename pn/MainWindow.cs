using CefSharp;

namespace pn
{
    public partial class MainWindow : Form
    {
        private Manager manager;
        private FileInfo image;

        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void KeepButton_Click(object sender, EventArgs e)
        {
            this.manager.RemoveImage(this.image, true);
            this.nextImage();
        }

        private void TossButton_Click(object sender, EventArgs e)
        {
            this.manager.RemoveImage(this.image, false);
            this.nextImage();
        }

        private void SkipButton_Click(object sender, EventArgs e) => this.nextImage();

        private void Button_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'w':
                    this.KeepButton_Click(null, null);
                    break;
                case 's':
                    this.TossButton_Click(null, null);
                    break;
                case 'd':
                    this.SkipButton_Click(null, null);
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
        private void MainWindow_Load(object sender, EventArgs e)
        {
            var directory = new DirectoryInfo(this.chooseDirectory());

            this.manager = new Manager(directory);
            if (this.manager.Pass == "Done")
            {
                this.Close();
                return;
            }

            this.nextImage();
        }

        /// <summary>
        /// Forces the user to choose a directory until they've selected one and clicked OK.
        /// </summary>
        /// <returns>Directory path.</returns>
        private string chooseDirectory()
        {
            while (this.FolderDialog.ShowDialog() != DialogResult.OK)
            {
                ;
            }

            return this.FolderDialog.SelectedPath;
        }

        /// <summary>
        /// Remove the current image from the manager and serve up the next one with updated text.
        /// </summary>
        private void nextImage(bool actuallyProgressForward = true)
        {
            // Check for end
            if (this.manager.ImagesRemaining == 0)
            {
                this.Close();
            }
            else
            {
                // Show the next image
                if (actuallyProgressForward)
                {
                    this.image = this.manager.RandomImage();
                }

                if (Manager.SupportedHtml5VideoFormats.Contains(this.image.Extension))
                {
                    this.updateBrowserVideo();
                }
                else
                {
                    this.ChromeBrowser.Load($"file:///{this.image.FullName}");
                    this.updateImageLabels(true);
                }
            }
        }

        /// <summary>
        /// Updates the remaining images text and current filename text.
        /// </summary>
        private void updateImageLabels(bool isImage = false)
        {
            this.RemainingImages.Text = $"{this.manager.ImagesRemaining - 1} Remaining...";

            if (!isImage)
            {
                this.Filename.Text = $"{this.manager.Pass} pass: {this.image.Name}";
            }
            else
            {
                var img = Image.FromFile(this.image.FullName);
                this.Filename.Text = $"{this.manager.Pass} pass: {this.image.Name} ({img.Width}x{img.Height})";
                img.Dispose();
            }
        }

        /// <summary>
        /// Resizes the HTML5 video content (when applicable)
        /// </summary>
        private void updateBrowserVideo()
        {
            if (Manager.SupportedHtml5VideoFormats.Contains(this.image.Extension))
            {
                _ = this.ChromeBrowser.LoadHtml($@"
<!DOCTYPE html><html><body>

<video width=""{this.ChromeBrowser.Width - 25}"" height=""{this.ChromeBrowser.Height - 25}"" autoplay controls loop src=""file:///{this.image.FullName}"">
    Your browser does not support HTML5 video.
</video>

</body></html>", $"file:///{this.image.Directory.FullName}");
            }

            this.updateImageLabels();
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e) => Cef.Shutdown();
    }
}
