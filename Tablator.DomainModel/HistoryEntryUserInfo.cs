namespace Tablator.DomainModel
{
    using Newtonsoft.Json;

    /// <summary>
    /// Informations au sujet de la personne ayant fait cette modification, si elle a souhaité affiché ses infos et s'il ne s'agit pas d'une modif automatique
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class HistoryEntryUserInfo
    {
        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "mail")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "ip")]
        public string IpAddress { get; set; }
    }
}