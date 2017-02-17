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
        [JsonProperty(PropertyName = "capodastre")]
        public int Capodastre { get; set; }

        [JsonProperty(PropertyName = "song")]
        public string SongName { get; set; }

        [JsonProperty(PropertyName = "artist")]
        public string ArtistName { get; set; }

        [JsonProperty(PropertyName = "tempo")]
        public int Tempo { get; set; }

        [JsonProperty(PropertyName = "tuning")]
        public string Tuning { get; set; }

        [JsonProperty(PropertyName = "enchainement")]
        public List<EnchainementItem> Enchainement { get; set; }

        [JsonProperty(PropertyName = "parties")]
        public List<Partie> Parties { get; set; }

        [JsonProperty(PropertyName = "chords")]
        public List<string> Accords { get; set; }

        [JsonProperty(PropertyName = "languages")]
        public List<Language> Languages { get; set; }

        public string GetPartName(int id, CultureInfo ci) => Languages.Where(x => x.LangCode == ci.TwoLetterISOLanguageName).FirstOrDefault()?.Content?.Where(x => x.Fieldcode == (int)LanguageContentItemPropertyEnum.Nom && x.Typecode == (int)LanguageContentItemEnum.Partie && x.Id == id).Select(x => x.Content).FirstOrDefault();
    }
}