namespace Tablator.Rendering.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessModel.Tablature;
    using Infrastructure.Enumerations;
    using Infrastructure.Models;

    public interface ITablatureRenderingBuilder
    {
        TabGenerationStatus BuildOutputContent(TablatureModel tab, TablatureRenderingOptions options, out string outputContent);
    }
}