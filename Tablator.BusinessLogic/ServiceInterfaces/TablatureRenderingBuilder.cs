namespace Tablator.BusinessLogic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Tablator.BusinessModel;
    using Tablator.Infrastructure.Enumerations;
    using Tablator.Infrastructure.Models;

    public interface ITablatureRenderingBuilderService
    {
        bool TryBuild(InstrumentEnum instrument, out TabGenerationStatus status, out string ret);
        void Init(TablatureRenderingOptions options, Tablature tab);
    }
}