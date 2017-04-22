using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace picture_narrowing
{
    /// <summary>
    /// Instead of actually moving the files everywhere, just keep track of the ones to move until the third round.
    /// </summary>
    /// <remarks> 
    /// General principals:
    ///     Take the directory in question.
    ///     Add a file to keep track of the positives and minuses.
    ///     Rotate through the directory worth of picutes 3 times.
    ///     Final pass: two minuses count as a toss. Move those images to the toss directory.
    /// </remarks>
    internal sealed class Manager
    {
        private List<FileInfo> Images { get; set; }
        private PictureNarrowingConfig Config { get; set; }
        private Random Random { get; set; } = new Random();

        public string Pass { get; private set; }
        public int ImagesRemaining => Images.Count();

        public Manager(string directoryPath)
            : this(new DirectoryInfo(directoryPath)) { }

        public Manager(DirectoryInfo dirInfo)
        {
            var configFile = new FileInfo(Path.Combine(dirInfo.FullName, PictureNarrowingConfig.Filename));

            // Either create the config file, or load an existing one
            if (!configFile.Exists)
            {
                this.Images = dirInfo.EnumerateFiles().ToList();
                this.trimOutNonimages();

                this.Config = new PictureNarrowingConfig(dirInfo);
                foreach (var info in this.Images)
                    this.Config.Files.Add(info.Name, new List<bool>());
                this.Config.Save();
            }
            else
            {
                this.Config = new PictureNarrowingConfig(dirInfo);
                this.Config.Load();
            }

            if (this.Config.Files.Any(kvp => kvp.Value.Count == 0))
            {
                this.Pass = "First";
                this.Images = this.Config.Files.Where(kvp => kvp.Value.Count <= 0)
                    .Select(kvp => new FileInfo(Path.Combine(dirInfo.FullName, kvp.Key)))
                    .ToList();
            }
            else if (this.Config.Files.Any(kvp => kvp.Value.Count == 1))
            {
                this.Pass = "Second";
                this.Images = this.Config.Files.Where(kvp => kvp.Value.Count <= 1)
                    .Select(kvp => new FileInfo(Path.Combine(dirInfo.FullName, kvp.Key)))
                    .ToList();
            }
            else if (this.Config.Files.Any(kvp => kvp.Value.Count == 2))
            {
                this.Pass = "Third";
                this.Images = this.Config.Files.Where(kvp => kvp.Value.Count <= 2)
                    .Select(kvp => new FileInfo(Path.Combine(dirInfo.FullName, kvp.Key)))
                    .ToList();
            }
            else
            {
                // Turns out we're done. Filter down to the ones that have at most one false value.
                var tossDir = new DirectoryInfo(Path.Combine(dirInfo.FullName, "toss"));
                if (!tossDir.Exists)
                    tossDir.Create();
                foreach (var info in this.Config.Files.Where(kvp => kvp.Value.Count(b => b) < 2).Select(kvp => kvp.Key))
                    new FileInfo(Path.Combine(dirInfo.FullName, info))
                        .MoveTo(Path.Combine(tossDir.FullName, info));
                this.Pass = "Done";
                this.Images = new List<FileInfo>();
            }
        }

        public FileInfo RandomImage()
        {
            return this.Images[this.Random.Next(this.Images.Count)];
        }

        public void RemoveImage(FileInfo image, bool vote)
        {
            this.Images.Remove(image);
            this.Config.Files[image.Name].Add(vote);
            this.Config.Save();
        }

        /// <summary>
        /// Attempt to open each file using the Image class and only keep the ones that successfully
        /// open as image files.
        /// </summary>
        private void trimOutNonimages()
        {
            // 25 MB
            const long bytesBeforeCollecting = 25 * 1000 * 1000;
            long bytesSeen = 0;

            List<FileInfo> actuallyImages = new List<FileInfo>();
            foreach (var info in this.Images)
            {
                try
                {
                    Image.FromFile(info.FullName);
                    bytesSeen += info.Length;
                }
                catch (OutOfMemoryException)
                {
                    continue;
                }
                actuallyImages.Add(info);

                // We're creating a lot of large objects and discarding them quickly
                // So force a GC.Collect every so often since the garbage collector doesn't keep up
                if (bytesSeen > bytesBeforeCollecting)
                {
                    GC.Collect();
                    bytesSeen = 0;
                }
            }
            this.Images = actuallyImages;

            // And once more, this time with feeling.
            GC.Collect();
        }
    }
}
