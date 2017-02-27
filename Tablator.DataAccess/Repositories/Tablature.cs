namespace Tablator.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Infrastructure.DataAccess.Bases;
    using System.Threading.Tasks;
    using DomainModel;

    /// <summary>
    /// Repository to deal with tablatures data
    /// </summary>
    public sealed class TablatureRepository : BaseFileRepository, ITablatureRepository
    {
        public TablatureRepository(string catalogRootDirectory)
               : base(catalogRootDirectory)
        { }

        public Tablature Get(Guid id)
        {
            Tablature ret = null;
            if (!TryParseTablatureFileContent<Tablature>(id, out ret))
                return null;

            return ret;
        }
    }
}