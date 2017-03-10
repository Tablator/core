namespace Tablator.DomainModel
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    [JsonObject(MemberSerialization.OptIn)]
    public sealed class PartSectionMesureTemps
    {
        /// <summary>
        /// Nombre de temps de cette partie de mesure
        /// </summary>
        /// <remarks>Le plus souvent 1, mais peu être 2 si c'est une blanque par exemple, ou même 4 pour une ronde</remarks>
        [JsonProperty(PropertyName = "nbTemps")]
        public int Count { get; set; }

        /// <summary>
        /// Elements (Notes, accords, silences, ...)
        /// </summary>
        [JsonProperty(PropertyName = "sons")]
        public IEnumerable<PartSectionMesureTempsItem> Sons { get; set; }
    }
}