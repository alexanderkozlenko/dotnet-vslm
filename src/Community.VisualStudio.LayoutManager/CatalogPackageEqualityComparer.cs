using System.Collections.Generic;

namespace Community.VisualStudio.LayoutManager
{
    /// <summary>Catalog package equality comparer.</summary>
    internal sealed class CatalogPackageEqualityComparer : IEqualityComparer<CatalogPackageInfo>
    {
        /// <summary>Initializes a new instance of the <see cref="CatalogPackageEqualityComparer" /> class.</summary>
        public CatalogPackageEqualityComparer()
        {
        }

        bool IEqualityComparer<CatalogPackageInfo>.Equals(CatalogPackageInfo x, CatalogPackageInfo y)
        {
            return
                (x.ID == y.ID) &&
                (x.Version == y.Version) &&
                (x.Chip == y.Chip) &&
                (x.Language == y.Language);
        }

        int IEqualityComparer<CatalogPackageInfo>.GetHashCode(CatalogPackageInfo obj)
        {
            return
                obj.ID.GetHashCode() ^
                obj.Version.GetHashCode() ^
                (obj.Chip ?? string.Empty).GetHashCode() ^
                (obj.Language ?? string.Empty).GetHashCode();
        }
    }
}