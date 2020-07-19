// © Alexander Kozlenko. Licensed under the MIT License.

namespace Anemonis.VisualStudio.LayoutManager.UnitTests

open System
open System.Linq

open Anemonis.Resources
open Anemonis.VisualStudio.LayoutManager.Data
open Anemonis.VisualStudio.LayoutManager.Engine

open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
[<Sealed>]
type public LayoutPackagesProviderTests() =
    class
        [<TestMethod>]
        member public this.GetCatalogPackagesWhenJsonIsNull() =
            let provider = new LayoutPackagesProvider()

            Assert.ThrowsException<ArgumentNullException>(fun () ->
                provider.GetCatalogPackages(null) :> Object) |> ignore
            ()

        [<TestMethod>]
        member public this.GetLocalPackagesWhenDirectoriesIsNull() =
            let provider = new LayoutPackagesProvider()

            Assert.ThrowsException<ArgumentNullException>(fun () ->
                provider.GetLocalPackages(null) :> Object) |> ignore
            ()

        [<TestMethod>]
        member public this.GetCatalogPackages() =
            let json =
                EmbeddedResourceManager.GetString("Assets.Catalog.json")

            let provider = new LayoutPackagesProvider()
            let packages = provider.GetCatalogPackages(json)

            Assert.IsNotNull(packages)
            Assert.AreEqual(1, packages.Count)

            let package = packages.First()

            Assert.IsNotNull(package)
            Assert.AreEqual("Microsoft.Build", package.ID)
            Assert.AreEqual("16.2.37902.1937902", package.Version)
            Assert.AreEqual("neutral", package.Architecture)
            Assert.AreEqual("neutral", package.Language)
            ()

        [<TestMethod>]
        member public this.GetLocalPackages() =
            let directories =
                EmbeddedResourceManager.GetString("Assets.Catalog.log")
                    .Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)

            let provider = new LayoutPackagesProvider()
            let packages = provider.GetLocalPackages(directories)

            Assert.IsNotNull(packages)
            Assert.AreEqual(1, packages.Count)

            let package = packages.First()

            Assert.IsNotNull(package)
            Assert.AreEqual("Microsoft.Build", package.ID)
            Assert.AreEqual("16.2.37902.1937902", package.Version)
            Assert.AreEqual("neutral", package.Architecture)
            Assert.AreEqual("neutral", package.Language)
            ()

        [<TestMethod>]
        member public this.GetObsoletePackagesWhenCatalogPackagesIsNull() =
            let provider = new LayoutPackagesProvider()
            let localPackages : LayoutPackage[] = [| |]

            Assert.ThrowsException<ArgumentNullException>(fun () ->
                provider.GetObsoletePackages(null, localPackages) :> Object) |> ignore
            ()

        [<TestMethod>]
        member public this.GetObsoletePackagesWhenLocalPackagesIsNull() =
            let provider = new LayoutPackagesProvider()
            let catalogPackages : LayoutPackage[] = [| |]

            Assert.ThrowsException<ArgumentNullException>(fun () ->
                provider.GetObsoletePackages(catalogPackages, null) :> Object) |> ignore
            ()

        [<TestMethod>]
        member public this.GetObsoletePackages() =
            let provider = new LayoutPackagesProvider()

            let catalogPackages =
                [|
                    new LayoutPackage("package_1", "2.0.0", null, null);
                    new LayoutPackage("package_2", "2.0.0", null, null);
                |]

            let localPackages =
                [|
                    new LayoutPackage("package_1", "1.0.0", null, null);
                    new LayoutPackage("package_1", "2.0.0", null, null);
                    new LayoutPackage("package_2", "1.0.0", null, null);
                    new LayoutPackage("package_2", "2.0.0", null, null);
                |]

            let obsoletePackages = provider.GetObsoletePackages(catalogPackages, localPackages)

            Assert.IsNotNull(obsoletePackages)
            Assert.AreEqual(2, obsoletePackages.Count)
            Assert.AreEqual(new LayoutPackage("package_1", "1.0.0", null, null), obsoletePackages.Skip(0).First())
            Assert.AreEqual(new LayoutPackage("package_2", "1.0.0", null, null), obsoletePackages.Skip(1).First())
            ()

    end
