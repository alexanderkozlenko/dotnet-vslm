﻿using Newtonsoft.Json;

namespace Community.VisualStudio.LayoutManager.Data
{
    /// <summary>Represents catalog information.</summary>
    internal sealed class CatalogInfo
    {
        /// <summary>Initializes a new instance of the <see cref="CatalogInfo" /> class.</summary>
        public CatalogInfo()
        {
        }

        /// <summary>Gets or sets product information.</summary>
        [JsonProperty("info")]
        public CatalogProductInfo Product
        {
            get;
            set;
        }

        /// <summary>Gets or sets packages collection.</summary>
        [JsonProperty("packages")]
        public CatalogPackageInfo[] Packages
        {
            get;
            set;
        }
    }
}