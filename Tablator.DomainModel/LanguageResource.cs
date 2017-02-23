namespace Tablator.DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.Text;
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
    }

    [JsonObject(MemberSerialization.OptIn)]
    public sealed class LanguageResourceContentItem
    {
        [JsonProperty(PropertyName = "type")]
        public int Type { get; set; }

        [JsonProperty(PropertyName = "field")]
        public int Field { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }
    }
}