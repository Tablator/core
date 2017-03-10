namespace Tablator.DomainModel
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// Partie de la structure d'une tablature (ex: refrain x2, couplet x1, ...)
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class StructureSection
    {
        [JsonProperty(PropertyName = "id")]
        public Guid PartId { get; set; }

        [JsonProperty(PropertyName = "rpt")]
        public int Repeat { get; set; }
    }
}