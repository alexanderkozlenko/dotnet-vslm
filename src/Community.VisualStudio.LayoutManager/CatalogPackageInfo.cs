using Newtonsoft.Json;

namespace Community.VisualStudio.LayoutManager
{
    /// <summary>Represents catalog package information.</summary>
    internal sealed class CatalogPackageInfo
    {
        /// <summary>Initializes a new instance of the <see cref="CatalogPackageInfo" /> class.</summary>
        public CatalogPackageInfo()
        {
        }

        public override string ToString() => Chip != null ? $"{ID},version={Version},chip={Chip}" : $"{ID},version={Version}";

        /// <summary>Gets or set identifier.</summary>
        [JsonProperty("id")]
        public string ID { get; set; }

        /// <summary>Gets or set version.</summary>
        [JsonProperty("version")]
        public string Version { get; set; }

        /// <summary>Gets or set chip identifier.</summary>
        [JsonProperty("chip")]
        public string Chip { get; set; }
    }
}