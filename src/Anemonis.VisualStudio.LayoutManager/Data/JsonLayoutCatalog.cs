// © Alexander Kozlenko. Licensed under the MIT License.

using Newtonsoft.Json;

#pragma warning disable CA1812

namespace Anemonis.VisualStudio.LayoutManager.Data
{
    /// <summary>Represents layout catalog information in JSON.</summary>
    internal sealed class JsonLayoutCatalog
    {
        /// <summary>Initializes a new instance of the <see cref="JsonLayoutCatalog" /> class.</summary>
        public JsonLayoutCatalog()
        {
        }

        /// <summary>Gets or sets the product packages.</summary>
        [JsonProperty("packages", Required = Required.Always)]
        public JsonLayoutPackage[] Packages
        {
            get;
            set;
        }
    }
}

#pragma warning restore CA1812
