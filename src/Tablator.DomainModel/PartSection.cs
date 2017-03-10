namespace Tablator.DomainModel
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Element de la structure d'une chanson (ex: bridge, intro, verse, chorus, ...)
    /// </summary>
    /// <see cref="https://en.wikipedia.org/wiki/Song_structure#Elements"/>
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class PartSection
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "mesures")]
        public IEnumerable<PartSectionMesure> Mesures { get; set; }
    }
}