using System;
using System.Collections.Generic;
using System.Text;
using Tablator.DomainModel;

namespace Tablator.DataAccess.Repositories
{
    /// <summary>
    /// Access to chord data
    /// </summary>
    public interface IChordRepository
    {
        Chord Get(Guid id);
    }
}