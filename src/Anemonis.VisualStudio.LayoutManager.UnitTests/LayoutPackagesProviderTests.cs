using System;
using System.Linq;

using Anemonis.VisualStudio.LayoutManager.Data;
using Anemonis.VisualStudio.LayoutManager.Engine;
using Anemonis.VisualStudio.LayoutManager.UnitTests.Resources;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Anemonis.VisualStudio.LayoutManager.UnitTests
{
    [TestClass]
    public sealed class LayoutPackagesProviderTests
    {
        [TestMethod]
        public void GetCatalogPackagesWhenJsonIsNull()
        {
            var provider = new LayoutPackagesProvider();

            Assert.ThrowsException<ArgumentNullException>(() =>
                provider.GetCatalogPackages(null));
        }

        [TestMethod]
        public void GetLocalPackagesWhenDirectoriesIsNull()
        {
            var provider = new LayoutPackagesProvider();

            Assert.ThrowsException<ArgumentNullException>(() =>
                provider.GetLocalPackages(null));
        }

        [TestMethod]
        public void GetCatalogPackages()
        {
            var json = EmbeddedResourceManager.GetString("Assets.Catalog.json");
            var provider = new LayoutPackagesProvider();

            var packages = provider.GetCatalogPackages(json);

            Assert.IsNotNull(packages);
            Assert.AreEqual(1, packages.Count);

            var package = packages.First();

            Assert.IsNotNull(package);
            Assert.AreEqual("Microsoft.Build", package.Id);
            Assert.AreEqual("16.2.37902.1937902", package.Version);
            Assert.AreEqual("neutral", package.Architecture);
            Assert.AreEqual("neutral", package.Language);
        }

        [TestMethod]
        public void GetLocalPackages()
        {
            var directories = EmbeddedResourceManager.GetString("Assets.Catalog.log")
                .Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            var provider = new LayoutPackagesProvider();

            var packages = provider.GetLocalPackages(directories);

            Assert.IsNotNull(packages);
            Assert.AreEqual(1, packages.Count);

            var package = packages.First();

            Assert.IsNotNull(package);
            Assert.AreEqual("Microsoft.Build", package.Id);
            Assert.AreEqual("16.2.37902.1937902", package.Version);
            Assert.AreEqual("neutral", package.Architecture);
            Assert.AreEqual("neutral", package.Language);
        }

        [TestMethod]
        public void GetObsoletePackagesWhenCatalogPackagesIsNull()
        {
            var provider = new LayoutPackagesProvider();
            var localPackages = new LayoutPackage[] { };

            Assert.ThrowsException<ArgumentNullException>(() =>
                provider.GetObsoletePackages(null, localPackages));
        }

        [TestMethod]
        public void GetObsoletePackagesWhenLocalPackagesIsNull()
        {
            var provider = new LayoutPackagesProvider();
            var catalogPackages = new LayoutPackage[] { };

            Assert.ThrowsException<ArgumentNullException>(() =>
                provider.GetObsoletePackages(catalogPackages, null));
        }

        [TestMethod]
        public void GetObsoletePackages()
        {
            var provider = new LayoutPackagesProvider();

            var catalogPackages = new[]
            {
                new LayoutPackage("package_1", "2.0.0", null, null),
                new LayoutPackage("package_2", "2.0.0", null, null)
            };
            var localPackages = new[]
            {
                new LayoutPackage("package_1", "1.0.0", null, null),
                new LayoutPackage("package_1", "2.0.0", null, null),
                new LayoutPackage("package_2", "1.0.0", null, null),
                new LayoutPackage("package_2", "2.0.0", null, null)
            };

            var obsoletePackages = provider.GetObsoletePackages(catalogPackages, localPackages);

            Assert.IsNotNull(obsoletePackages);
            Assert.AreEqual(2, obsoletePackages.Count);
            Assert.AreEqual(new LayoutPackage("package_1", "1.0.0", null, null), obsoletePackages.Skip(0).First());
            Assert.AreEqual(new LayoutPackage("package_2", "1.0.0", null, null), obsoletePackages.Skip(1).First());
        }
    }
}
