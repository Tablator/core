namespace Tablator.DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class Chord
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "attrs")]
        public IEnumerable<ChordAttribute> Attributes { get; set; }

        [JsonProperty(PropertyName = "compositions")]
        public IEnumerable<ChordComposition> Compositions { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class ChordComposition
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "default")]
        public bool? IsDefault { get; set; }

        [JsonProperty(PropertyName = "attrs")]
        public IEnumerable<ChordCompositionAttribute> Attributes { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class ChordAttribute : BasePropertyItem
    {

    }

    [JsonObject(MemberSerialization.OptIn)]
    public class ChordCompositionAttribute : BasePropertyItem
    {

    }
}