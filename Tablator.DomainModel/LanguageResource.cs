namespace Tablator.DomainModel
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Elements de language d'une tab
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class LanguageResource
    {
        [JsonProperty(PropertyName = "lng")]
        public string Lang { get; set; }

        [JsonProperty(PropertyName = "cmt")]
        public string Comment { get; set; }

        [JsonProperty(PropertyName = "cnt")]
        public IEnumerable<LanguageResourceContentItem> Content { get; set; }

        [JsonProperty(PropertyName = "tags")]
        public IEnumerable<string> Tags { get; set; }
    }
}