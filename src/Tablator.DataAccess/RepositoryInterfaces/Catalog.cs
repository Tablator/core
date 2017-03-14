namespace Tablator.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using DomainModel;
    using System.Threading.Tasks;

    /// <summary>
    /// Repository to deal with catalog data
    /// </summary>
    public interface ICatalogRepository
    {
        CatalogHierarchyCollectionLevel ListHierarchyLevels();

        /// <summary>
        /// Renvoie l'identifiant de la tablature correspondant au chemin d'accès
        /// </summary>
        /// <param name="urlPath">chemin d'accès url de la tab (ex=> "guitar-tab-francis-cabrel-jelaimeamourir")</param>
        /// <returns>identifiant de la tablature ou null</returns>
        Task<Guid?> GetTablatureId(string urlPath);

        Task<CatalogHierarchyTabReferenceCollection> ListReferences();
    }
}