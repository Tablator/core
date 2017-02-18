namespace Tablator.BusinessModel
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using Tablator.Infrastructure.Constants;

    [JsonObject(MemberSerialization.OptIn)]
    public class Chord
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; }

        [JsonProperty(PropertyName = "key")]
        public char Key { get; }

        [JsonProperty(PropertyName = "positions")]
        public Dictionary<int, int?> Positions { get; }

        public virtual string Composition { get; }
    }

    public class ChordCollection : List<Chord>
    {
        public ChordCollection()
        { }
    }
}