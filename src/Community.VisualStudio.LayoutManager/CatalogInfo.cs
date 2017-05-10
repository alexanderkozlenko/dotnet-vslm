using Newtonsoft.Json;

namespace Community.VisualStudio.LayoutManager
{
    /// <summary>Represents catalog information.</summary>
    internal sealed class CatalogInfo
    {
        /// <summary>Initializes a new instance of the <see cref="CatalogInfo" /> class.</summary>
        public CatalogInfo()
        {
        }

        /// <summary>Gets or set product information.</summary>
        [JsonProperty("info")]
        public CatalogProductInfo Product { get; set; }

        /// <summary>Gets or set packages.</summary>
        [JsonProperty("packages")]
        public CatalogPackageInfo[] Packages { get; set; }
    }
}