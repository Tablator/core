namespace Tablator.BusinessModel
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class EnchainementItem
    {
        [JsonProperty(PropertyName = "id")]
        public int PartieId { get; set; }

        [JsonProperty(PropertyName = "repeat")]
        public int Repeat { get; set; }
    }
}