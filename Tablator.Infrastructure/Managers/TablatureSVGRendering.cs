using System;
using System.Collections.Generic;
using System.Text;

namespace Tablator.Infrastructure.Managers
{
    //public sealed class TablatureSVGRenderingManager
    //{

    //}

    //private sealed class GuitarChordSVGRenderingManager
    //{
    //    public GuitarChordSVGRenderingManager()
    //    { }
    //}

    //private sealed class GuitarTablatureSVGRenderingManager
    //{
    //    private TablatureRenderingOptions Options { get; set; }

    //    private int cursorWith { get; set; } = 0;

    //    private int cursorHeight { get; set; } = 20;

    //    private int svgHeight { get; set; } = 20;

    //    private TablatureModel Tablature { get; set; }

    //    private string SVGContent { get; set; } = string.Empty;

    //    public GuitarTablatureSVGRenderingManager()
    //    { }

    //    public void Init(TablatureRenderingOptions options, TablatureModel tab, int crsrWidth, int crsrHeight)
    //    {
    //        Options = options;
    //        Tablature = tab;
    //        cursorWith = crsrWidth;
    //        cursorHeight = crsrHeight;
    //    }

    //    public bool TryBuild(ref string ret)
    //    {
    //        SVGContent = ret;

    //        try
    //        {
    //            // en-tête du document

    //            Dictionary<int, string> _s = Tablature.Instruments.Where(x => x.Instrument == InstrumentEnum.Guitar).First().Settings;

    //            if (Convert.ToInt32(_s.Where(x => x.Key == (int)GuitarSettingsEnum.Capodastre).First().Value) > 0)
    //            {
    //                cursorHeight += 5;
    //                svgHeight += 5;
    //                SVGContent += "<text x=\"0\" y=\"" + cursorHeight + "\" font-family=\"" + Options.Typeface + "\" font-size=\"12\" text-anchor=\"left\">Capodastre: " + _s.Where(x => x.Key == (int)GuitarSettingsEnum.Capodastre).First().Value + "</text>";
    //                cursorHeight += 15;
    //                svgHeight += 15;
    //            }

    //            if (!string.IsNullOrWhiteSpace(_s.Where(x => x.Key == (int)GuitarSettingsEnum.Tuning).FirstOrDefault().Value))
    //            {
    //                cursorHeight += 5;
    //                svgHeight += 5;
    //                SVGContent += "<text x=\"0\" y=\"" + cursorHeight + "\" font-family=\"" + Options.Typeface + "\" font-size=\"12\" text-anchor=\"left\">Tuning: " + _s.Where(x => x.Key == (int)GuitarSettingsEnum.Tuning).First().Value + "</text>";
    //                cursorHeight += 15;
    //                svgHeight += 15;
    //            }

    //            // Accord d'en-tête

    //            if (Options.DisplayChordsHeader && _s.Where(x => x.Key == (int)GuitarSettingsEnum.Chords).FirstOrDefault().Value != null)
    //            {
    //                if (_s.Where(x => x.Key == (int)GuitarSettingsEnum.Chords).FirstOrDefault().Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).Length > 0)
    //                {
    //                    string[] _accords = _s.Where(x => x.Key == (int)GuitarSettingsEnum.Chords).FirstOrDefault().Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
    //                    if (_accords != null && _accords.Count() > 0)
    //                    {
    //                        cursorWith = 0;
    //                        cursorHeight += 10;
    //                        svgHeight += 10;

    //                        int i = 0;
    //                        foreach (string s in _accords)
    //                        {
    //                            cursorWith += 5;

    //                            if (i == 0)
    //                            {
    //                                // New line
    //                            }

    //                            string chordSVG = string.Empty;

    //                            if (GuitarChordRenderingBuilderService.DrawChord(_accords[i], out chordSVG, cursorWidth: cursorWith, cursorHeight: cursorHeight))
    //                                SVGContent += chordSVG;

    //                            cursorWith += GuitarChordRenderingBuilderService.GetWidth() + 10;

    //                            chordSVG = null;

    //                            if ((Options.Width - cursorWith) < GuitarChordRenderingBuilderService.GetWidth() + 10)
    //                            {
    //                                // New Line

    //                                i = 0;
    //                                cursorHeight += GuitarChordRenderingBuilderService.GetHeight();
    //                                svgHeight += GuitarChordRenderingBuilderService.GetHeight();
    //                                cursorWith = 0;
    //                            }
    //                            else
    //                            {
    //                                i++;
    //                            }
    //                        }

    //                        // On passe la ligne en cours

    //                        cursorHeight += GuitarChordRenderingBuilderService.GetHeight();
    //                        svgHeight += GuitarChordRenderingBuilderService.GetHeight();
    //                        cursorWith = 0;
    //                    }
    //                }
    //            }

    //            // contenu tab

    //            cursorWith = 0;

