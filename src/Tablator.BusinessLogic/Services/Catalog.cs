namespace Tablator.BusinessLogic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Tablator.DataAccess.Repositories;
    using System.Threading.Tasks;
    using BusinessModel;

    public sealed class CatalogService : ICatalogService
    {
        private readonly ICatalogRepository _repository;

        //private readonly IStorageFileService _storageService;

        public CatalogService(
            ICatalogRepository catalogRepository
            //, IStorageFileService storageService
            )
        {
            _repository = catalogRepository;
        }

        public async Task<CatalogModel> GetCatalog()
        {
            CatalogModel ret = (CatalogModel)_repository.ListHierarchyLevels(); // TODO: replace this cast!
            return ret;
        }

        /// <summary>
        /// Renvoie l'identifiant de la tablature correspondant au chemin d'accès
        /// </summary>
        /// <param name="urlPath">chemin d'accès url de la tab (ex=> "guitar-tab-francis-cabrel-jelaimeamourir")</param>
        /// <returns>identifiant de la tablature ou null</returns>
        public async Task<Guid?> GetTablatureId(string urlPath)
        {
            if (string.IsNullOrEmpty(urlPath))
                throw new ArgumentNullException(nameof(urlPath));

            return  _repository.GetTablatureId(urlPath);
        }

        public async Task<HierarchyTabReferenceCollectionModel> ListReferences()
        {
           return new HierarchyTabReferenceCollectionModel( _repository.ListReferences());
        }
    }
}