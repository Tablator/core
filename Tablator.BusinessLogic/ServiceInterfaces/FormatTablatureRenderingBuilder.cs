namespace Tablator.BusinessLogic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Tablator.Infrastructure.Enumerations;

    internal interface IFormatTablatureRenderingBuilderService
    {
        bool TryBuild(out TabGenerationStatus status);
    }
}