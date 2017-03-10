namespace Tablator.DomainModel
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Element composant un temps (Notes, accords, silences, ...)
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class PartSectionMesureTempsItem
    {
        [JsonProperty(PropertyName = "properties")]
        public IEnumerable<PartSectionMesureTempsItemProperty> Properties { get; set; }
    }
}