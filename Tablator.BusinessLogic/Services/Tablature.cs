namespace Tablator.BusinessLogic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Tablator.DataAccess.Repositories;
    using System.Threading.Tasks;
    using BusinessModel;

    public sealed class TablatureService : ITablatureService
    {
        private readonly ITablatureRepository _repository;

        public TablatureService(ITablatureRepository tablatureRepository)
        {
            _repository = tablatureRepository;
        }

        public async Task<TablatureModel> Load(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}