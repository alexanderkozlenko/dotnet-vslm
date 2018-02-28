using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Community.VisualStudio.LayoutManager.Data;
using Newtonsoft.Json;

namespace Community.VisualStudio.LayoutManager.Engine
{
    /// <summary>Represents the layout packages provider.</summary>
    public sealed class LayoutPackagesProvider
    {
        private static readonly Regex _packageNameRegex = new Regex("^(?<i>[^,]+),version=(?<v>[^,]+)(?:,chip=(?<c>[^,]+))?(?:,language=(?<l>[^,]+))?$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>Initializes a new instance of the <see cref="LayoutPackagesProvider" /> class.</summary>
        public LayoutPackagesProvider()
        {
        }

        /// <summary>Acquires defined packages collection from the installation layout.</summary>
        /// <param name="json">The layout manifest JSON content.</param>
        /// <returns>The layout packages collection.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="layoutPath" /> is <see langword="null" />.</exception>
        public IReadOnlyList<LayoutPackage> AcquireCatalogPackages(string json)
        {
            if (json == null)
            {
                throw new ArgumentNullException(nameof(json));
            }

            var catalog = JsonConvert.DeserializeObject<JsonLayoutCatalog>(json);
            var packages = new LayoutPackage[catalog.Packages.Length];

            for (var i = 0; i < packages.Length; i++)
            {
                var jsonPackage = catalog.Packages[i];

                packages[i] = new LayoutPackage(jsonPackage.Id, jsonPackage.Version, jsonPackage.Chip, jsonPackage.Language);
            }

            return packages;
        }

        /// <summary>Acquires actual packages collection from the installation layout.</summary>
        /// <param name="directories">The layout directories.</param>
        /// <returns>The layout packages collection.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="layoutPath" /> is <see langword="null" />.</exception>
        public IReadOnlyList<LayoutPackage> AcquireLocalPackages(IReadOnlyList<string> directories)
        {
            if (directories == null)
            {
                throw new ArgumentNullException(nameof(directories));
            }

            var packages = new List<LayoutPackage>(directories.Count);

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
    }
}