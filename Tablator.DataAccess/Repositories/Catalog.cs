namespace Tablator.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Infrastructure.DataAccess.Bases;
    using System.Threading.Tasks;
    using BusinessModel;
    using DomainModel;
    using System.IO;

    /// <summary>
    /// Repository to deal with catalog data
    /// </summary>
    public sealed class CatalogRepository : BaseFileRepository, ICatalogRepository
    {
        public CatalogRepository(string catalogRootDirectory)
               : base(catalogRootDirectory)
        { }

        public CatalogHierarchyCollectionLevel ListHierarchyLevels()
        {
            CatalogHierarchyCollectionLevel ret = null;

            if (!TryParseFileContent<CatalogHierarchyCollectionLevel>(StorageFileEnum.CatalogHierarchy, out ret))
                return null;

            return ret;
        }
    }
}