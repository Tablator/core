namespace Tablator.BusinessLogic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using BusinessModel;

    public interface ICatalogService
    {
        Task<CatalogModel> GetCatalog();
    }
}