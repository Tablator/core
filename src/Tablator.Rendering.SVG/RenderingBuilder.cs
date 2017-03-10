namespace Tablator.Rendering.SVG
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessModel.Tablature;
    using Infrastructure.Models;
    using Infrastructure.Enumerations;
    using System.Linq;
    using Tablator.Infrastructure.Constants;
    using Tablator.Infrastructure.Extensions;

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
            Tablature = (GuitarTablatureModel)tab; // TODO: remove casting => https://adrientorris.github.io/elegant-code/why-i-always-think-twice-before-using-casting.html
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

                if (Tablature.Tempo.HasValue && Tablature.Tempo.Value > 0)
                {
                    cursorHeight += 5;
                    svgHeight += 5;
                    SVGContent += "<text x=\"0\" y=\"" + cursorHeight + "\" font-family=\"" + Options.Typeface + "\" font-size=\"12\" text-anchor=\"left\">Tempo: " + Tablature.Tempo.Value + "</text>";
                    cursorHeight += 15;
                    svgHeight += 15;
                }

                if (Options.DisplayEnchainement && Tablature.Structure != null && Tablature.Structure.Count > 0)
                {
                    cursorHeight += 5;
                    svgHeight += 5;

                    if (!Options.AffichageEnchainementDetaille.HasValue || !Options.AffichageEnchainementDetaille.Value)
                    {
                        // Affichage simple
                        SVGContent += "<text x=\"0\" y=\"" + cursorHeight + "\" font-family=\"" + Options.Typeface + "\" font-size=\"12\" text-anchor=\"left\">Enchaînement: ";
                        foreach (StructureSectionModel ei in Tablature.Structure)
                        {
                            SVGContent += "(" + Tablature.GetPartName(ei.PartId, Options.Culture) + " x" + ei.Repeat + ") ";
                        }
                        SVGContent += "</text>";
                    }
                    else
                    {
                        // Affichage détaillé
                        SVGContent += "<text x=\"0\" y=\"" + cursorHeight + "\" font-family=\"" + Options.Typeface + "\" font-size=\"12\" text-anchor=\"left\">Enchaînement:</text>";
                        foreach (StructureSectionModel ei in Tablature.Structure)
                        {
                            cursorHeight += 15;
                            svgHeight += 15;
                            SVGContent += "<text x=\"20\" y=\"" + cursorHeight + "\" font-family=\"" + Options.Typeface + "\" font-size=\"12\" text-anchor=\"left\">- " + Tablature.GetPartName(ei.PartId, Options.Culture) + " x" + ei.Repeat + "</text>";
                        }
                    }

                    cursorHeight += 15;
                    svgHeight += 15;
                }

                // Implement instrument stuff (only guitar for now)

                switch (Tablature.Instrument)
                {
                    case InstrumentEnum.Guitar:
                        GuitarRenderingBuilder guitarRenderingBuilder = new GuitarRenderingBuilder();
                        guitarRenderingBuilder.Init(Options, Tablature, cursorWith, cursorHeight);
                        string _SVGContent = SVGContent;
                        guitarRenderingBuilder.TryBuild(ref _SVGContent);
                        SVGContent = _SVGContent;

                        break;
                    default:
                        throw new NotImplementedException();
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

    public sealed class GuitarRenderingBuilder
    {
        private TablatureRenderingOptions Options { get; set; }

        private GuitarTablatureModel Tablature { get; set; }

        private int cursorWith { get; set; } = 0;

        private int cursorHeight { get; set; } = 20;

        private int svgHeight { get; set; } = 20;

        private string SVGContent { get; set; } = string.Empty;

        public GuitarRenderingBuilder()
        { }

        public void Init(TablatureRenderingOptions options, GuitarTablatureModel tab, int crsrWidth, int crsrHeight)
        {
            Options = options;
            Tablature = tab;
            cursorWith = crsrWidth;
            cursorHeight = crsrHeight;
        }

        public bool TryBuild(ref string ret)
        {
            SVGContent = ret;

            try
            {
                // en-tête du document

                if (Tablature.Capodastre.HasValue && Tablature.Capodastre.Value > 0)
                {
                    cursorHeight += 5;
                    svgHeight += 5;
                    SVGContent += "<text x=\"0\" y=\"" + cursorHeight + "\" font-family=\"" + Options.Typeface + "\" font-size=\"12\" text-anchor=\"left\">Capodastre: " + Tablature.Capodastre.Value + "</text>";
                    cursorHeight += 15;
                    svgHeight += 15;
                }

                if (!string.IsNullOrWhiteSpace(Tablature.Tuning))
                {
                    cursorHeight += 5;
                    svgHeight += 5;
                    SVGContent += "<text x=\"0\" y=\"" + cursorHeight + "\" font-family=\"" + Options.Typeface + "\" font-size=\"12\" text-anchor=\"left\">Tuning: " + Tablature.Tuning + "</text>";
                    cursorHeight += 15;
                    svgHeight += 15;
                }

                // en-tête accords utilisés

                if (Options.DisplayChordsHeader && Tablature.Chords != null && Tablature.Chords.Count() > 0)
                {
                    cursorWith = 0;
                    cursorHeight += 10;
                    svgHeight += 10;

                    GuitarChordRenderingBuilder chordBuilder = new GuitarChordRenderingBuilder();
                    chordBuilder.Init(new GuitarChordRenderingOptions() { });

                    int i = 0;
                    foreach (string s in Tablature.Chords)
                    {
                        cursorWith += 5;

                        if (i == 0)
                        {
                            // New line
                        }

                        string chordSVG = string.Empty;

                        if (chordBuilder.DrawChord(Tablature.Chords[i], out chordSVG, cursorWidth: cursorWith, cursorHeight: cursorHeight))
                            SVGContent += chordSVG;

                        cursorWith += chordBuilder.GetWidth() + 10;

                        chordSVG = null;

                        if ((Options.Width - cursorWith) < chordBuilder.GetWidth() + 10)
                        {
                            // New Line

                            i = 0;
                            cursorHeight += chordBuilder.GetHeight();
                            svgHeight += chordBuilder.GetHeight();
                            cursorWith = 0;
                        }
                        else
                        {
                            i++;
                        }
                    }

                    // On passe la ligne en cours

                    cursorHeight += chordBuilder.GetHeight();
                    svgHeight += chordBuilder.GetHeight();
                    cursorWith = 0;
                }

                // contenu tab

                cursorWith = 0;

                foreach (PartSectionModel part in Tablature.PartSections)
                {
                    if (!string.IsNullOrWhiteSpace(Tablature.GetPartName(part.Id, Options.Culture)))
                    {
                        cursorHeight += 30;
                        SVGContent += "<text x=\"0\"  y=\"" + cursorHeight + "\" font-family=\"" + Options.Typeface + "\" font-size=\"15\">" + Tablature.GetPartName(part.Id, Options.Culture) + "</text>";
                        cursorHeight += 15;
                        cursorHeight += 5;
                        svgHeight += 50;
                    }

                    // On crée une ligne vide de tab
                    CreateNewLine();

                    // Et on mets les notes

                    int iMesures = 0;

                    for (int i = 0; i < part.Mesures.Count; i++)
                    {
                        iMesures++;

                        foreach (PartSectionMesureTempsModel tmp in part.Mesures[i].Temps)
                        {
                            foreach (PartSectionMesureTempsItemModel s in tmp.Sons)
                            {
                                //if (s.Type == TypeSonEnum.Note)
                                //    CreateNote(s.Corde, s.Position);
                                //else if (s.Type == TypeSonEnum.Accord)
                                //    CreateChord(s.Chord, s.SensGrattageCode);

                                if (Convert.ToInt32(s.Pprts.Where(x => x.Code == (int)GuitarPropertyEnum.Type).First().Value) == (int)TypeSonEnum.Note)
                                    CreateNote(Convert.ToInt32(s.Pprts.Where(x => x.Code == (int)GuitarPropertyEnum.Corde).First().Value), Convert.ToInt32(s.Pprts.Where(x => x.Code == (int)GuitarPropertyEnum.Position).First().Value));
                                else
                                    CreateChord(s.Pprts.Where(x => x.Code == (int)GuitarPropertyEnum.Chord).First().Value, Convert.ToInt32(s.Pprts.Where(x => x.Code == (int)GuitarPropertyEnum.Direction).First().Value));
                            }
                        }

                        if (iMesures < part.Mesures.Count)
                        {
                            int nbNotesNextMesure = 0;
                            if (part.Mesures[i + 1] != null && part.Mesures[i + 1].Temps != null && part.Mesures[i + 1].Temps.Count > 0)
                            {
                                part.Mesures[i + 1].Temps.ForEach(delegate (PartSectionMesureTempsModel t)
                                {
                                    nbNotesNextMesure += t.Sons != null ? t.Sons.Count() : 0;
                                });
                            }

                            if (cursorWith < (Options.Width - (20 + nbNotesNextMesure * 20)))
                                CreateVerticalLine();
                            else
                            {
                                cursorHeight += Options.StringSpacing * 6 + 20;
                                CreateNewLine();
                            }
                        }
                    }

                    // on mets à jour la hauteur du svg

                    cursorHeight += Options.StringSpacing * 5;
                    svgHeight += cursorHeight;
                }

                // Response

                SVGContent = "<svg width=\"" + Options.Width + "\" height=\"" + (svgHeight + 20) + "\">" + SVGContent + "</svg>";
                ret = SVGContent;
                return true;
            }
            catch (Exception)
            {
                ret = null;
                throw;
            }
            finally
            {
                SVGContent = null;
            }
        }

        //public TabGenerationStatus BuildOutputContent(IInstrumentTablature tab, TablatureRenderingOptions options, out string outputContent)
        //{
        //    outputContent = null;
        //    Tablature = (GuitarTablatureModel)tab;
        //    Options = options;

        //    try
        //    {
        //        // Build common part of the document (title, ...)

        //        if (!string.IsNullOrWhiteSpace(Tablature.SongName))
        //        {
        //            cursorHeight += 8;
        //            svgHeight += 8;
        //            SVGContent += "<text x=\"50%\" y=\"" + cursorHeight + "\" font-family=\"" + Options.Typeface + "\" font-size=\"30\" text-anchor=\"middle\">" + Tablature.SongName + "</text>";
        //            cursorHeight += 30;
        //            svgHeight += 30;
        //        }

        //        if (!string.IsNullOrWhiteSpace(Tablature.ArtistName))
        //        {
        //            cursorHeight += 5;
        //            svgHeight += 5;
        //            SVGContent += "<text x=\"50%\" y=\"" + cursorHeight + "\" font-family=\"" + Options.Typeface + "\" font-size=\"15\" text-anchor=\"middle\" font-style=\"italic\">" + Tablature.ArtistName + "</text>";
        //            cursorHeight += 15;
        //            svgHeight += 15;
        //        }

        //        if (Tablature.Tempo.HasValue && Tablature.Tempo.Value > 0)
        //        {
        //            cursorHeight += 5;
        //            svgHeight += 5;
        //            SVGContent += "<text x=\"0\" y=\"" + cursorHeight + "\" font-family=\"" + Options.Typeface + "\" font-size=\"12\" text-anchor=\"left\">Tempo: " + Tablature.Tempo.Value + "</text>";
        //            cursorHeight += 15;
        //            svgHeight += 15;
        //        }

        //        if (Options.DisplayEnchainement && Tablature.Structure != null && Tablature.Structure.Count > 0)
        //        {
        //            cursorHeight += 5;
        //            svgHeight += 5;

        //            if (!Options.AffichageEnchainementDetaille.HasValue || !Options.AffichageEnchainementDetaille.Value)
        //            {
        //                // Affichage simple
        //                SVGContent += "<text x=\"0\" y=\"" + cursorHeight + "\" font-family=\"" + Options.Typeface + "\" font-size=\"12\" text-anchor=\"left\">Enchaînement: ";
        //                foreach (StructureSectionModel ei in Tablature.Structure)
        //                {
        //                    SVGContent += "(" + Tablature.GetPartName(ei.PartId, Options.Culture) + " x" + ei.Repeat + ") ";
        //                }
        //                SVGContent += "</text>";
        //            }
        //            else
        //            {
        //                // Affichage détaillé
        //                SVGContent += "<text x=\"0\" y=\"" + cursorHeight + "\" font-family=\"" + Options.Typeface + "\" font-size=\"12\" text-anchor=\"left\">Enchaînement:</text>";
        //                foreach (StructureSectionModel ei in Tablature.Structure)
        //                {
        //                    cursorHeight += 15;
        //                    svgHeight += 15;
        //                    SVGContent += "<text x=\"20\" y=\"" + cursorHeight + "\" font-family=\"" + Options.Typeface + "\" font-size=\"12\" text-anchor=\"left\">- " + Tablature.GetPartName(ei.PartId, Options.Culture) + " x" + ei.Repeat + "</text>";
        //                }
        //            }

        //            cursorHeight += 15;
        //            svgHeight += 15;
        //        }

        //        // Implement instrument stuff (only guitar for now)

        //        // En-tête guitar settings

        //        if (Tablature.Capodastre.HasValue && Tablature.Capodastre.Value > 0)
        //        {
        //            cursorHeight += 5;
        //            svgHeight += 5;
        //            SVGContent += "<text x=\"0\" y=\"" + cursorHeight + "\" font-family=\"" + Options.Typeface + "\" font-size=\"12\" text-anchor=\"left\">Capodastre: " + Tablature.Capodastre.Value + "</text>";
        //            cursorHeight += 15;
        //            svgHeight += 15;
        //        }

        //        if (!string.IsNullOrWhiteSpace(Tablature.Tuning))
        //        {
        //            cursorHeight += 5;
        //            svgHeight += 5;
        //            SVGContent += "<text x=\"0\" y=\"" + cursorHeight + "\" font-family=\"" + Options.Typeface + "\" font-size=\"12\" text-anchor=\"left\">Tuning: " + Tablature.Tuning + "</text>";
        //            cursorHeight += 15;
        //            svgHeight += 15;
        //        }

        //        // en-tête accords utilisés

        //        if (Options.DisplayChordsHeader && Tablature.Chords != null && Tablature.Chords.Count > 0)
        //        {
        //            cursorWith = 0;
        //            cursorHeight += 10;
        //            svgHeight += 10;

        //            GuitarChordRenderingBuilder chordBuilder = new GuitarChordRenderingBuilder();

        //            int i = 0;
        //            foreach (string s in Tablature.Chords)
        //            {
        //                cursorWith += 5;

        //                if (i == 0)
        //                {
        //                    // New line
        //                }

        //                string chordSVG = string.Empty;

        //                if (chordBuilder.DrawChord(Tablature.Chords[i], out chordSVG, cursorWidth: cursorWith, cursorHeight: cursorHeight))
        //                    SVGContent += chordSVG;

        //                cursorWith += chordBuilder.GetWidth() + 10;

        //                chordSVG = null;

        //                if ((Options.Width - cursorWith) < chordBuilder.GetWidth() + 10)
        //                {
        //                    // New Line

        //                    i = 0;
        //                    cursorHeight += chordBuilder.GetHeight();
        //                    svgHeight += chordBuilder.GetHeight();
        //                    cursorWith = 0;
        //                }
        //                else
        //                {
        //                    i++;
        //                }
        //            }

        //            // On passe la ligne en cours

        //            cursorHeight += chordBuilder.GetHeight();
        //            svgHeight += chordBuilder.GetHeight();
        //            cursorWith = 0;
        //        }

        //        //

        //        outputContent = "<svg width=\"" + Options.Width + "\" height=\"" + (svgHeight + 20) + "\">" + SVGContent + "</svg>";
        //        return TabGenerationStatus.Succeed;
        //    }
        //    catch (Exception)
        //    {
        //        return TabGenerationStatus.Failed;
        //    }
        //    finally
        //    {
        //        Options = null;
        //        Tablature = null;
        //        SVGContent = null;
        //    }
        //}

        private void CreateChord(string chord, int? sensGrattageCode)
        {
            string[] chordComp = chord.Split(new char[] { '|' }, StringSplitOptions.None);
            if (chordComp.Length != 6)
                throw new Exception();

            chordComp = chordComp.Reverse().ToArray();

            int yPosi = cursorHeight;
            cursorWith += 10;

            if (sensGrattageCode.HasValue)
            {
                if ((SensGrattageCordes)sensGrattageCode.Value == SensGrattageCordes.Down)
                {
                    int startPosi = yPosi;
                    int endPosi = yPosi;

                    List<int> cordesJouees = new List<int>();
                    for (int i = 0; i < 5; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(chordComp[i]))
                            cordesJouees.Add(i);
                    }

                    startPosi += cordesJouees.Max() * 20;
                    endPosi += cordesJouees.Min() * 20;

                    SVGContent += "<line x1=\"" + cursorWith + "\" y1=\"" + (startPosi + 8) + "\" x2=\"" + cursorWith + "\" y2=\"" + (endPosi - 8) + "\" stroke-width=\"1\" stroke=\"black\"/>";
                    SVGContent += "<line x1=\"" + cursorWith + "\" y1=\"" + (endPosi - 8) + "\" x2=\"" + (cursorWith - 4) + "\" y2=\"" + (endPosi + 6) + "\" stroke-width=\"1\" stroke=\"black\"/>";
                    SVGContent += "<line x1=\"" + cursorWith + "\" y1=\"" + (endPosi - 8) + "\" x2=\"" + (cursorWith + 4) + "\" y2=\"" + (endPosi + 6) + "\" stroke-width=\"1\" stroke=\"black\"/>";
                    //cf http://vanseodesign.com/web-design/svg-markers/
                }
                else if ((SensGrattageCordes)sensGrattageCode.Value == SensGrattageCordes.Up)
                {

                }

                cursorWith += 5;
            }

            for (int i = 0; i < 5; i++)
            {
                if (i > 0)
                    yPosi += 20;

                if (string.IsNullOrWhiteSpace(chordComp[i]))
                    continue;

                SVGContent += "<circle cx=\"" + (cursorWith + 8) + "\" cy=\"" + yPosi + "\" r=\"8\" stroke=\"rgb(255, 255, 255)\" stroke-width=\"0\" fill=\"rgb(255, 255, 255)\" /><text x=\"" + (cursorWith + 4) + "\" y=\"" + (yPosi + 4) + "\" font-family=\"" + Options.Typeface + "\" font-size=\"15\" fill=\"black\">" + chordComp[i] + "</text>";
            }
        }

        private void CreateNote(int corde, int posi)
        {
            int yPosi = cursorHeight;
            yPosi += (6 - corde) * 20;
            SVGContent += "<circle cx=\"" + (cursorWith + 8) + "\" cy=\"" + yPosi + "\" r=\"8\" stroke=\"rgb(255, 255, 255)\" stroke-width=\"0\" fill=\"rgb(255, 255, 255)\" /><text x=\"" + (cursorWith + 4) + "\" y=\"" + (yPosi + 4) + "\" font-family=\"" + Options.Typeface + "\" font-size=\"15\" fill=\"black\">" + posi + "</text>";
            cursorWith += 25;
        }

        private void CreateVerticalLine()
        {
            SVGContent += "<line x1=\"" + (cursorWith + 7) + "\" y1=\"" + cursorHeight + "\" x2=\"" + (cursorWith + 7) + "\" y2=\"" + (cursorHeight + (Options.StringSpacing * 5)) + "\" stroke=\"" + Options.StringColor + "\" stroke-width=\"" + Options.StringWidth + "\" fill=\"" + Options.StringColor + "\"></line>";
            cursorWith += 14;
        }

        /// <summary>
        /// On crée une nouvelle ligne vide (cordes + inscription TAB etc, sans aucune note)
        /// </summary>
        /// <returns></returns>
        private void CreateNewLine()
        {
            cursorWith = 0;

            // ligne début tab

            SVGContent += "<line x1=\"0\" y1=\"" + cursorHeight + "\" x2=\"0\" y2=\"" + (cursorHeight + (Options.StringSpacing * 5)) + "\" stroke=\"" + Options.StringColor + "\" stroke-width=\"" + Options.StringWidth + "\" fill=\"" + Options.StringColor + "\"></line>";

            // cordes guitare

            for (int i = 0; i < 6; i++)
                SVGContent += "<line x1=\"0\" y1=\"" + (cursorHeight + (Options.StringSpacing * i)) + "\" x2=\"100%\" y2=\"" + (cursorHeight + (Options.StringSpacing * i)) + "\" stroke=\"" + Options.StringColor + "\" stroke-width=\"" + Options.StringWidth + "\" fill=\"" + Options.StringColor + "\"></line>";

            // inscription "TAB"

            SVGContent += "<text x=\"10\" y=\"" + (cursorHeight + Options.StringSpacing + 15) + "\" text-anchor=\"middle\" font-family=\"" + Options.Typeface + "\" font-size=\"11\" stroke=\"" + Options.StringColor + "\" fill=\"" + Options.StringColor + "\">T</text>";
            SVGContent += "<text x=\"10\" y=\"" + (cursorHeight + (Options.StringSpacing * 2) + 15) + "\" text-anchor=\"middle\" font-family=\"" + Options.Typeface + "\" font-size=\"11\" stroke=\"" + Options.StringColor + "\" fill=\"" + Options.StringColor + "\">A</text>";
            SVGContent += "<text x=\"10\" y=\"" + (cursorHeight + (Options.StringSpacing * 3) + 15) + "\" text-anchor=\"middle\" font-family=\"" + Options.Typeface + "\" font-size=\"11\" stroke=\"" + Options.StringColor + "\" fill=\"" + Options.StringColor + "\">B</text>";

            cursorWith += 50; // taille de tab plus un peu d'espace

            // ligne début tab

            SVGContent += "<line x1=\"100%\" y1=\"" + cursorHeight + "\" x2=\"100%\" y2=\"" + (cursorHeight + (Options.StringSpacing * 5)) + "\" stroke=\"" + Options.StringColor + "\" stroke-width=\"" + Options.StringWidth + "\" fill=\"" + Options.StringColor + "\"></line>";

        }
    }

    public sealed class GuitarChordRenderingBuilder
    {
        public GuitarChordRenderingOptions Options { get; private set; }

        private string SVGContent { get; set; } = string.Empty;

        public GuitarChordRenderingBuilder()
        {

        }

        public int GetWidth() => Options.Width;

        public int GetHeight() => Options.Height;

        public void Init(GuitarChordRenderingOptions options)
        {
            Options = options;
        }

        public bool DrawChord(string chord, out string svg, int cursorWidth = 0, int cursorHeight = 0)
        {
            SVGContent = string.Empty;
            svg = string.Empty;

            if (Options.DisplayLabel)
            {
                // Nom accord
                SVGContent += "<text x=\"" + (cursorWidth + (Options.Width / 2)) + "\" y=\"" + (cursorHeight + 5) + "\" font-family=\"" + Options.Typeface + "\" font-size=\"17\" fill=\"black\" text-anchor=\"middle\">" + chord + "</text>";
            }

            // ligne haut

            SVGContent += "<line x1=\"" + (cursorWidth + 5) + "\" y1=\"" + (cursorHeight + 25) + "\" x2=\"" + (cursorWidth + 105) + "\" y2=\"" + (cursorHeight + 25) + "\" stroke=\"" + Options.StringColor + "\" stroke-width=\"3\" fill=\"" + Options.StringColor + "\"></line>";

            // lignes guitare

            for (int i = 0; i < 6; i++)
            {
                SVGContent += "<line x1=\"" + (cursorWidth + 5 + i * 20) + "\" y1=\"" + (cursorHeight + 25) + "\" x2=\"" + (cursorWidth + 5 + i * 20) + "\" y2=\"" + (cursorHeight + 145) + "\" stroke=\"" + Options.StringColor + "\" stroke-width=\"1\" fill=\"" + Options.StringColor + "\"></line>";
            }

            // Frets

            for (int i = 0; i < 4; i++)
            {
                SVGContent += "<line x1=\"" + (cursorWidth + 5) + "\" y1=\"" + (cursorHeight + 55 + i * 30) + "\" x2=\"" + (cursorWidth + 105) + "\" y2=\"" + (cursorHeight + 55 + i * 30) + "\" stroke=\"" + Options.StringColor + "\" stroke-width=\"1\" fill=\"" + Options.StringColor + "\"></line>";
            }

            try
            {
                //switch (chord)
                //{
                //    case "eeer":
                //        // Capo
                //        // Finger positions
                //        // Free strings
                //        return false;
                //    case "C":
                //        AddFingersPositions(GuitarChordEnum.C.GetDisplayDescription(), cursorWidth, cursorHeight);
                //        break;
                //    case "A":
                //        AddFingersPositions(GuitarChordEnum.A.GetDisplayDescription(), cursorWidth, cursorHeight);
                //        break;
                //    case "G":
                //        AddFingersPositions(GuitarChordEnum.G.GetDisplayDescription(), cursorWidth, cursorHeight);
                //        break;
                //    case "Am":
                //        AddFingersPositions(GuitarChordEnum.Am.GetDisplayDescription(), cursorWidth, cursorHeight);
                //        break;
                //    default:
                //        AddFingersPositions(GuitarChordEnum.A.GetDisplayDescription(), cursorWidth, cursorHeight);
                //        break;
                //        //default:
                //        //    // Capo
                //        //    // Finger positions
                //        //    // Free strings
                //        //    return false;
                //}
                //TODO here : remove switch du dessus et aller chercher l'enum dynamiquement
                AddFingersPositions(EnumerationExtensions.GetValueFromDisplayShortName<GuitarChordEnum>(chord).GetDisplayDescription(), cursorWidth, cursorHeight);

                svg = SVGContent;
                return true;
            }
            catch (Exception)
            {

            }
            finally
            {
                SVGContent = string.Empty;
            }

            return false;
        }

        private void GenerateGrille()
        {

        }

        private void AddFingersPositions(string chordComposition, int cursorWidth, int cursorHeight)
        {
            string[] strings = chordComposition.Split(new char[] { ChordConstants.CompositionSeparator }, StringSplitOptions.None);

            if (strings.Length != 7)
                throw new Exception();

            for (int i = 0; i < 6; i++)
            {
                if (string.IsNullOrWhiteSpace(strings[i]))
                {
                    // corde mutée
                    SVGContent += "<text x=\"" + (cursorWidth + 2 + i * 20) + "\" y=\"" + (cursorHeight + 17) + "\" font-family=\"" + Options.Typeface + "\" font-size=\"13\" fill =\"" + Options.MutedFreeStringColor + "\" text-anchor=\"start\">x</text>";
                    continue;
                }

                int posi = Convert.ToInt32(strings[i]);
                if (posi == 0)
                {
                    SVGContent += "<circle cx=\"" + (cursorWidth + 5 + i * 20) + "\" cy=\"" + (cursorHeight + 15) + "\" r=\"5\" stroke=\"" + Options.FingerPositionBackgroundColor + "\" stroke-width=\"1\" fill=\"" + Options.PlayedFreeStringColor + "\" />";
                }
                else if (posi > 0)
                {
                    if (Options.DisplayFretNumberInFingerPositions)
                        SVGContent += "<g><circle cx=\"" + (cursorWidth + 5 + i * 20) + "\" cy=\"" + (cursorHeight + 10 + posi * 30) + "\" r=\"8\" stroke=\"" + Options.FingerPositionBackgroundColor + "\" stroke-width=\"0\" fill=\"" + Options.FingerPositionBackgroundColor + "\" /><text x=\"" + (cursorWidth + 5 + i * 20) + "\" y=\"" + (cursorHeight + 10 + 4 + posi * 30) + "\" font-family=\"" + Options.Typeface + "\" font-size=\"12\" fill=\"" + Options.FingerPositionTextColor + "\" text-anchor=\"middle\">" + posi + "</text></g>";
                    else
                        SVGContent += "<circle cx=\"" + (cursorWidth + 5 + i * 20) + "\" cy=\"" + (cursorHeight + 10 + posi * 30) + "\" r=\"8\" stroke=\"" + Options.FingerPositionBackgroundColor + "\" stroke-width=\"0\" fill=\"" + Options.FingerPositionBackgroundColor + "\" />";
                }
                else
                {
                    throw new Exception();
                }
            }

            int capo = Convert.ToInt32(strings[6]);
            if (capo > 0)
            {

            }
        }
    }
}