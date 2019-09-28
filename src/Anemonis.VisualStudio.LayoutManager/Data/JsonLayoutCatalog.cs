// © Alexander Kozlenko. Licensed under the MIT License.

using System.Text.Json.Serialization;

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
        [JsonPropertyName("packages")]
        public JsonLayoutPackage[] Packages
        {
            get;
            set;
        }
    }
}
