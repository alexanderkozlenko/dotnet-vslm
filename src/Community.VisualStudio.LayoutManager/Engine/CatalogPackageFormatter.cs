using System;
using System.Globalization;
using System.Text;
using Community.VisualStudio.LayoutManager.Data;

namespace Community.VisualStudio.LayoutManager.Engine
{
    /// <summary>A <see cref="CatalogPackageInfo" /> formatter.</summary>
    internal static class CatalogPackageFormatter
    {
        /// <summary>Gets package display name.</summary>
        /// <param name="package">The package to get the name for.</param>
        /// <returns>The display name.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="package"/> is <see langword="null" /></exception>
        public static string GetPackageName(CatalogPackageInfo package)
        {
            if (package == null)
            {
                throw new ArgumentNullException(nameof(package));
            }

            var builder = new StringBuilder(string.Format(CultureInfo.InvariantCulture, "{0} {1}", package.ID, package.Version));

            if (package.Chip != null)
            {
                builder.AppendFormat(CultureInfo.InvariantCulture, " {0}", package.Chip);
            }
            if (package.Language != null)
            {
                builder.AppendFormat(CultureInfo.InvariantCulture, " {0}", package.Language);
            }

            return builder.ToString();
        }

        /// <summary>Gets package directory name.</summary>
        /// <param name="package">The package to get the name for.</param>
        /// <returns>The directory name.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="package"/> is <see langword="null" /></exception>
        public static string GetPackageDirectoryName(CatalogPackageInfo package)
        {
            if (package == null)
            {
                throw new ArgumentNullException(nameof(package));
            }

            var builder = new StringBuilder(string.Format(CultureInfo.InvariantCulture, "{0},version={1}", package.ID, package.Version));

            if (package.Chip != null)
            {
                builder.AppendFormat(CultureInfo.InvariantCulture, ",chip={0}", package.Chip);
            }
            if (package.Language != null)
            {
                builder.AppendFormat(CultureInfo.InvariantCulture, ",language={0}", package.Language);
            }

            return builder.ToString();
        }
    }
}