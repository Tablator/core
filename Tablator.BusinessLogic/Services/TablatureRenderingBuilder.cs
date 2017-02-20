namespace Tablator.BusinessLogic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Tablator.BusinessModel;
    using Tablator.Infrastructure.Enumerations;
    using Tablator.Infrastructure.Models;

    public class TablatureRenderingBuilderService : ITablatureRenderingBuilderService
    {
        private TablatureRenderingOptions Options { get; set; }

        private int cursorWith { get; set; } = 0;

        private int cursorHeight { get; set; } = 20;

        private int svgHeight { get; set; } = 20;

        private Tablature Tablature { get; set; }

        private string SVGContent { get; set; } = string.Empty;

        private IGuitarTablatureRenderingBuilderService GuitarTablatureRenderingBuilderService { get; }

        public TablatureRenderingBuilderService(
            IGuitarTablatureRenderingBuilderService guitarTablatureRenderingBuilderService)
        {
            GuitarTablatureRenderingBuilderService = guitarTablatureRenderingBuilderService;
        }

        public void Init(TablatureRenderingOptions options, Tablature tab)
        {
            Options = options;
            Tablature = tab;

            //GuitarTablatureRenderingBuilderService.Init(Options, Tablature);
        }

        public bool TryBuild(InstrumentEnum instrument, out TabGenerationStatus status, out string ret)
        {
            ret = string.Empty;
            status = TabGenerationStatus.Failed;

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

                if (Tablature.Tempo > 0)
                {
                    cursorHeight += 5;
                    svgHeight += 5;
                    SVGContent += "<text x=\"0\" y=\"" + cursorHeight + "\" font-family=\"" + Options.Typeface + "\" font-size=\"12\" text-anchor=\"left\">Tempo: " + Tablature.Tempo + "</text>";
                    cursorHeight += 15;
                    svgHeight += 15;
                }

                if (Options.DisplayEnchainement && Tablature.Enchainement != null && Tablature.Enchainement.Count > 0)
                {
                    cursorHeight += 5;
                    svgHeight += 5;

                    if (!Options.AffichageEnchainementDetaille.HasValue || !Options.AffichageEnchainementDetaille.Value)
                    {
                        // Affichage simple
                        SVGContent += "<text x=\"0\" y=\"" + cursorHeight + "\" font-family=\"" + Options.Typeface + "\" font-size=\"12\" text-anchor=\"left\">Enchaînement: ";
                        foreach (EnchainementItem ei in Tablature.Enchainement)
                        {
                            SVGContent += "(" + Tablature.GetPartName(ei.PartieId, Options.Culture) + " x" + ei.Repeat + ") ";
                        }
                        SVGContent += "</text>";
                    }
                    else
                    {
                        // Affichage détaillé
                        SVGContent += "<text x=\"0\" y=\"" + cursorHeight + "\" font-family=\"" + Options.Typeface + "\" font-size=\"12\" text-anchor=\"left\">Enchaînement:</text>";
                        foreach (EnchainementItem ei in Tablature.Enchainement)
                        {
                            cursorHeight += 15;
                            svgHeight += 15;
                            SVGContent += "<text x=\"20\" y=\"" + cursorHeight + "\" font-family=\"" + Options.Typeface + "\" font-size=\"12\" text-anchor=\"left\">- " + Tablature.GetPartName(ei.PartieId, Options.Culture) + " x" + ei.Repeat + "</text>";
                        }
                    }

                    cursorHeight += 15;
                    svgHeight += 15;
                }

                // Implement instrument stuff

                switch (instrument)
                {
                    case InstrumentEnum.Guitar:

                        GuitarTablatureRenderingBuilderService.Init(Options, Tablature, cursorWith, cursorHeight);
                        string _SVGContent = SVGContent;
                        GuitarTablatureRenderingBuilderService.TryBuild(ref _SVGContent);
                        SVGContent = _SVGContent;

                        break;
                    default:
                        throw new NotImplementedException();
                }

                // Response

                status = TabGenerationStatus.Succeed;
                ret = SVGContent;

                return true;
            }
            catch (Exception)
            {
                status = TabGenerationStatus.Failed;
                ret = null;
                throw;
            }
            finally
            {
                SVGContent = null;
            }
        }
    }
}