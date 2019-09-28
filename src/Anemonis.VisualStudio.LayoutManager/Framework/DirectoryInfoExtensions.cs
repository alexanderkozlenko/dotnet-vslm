// © Alexander Kozlenko. Licensed under the MIT License.

namespace System.IO
{
    /// <summary>Defines extension methods for the <see cref="DirectoryInfo" /> type.</summary>
    public static class DirectoryInfoExtensions
    {
        /// <summary>Gets directory size in bytes.</summary>
        /// <param name="directoryInfo">The <see cref="DirectoryInfo" /> instance to get the size for.</param>
        /// <returns>Directory size in bytes.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="directoryInfo" /> is <see langword="null" />.</exception>
        public static long GetSize(this DirectoryInfo directoryInfo)
        {
            if (directoryInfo == null)
            {
                throw new ArgumentNullException(nameof(directoryInfo));
            }

            var files = directoryInfo.GetFiles("*", SearchOption.AllDirectories);
            var result = 0L;

            for (var i = 0; i < files.Length; i++)
            {
                result += files[i].Length;
            }

            return result;
        }
    }
}
