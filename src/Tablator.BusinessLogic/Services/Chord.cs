using System;
using System.Collections.Generic;
using System.Text;
using Tablator.DataAccess.Repositories;

namespace Tablator.BusinessLogic.Services
{
    public sealed class ChordService : IChordService
    {
        private readonly IChordRepository _repository;

        public ChordService(IChordRepository repository)
        {
            _repository = repository;
        }

        
    }
}