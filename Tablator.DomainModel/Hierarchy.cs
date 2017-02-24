﻿namespace Tablator.DomainModel
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Catalog's hierarchy level
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    [Obsolete("use CatalogHierarchyLevel", false)]
    public class Hierarchy
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "desc")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "pic")]
        public string Picture { get; set; }

        [JsonProperty(PropertyName = "pid")]
        public Guid? ParentId { get; set; }

        [JsonProperty(PropertyName = "posi")]
        public int Position { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    [Obsolete("use CatalogHierarchyCollectionLevel", false)]
    public class Catalog
    {
        [JsonProperty(PropertyName = "hrrch")]
        public List<Hierarchy> Hierarchy { get; set; }
    }
}