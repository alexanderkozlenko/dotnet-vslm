using System;
using Community.VisualStudio.LayoutManager.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Community.VisualStudio.LayoutManager.UnitTests
{
    [TestClass]
    public sealed class LayoutPackageTests
    {
        [TestMethod]
        public void ConstructorWhenIdIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                new LayoutPackage(null, "1.0.0", null, null));
        }

        [TestMethod]
        public void ConstructorWhenVersionIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                new LayoutPackage("package", null, null, null));
        }

        [TestMethod]
        public void ObjectToString()
        {
            Assert.AreEqual("package,version=1.0.0",
               new LayoutPackage("package", "1.0.0", null, null)
                   .ToString());
            Assert.AreEqual("package,version=1.0.0,chip=neutral",
                new LayoutPackage("package", "1.0.0", "neutral", null)
                    .ToString());
            Assert.AreEqual("package,version=1.0.0,language=en-US",
                new LayoutPackage("package", "1.0.0", null, "en-US")
                    .ToString());
            Assert.AreEqual("package,version=1.0.0,chip=neutral,language=en-US",
                new LayoutPackage("package", "1.0.0", "neutral", "en-US")
                    .ToString());
        }

        [TestMethod]
        public void ObjectGetHashCode()
        {
            Assert.AreEqual(
                new LayoutPackage("package", "1.0.0", null, null)
                    .GetHashCode(),
                new LayoutPackage("Package", "1.0.0", null, null)
                    .GetHashCode());
            Assert.AreNotEqual(
                new LayoutPackage("package", "1.0.0", null, null)
                    .GetHashCode(),
                new LayoutPackage("package", "1.0.0", "neutral", null)
                    .GetHashCode());
            Assert.AreNotEqual(
                new LayoutPackage("package", "1.0.0", null, null)
                    .GetHashCode(),
                new LayoutPackage("package", "1.0.0", null, "en-US")
                    .GetHashCode());
            Assert.AreNotEqual(
                new LayoutPackage("package", "1.0.0", null, null)
                    .GetHashCode(),
                new LayoutPackage("package", "1.0.0", "neutral", "en-US")
                    .GetHashCode());
            Assert.AreNotEqual(
                new LayoutPackage("package", "1.0.0", "neutral", null)
                    .GetHashCode(),
                new LayoutPackage("package", "1.0.0", null, "en-US")
                    .GetHashCode());
            Assert.AreNotEqual(
                new LayoutPackage("package", "1.0.0", "neutral", null)
                    .GetHashCode(),
                new LayoutPackage("package", "1.0.0", "neutral", "en-US")
                    .GetHashCode());
            Assert.AreNotEqual(
                new LayoutPackage("package", "1.0.0", null, "en-US")
                    .GetHashCode(),
                new LayoutPackage("package", "1.0.0", "neutral", "en-US")
                    .GetHashCode());
        }

        [TestMethod]
        public void ObjectEquals()
        {
            Assert.IsFalse(
                new LayoutPackage("package", "1.0.0", null, null)
                    .Equals(
                (object)null));
            Assert.IsTrue(
                new LayoutPackage("package", "1.0.0", null, null)
                    .Equals(
                (object)new LayoutPackage("Package", "1.0.0", null, null)));
            Assert.IsFalse(
                new LayoutPackage("package", "1.0.0", null, null)
                    .Equals(
                (object)new LayoutPackage("package", "1.0.0", "neutral", null)));
            Assert.IsFalse(
                new LayoutPackage("package", "1.0.0", null, null)
                    .Equals(
                (object)new LayoutPackage("package", "1.0.0", null, "en-US")));
            Assert.IsFalse(
                new LayoutPackage("package", "1.0.0", null, null)
                    .Equals(
                (object)new LayoutPackage("package", "1.0.0", "neutral", "en-US")));
            Assert.IsFalse(
                new LayoutPackage("package", "1.0.0", "neutral", null)
                    .Equals(
                (object)new LayoutPackage("package", "1.0.0", null, "en-US")));
            Assert.IsFalse(
                new LayoutPackage("package", "1.0.0", "neutral", null)
                    .Equals(
                (object)new LayoutPackage("package", "1.0.0", "neutral", "en-US")));
            Assert.IsFalse(
                new LayoutPackage("package", "1.0.0", null, "en-US")
                    .Equals(
                (object)new LayoutPackage("package", "1.0.0", "neutral", "en-US")));
        }

        [TestMethod]
        public void EquatableEquals()
        {
            Assert.IsTrue(
                new LayoutPackage("package", "1.0.0", null, null)
                    .Equals(
                new LayoutPackage("Package", "1.0.0", null, null)));
            Assert.IsFalse(
                new LayoutPackage("package", "1.0.0", null, null)
                    .Equals(
                new LayoutPackage("package", "1.0.0", "neutral", null)));
            Assert.IsFalse(
                new LayoutPackage("package", "1.0.0", null, null)
                    .Equals(
                new LayoutPackage("package", "1.0.0", null, "en-US")));
            Assert.IsFalse(
                new LayoutPackage("package", "1.0.0", null, null)
                    .Equals(
                new LayoutPackage("package", "1.0.0", "neutral", "en-US")));
            Assert.IsFalse(
                new LayoutPackage("package", "1.0.0", "neutral", null)
                    .Equals(
                new LayoutPackage("package", "1.0.0", null, "en-US")));
            Assert.IsFalse(
                new LayoutPackage("package", "1.0.0", "neutral", null)
                    .Equals(
                new LayoutPackage("package", "1.0.0", "neutral", "en-US")));
            Assert.IsFalse(
                new LayoutPackage("package", "1.0.0", null, "en-US")
                    .Equals(
                new LayoutPackage("package", "1.0.0", "neutral", "en-US")));
        }
    }
}