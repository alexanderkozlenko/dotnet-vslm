using System;
using Community.VisualStudio.LayoutManager.Data;
using Xunit;

namespace Community.VisualStudio.LayoutManager.Tests
{
    public sealed class LayoutPackageTests
    {
        [Fact]
        public void ConstructorWhenIdIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new LayoutPackage(null, "1.0.0", null, null));
        }

        [Fact]
        public void ConstructorWhenVersionIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new LayoutPackage("package", null, null, null));
        }

        [Fact]
        public void ObjectToString()
        {
            Assert.Equal("package,version=1.0.0",
               new LayoutPackage("package", "1.0.0", null, null)
                   .ToString());
            Assert.Equal("package,version=1.0.0,chip=neutral",
                new LayoutPackage("package", "1.0.0", "neutral", null)
                    .ToString());
            Assert.Equal("package,version=1.0.0,language=en-US",
                new LayoutPackage("package", "1.0.0", null, "en-US")
                    .ToString());
            Assert.Equal("package,version=1.0.0,chip=neutral,language=en-US",
                new LayoutPackage("package", "1.0.0", "neutral", "en-US")
                    .ToString());
        }

        [Fact]
        public void ObjectGetHashCode()
        {
            Assert.Equal(
                new LayoutPackage("package", "1.0.0", null, null)
                    .GetHashCode(),
                new LayoutPackage("Package", "1.0.0", null, null)
                    .GetHashCode());
            Assert.NotEqual(
                new LayoutPackage("package", "1.0.0", null, null)
                    .GetHashCode(),
                new LayoutPackage("package", "1.0.0", "neutral", null)
                    .GetHashCode());
            Assert.NotEqual(
                new LayoutPackage("package", "1.0.0", null, null)
                    .GetHashCode(),
                new LayoutPackage("package", "1.0.0", null, "en-US")
                    .GetHashCode());
            Assert.NotEqual(
                new LayoutPackage("package", "1.0.0", null, null)
                    .GetHashCode(),
                new LayoutPackage("package", "1.0.0", "neutral", "en-US")
                    .GetHashCode());
            Assert.NotEqual(
                new LayoutPackage("package", "1.0.0", "neutral", null)
                    .GetHashCode(),
                new LayoutPackage("package", "1.0.0", null, "en-US")
                    .GetHashCode());
            Assert.NotEqual(
                new LayoutPackage("package", "1.0.0", "neutral", null)
                    .GetHashCode(),
                new LayoutPackage("package", "1.0.0", "neutral", "en-US")
                    .GetHashCode());
            Assert.NotEqual(
                new LayoutPackage("package", "1.0.0", null, "en-US")
                    .GetHashCode(),
                new LayoutPackage("package", "1.0.0", "neutral", "en-US")
                    .GetHashCode());
        }

        [Fact]
        public void ObjectEquals()
        {
            Assert.False(
                new LayoutPackage("package", "1.0.0", null, null)
                    .Equals(
                (object)null));
            Assert.True(
                new LayoutPackage("package", "1.0.0", null, null)
                    .Equals(
                (object)new LayoutPackage("Package", "1.0.0", null, null)));
            Assert.False(
                new LayoutPackage("package", "1.0.0", null, null)
                    .Equals(
                (object)new LayoutPackage("package", "1.0.0", "neutral", null)));
            Assert.False(
                new LayoutPackage("package", "1.0.0", null, null)
                    .Equals(
                (object)new LayoutPackage("package", "1.0.0", null, "en-US")));
            Assert.False(
                new LayoutPackage("package", "1.0.0", null, null)
                    .Equals(
                (object)new LayoutPackage("package", "1.0.0", "neutral", "en-US")));
            Assert.False(
                new LayoutPackage("package", "1.0.0", "neutral", null)
                    .Equals(
                (object)new LayoutPackage("package", "1.0.0", null, "en-US")));
            Assert.False(
                new LayoutPackage("package", "1.0.0", "neutral", null)
                    .Equals(
                (object)new LayoutPackage("package", "1.0.0", "neutral", "en-US")));
            Assert.False(
                new LayoutPackage("package", "1.0.0", null, "en-US")
                    .Equals(
                (object)new LayoutPackage("package", "1.0.0", "neutral", "en-US")));
        }

        [Fact]
        public void EquatableEquals()
        {
            Assert.True(
                new LayoutPackage("package", "1.0.0", null, null)
                    .Equals(
                new LayoutPackage("Package", "1.0.0", null, null)));
            Assert.False(
                new LayoutPackage("package", "1.0.0", null, null)
                    .Equals(
                new LayoutPackage("package", "1.0.0", "neutral", null)));
            Assert.False(
                new LayoutPackage("package", "1.0.0", null, null)
                    .Equals(
                new LayoutPackage("package", "1.0.0", null, "en-US")));
            Assert.False(
                new LayoutPackage("package", "1.0.0", null, null)
                    .Equals(
                new LayoutPackage("package", "1.0.0", "neutral", "en-US")));
            Assert.False(
                new LayoutPackage("package", "1.0.0", "neutral", null)
                    .Equals(
                new LayoutPackage("package", "1.0.0", null, "en-US")));
            Assert.False(
                new LayoutPackage("package", "1.0.0", "neutral", null)
                    .Equals(
                new LayoutPackage("package", "1.0.0", "neutral", "en-US")));
            Assert.False(
                new LayoutPackage("package", "1.0.0", null, "en-US")
                    .Equals(
                new LayoutPackage("package", "1.0.0", "neutral", "en-US")));
        }
    }
}