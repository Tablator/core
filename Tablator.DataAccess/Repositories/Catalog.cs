namespace Tablator.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Infrastructure.DataAccess.Bases;

    /// <summary>
    /// Repository to deal with catalog data
    /// </summary>
    public sealed class CatalogRepository : BaseFileRepository, ICatalogRepository
    {
        public CatalogRepository()
            : base()
        { }
    }
}