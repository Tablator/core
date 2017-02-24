namespace Tablator.DomainModel
{
    using Newtonsoft.Json;
    using System;
    using Tablator.Infrastructure.DomainModel.Constants;

    /// <summary>
    /// Version du format de la tablature
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class SoftwareVersion
    {
        /// <summary>
        /// Date du versionning de la tab (création ou mise à jour format)
        /// </summary>
        [JsonProperty(PropertyName = SoftwareVersionSerializationConstants.Date)]
        public DateTime Date { get; set; }

        /// <summary>
        /// Major version number
        /// </summary>
        [JsonProperty(PropertyName = SoftwareVersionSerializationConstants.Major)]
        public int Major { get; set; }

        /// <summary>
        /// Minor version number
        /// </summary>
        [JsonProperty(PropertyName = SoftwareVersionSerializationConstants.Minor)]
        public int Minor { get; set; }

        /// <summary>
        /// Revision number
        /// </summary>
        [JsonProperty(PropertyName = SoftwareVersionSerializationConstants.Revision)]
        public int Revision { get; set; }
    }
}