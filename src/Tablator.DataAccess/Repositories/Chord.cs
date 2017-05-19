using System;
using System.Collections.Generic;
using System.Text;
using Tablator.DomainModel;
using Tablator.Infrastructure.DataAccess.Bases;

namespace Tablator.DataAccess.Repositories
{
    /// <summary>
    /// Access to chord data
    /// </summary>
    public sealed class ChordRepository : ChordBaseFileRepository, IChordRepository
    {
        public ChordRepository(string fileDirectoryPath)
                : base(fileDirectoryPath)
        { }

        public Chord Get(Guid id)
        {
            if (!TryParseContent<Chord>(id, out Chord ret))
                return null;

            return ret;
        }
    }
}