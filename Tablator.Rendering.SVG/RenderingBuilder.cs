namespace Tablator.Rendering.SVG
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessModel.Tablature;
    using Infrastructure.Models;
    using Infrastructure.Rendering;
    using Infrastructure.Enumerations;

    public class RenderingBuilder : ITablatureRenderingBuilder
    {
        public RenderingBuilder()
        { }

        public TabGenerationStatus BuildOutputContent(TablatureModel tab, TablatureRenderingOptions options, out string outputContent)
        {
            outputContent = null;

            try
            {

                return TabGenerationStatus.Succeed;
            }
            catch (Exception)
            {
                return TabGenerationStatus.Failed;
            }
            finally
            {

            }
        }
    }
}