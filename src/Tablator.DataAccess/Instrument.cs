namespace Tablator.DomainModel
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// Détails et configuration de l'instrument
    /// </summary>
    public sealed class Instrument
    {
        /// <summary>
        /// Instrument de la tablature (guitar, bass guitar, banjo, ...)
        /// </summary>
        [JsonProperty(PropertyName = "cod")]
        public int Code { get; set; }

        /// <summary>
        /// Type d'instrument (acoustic guitar, banjo 5 strings, electro-acoustic guitar, ...)
        /// </summary>
        [JsonProperty(PropertyName = "typ")]
        public int? InstrumentType { get; set; }

        /// <summary>
        /// Sections de confiruation de l'instrument (propriétés, effets, ...)
        /// </summary>
        [JsonProperty(PropertyName = "conf")]
        public IEnumerable<InstrumentConfiguationSection> ConfigurationSections { get; set; }
    }
}