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
                new LayoutPackage(null, "15.5.180.51428", null, null));
        }

        [Fact]
        public void ConstructorWhenVersionIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new LayoutPackage("Microsoft.Build", null, null, null));
        }

        [Fact]
        public void ObjectToString()
        {
            Assert.Equal("Microsoft.Build-15.5.180.51428",
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", null, null)
                    .ToString());
            Assert.Equal("Microsoft.Build-15.5.180.51428-neutral",
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", "neutral", null)
                    .ToString());
            Assert.Equal("Microsoft.Build-15.5.180.51428-en-US",
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", null, "en-US")
                    .ToString());
            Assert.Equal("Microsoft.Build-15.5.180.51428-neutral-en-US",
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", "neutral", "en-US")
                    .ToString());
        }

        [Fact]
        public void ObjectGetHashCode()
        {
            Assert.Equal(
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", null, null)
                    .GetHashCode(),
                new LayoutPackage("microsoft.build", "15.5.180.51428", null, null)
                    .GetHashCode());
            Assert.NotEqual(
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", null, null)
                    .GetHashCode(),
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", "neutral", null)
                    .GetHashCode());
            Assert.NotEqual(
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", null, null)
                    .GetHashCode(),
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", null, "en-US")
                    .GetHashCode());
            Assert.NotEqual(
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", null, null)
                    .GetHashCode(),
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", "neutral", "en-US")
                    .GetHashCode());
            Assert.NotEqual(
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", "neutral", null)
                    .GetHashCode(),
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", null, "en-US")
                    .GetHashCode());
            Assert.NotEqual(
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", "neutral", null)
                    .GetHashCode(),
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", "neutral", "en-US")
                    .GetHashCode());
            Assert.NotEqual(
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", null, "en-US")
                    .GetHashCode(),
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", "neutral", "en-US")
                    .GetHashCode());
        }

        [Fact]
        public void ObjectEquals()
        {
            Assert.False(
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", null, null)
                    .Equals(
                (object)null));
            Assert.True(
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", null, null)
                    .Equals(
                (object)new LayoutPackage("microsoft.build", "15.5.180.51428", null, null)));
            Assert.False(
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", null, null)
                    .Equals(
                (object)new LayoutPackage("Microsoft.Build", "15.5.180.51428", "neutral", null)));
            Assert.False(
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", null, null)
                    .Equals(
                (object)new LayoutPackage("Microsoft.Build", "15.5.180.51428", null, "en-US")));
            Assert.False(
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", null, null)
                    .Equals(
                (object)new LayoutPackage("Microsoft.Build", "15.5.180.51428", "neutral", "en-US")));
            Assert.False(
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", "neutral", null)
                    .Equals(
                (object)new LayoutPackage("Microsoft.Build", "15.5.180.51428", null, "en-US")));
            Assert.False(
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", "neutral", null)
                    .Equals(
                (object)new LayoutPackage("Microsoft.Build", "15.5.180.51428", "neutral", "en-US")));
            Assert.False(
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", null, "en-US")
                    .Equals(
                (object)new LayoutPackage("Microsoft.Build", "15.5.180.51428", "neutral", "en-US")));
        }

        [Fact]
        public void EquatableEquals()
        {
            Assert.True(
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", null, null)
                    .Equals(
                new LayoutPackage("microsoft.build", "15.5.180.51428", null, null)));
            Assert.False(
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", null, null)
                    .Equals(
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", "neutral", null)));
            Assert.False(
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", null, null)
                    .Equals(
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", null, "en-US")));
            Assert.False(
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", null, null)
                    .Equals(
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", "neutral", "en-US")));
            Assert.False(
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", "neutral", null)
                    .Equals(
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", null, "en-US")));
            Assert.False(
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", "neutral", null)
                    .Equals(
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", "neutral", "en-US")));
            Assert.False(
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", null, "en-US")
                    .Equals(
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", "neutral", "en-US")));
        }

        [Fact]
        public void GetDirectoryName()
        {
            Assert.Equal("Microsoft.Build,version=15.5.180.51428",
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", null, null)
                    .GetDirectoryName());
            Assert.Equal("Microsoft.Build,version=15.5.180.51428,chip=neutral",
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", "neutral", null)
                    .GetDirectoryName());
            Assert.Equal("Microsoft.Build,version=15.5.180.51428,language=en-US",
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", null, "en-US")
                    .GetDirectoryName());
            Assert.Equal("Microsoft.Build,version=15.5.180.51428,chip=neutral,language=en-US",
                new LayoutPackage("Microsoft.Build", "15.5.180.51428", "neutral", "en-US")
                    .GetDirectoryName());
        }
    }
}