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
            image.CopyTo(Path.Combine(new string[] { image.DirectoryName, "keep", image.Name }));
            nextImage();
        }

        private void TossButton_Click(object sender, EventArgs e)
        {
            image.CopyTo(Path.Combine(new string[] { image.DirectoryName, "toss", image.Name }));
            nextImage();
        }

        private void SkipButton_Click(object sender, EventArgs e)
        {
            nextImage(false);
        }

        private void MainWindow_KeyPress(object sender, KeyPressEventArgs e)
        {

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

            // Create the necessary subdirectories if not already existing
            try
            {
                directory.CreateSubdirectory("keep");
            }
            catch (IOException) { }
            try
            {
                directory.CreateSubdirectory("toss");
            }
            catch (IOException) { }

            manager = new Manager(directory);
            nextImage(false);
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
        /// <param name="removeImage">True to remove the image from the manager, false to keep in the queue.</param>
        private void nextImage(bool removeImage = true)
        {
            if (removeImage)
            {
                manager.RemoveImage(image);
                updateImageLabels();
            }

            // Check for end
            if (manager.ImagesRemaining == 0)
            {
                Close();
            }
            else
            {
                // Show the next image
                image = manager.RandomImage;
                ImageViewer.Load(image.FullName);
                updateImageLabels();
            }
        }

        /// <summary>
        /// Updates the remaining images text and current filename text.
        /// </summary>
        private void updateImageLabels()
        {
            RemainingImages.Text = $"{manager.ImagesRemaining - 1} Remaining...";
            Filename.Text = image.Name;
        }

        /// <summary>
        /// After the form has closed, delete all the files that are scheduled for deletion.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            ImageViewer.Dispose();
            Thread.Sleep(100); // Make sure the image viewer is disposed.
            manager.DeleteFiles();
        }
    }
}
