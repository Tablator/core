using System;
using System.Collections.Generic;
using System.Text;

namespace Tablator.DataAccess.Repositories
{
    public sealed class ChordRepository : IChordRepository
    {
        private readonly string _rootDirectory;

        public ChordRepository(string catalogRootDirectory)
        {
            _rootDirectory = catalogRootDirectory;
        }

        public T Get<T>(string name)
        {
            throw new NotImplementedException();
        }
    }
}