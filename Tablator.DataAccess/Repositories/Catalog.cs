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

        public async Task<List<CatalogHierarchyLevel>> ListHierarchyLevels()
        {
            using (StreamReader file = File.OpenText(await GetCatalogHierarchyPath()))
            {
                using (JsonTextReader rdr = new JsonTextReader(file))
                {
                    o2 = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.Linq.JToken.ReadFrom(rdr);
                    json = o2.ToString();
                }
            }
        }
    }
}