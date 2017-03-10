namespace Tablator.DomainModel
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Effet appliqué à l'instrument (distortion, reverb, ...)
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class InstrumentEffect
    {
        /// <summary>
        /// Code de l'effet (enums)
        /// </summary>
        /// <example>1 equals 'Distortion'</example>
        [JsonProperty(PropertyName = "cod")]
        public int Code { get; set; }

        /// <summary>
        /// Niveau/puissance de l'effet (enum)
        /// </summary>
        /// <example>1 means 'high' if the code equals 'distortion'</example>
        [JsonProperty(PropertyName = "lvl")]
        public int Level { get; set; }
    }
}