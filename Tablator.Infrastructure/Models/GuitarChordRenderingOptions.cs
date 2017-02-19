namespace Tablator.Infrastructure.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public sealed class GuitarChordRenderingOptions
    {
        public int Width { get; } = 130;

        public int Height { get; }

        public bool DisplayLabel { get; } = true;

        /// <summary>
        /// Affiche les numéros de fret dans les positions des doigts
        /// </summary>
        public bool DisplayFretNumberInFingerPositions { get; } = true;

        /// <summary>
        /// Couleur des cordes et des frets
        /// </summary>
        public string StringColor { get; set; } = "rgb(20, 20, 20)";

        public string FingerPositionBackgroundColor { get; set; } = "rgb(30, 30, 30)";

        public string FingerPositionTextColor { get; set; } = "rgb(255, 255, 255)";

        /// <summary>
        /// Couleur des cercles indiquant qu'une corde vide doit être jouée
        /// </summary>
        public string PlayedFreeStringColor { get; set; } = "rgb(255, 255, 255)";

        /// <summary>
        /// Couleur du cigle indiquant qu'une corde vide ne doit pas être jouée
        /// </summary>
        public string MutedFreeStringColor { get; set; } = "rgb(0, 0, 0)";

        public string Typeface { get; set; } = "Verdana";

        public GuitarChordRenderingOptions()
        {
            Height = Convert.ToInt32(Width * 1.4);
        }
    }
}