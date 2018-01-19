using System;
using System.IO;

namespace Community.VisualStudio.LayoutManager.Engine
{
    /// <summary>Directory content information provider.</summary>
    internal static class DirectoryContentInfo
    {
        /// <summary>Gets directory size in bytes.</summary>
        /// <param name="path">The directory path.</param>
        /// <returns>Directory size in bytes.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="path" /> is <see langword="null" />.</exception>
        public static long GetSize(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            var files = new DirectoryInfo(path).GetFiles("*", SearchOption.AllDirectories);
            var result = 0L;

            for (var i = 0; i < files.Length; i++)
            {
                result += files[i].Length;
            }

            return result;
        }
    }
}