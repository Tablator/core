using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Tablator.DomainModel
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GuitarChord
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "short_name")]
        public string ShortName { get; set; }

        [JsonProperty(PropertyName = "compositions")]
        public IEnumerable<GuitarChordComposition> Compositions { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class GuitarChordComposition
    {
        [JsonProperty(PropertyName = "composition")]
        public string Composition { get; set; }
    }
}