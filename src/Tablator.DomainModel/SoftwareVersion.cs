namespace Tablator.DomainModel
{
    using Newtonsoft.Json;
    using static Tablator.Infrastructure.DomainModel.Constants.TablatureSerializationConstants.SoftwareVersionConstants;

    /// <summary>
    /// Version du format de la tablature
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class SoftwareVersion
    {
        /// <summary>
        /// Major version number
        /// </summary>
        [JsonProperty(PropertyName = SectionPropertiesConstants.Major)]
        public int Major { get; set; }

        /// <summary>
        /// Minor version number
        /// </summary>
        [JsonProperty(PropertyName = SectionPropertiesConstants.Minor)]
        public int Minor { get; set; }

        /// <summary>
        /// Revision number
        /// </summary>
        [JsonProperty(PropertyName = SectionPropertiesConstants.Revision)]
        public int Revision { get; set; }
    }
}