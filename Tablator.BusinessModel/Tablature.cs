namespace Tablator.BusinessModel
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Tablator.Infrastructure.Enumerations;

    [JsonObject(MemberSerialization.OptIn)]
    public class Tablature
    {
        // Moved to guitar settings
        //[JsonProperty(PropertyName = "capodastre")]
        //public int Capodastre { get; set; }

        [JsonProperty(PropertyName = "song")]
        public string SongName { get; set; }

        [JsonProperty(PropertyName = "artist")]
        public string ArtistName { get; set; }

        [JsonProperty(PropertyName = "tempo")]
        public int Tempo { get; set; }

        // Moved to guitar settings
        //[JsonProperty(PropertyName = "tuning")]
        //public string Tuning { get; set; }

        [JsonProperty(PropertyName = "enchainement")]
        public List<EnchainementItem> Enchainement { get; set; }

        [JsonProperty(PropertyName = "parties")]
        public List<Partie> Parties { get; set; }

        // Moved to guitar settings
        //[JsonProperty(PropertyName = "chords")]
        //public List<string> Accords { get; set; }

        [JsonProperty(PropertyName = "languages")]
        public List<Language> Languages { get; set; }

        [JsonProperty(PropertyName = "instruments")]
        public List<InstrumentPart> Instruments { get; set; }

        [JsonProperty(PropertyName = "src")]
        public TablatureSource Source { get; set; }

        public string GetPartName(int id, CultureInfo ci) => Languages.Where(x => x.LangCode == ci.TwoLetterISOLanguageName).FirstOrDefault()?.Content?.Where(x => x.Fieldcode == (int)LanguageContentItemPropertyEnum.Nom && x.Typecode == (int)LanguageContentItemEnum.Partie && x.Id == id).Select(x => x.Content).FirstOrDefault();
    }

    public class InstrumentPart
    {
        [JsonProperty(PropertyName = "instrument")]
        public int Code { get; set; }

        public InstrumentEnum Instrument => (InstrumentEnum)Code;

        [JsonProperty(PropertyName = "settings")]
        public List<InstrumentPartSettingsItem> SettingsEntries { get; set; }

        public Dictionary<int, string> Settings => SettingsEntries.Select(x => new { x.Code, x.Val }).ToDictionary(t => t.Code, t => t.Val);
    }

    public class InstrumentPartSettingsItem
    {
        [JsonProperty(PropertyName = "cod")]
        public int Code { get; set; }

        [JsonProperty(PropertyName = "val")]
        public string Val { get; set; }
    }

    public class TablatureSource
    {
        [JsonProperty(PropertyName = "type")]
        public int TypeCode { get; set; }

        public TablatureSourceTypeEnum Type => (TablatureSourceTypeEnum)TypeCode;

        [JsonProperty(PropertyName = "support")]
        public int SupportCode { get; set; }

        public TablatureSourceSupportEnum Support => (TablatureSourceSupportEnum)SupportCode;

        /// <summary>
        /// Nom de la source, donné par l'auteur
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "val")]
        public string Value { get; set; }

        /// <summary>
        /// Description de la source par l'auteur de la tab
        /// </summary>
        [JsonProperty(PropertyName = "desc")]
        public string Description { get; set; }
    }
}