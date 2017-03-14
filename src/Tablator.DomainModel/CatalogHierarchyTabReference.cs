namespace Tablator.DomainModel
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Attache d'une tablature à un ou plusieurs parents
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class CatalogHierarchyTabReference
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "rlpth")]
        public string UrlPath { get; set; }

        [JsonProperty(PropertyName = "parents")]
        public IEnumerable<CatalogHierarchyTabParentReference> Parents { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public sealed class CatalogHierarchyTabParentReference
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "posi")]
        public int Position { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public sealed class CatalogHierarchyTabReferenceCollection
    {
        [JsonProperty(PropertyName = "hrrchrefs")]
        public IEnumerable<CatalogHierarchyTabReference> Refs { get; set; }
    }
}