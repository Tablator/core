namespace Tablator.Infrastructure.Models
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using Tablator.Infrastructure.Enumerations;

    public class TablatureRenderingOptions
    {
        public CultureInfo Culture { get; } = CultureInfo.CurrentUICulture;

        /// <summary>
        /// Affiche les accords composant la chanson en en-tête du document
        /// </summary>
        public bool DisplayChordsHeader { get; set; } = true;

        /// <summary>
        /// Affiche les accords le long de la tablature
        /// </summary>
        public bool DisplayTabChords { get; set; } = true;

        /// <summary>
        /// Affiche l'enchainement des parties
        /// </summary>
        public bool DisplayEnchainement { get; set; } = true;

        /// <summary>
        /// Mode d'affichage de l'enchainement
        /// true = détaillé (plusieurs lignes), false = simple
        /// </summary>
        public bool? AffichageEnchainementDetaille { get; set; } = null;

        /// <summary>
        /// Largeur du document
        /// </summary>
        public int Width { get; set; } = 890;

        public string StringColor { get; set; } = "rgb(20, 20, 20)";

        public int StringWidth { get; set; } = 1;

        public int StringSpacing { get; set; } = 20;

        public string Typeface { get; set; } = "Verdana";

        public TablatureRenderingOptions()
        {
        }
    }
}