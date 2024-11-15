using Newtonsoft.Json;

namespace pn
{
    internal sealed class PictureNarrowingConfig
    {
        [JsonIgnore]
        public const string Filename = "picture-narrowing.config";
        [JsonIgnore]
        public DirectoryInfo DirectoryInfo { get; set; }

        public Dictionary<string, List<bool>> Files { get; set; }
            = [];

        public PictureNarrowingConfig() { }

        public PictureNarrowingConfig(DirectoryInfo dirInfo)
        {
            this.DirectoryInfo = dirInfo;
        }

        public void Load()
        {
            this.Files = JsonConvert.DeserializeObject<PictureNarrowingConfig>(
                File.ReadAllText(Path.Combine(this.DirectoryInfo.FullName, Filename))).Files;
        }

        public void Save()
        {
            string output = Path.Combine(this.DirectoryInfo.FullName, Filename);
            string stroutput = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(output, stroutput);
        }
    }
}
