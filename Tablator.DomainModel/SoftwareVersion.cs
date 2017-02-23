namespace Tablator.DomainModel
{
    using Newtonsoft.Json;
    using System;
    
    /// <summary>
    /// Version du format de la tablature
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class SoftwareVersion
    {
        /// <summary>
        /// Date du versionning de la tab (création ou mise à jour format)
        /// </summary>
        [JsonProperty(PropertyName = "dt")]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "mjr")]
        public int Major { get; set; }

        [JsonProperty(PropertyName = "mnr")]
        public int Minor { get; set; }

        [JsonProperty(PropertyName = "rev")]
        public int Revision { get; set; }
    }
}