namespace Tablator.BusinessModel
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Mesure
    /// </summary>
    /// <see cref="https://fr.wikipedia.org/wiki/Mesure_(notation_musicale)"/>
    [JsonObject(MemberSerialization.OptIn)]
    public class Mesure
    {
        [JsonProperty(PropertyName = "temps")]
        public List<Temps> Temps { get; set; }

        /// <summary>
        /// Liste des accords utilisés dans ce temps
        /// </summary>
        public List<string> ChordList
        {
            get
            {
                if (Temps == null)
                    return new List<string>();

                if (Temps.Count == 0)
                    return new List<string>();

                return Temps.Select(x => x.Accord).Distinct().ToList();
            }
        }
    }
}