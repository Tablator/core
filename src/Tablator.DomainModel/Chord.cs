namespace Tablator.DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class Chord
    {
        [JsonProperty(PropertyName = "attrs")]
        public IEnumerable<ChordAttribute> Attributes { get; set; }

        [JsonProperty(PropertyName = "compositions")]
        public IEnumerable<ChordComposition> Compositions { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class ChordComposition
    {
        [JsonProperty(PropertyName = "attrs")]
        public IEnumerable<ChordCompositionAttribute> Attributes { get; set; }
    }

    public class ChordAttribute : BasePropertyItem
    {

    }

    public class ChordCompositionAttribute : BasePropertyItem
    {

    }
}