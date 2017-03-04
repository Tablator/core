namespace Tablator.DomainModel
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// Section de configuration d'un instrument (effets, propriétés, ...)
    /// </summary>
    public sealed class InstrumentConfiguationSection
    {
        [JsonProperty(PropertyName = "cod")]
        public int Code { get; set; }

        [JsonProperty(PropertyName = "pprts")]
        public IEnumerable<InstrumentConfiguationSectionProperty> Settings { get; set; }
    }
}