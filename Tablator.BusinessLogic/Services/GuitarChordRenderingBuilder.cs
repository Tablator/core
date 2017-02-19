namespace Tablator.BusinessLogic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Tablator.Infrastructure.Constants;
    using Tablator.Infrastructure.Enumerations;
    using Tablator.Infrastructure.Extensions;
    using Tablator.Infrastructure.Models;

    public class GuitarChordRenderingBuilderService : IGuitarChordRenderingBuilderService
    {
        public GuitarChordRenderingOptions Options { get; private set; }

        private string SVGContent { get; set; } = string.Empty;

        public GuitarChordRenderingBuilderService()
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
                switch (chord)
                {
                    case "eeer":
                        // Capo
                        // Finger positions
                        // Free strings
                        return false;
                    case "C":
                        AddFingersPositions(GuitarChordEnum.C.GetDisplayDescription(), cursorWidth, cursorHeight);
                        break;
                    case "A":
                        AddFingersPositions(GuitarChordEnum.A.GetDisplayDescription(), cursorWidth, cursorHeight);
                        break;
                    case "G":
                        AddFingersPositions(GuitarChordEnum.G.GetDisplayDescription(), cursorWidth, cursorHeight);
                        break;
                    case "Am":
                        AddFingersPositions(GuitarChordEnum.Am.GetDisplayDescription(), cursorWidth, cursorHeight);
                        break;
                    default:
                        // Fonctionne mais statique

                        // Capo
                        // Finger positions
                        //if (Options.DisplayFretNumberInFingerPositions)
                        //    SVGContent += "<g><circle cx=\"" + (cursorWidth + 66) + "\" cy=\"" + (cursorHeight + 40) + "\" r=\"8\" stroke=\"" + Options.FingerPositionBackgroundColor + "\" stroke-width=\"0\" fill=\"" + Options.FingerPositionBackgroundColor + "\" /><text x=\"" + (cursorWidth + 66) + "\" y=\"" + (cursorHeight + 44) + "\" font-family=\"" + Options.Typeface + "\" font-size=\"12\" fill=\"" + Options.FingerPositionTextColor + "\" text-anchor=\"middle\">1</text></g>";
                        //else
                        //    SVGContent += "<circle cx=\"" + (cursorWidth + 66) + "\" cy=\"" + (cursorHeight + 40) + "\" r=\"8\" stroke=\"" + Options.FingerPositionBackgroundColor + "\" stroke-width=\"0\" fill=\"" + Options.FingerPositionBackgroundColor + "\" />";
                        //SVGContent += "<g><circle cx=\"" + (cursorWidth + 45) + "\" cy=\"" + (cursorHeight + 70) + "\" r=\"8\" stroke=\"" + Options.FingerPositionBackgroundColor + "\" stroke-width=\"0\" fill =\"" + Options.FingerPositionBackgroundColor + "\" /><text x=\"" + (cursorWidth + 45) + "\" y=\"" + (cursorHeight + 74) + "\" font-family=\"" + Options.Typeface + "\" font-size=\"12\" fill=\"" + Options.FingerPositionTextColor + "\" text-anchor=\"middle\">2</text></g>";
                        //SVGContent += "<g><circle cx=\"" + (cursorWidth + 25) + "\" cy=\"" + (cursorHeight + 70) + "\" r=\"8\" stroke=\"" + Options.FingerPositionBackgroundColor + "\" stroke-width=\"0\" fill=\"" + Options.FingerPositionBackgroundColor + "\" /><text x=\"" + (cursorWidth + 25) + "\" y=\"" + (cursorHeight + 74) + "\" font-family=\"" + Options.Typeface + "\" font-size=\"12\" fill=\"" + Options.FingerPositionTextColor + "\" text-anchor=\"middle\">2</text></g>";
                        //// Free strings
                        //SVGContent += "<circle cx=\"" + (cursorWidth + 85) + "\" cy=\"" + (cursorHeight + 15) + "\" r=\"5\" stroke=\"" + Options.FingerPositionBackgroundColor + "\" stroke-width=\"1\" fill=\"" + Options.PlayedFreeStringColor + "\" />";
                        //SVGContent += "<circle cx=\"" + (cursorWidth + 105) + "\" cy=\"" + (cursorHeight + 15) + "\" r=\"5\" stroke=\"" + Options.FingerPositionBackgroundColor + "\" stroke-width=\"1\" fill=\"" + Options.PlayedFreeStringColor + "\" />";
                        //SVGContent += "<text x=\"" + (cursorWidth + 2) + "\" y=\"" + (cursorHeight + 17) + "\" font-family=\"" + Options.Typeface + "\" font-size=\"13\" fill =\"" + Options.MutedFreeStringColor + "\" text-anchor=\"start\">x</text>";

                        // Fonctionne et dynamique
                        AddFingersPositions(GuitarChordEnum.A.GetDisplayDescription(), cursorWidth, cursorHeight);

                        break;
                        //default:
                        //    // Capo
                        //    // Finger positions
                        //    // Free strings
                        //    return false;
                }

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