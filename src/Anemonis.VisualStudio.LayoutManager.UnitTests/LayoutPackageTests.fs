// © Alexander Kozlenko. Licensed under the MIT License.

namespace Anemonis.VisualStudio.LayoutManager.UnitTests

open System

open Anemonis.VisualStudio.LayoutManager.Data

open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
[<Sealed>]
type public LayoutPackageTests() =
    class
        [<TestMethod>]
        member public this.ConstructorWhenIdIsNull() =
            Assert.ThrowsException<ArgumentNullException>(fun () ->
                new LayoutPackage(null, "1.0.0", null, null) :> Object) |> ignore
            ()

        [<TestMethod>]
        member public this.ConstructorWhenVersionIsNull() =
            Assert.ThrowsException<ArgumentNullException>(fun () ->
                new LayoutPackage("package", null, null, null) :> Object) |> ignore
            ()

        [<TestMethod>]
        member public this.ObjectToString() =
            Assert.AreEqual("package,version=1.0.0",
                (new LayoutPackage("package", "1.0.0", null, null)).ToString()) |> ignore
            Assert.AreEqual("package,version=1.0.0,chip=neutral",
                (new LayoutPackage("package", "1.0.0", "neutral", null)).ToString()) |> ignore
            Assert.AreEqual("package,version=1.0.0,language=en-US",
                (new LayoutPackage("package", "1.0.0", null, "en-US")).ToString()) |> ignore
            Assert.AreEqual("package,version=1.0.0,chip=neutral,language=en-US",
                (new LayoutPackage("package", "1.0.0", "neutral", "en-US")).ToString()) |> ignore
            ()

        [<TestMethod>]
        member public this.ObjectGetHashCode() =
            Assert.AreEqual(
                (new LayoutPackage("package", "1.0.0", null, null))
                    .GetHashCode(),
                (new LayoutPackage("Package", "1.0.0", null, null))
                    .GetHashCode()) |> ignore
            Assert.AreNotEqual(
                (new LayoutPackage("package", "1.0.0", null, null))
                    .GetHashCode(),
                (new LayoutPackage("package", "1.0.0", "neutral", null))
                    .GetHashCode()) |> ignore
            Assert.AreNotEqual(
                (new LayoutPackage("package", "1.0.0", null, null))
                    .GetHashCode(),
                (new LayoutPackage("package", "1.0.0", null, "en-US"))
                    .GetHashCode()) |> ignore
            Assert.AreNotEqual(
                (new LayoutPackage("package", "1.0.0", null, null))
                    .GetHashCode(),
                (new LayoutPackage("package", "1.0.0", "neutral", "en-US"))
                    .GetHashCode()) |> ignore
            Assert.AreNotEqual(
                (new LayoutPackage("package", "1.0.0", "neutral", null))
                    .GetHashCode(),
                (new LayoutPackage("package", "1.0.0", null, "en-US"))
                    .GetHashCode()) |> ignore
            Assert.AreNotEqual(
                (new LayoutPackage("package", "1.0.0", "neutral", null))
                    .GetHashCode(),
                (new LayoutPackage("package", "1.0.0", "neutral", "en-US"))
                    .GetHashCode()) |> ignore
            Assert.AreNotEqual(
                (new LayoutPackage("package", "1.0.0", null, "en-US"))
                    .GetHashCode(),
                (new LayoutPackage("package", "1.0.0", "neutral", "en-US"))
                    .GetHashCode()) |> ignore
            ()

        [<TestMethod>]
        member public this.ObjectEquals() =
            Assert.IsFalse(
                (new LayoutPackage("package", "1.0.0", null, null))
                    .Equals(
                        (null) :> Object)) |> ignore
            Assert.IsTrue(
                (new LayoutPackage("package", "1.0.0", null, null))
                    .Equals(
                        (new LayoutPackage("Package", "1.0.0", null, null)) :> Object)) |> ignore
            Assert.IsFalse(
                (new LayoutPackage("package", "1.0.0", null, null))
                    .Equals(
                        (new LayoutPackage("package", "1.0.0", "neutral", null)) :> Object)) |> ignore
            Assert.IsFalse(
                (new LayoutPackage("package", "1.0.0", null, null))
                    .Equals(
                        (new LayoutPackage("package", "1.0.0", null, "en-US")) :> Object)) |> ignore
            Assert.IsFalse(
                (new LayoutPackage("package", "1.0.0", null, null))
                    .Equals(
                        (new LayoutPackage("package", "1.0.0", "neutral", "en-US")) :> Object)) |> ignore
            Assert.IsFalse(
                (new LayoutPackage("package", "1.0.0", "neutral", null))
                    .Equals(
                        (new LayoutPackage("package", "1.0.0", null, "en-US")) :> Object)) |> ignore
            Assert.IsFalse(
                (new LayoutPackage("package", "1.0.0", "neutral", null))
                    .Equals(
                        (new LayoutPackage("package", "1.0.0", "neutral", "en-US")) :> Object)) |> ignore
            Assert.IsFalse(
                (new LayoutPackage("package", "1.0.0", null, "en-US"))
                    .Equals(
                        (new LayoutPackage("package", "1.0.0", "neutral", "en-US")) :> Object)) |> ignore
            ()

        [<TestMethod>]
        member public this.EquatableEquals() =
            Assert.IsTrue(
                (new LayoutPackage("package", "1.0.0", null, null))
                    .Equals(
                        (new LayoutPackage("Package", "1.0.0", null, null)))) |> ignore
            Assert.IsFalse(
                (new LayoutPackage("package", "1.0.0", null, null))
                    .Equals(
                        (new LayoutPackage("package", "1.0.0", "neutral", null)))) |> ignore
            Assert.IsFalse(
                (new LayoutPackage("package", "1.0.0", null, null))
                    .Equals(
                        (new LayoutPackage("package", "1.0.0", null, "en-US")))) |> ignore
            Assert.IsFalse(
                (new LayoutPackage("package", "1.0.0", null, null))
                    .Equals(
                        (new LayoutPackage("package", "1.0.0", "neutral", "en-US")))) |> ignore
            Assert.IsFalse(
                (new LayoutPackage("package", "1.0.0", "neutral", null))
                    .Equals(
                        (new LayoutPackage("package", "1.0.0", null, "en-US")))) |> ignore
            Assert.IsFalse(
                (new LayoutPackage("package", "1.0.0", "neutral", null))
                    .Equals(
                        (new LayoutPackage("package", "1.0.0", "neutral", "en-US")))) |> ignore
            Assert.IsFalse(
                (new LayoutPackage("package", "1.0.0", null, "en-US"))
                    .Equals(
                        (new LayoutPackage("package", "1.0.0", "neutral", "en-US")))) |> ignore
            ()
    end
