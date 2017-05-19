namespace Tablator.BusinessLogic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Tablator.DataAccess.Repositories;
    using System.Threading.Tasks;
    using BusinessModel;
    using Infrastructure.Enumerations;
    using Tablator.BusinessModel.Builders;

    public sealed class TablatureService : ITablatureService
    {
        private readonly ITablatureRepository _repository;

        public TablatureService(ITablatureRepository tablatureRepository)
        {
            _repository = tablatureRepository;
        }

        //public IInstrumentTablature Get(Guid id, InstrumentEnum instrument)
        //{
        //    if (id == Guid.Empty)
        //        throw new ArgumentNullException(nameof(id));

        //    switch (instrument)
        //    {
        //        case InstrumentEnum.Guitar:
        //            GuitarTablatureModel tab = (GuitarTablatureModel)_repository.Get(id);
        //            return tab;
        //        default:
        //            throw new NotImplementedException();
        //    }
        //}

        public TablatureModel Get(Guid id)
        {
            TablatureBuilder builder = new TablatureBuilder(_repository.Get(id));
            return builder.Build();
        }
    }
}