    //            foreach (Partie part in Tablature.Parties)
    //            {
    //                if (!string.IsNullOrWhiteSpace(Tablature.GetPartName(part.Id, Options.Culture)))
    //                {
    //                    cursorHeight += 30;
    //                    SVGContent += "<text x=\"0\"  y=\"" + cursorHeight + "\" font-family=\"" + Options.Typeface + "\" font-size=\"15\">" + Tablature.GetPartName(part.Id, Options.Culture) + "</text>";
    //                    cursorHeight += 15;
    //                    cursorHeight += 5;
    //                    svgHeight += 50;
    //                }

    //                // On crée une ligne vide de tab
    //                CreateNewLine();

    //                // Et on mets les notes

    //                int iMesures = 0;

    //                for (int i = 0; i < part.Mesures.Count; i++)
    //                {
    //                    iMesures++;

    //                    foreach (Temps tmp in part.Mesures[i].Instruments.Where(x => x.Instrument == InstrumentEnum.Guitar).First().Temps)
    //                    {
    //                        foreach (Son s in tmp.Sons)
    //                        {
    //                            if (s.Type == TypeSonEnum.Note)
    //                                CreateNote(s.Corde, s.Position);
    //                            else if (s.Type == TypeSonEnum.Accord)
    //                                CreateChord(s.Chord, s.SensGrattageCode);
    //                        }
    //                    }

    //                    if (iMesures < part.Mesures.Count)
    //                    {
    //                        int nbNotesNextMesure = 0;
    //                        if (part.Mesures[i + 1] != null && part.Mesures[i + 1].Instruments.Where(x => x.Instrument == InstrumentEnum.Guitar).First().Temps != null && part.Mesures[i + 1].Instruments.Where(x => x.Instrument == InstrumentEnum.Guitar).First().Temps.Count > 0)
    //                        {
    //                            part.Mesures[i + 1].Instruments.Where(x => x.Instrument == InstrumentEnum.Guitar).First().Temps.ForEach(delegate (Temps t)
    //                            {
    //                                nbNotesNextMesure += t.Sons != null ? t.Sons.Count() : 0;
    //                            });
    //                        }

    //                        if (cursorWith < (Options.Width - (20 + nbNotesNextMesure * 20)))
    //                            CreateVerticalLine();
    //                        else
    //                        {
    //                            cursorHeight += Options.StringSpacing * 6 + 20;
    //                            CreateNewLine();
    //                        }
    //                    }
    //                }

    //                // on mets à jour la hauteur du svg

    //                cursorHeight += Options.StringSpacing * 5;
    //                svgHeight += cursorHeight;
    //            }

    //            // Response

    //            SVGContent = "<svg width=\"" + Options.Width + "\" height=\"" + (svgHeight + 20) + "\">" + SVGContent + "</svg>";

    //            ret = SVGContent;

    //            return true;
    //        }
    //        catch (Exception)
    //        {
    //            ret = null;
    //            throw;
    //        }
    //        finally
    //        {
    //            SVGContent = null;
    //        }
    //    }

    //    private void CreateChord(string chord, int? sensGrattageCode)
    //    {
    //        string[] chordComp = chord.Split(new char[] { '|' }, StringSplitOptions.None);
    //        if (chordComp.Length != 6)
    //            throw new Exception();

    //        chordComp = chordComp.Reverse().ToArray();

    //        int yPosi = cursorHeight;
    //        cursorWith += 10;

    //        if (sensGrattageCode.HasValue)
    //        {
    //            if ((SensGrattageCordes)sensGrattageCode.Value == SensGrattageCordes.Down)
    //            {
    //                int startPosi = yPosi;
    //                int endPosi = yPosi;

    //                List<int> cordesJouees = new List<int>();
    //                for (int i = 0; i < 5; i++)
    //                {
    //                    if (!string.IsNullOrWhiteSpace(chordComp[i]))
    //                        cordesJouees.Add(i);
    //                }

    //                startPosi += cordesJouees.Max() * 20;
    //                endPosi += cordesJouees.Min() * 20;

    //                SVGContent += "<line x1=\"" + cursorWith + "\" y1=\"" + (startPosi + 8) + "\" x2=\"" + cursorWith + "\" y2=\"" + (endPosi - 8) + "\" stroke-width=\"1\" stroke=\"black\"/>";
    //                SVGContent += "<line x1=\"" + cursorWith + "\" y1=\"" + (endPosi - 8) + "\" x2=\"" + (cursorWith - 4) + "\" y2=\"" + (endPosi + 6) + "\" stroke-width=\"1\" stroke=\"black\"/>";
    //                SVGContent += "<line x1=\"" + cursorWith + "\" y1=\"" + (endPosi - 8) + "\" x2=\"" + (cursorWith + 4) + "\" y2=\"" + (endPosi + 6) + "\" stroke-width=\"1\" stroke=\"black\"/>";
    //                //cf http://vanseodesign.com/web-design/svg-markers/
    //            }
    //            else if ((SensGrattageCordes)sensGrattageCode.Value == SensGrattageCordes.Up)
    //            {

