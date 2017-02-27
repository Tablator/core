namespace Tablator.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DomainModel;

    /// <summary>
    /// Repository to deal with tablatures data
    /// </summary>
    public interface ITablatureRepository
    {
        Tablature Get(Guid id);
    }
}