// © Alexander Kozlenko. Licensed under the MIT License.

using Newtonsoft.Json;

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
        [JsonProperty("id", Required = Required.Always)]
        public string Id
        {
            get;
            set;
        }

        /// <summary>Gets or sets the package version.</summary>
        [JsonProperty("version", Required = Required.Always)]
        public string Version
        {
            get;
            set;
        }

        /// <summary>Gets or sets the package architecture code.</summary>
        [JsonProperty("chip")]
        public string Architecture
        {
            get;
            set;
        }

        /// <summary>Gets or sets the package language code.</summary>
        [JsonProperty("language")]
        public string Language
        {
            get;
            set;
        }
    }
}
