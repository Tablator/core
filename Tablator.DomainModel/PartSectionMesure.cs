namespace Tablator.DomainModel
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Une mesure d'une section (mesure de couplet, mesure de refrain, ...)
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class PartSectionMesure
    {
        [JsonProperty(PropertyName = "temps")]
        public IEnumerable<PartSectionMesureTemps> Temps { get; set; }

        [JsonProperty(PropertyName = "properties")]
        public IEnumerable<PartSectionMesureProperty> Properties { get; set; }
    }
}