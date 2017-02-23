namespace Tablator.DomainModel
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Format d'une tablature telle qu'elles sont stockées dans les fichiers
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class Tablature
    {
        /// <summary>
        /// Attributs de la tablature (nom de la chanson, ...)
        /// </summary>
        [JsonProperty(PropertyName = "pprts")]
        public IEnumerable<TablatureProperty> Properties { get; set; }

        /// <summary>
        /// Source de la tablature (d'après un tuto youtube ? un livre ? une tab trouvée quelque part ? à l'oreille ?)
        /// </summary>
        [JsonProperty(PropertyName = "src")]
        public TablatureSource Source { get; set; }

        /// <summary>
        /// Historique des versions de format de la tab
        /// </summary>
        [JsonProperty(PropertyName = "sftvrsns")]
        public IEnumerable<SoftwareVersion> Versions { get; set; }

        /// <summary>
        /// Configuration de l'instrument (tuning, capodastre, ...)
        /// </summary>
        [JsonProperty(PropertyName = "instrument_settings")]
        public IEnumerable<InstrumentSettings> InstrumentSettings { get; set; }

        /// <summary>
        /// Effets appliqués ('distorition', 'chorus', 'reverb', ...)
        /// </summary>
        [JsonProperty(PropertyName = "instrument_effects")]
        public IEnumerable<InstrumentEffect> InstrumentEffects { get; set; }

        /// <summary>
        /// Structure de la tablature
        /// </summary>
        /// <example>introx1, coupletx2, refrainx2, coupletx2, ...</example>
        [JsonProperty(PropertyName = "strctr")]
        public IEnumerable<StructureSection> Structure { get; set; }

        /// <summary>
        /// Détails des sections composant la tablature
        /// </summary>
        [JsonProperty(PropertyName = "prts")]
        public IEnumerable<PartSection> PartSections { get; set; }

        /// <summary>
        /// Elements de languages d'une tab (traductions, etc)
        /// </summary>
        [JsonProperty(PropertyName = "lngrsrcs")]
        public IEnumerable<LanguageResource> LanguageResources { get; set; }
    }
}