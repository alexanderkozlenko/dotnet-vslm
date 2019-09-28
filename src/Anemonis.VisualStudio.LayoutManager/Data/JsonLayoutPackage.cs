// © Alexander Kozlenko. Licensed under the MIT License.

using System.Text.Json.Serialization;

#pragma warning disable CA1812

namespace Anemonis.VisualStudio.LayoutManager.Data
{
    /// <summary>Represents layout package information in JSON.</summary>
    internal sealed class JsonLayoutPackage
    {
        /// <summary>Initializes a new instance of the <see cref="JsonLayoutPackage" /> class.</summary>
        public JsonLayoutPackage()
        {
        }

        /// <summary>Gets or sets the package identifier.</summary>
        [JsonPropertyName("id")]
        public string Id
        {
            get;
            set;
        }

        /// <summary>Gets or sets the package version.</summary>
        [JsonPropertyName("version")]
        public string Version
        {
            get;
            set;
        }

        /// <summary>Gets or sets the package architecture code.</summary>
        [JsonPropertyName("chip")]
        public string Architecture
        {
            get;
            set;
        }

        /// <summary>Gets or sets the package language code.</summary>
        [JsonPropertyName("language")]
        public string Language
        {
            get;
            set;
        }
    }
}
