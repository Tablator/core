namespace Tablator.BusinessModel
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// Temps
    /// </summary>
    /// <see cref="https://fr.wikipedia.org/wiki/Temps_(musique)"/>
    [JsonObject(MemberSerialization.OptIn)]
    public class Temps
    {
        [JsonProperty(PropertyName = "chord")]
        public string Accord { get; set; }

        /// <summary>
        /// Nombre de temps de cette partie de mesure
        /// </summary>
        /// <remarks>Le plus souvent 1, mais peu être 2 si c'est une blanque par exemple, ou même 4 pour une ronde</remarks>
        [JsonProperty(PropertyName = "nbTemps")]
        public int nbTemps { get; set; }

        [JsonProperty(PropertyName = "sons")]
        public List<Son> Sons { get; set; }
    }
}