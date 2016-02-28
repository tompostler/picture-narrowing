namespace picture_narrowing
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;

    internal sealed class Manager
    {
        public DirectoryInfo Directory { get; private set; }
        public int ImagesRemaining => _images.Count;
        public FileInfo RandomImage => _images[_random.Next(_images.Count)];

        private List<FileInfo> _images;
        private List<FileInfo> _toDelete;
        private Random _random;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="directory">The path to the directory with images in it.</param>
        public Manager(string directory) : this(new DirectoryInfo(directory)) { }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="directory">The DirectoryInfo object for the directory containing the images.</param>
        public Manager(DirectoryInfo directory)
        {
            Directory = directory;
            _images = new List<FileInfo>();
            _random = new Random();

            _images = Directory.EnumerateFiles().ToList();
            trimOutNonimages();
        }

        /// <summary>
        /// Removes the image from the list of remaining images.
        /// </summary>
        /// <param name="image"></param>
        public void RemoveImage(FileInfo image)
        {
            _images.Remove(image);
            _toDelete.Add(image);
        }

        /// <summary>
        /// Run through and delete all the files scheduled for deletion.
        /// </summary>
        public void DeleteFiles()
        {
            foreach (FileInfo info in _toDelete)
            {
                info.Delete();
            }
        }

        /// <summary>
        /// Attempt to open each file using the Image class and only keep the ones that successfully
        /// open as image files.
        /// </summary>
        private void trimOutNonimages()
        {
            List<FileInfo> actuallyImages = new List<FileInfo>();
            for (int i = 0; i < _images.Count; i++)
            {
                FileInfo info = _images[i];
                try
                {
                    Image.FromFile(info.FullName);
                }
                catch (OutOfMemoryException)
                {
                    continue;
                }
                actuallyImages.Add(info);

                // We're creating a lot of large objects and discarding them quickly
                // So force a GC.Collect every 25 images since the garbage collector doesn't keep up
                if (i % 25 == 0)
                    GC.Collect();
            }
            _images = actuallyImages;
            _toDelete = new List<FileInfo>(_images.Count);
        }
    }
}
