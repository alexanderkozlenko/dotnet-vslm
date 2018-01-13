using Newtonsoft.Json;

namespace Community.VisualStudio.LayoutManager.Data
{
    /// <summary>Represents catalog package information.</summary>
    internal sealed class CatalogPackageInfo
    {
        /// <summary>Initializes a new instance of the <see cref="CatalogPackageInfo" /> class.</summary>
        public CatalogPackageInfo()
        {
        }

        /// <summary>Gets or sets identifier.</summary>
        [JsonProperty("id")]
        public string ID
        {
            get;
            set;
        }

        /// <summary>Gets or sets version.</summary>
        [JsonProperty("version")]
        public string Version
        {
            get;
            set;
        }

        /// <summary>Gets or sets chip identifier.</summary>
        [JsonProperty("chip")]
        public string Chip
        {
            get;
            set;
        }

        /// <summary>Gets or sets language identifier.</summary>
        [JsonProperty("language")]
        public string Language
        {
            get;
            set;
        }
    }
}