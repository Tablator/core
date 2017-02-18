namespace Tablator.BusinessLogic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Tablator.Infrastructure.Enumerations;
    using Tablator.Infrastructure.Models;

    public class TablatureRenderingBuilderService : ITablatureRenderingBuilderService
    {
        private TablatureRenderingOptions Options { get; }

        public TablatureRenderingBuilderService(TablatureRenderingOptions options)
        {
            Options = options;
        }
    }

    
}