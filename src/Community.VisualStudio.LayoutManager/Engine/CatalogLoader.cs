using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Community.VisualStudio.LayoutManager.Data;
using Newtonsoft.Json;

namespace Community.VisualStudio.LayoutManager.Engine
{
    /// <summary>Catalog information loader.</summary>
    internal static class CatalogLoader
    {
        private static readonly Regex _packageNameRegex = new Regex("^(?<i>[^,]+),version=(?<v>[^,]+)(?:,chip=(?<c>[^,]+))?(?:,language=(?<l>[^,]+))?$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>Loads packages collection and catalog version from manifest file.</summary>
        /// <param name="layoutPath">The installation layout path.</param>
        /// <returns>Packages collection and catalog version.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="layoutPath" /> is <see langword="null" />.</exception>
        public static (IReadOnlyList<CatalogPackageInfo>, string) LoadManifestPackages(string layoutPath)
        {
            if (layoutPath == null)
            {
                throw new ArgumentNullException(nameof(layoutPath));
            }

            var catalogInfo = default(CatalogInfo);

            using (var stream = new FileStream(Path.Combine(layoutPath, "Catalog.json"), FileMode.Open, FileAccess.Read, FileShare.Read, 8 * 1024 * 1024))
            {
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    catalogInfo = JsonConvert.DeserializeObject<CatalogInfo>(reader.ReadToEnd());
                }
            }

            return (catalogInfo.Packages, catalogInfo.Product.SemanticVersion);
        }

        /// <summary>Loads packages collection from installation layout.</summary>
        /// <param name="layoutPath">The installation layout path.</param>
        /// <returns>Packages collection.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="layoutPath" /> is <see langword="null" />.</exception>
        public static IReadOnlyList<CatalogPackageInfo> LoadLayoutPackages(string layoutPath)
        {
            if (layoutPath == null)
            {
                throw new ArgumentNullException(nameof(layoutPath));
            }

            var directories = Directory.GetDirectories(layoutPath);
            var result = new List<CatalogPackageInfo>(directories.Length);

            for (var i = 0; i < directories.Length; i++)
            {
                var match = _packageNameRegex.Match(Path.GetFileName(directories[i]));

                if (match.Success)
                {
                    var package = new CatalogPackageInfo
                    {
                        ID = match.Groups["i"].Value,
                        Version = match.Groups["v"].Value,
                        Chip = match.Groups["c"].Success ? match.Groups["c"].Value : null,
                        Language = match.Groups["l"].Success ? match.Groups["l"].Value : null
                    };

                    result.Add(package);
                }
            }

            return result;
        }
    }
}