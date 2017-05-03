﻿namespace Tablator.DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class GuitarChord
    {
        [JsonProperty(PropertyName = "attrs")]
        public IEnumerable<GuitarChordAttribute> Attributes { get; set; }

        [JsonProperty(PropertyName = "compositions")]
        public IEnumerable<GuitarChordComposition> Compositions { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class GuitarChordComposition
    {
        [JsonProperty(PropertyName = "attrs")]
        public IEnumerable<GuitarChordCompositionAttribute> Attributes { get; set; }
    }

    public class GuitarChordAttribute : BasePropertyItem
    {

    }

    public class GuitarChordCompositionAttribute : BasePropertyItem
    {

    }
}