    //            }

    //            cursorWith += 5;
    //        }

    //        for (int i = 0; i < 5; i++)
    //        {
    //            if (i > 0)
    //                yPosi += 20;

    //            if (string.IsNullOrWhiteSpace(chordComp[i]))
    //                continue;

    //            SVGContent += "<circle cx=\"" + (cursorWith + 8) + "\" cy=\"" + yPosi + "\" r=\"8\" stroke=\"rgb(255, 255, 255)\" stroke-width=\"0\" fill=\"rgb(255, 255, 255)\" /><text x=\"" + (cursorWith + 4) + "\" y=\"" + (yPosi + 4) + "\" font-family=\"" + Options.Typeface + "\" font-size=\"15\" fill=\"black\">" + chordComp[i] + "</text>";
    //        }
    //    }

    //    private void CreateNote(int corde, int posi)
    //    {
    //        int yPosi = cursorHeight;
    //        yPosi += (6 - corde) * 20;
    //        SVGContent += "<circle cx=\"" + (cursorWith + 8) + "\" cy=\"" + yPosi + "\" r=\"8\" stroke=\"rgb(255, 255, 255)\" stroke-width=\"0\" fill=\"rgb(255, 255, 255)\" /><text x=\"" + (cursorWith + 4) + "\" y=\"" + (yPosi + 4) + "\" font-family=\"" + Options.Typeface + "\" font-size=\"15\" fill=\"black\">" + posi + "</text>";
    //        cursorWith += 25;
    //    }

    //    private void CreateVerticalLine()
    //    {
    //        SVGContent += "<line x1=\"" + (cursorWith + 7) + "\" y1=\"" + cursorHeight + "\" x2=\"" + (cursorWith + 7) + "\" y2=\"" + (cursorHeight + (Options.StringSpacing * 5)) + "\" stroke=\"" + Options.StringColor + "\" stroke-width=\"" + Options.StringWidth + "\" fill=\"" + Options.StringColor + "\"></line>";
    //        cursorWith += 14;
    //    }

    //    /// <summary>
    //    /// On crée une nouvelle ligne vide (cordes + inscription TAB etc, sans aucune note)
    //    /// </summary>
    //    /// <returns></returns>
    //    private void CreateNewLine()
    //    {
    //        cursorWith = 0;

    //        // ligne début tab

    //        SVGContent += "<line x1=\"0\" y1=\"" + cursorHeight + "\" x2=\"0\" y2=\"" + (cursorHeight + (Options.StringSpacing * 5)) + "\" stroke=\"" + Options.StringColor + "\" stroke-width=\"" + Options.StringWidth + "\" fill=\"" + Options.StringColor + "\"></line>";

    //        // cordes guitare

    //        for (int i = 0; i < 6; i++)
    //            SVGContent += "<line x1=\"0\" y1=\"" + (cursorHeight + (Options.StringSpacing * i)) + "\" x2=\"100%\" y2=\"" + (cursorHeight + (Options.StringSpacing * i)) + "\" stroke=\"" + Options.StringColor + "\" stroke-width=\"" + Options.StringWidth + "\" fill=\"" + Options.StringColor + "\"></line>";

    //        // inscription "TAB"

    //        SVGContent += "<text x=\"10\" y=\"" + (cursorHeight + Options.StringSpacing + 15) + "\" text-anchor=\"middle\" font-family=\"" + Options.Typeface + "\" font-size=\"11\" stroke=\"" + Options.StringColor + "\" fill=\"" + Options.StringColor + "\">T</text>";
    //        SVGContent += "<text x=\"10\" y=\"" + (cursorHeight + (Options.StringSpacing * 2) + 15) + "\" text-anchor=\"middle\" font-family=\"" + Options.Typeface + "\" font-size=\"11\" stroke=\"" + Options.StringColor + "\" fill=\"" + Options.StringColor + "\">A</text>";
    //        SVGContent += "<text x=\"10\" y=\"" + (cursorHeight + (Options.StringSpacing * 3) + 15) + "\" text-anchor=\"middle\" font-family=\"" + Options.Typeface + "\" font-size=\"11\" stroke=\"" + Options.StringColor + "\" fill=\"" + Options.StringColor + "\">B</text>";

    //        cursorWith += 50; // taille de tab plus un peu d'espace

    //        // ligne début tab

    //        SVGContent += "<line x1=\"100%\" y1=\"" + cursorHeight + "\" x2=\"100%\" y2=\"" + (cursorHeight + (Options.StringSpacing * 5)) + "\" stroke=\"" + Options.StringColor + "\" stroke-width=\"" + Options.StringWidth + "\" fill=\"" + Options.StringColor + "\"></line>";

    //    }
    //}
}