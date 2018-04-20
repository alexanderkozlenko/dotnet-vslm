using System;
using System.Globalization;
using System.Text;

namespace Community.VisualStudio.LayoutManager.Data
{
    /// <summary>Represents layout package information.</summary>
    public sealed class LayoutPackage : IEquatable<LayoutPackage>
    {
        /// <summary>Initializes a new instance of the <see cref="LayoutPackage" /> class.</summary>
        /// <param name="id">The package identifier.</param>
        /// <param name="version">The package version.</param>
        /// <param name="chip">The package chip code.</param>
        /// <param name="language">The package language code.</param>
        /// <exception cref="ArgumentNullException"><paramref name="id" /> or <paramref name="version" /> is <see langword="null" />.</exception>
        public LayoutPackage(string id, string version, string chip, string language)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            if (version == null)
            {
                throw new ArgumentNullException(nameof(version));
            }

            Id = id;
            Version = version;
            Chip = chip;
            Language = language;
        }

        /// <summary>Indicates whether the current <see cref="LayoutPackage" /> is equal to another <see cref="LayoutPackage" />.</summary>
        /// <param name="other">A <see cref="LayoutPackage" /> to compare with this <see cref="LayoutPackage" />.</param>
        /// <returns><see langword="true" /> if the current <see cref="LayoutPackage" /> is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
        public bool Equals(LayoutPackage other)
        {
            if (other == null)
            {
                return false;
            }

            return
                (string.Compare(Id, other.Id, StringComparison.InvariantCultureIgnoreCase) == 0) &&
                (string.Compare(Version, other.Version, StringComparison.InvariantCultureIgnoreCase) == 0) &&
                (string.Compare(Chip, other.Chip, StringComparison.InvariantCultureIgnoreCase) == 0) &&
                (string.Compare(Language, other.Language, StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        /// <summary>Indicates whether the current <see cref="LayoutPackage" /> is equal to another object.</summary>
        /// <param name="obj">An object to compare with this <see cref="LayoutPackage" />.</param>
        /// <returns><see langword="true" /> if the current <see cref="LayoutPackage" /> is equal to the <paramref name="obj" /> parameter; otherwise, <see langword="false" />.</returns>
        public override bool Equals(object obj)
        {
            return obj is LayoutPackage other ? Equals(other) : false;
        }

        /// <summary>Serves as the default hash function.</summary>
        /// <returns>A hash code for the current <see cref="LayoutPackage" />.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var result = (int)2166136261;

                result = (result * 16777619) ^ Id.ToLowerInvariant().GetHashCode();
                result = (result * 16777619) ^ Version.ToLowerInvariant().GetHashCode();

                if (Chip != null)
                {
                    result = (result * 16777619) ^ Chip.ToLowerInvariant().GetHashCode();
                }
                if (Language != null)
                {
                    result = (result * 16777619) ^ Language.ToLowerInvariant().GetHashCode();
                }

                return result;
            }
        }

        /// <summary>Returns a string that represents the current <see cref="LayoutPackage" />.</summary>
        /// <returns>A string that represents the current <see cref="LayoutPackage" />.</returns>
        public override string ToString()
        {
            var builder = new StringBuilder(string.Format(CultureInfo.InvariantCulture, "{0}-{1}", Id, Version));

            if (Chip != null)
            {
                builder.Append("-" + Chip);
            }
            if (Language != null)
            {
                builder.Append("-" + Language);
            }

            return builder.ToString();
        }

        /// <summary>Gets the package directory name.</summary>
        /// <returns>The package directory name.</returns>
        public string GetDirectoryName()
        {
            var builder = new StringBuilder(string.Format(CultureInfo.InvariantCulture, "{0},version={1}", Id, Version));

            if (Chip != null)
            {
                builder.Append(',');
                builder.Append("chip=" + Chip);
            }
            if (Language != null)
            {
                builder.Append(',');
                builder.Append("language=" + Language);
            }

            return builder.ToString();
        }

        /// <summary>Gets the package identifier.</summary>
        public string Id
        {
            get;
        }

        /// <summary>Gets the package version.</summary>
        public string Version
        {
            get;
        }

        /// <summary>Gets the package chip code.</summary>
        public string Chip
        {
            get;
        }

        /// <summary>Gets the package language code.</summary>
        public string Language
        {
            get;
        }
    }
}