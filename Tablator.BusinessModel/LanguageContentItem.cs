namespace Tablator.BusinessModel
{
    using Newtonsoft.Json;
    using Tablator.Infrastructure.Enumerations;

    [JsonObject(MemberSerialization.OptIn)]
    public class LanguageContentItem
    {
        [JsonProperty(PropertyName = "type")]
        public int Typecode { get; set; }

        public LanguageContentItemEnum Type => (LanguageContentItemEnum)Typecode;

        [JsonProperty(PropertyName = "field")]
        public int Fieldcode { get; set; }

        public LanguageContentItemPropertyEnum Field => (LanguageContentItemPropertyEnum)Fieldcode;

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }
    }
}