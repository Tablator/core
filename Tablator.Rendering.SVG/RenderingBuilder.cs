namespace Tablator.Rendering.SVG
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessModel.Tablature;
    using Infrastructure.Models;
    using Infrastructure.Enumerations;

    public sealed class RenderingBuilder : Tablator.Rendering.Core.ITablatureRenderingBuilder
    {
        private TablatureRenderingOptions Options { get; set; }

        private GuitarTablatureModel Tablature { get; set; }

        private int cursorWith { get; set; } = 0;

        private int cursorHeight { get; set; } = 20;

        private int svgHeight { get; set; } = 20;

        private string SVGContent { get; set; } = string.Empty;

        public RenderingBuilder()
        { }

        public TabGenerationStatus BuildOutputContent(IInstrumentTablature tab, TablatureRenderingOptions options, out string outputContent)
        {
            outputContent = null;
            Tablature = (GuitarTablatureModel)tab;
            Options = options;

            try
            {
                // Build common part of the document (title, ...)

                if (!string.IsNullOrWhiteSpace(Tablature.SongName))
                {
                    cursorHeight += 8;
                    svgHeight += 8;
                    SVGContent += "<text x=\"50%\" y=\"" + cursorHeight + "\" font-family=\"" + Options.Typeface + "\" font-size=\"30\" text-anchor=\"middle\">" + Tablature.SongName + "</text>";
                    cursorHeight += 30;
                    svgHeight += 30;
                }

                if (!string.IsNullOrWhiteSpace(Tablature.ArtistName))
                {
                    cursorHeight += 5;
                    svgHeight += 5;
                    SVGContent += "<text x=\"50%\" y=\"" + cursorHeight + "\" font-family=\"" + Options.Typeface + "\" font-size=\"15\" text-anchor=\"middle\" font-style=\"italic\">" + Tablature.ArtistName + "</text>";
                    cursorHeight += 15;
                    svgHeight += 15;
                }

                outputContent = SVGContent;
                return TabGenerationStatus.Succeed;
            }
            catch (Exception)
            {
                return TabGenerationStatus.Failed;
            }
            finally
            {
                Options = null;
                Tablature = null;
                SVGContent = null;
            }
        }
    }
}