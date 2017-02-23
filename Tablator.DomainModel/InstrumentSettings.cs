namespace Tablator.DomainModel
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Element de configuration de l'instrument (ex: capodastre, tuning, ...)
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class InstrumentSettings
    {
        /// <summary>
        /// Code de l'élément de configuration (enums)
        /// </summary>
        /// <example>1 equals 'Tuning'</example>
        [JsonProperty(PropertyName = "cod")]
        public int Code { get; set; }

        /// <summary>
        /// Valeur de configuration
        /// </summary>
        /// <example>5 means capodastre on the fifth fret if the code equals 'capodastre'</example>
        [JsonProperty(PropertyName = "val")]
        public string Value { get; set; }
    }
}