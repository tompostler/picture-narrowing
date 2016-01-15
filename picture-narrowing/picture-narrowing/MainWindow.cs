namespace picture_narrowing
{
    using System;
    using System.IO;
    using System.Windows.Forms;

    public partial class MainWindow : Form
    {
        private Manager manager;
        private FileInfo image;
        // TODO FIX
        private string randomImageToReopenAtEnd;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void KeepButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(randomImageToReopenAtEnd))
            {
                randomImageToReopenAtEnd = Path.Combine(new string[] { image.DirectoryName, "keep", image.Name });
            }
            image.CopyTo(Path.Combine(new string[] { image.DirectoryName, "keep", image.Name }));
            nextImage();
        }

        private void TossButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(randomImageToReopenAtEnd))
            {
                randomImageToReopenAtEnd = Path.Combine(new string[] { image.DirectoryName, "toss", image.Name });
            }
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
        /// Remove the current image from the manager and server up the next one with updated text.
        /// </summary>
        /// <param name="removeImage">True to remove the image from the manager, false to keep in the queue.</param>
        private void nextImage(bool removeImage = true)
        {
            if (removeImage)
            {
                manager.RemoveImage(image);
                updateRemainingLabel();
            }

            // Check for end
            if (manager.ImagesRemaining == 0)
            {
                ImageViewer.Load(randomImageToReopenAtEnd);
                Close();
            }
            else
            {
                // Show the next image
                image = manager.RandomImage;
                ImageViewer.Load(image.FullName);
                updateRemainingLabel();
            }
        }

        /// <summary>
        /// Updates the remaining images text based on the count in the manager.
        /// </summary>
        private void updateRemainingLabel()
        {
            RemainingImages.Text = $"{manager.ImagesRemaining - 1} Remaining...";
        }

        /// <summary>
        /// After the form has closed, delete all the files that are scheduled for deletion.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            manager.DeleteFiles();
        }
    }
}
