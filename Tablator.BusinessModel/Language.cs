namespace Tablator.BusinessModel
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    [JsonObject(MemberSerialization.OptIn)]
    public class Language
    {
        [JsonProperty(PropertyName = "lang")]
        public string LangCode { get; set; }

        [JsonProperty(PropertyName = "comment")]
        public string Remark { get; set; }

        [JsonProperty(PropertyName = "content")]
        public List<LanguageContentItem> Content { get; set; }

        [JsonProperty(PropertyName = "tags")]
        public List<string> Tags { get; set; }
    }
}