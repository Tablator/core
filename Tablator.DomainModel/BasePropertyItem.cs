namespace Tablator.DomainModel
{
    using Newtonsoft.Json;
    using Tablator.Infrastructure.DomainModel.Constants;

    [JsonObject(MemberSerialization.OptIn)]
    public abstract class BasePropertyItem
    {
        [JsonProperty(PropertyName = BasePropertyItemSerializationConstants.Code)]
        public int Code { get; set; }

        [JsonProperty(PropertyName = BasePropertyItemSerializationConstants.Value)]
        public string Value { get; set; }
    }
}