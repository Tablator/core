namespace Tablator.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Infrastructure.DataAccess.Bases;
    using System.Threading.Tasks;
    using BusinessModel;

    /// <summary>
    /// Repository to deal with tablatures data
    /// </summary>
    public sealed class TablatureRepository : BaseFileRepository, ITablatureRepository
    {
        public TablatureRepository(string catalogRootDirectory)
               : base(catalogRootDirectory)
        { }

        public async Task<TablatureInformationModel> LoadInfo(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}