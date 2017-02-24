namespace Tablator.BusinessLogic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Tablator.DataAccess.Repositories;

    public sealed class TablatureService : ITablatureService
    {
        private readonly ITablatureRepository _repository;

        public TablatureService(ITablatureRepository tablatureRepository)
        {
            _repository = tablatureRepository;
        }
    }
}