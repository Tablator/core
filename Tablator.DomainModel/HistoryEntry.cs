namespace Tablator.DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Newtonsoft.Json;

    /// <summary>
    /// Entrée d'historique de la tab (création, modification, migration, ....)
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class HistoryEntry
    {
        /// <summary>
        /// Date de la modification
        /// </summary>
        [JsonProperty(PropertyName = "dt")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Action effectuée => correspond à un verbe (création, modification ...)
        /// </summary>
        [JsonProperty(PropertyName = "actn")]
        public int Action { get; set; }

        /// <summary>
        /// Modification manuelle ou automatique (comme une migration)
        /// </summary>
        [JsonProperty(PropertyName = "mnl")]
        public bool Manual { get; set; }

        [JsonProperty(PropertyName = "cmnt")]
        public string Comment { get; set; }

        /// <summary>
        /// Informations au sujet de la personne ayant fait cette modification, si elle a souhaité affiché ses infos et s'il ne s'agit pas d'une modif automatique
        /// </summary>
        [JsonProperty(PropertyName = "usr")]
        public HistoryEntryUserInfo User { get; set; }
    }

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