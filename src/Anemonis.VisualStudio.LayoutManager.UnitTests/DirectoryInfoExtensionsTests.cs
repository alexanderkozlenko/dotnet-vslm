using System;
using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Anemonis.VisualStudio.LayoutManager.UnitTests
{
    [TestClass]
    public sealed class DirectoryInfoExtensionsTests
    {
        [TestMethod]
        public void GetSizeWhenDirectoryInfoIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                DirectoryInfoExtensions.GetSize(null));
        }
    }
}
