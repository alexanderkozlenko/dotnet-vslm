using System;
using System.Linq;
using Community.VisualStudio.LayoutManager.Engine;
using Community.VisualStudio.LayoutManager.Tests.Resources;
using Xunit;

namespace Community.VisualStudio.LayoutManager.Tests
{
    public sealed class LayoutPackagesProviderTests
    {
        [Fact]
        public void AcquireCatalogPackagesWhenJsonIsNull()
        {
            var provider = new LayoutPackagesProvider();

            Assert.Throws<ArgumentNullException>(() =>
                provider.AcquireCatalogPackages(null));
        }

        [Fact]
        public void AcquireLocalPackagesWhenDirectoriesIsNull()
        {
            var provider = new LayoutPackagesProvider();

            Assert.Throws<ArgumentNullException>(() =>
                provider.AcquireLocalPackages(null));
        }

        [Fact]
        public void AcquireCatalogPackages()
        {
            var json = EmbeddedResourceManager.GetString("Assets.Catalog.json");
            var provider = new LayoutPackagesProvider();

            var packages = provider.AcquireCatalogPackages(json);

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
        public void AcquireLocalPackages()
        {
            var directories = EmbeddedResourceManager.GetString("Assets.Catalog.log")
                .Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            var provider = new LayoutPackagesProvider();

            var packages = provider.AcquireLocalPackages(directories);

            Assert.NotNull(packages);
            Assert.Equal(2, packages.Count);

            var package = packages.First();

            Assert.NotNull(package);
            Assert.Equal("Anaconda2.Exe.x64", package.Id);
            Assert.Equal("5.0.0", package.Version);
            Assert.Equal("x64", package.Chip);
            Assert.Null(package.Language);
        }
    }
}