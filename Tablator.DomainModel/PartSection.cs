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

    [JsonObject(MemberSerialization.OptIn)]
    public sealed class PartSectionMesureTemps
    {
        /// <summary>
        /// Nombre de temps de cette partie de mesure
        /// </summary>
        /// <remarks>Le plus souvent 1, mais peu être 2 si c'est une blanque par exemple, ou même 4 pour une ronde</remarks>
        [JsonProperty(PropertyName = "nbTemps")]
        public int Count { get; set; }

        /// <summary>
        /// Elements (Notes, accords, silences, ...)
        /// </summary>
        [JsonProperty(PropertyName = "sons")]
        public List<PartSectionMesureTempsItem> Sons { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public sealed class PartSectionMesureProperty : BasePropertyItem
    {
    }

    /// <summary>
    /// Element composant un temps (Notes, accords, silences, ...)
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class PartSectionMesureTempsItem
    {
        [JsonProperty(PropertyName = "properties")]
        public IEnumerable<PartSectionMesureTempsItemProperty> Properties { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public sealed class PartSectionMesureTempsItemProperty : BasePropertyItem
    {
    }

    [JsonObject(MemberSerialization.OptIn)]
    public abstract class BasePropertyItem
    {
        [JsonProperty(PropertyName = "cod")]
        public int Code { get; set; }

        [JsonProperty(PropertyName = "val")]
        public string Value { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public sealed class TablatureProperty : BasePropertyItem
    {
    }
}