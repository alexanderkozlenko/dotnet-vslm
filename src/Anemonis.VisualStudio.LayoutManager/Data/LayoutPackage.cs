// © Alexander Kozlenko. Licensed under the MIT License.

using System;
using System.Text;

#pragma warning disable CA1812

namespace Anemonis.VisualStudio.LayoutManager.Data
{
    /// <summary>Represents layout package information.</summary>
    public sealed class LayoutPackage : IEquatable<LayoutPackage>
    {
        private readonly string _id;
        private readonly string _version;
        private readonly string _architecture;
        private readonly string _language;

        /// <summary>Initializes a new instance of the <see cref="LayoutPackage" /> class.</summary>
        /// <param name="id">The package identifier.</param>
        /// <param name="version">The package version.</param>
        /// <param name="architecture">The package architecture code.</param>
        /// <param name="language">The package language code.</param>
        /// <exception cref="ArgumentNullException"><paramref name="id" /> or <paramref name="version" /> is <see langword="null" />.</exception>
        public LayoutPackage(string id, string version, string architecture, string language)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            if (version == null)
            {
                throw new ArgumentNullException(nameof(version));
            }

            _id = id;
            _version = version;
            _architecture = architecture;
            _language = language;
        }

        /// <inheritdoc />
        public bool Equals(LayoutPackage other)
        {
            if (other == null)
            {
                return false;
            }

            return
                (string.Compare(_id, other._id, StringComparison.OrdinalIgnoreCase) == 0) &&
                (string.Compare(_version, other._version, StringComparison.OrdinalIgnoreCase) == 0) &&
                (string.Compare(_architecture, other._architecture, StringComparison.OrdinalIgnoreCase) == 0) &&
                (string.Compare(_language, other._language, StringComparison.OrdinalIgnoreCase) == 0);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return (obj is LayoutPackage other) && Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return HashCode.Combine(
                _id.ToUpperInvariant(),
                _version.ToUpperInvariant(),
                _architecture?.ToUpperInvariant(),
                _language?.ToUpperInvariant());
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var builder = new StringBuilder(_id);

            builder.Append(',');
            builder.Append("version");
            builder.Append('=');
            builder.Append(_version);

            if (_architecture != null)
            {
                builder.Append(',');
                builder.Append("chip");
                builder.Append('=');
                builder.Append(_architecture);
            }
            if (_language != null)
            {
                builder.Append(',');
                builder.Append("language");
                builder.Append('=');
                builder.Append(_language);
            }

            return builder.ToString();
        }

        /// <summary>Gets the package identifier.</summary>
        public string Id
        {
            get => _id;
        }

        /// <summary>Gets the package version.</summary>
        public string Version
        {
            get => _version;
        }

        /// <summary>Gets the package architecture code.</summary>
        public string Architecture
        {
            get => _architecture;
        }

        /// <summary>Gets the package language code.</summary>
        public string Language
        {
            get => _language;
        }
    }
}
