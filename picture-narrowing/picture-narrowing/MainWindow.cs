using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace picture_narrowing
{
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

        private string chooseDirectory()
        {
            while (FolderDialog.ShowDialog() != DialogResult.OK) ;
            return FolderDialog.SelectedPath;
        }

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

        private void updateRemainingLabel()
        {
            RemainingImages.Text = $"{manager.ImagesRemaining - 1} Remaining...";
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            manager.DeleteFiles();
        }
    }
}
