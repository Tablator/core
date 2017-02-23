namespace Tablator.DomainModel
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public abstract class BasePropertyItem
    {
        [JsonProperty(PropertyName = "cod")]
        public int Code { get; set; }

        [JsonProperty(PropertyName = "val")]
        public string Value { get; set; }
    }
}