namespace Tablator.DomainModel
{
    using Newtonsoft.Json;

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