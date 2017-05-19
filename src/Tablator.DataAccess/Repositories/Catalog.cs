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
    using System.Linq;

    /// <summary>
    /// Repository to deal with catalog data
    /// </summary>
    public sealed class CatalogRepository : CatalogBaseFileRepository, ICatalogRepository
    {
        public CatalogRepository(string catalogRootDirectory)
               : base(catalogRootDirectory)
        { }

        public CatalogHierarchyCollectionLevel ListHierarchyLevels()
        {
            CatalogHierarchyCollectionLevel ret = null;

            if (!TryParseContent<CatalogHierarchyCollectionLevel>(StorageFileEnum.CatalogHierarchy, out ret))
                return null;

            return ret;
        }

        /// <summary>
        /// Renvoie l'identifiant de la tablature correspondant au chemin d'accès
        /// </summary>
        /// <param name="urlPath">chemin d'accès url de la tab (ex=> "guitar-tab-francis-cabrel-jelaimeamourir")</param>
        /// <returns>identifiant de la tablature ou null</returns>
        public async Task<Guid?> GetTablatureId(string urlPath)
        {
            CatalogHierarchyTabReferenceCollection refs = null;
            if (!TryParseContent<CatalogHierarchyTabReferenceCollection>(StorageFileEnum.CatalogReference, out refs))
                return null;

            if (refs == null)
                return null;

            if (refs.Refs == null)
                return null;

            if (refs.Refs.Count() == 0)
                return null;

            return refs.Refs.Where(x => x.UrlPath.ToLower() == urlPath.ToLower()).Select(x => x.Id).FirstOrDefault();
        }

        public async Task<CatalogHierarchyTabReferenceCollection> ListReferences()
        {
            CatalogHierarchyTabReferenceCollection ret = null;

            if (!TryParseContent<CatalogHierarchyTabReferenceCollection>(StorageFileEnum.CatalogReference, out ret))
                return null;

            return ret;
        }
    }
}