namespace Tablator.DomainModel
{
    using Newtonsoft.Json;

    /// <summary>
    /// WHAT? Storage format version
    /// WHY? To know in which format the tab was written and how to parse it
    /// HOW? We store the different versions on a two-number number, [Major].[Minor]. Each bug fixing or improvment increments, at least, the minor version.
    /// </summary>
    /// <remarks>
    /// It's required, each tab has to contain his format version
    /// </remarks>
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class StorageFormatVersion
    {
        /// <summary>
        /// Major version number
        /// </summary>
        [JsonProperty(PropertyName = "mjr")]
        public int Major { get; set; }

        /// <summary>
        /// Minor version number
        /// </summary>
        [JsonProperty(PropertyName = "mnr")]
        public int Minor { get; set; }
    }
}