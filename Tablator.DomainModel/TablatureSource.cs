namespace Tablator.DomainModel
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Source de la tablature (d'après un tuto youtube ? un livre ? une tab trouvée quelque part ? à l'oreille ?)
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class TablatureSource
    {
        [JsonProperty(PropertyName = "type")]
        public int Type { get; set; }

        [JsonProperty(PropertyName = "support")]
        public int Support { get; set; }

        /// <summary>
        /// Nom de la source, donné par l'auteur
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "val")]
        public string Value { get; set; }

        /// <summary>
        /// Description de la source par l'auteur de la tab
        /// </summary>
        [JsonProperty(PropertyName = "desc")]
        public string Description { get; set; }
    }
}