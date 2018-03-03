using System;
using System.Collections.Generic;
using System.Linq;
using Community.VisualStudio.LayoutManager.Data;
using Community.VisualStudio.LayoutManager.Engine;
using Community.VisualStudio.LayoutManager.Tests.Resources;
using Xunit;

namespace Community.VisualStudio.LayoutManager.Tests
{
    public sealed class LayoutPackagesProviderTests
    {
        [Fact]
        public void GetCatalogPackagesWhenJsonIsNull()
        {
            var provider = new LayoutPackagesProvider();

            Assert.Throws<ArgumentNullException>(() =>
                provider.GetCatalogPackages(null));
        }

        [Fact]
        public void GetLocalPackagesWhenDirectoriesIsNull()
        {
            var provider = new LayoutPackagesProvider();

            Assert.Throws<ArgumentNullException>(() =>
                provider.GetLocalPackages(null));
        }

        [Fact]
        public void GetCatalogPackages()
        {
            var json = EmbeddedResourceManager.GetString("Assets.Catalog.json");
            var provider = new LayoutPackagesProvider();

            var packages = provider.GetCatalogPackages(json);

            Assert.NotNull(packages);
            Assert.Equal(2, packages.Count);

            var package = packages.First();

            Assert.NotNull(package);
            Assert.Equal("Anaconda2.Exe.x64", package.Id);
            Assert.Equal("5.0.0", package.Version);
            Assert.Equal("x64", package.Chip);
            Assert.Null(package.Language);
        }

        [Fact]
        public void GetLocalPackages()
        {
            var directories = EmbeddedResourceManager.GetString("Assets.Catalog.log")
                .Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            var provider = new LayoutPackagesProvider();

            var packages = provider.GetLocalPackages(directories);

            Assert.NotNull(packages);
            Assert.Equal(2, packages.Count);

            var package = packages.First();

            Assert.NotNull(package);
            Assert.Equal("Anaconda2.Exe.x64", package.Id);
            Assert.Equal("5.0.0", package.Version);
            Assert.Equal("x64", package.Chip);
            Assert.Null(package.Language);
        }

        [Fact]
        public void GetObsoletePackagesWhenCatalogPackagesIsNull()
        {
            var provider = new LayoutPackagesProvider();
            var localPackages = new HashSet<LayoutPackage> { };

            Assert.Throws<ArgumentNullException>(() =>
                provider.GetObsoletePackages(null, localPackages));
        }

        [Fact]
        public void GetObsoletePackagesWhenLocalPackagesIsNull()
        {
            var provider = new LayoutPackagesProvider();
            var catalogPackages = new HashSet<LayoutPackage> { };

            Assert.Throws<ArgumentNullException>(() =>
                provider.GetObsoletePackages(catalogPackages, null));
        }

        [Fact]
        public void GetObsoletePackages()
        {
            var provider = new LayoutPackagesProvider();

            var catalogPackages = new HashSet<LayoutPackage>
            {
                new LayoutPackage("package_1", "2.0.0", null, null),
                new LayoutPackage("package_2", "2.0.0", null, null)
            };
            var localPackages = new HashSet<LayoutPackage>
            {
                new LayoutPackage("package_1", "1.0.0", null, null),
                new LayoutPackage("package_1", "2.0.0", null, null),
                new LayoutPackage("package_2", "1.0.0", null, null),
                new LayoutPackage("package_2", "2.0.0", null, null)
            };

            var obsoletePackages = provider.GetObsoletePackages(catalogPackages, localPackages);

            Assert.NotNull(obsoletePackages);
            Assert.Equal(2, obsoletePackages.Count);
            Assert.Equal(new LayoutPackage("package_1", "1.0.0", null, null), obsoletePackages.Skip(0).First());
            Assert.Equal(new LayoutPackage("package_2", "1.0.0", null, null), obsoletePackages.Skip(1).First());
        }
    }
}