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
            CatalogModel ret = (CatalogModel)_repository.ListHierarchyLevels();
            return ret;
        }
    }
}