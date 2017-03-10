namespace Tablator.BusinessLogic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessModel.Tablature;
    using Infrastructure.Enumerations;

    public interface ITablatureService
    {
        IInstrumentTablature Get(Guid id, InstrumentEnum instrument);
    }
}