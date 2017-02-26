namespace Tablator.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DomainModel;

    /// <summary>
    /// Repository to deal with catalog data
    /// </summary>
    public interface ICatalogRepository
    {
        CatalogHierarchyCollectionLevel ListHierarchyLevels();
    }
}