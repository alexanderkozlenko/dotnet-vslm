using System;
using System.Collections.Generic;
using Community.VisualStudio.LayoutManager.Data;

namespace Community.VisualStudio.LayoutManager.Engine
{
    /// <summary>Catalog package equality comparer.</summary>
    internal sealed class CatalogPackageComparer : IEqualityComparer<CatalogPackageInfo>
    {
        private static readonly CatalogPackageComparer _default = new CatalogPackageComparer();

        /// <summary>Initializes a new instance of the <see cref="CatalogPackageComparer" /> class.</summary>
        public CatalogPackageComparer()
        {
        }

        bool IEqualityComparer<CatalogPackageInfo>.Equals(CatalogPackageInfo item1, CatalogPackageInfo item2)
        {
            if (item1 == null)
            {
                throw new ArgumentNullException(nameof(item1));
            }
            if (item2 == null)
            {
                throw new ArgumentNullException(nameof(item2));
            }

            return
                (string.Compare(item1.ID, item2.ID, StringComparison.InvariantCultureIgnoreCase) == 0) &&
                (string.Compare(item1.Version, item2.Version, StringComparison.InvariantCultureIgnoreCase) == 0) &&
                (string.Compare(item1.Chip, item2.Chip, StringComparison.InvariantCultureIgnoreCase) == 0) &&
                (string.Compare(item1.Language, item2.Language, StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        int IEqualityComparer<CatalogPackageInfo>.GetHashCode(CatalogPackageInfo item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            unchecked
            {
                var result = (int)2166136261;

                result = (result * 16777619) ^ (item.ID?.ToLowerInvariant()?.GetHashCode() ?? 0);
                result = (result * 16777619) ^ (item.Version?.ToLowerInvariant()?.GetHashCode() ?? 0);
                result = (result * 16777619) ^ (item.Chip?.ToLowerInvariant()?.GetHashCode() ?? 0);
                result = (result * 16777619) ^ (item.Language?.ToLowerInvariant()?.GetHashCode() ?? 0);

                return result;
            }
        }

        /// <summary>Gets an instance of the <see cref="CatalogPackageComparer" /> comparer.</summary>
        public static CatalogPackageComparer Default
        {
            get => _default;
        }
    }
}