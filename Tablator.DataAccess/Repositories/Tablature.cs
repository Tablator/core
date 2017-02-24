namespace Tablator.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Infrastructure.DataAccess.Bases;

    /// <summary>
    /// Repository to deal with tablatures data
    /// </summary>
    public sealed class TablatureRepository : BaseFileRepository, ITablatureRepository
    {
        public TablatureRepository()
               : base()
        { }
    }
}