// © Alexander Kozlenko. Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

using Anemonis.VisualStudio.LayoutManager.Data;

using Newtonsoft.Json;

#pragma warning disable CA1822

namespace Anemonis.VisualStudio.LayoutManager.Engine
{
    /// <summary>Represents the layout packages provider.</summary>
    public sealed class LayoutPackagesProvider
    {
        private static readonly Regex _packageNameRegex = new Regex("^(?<i>[^,]+),version=(?<v>[^,]+)(?:,chip=(?<c>[^,]+))?(?:,language=(?<l>[^,]+))?$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>Initializes a new instance of the <see cref="LayoutPackagesProvider" /> class.</summary>
        public LayoutPackagesProvider()
        {
        }

        /// <summary>Gets catalog packages from the installation layout.</summary>
        /// <param name="json">The layout manifest JSON content.</param>
        /// <returns>The packages collection.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="layoutPath" /> is <see langword="null" />.</exception>
        public IReadOnlyCollection<LayoutPackage> GetCatalogPackages(string json)
        {
            if (json == null)
            {
                throw new ArgumentNullException(nameof(json));
            }

            var catalog = JsonConvert.DeserializeObject<JsonLayoutCatalog>(json);
            var packages = new HashSet<LayoutPackage>(catalog.Packages.Length);

            for (var i = 0; i < catalog.Packages.Length; i++)
            {
                var package = new LayoutPackage(
                    catalog.Packages[i].Id,
                    catalog.Packages[i].Version,
                    catalog.Packages[i].Architecture,
                    catalog.Packages[i].Language
                );

                packages.Add(package);
            }

            return packages;
        }

        /// <summary>Gets actual packages from the installation layout.</summary>
        /// <param name="directories">The layout directories.</param>
        /// <returns>The packages collection.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="layoutPath" /> is <see langword="null" />.</exception>
        public IReadOnlyCollection<LayoutPackage> GetLocalPackages(IReadOnlyList<string> directories)
        {
            if (directories == null)
            {
                throw new ArgumentNullException(nameof(directories));
            }

            var packages = new HashSet<LayoutPackage>(directories.Count);

            for (var i = 0; i < directories.Count; i++)
            {
                var match = _packageNameRegex.Match(Path.GetFileName(directories[i]));

                if (match.Success)
                {
                    var package = new LayoutPackage(
                        match.Groups["i"].Value,
                        match.Groups["v"].Value,
                        match.Groups["c"].Success ? match.Groups["c"].Value : null,
                        match.Groups["l"].Success ? match.Groups["l"].Value : null
                    );

                    packages.Add(package);
                }
            }

            return packages;
        }

        /// <summary>Gets obsolete packages from the installation layout.</summary>
        /// <param name="catalogPackages">The catalog packages from the installation layout.</param>
        /// <param name="localPackages">The local packages from the installation layout.</param>
        /// <returns>The packages collection.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="catalogPackages" /> or <paramref name="localPackages" /> is <see langword="null" />.</exception>
        public IReadOnlyCollection<LayoutPackage> GetObsoletePackages(IReadOnlyCollection<LayoutPackage> catalogPackages, IReadOnlyCollection<LayoutPackage> localPackages)
        {
            if (catalogPackages == null)
            {
                throw new ArgumentNullException(nameof(catalogPackages));
            }
            if (localPackages == null)
            {
                throw new ArgumentNullException(nameof(localPackages));
            }

            return localPackages.Except(catalogPackages)
                .OrderBy(p => p.Id)
                .ThenBy(p => p.Version)
                .ThenBy(p => p.Architecture)
                .ThenBy(p => p.Language)
                .ToHashSet();
        }
    }
}
