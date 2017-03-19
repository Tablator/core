namespace Tablator.DomainModel
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Format d'une tablature telle qu'elles sont stockées dans les fichiers
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class Tablature
    {
        /// <summary>
        /// Attributs de la tablature (nom de la chanson, ...)
        /// </summary>
        [JsonProperty(PropertyName = "pprts")]
        public IEnumerable<TablatureProperty> Properties { get; set; }

        /// <summary>
        /// Source de la tablature (d'après un tuto youtube ? un livre ? une tab trouvée quelque part ? à l'oreille ?)
        /// </summary>
        [JsonProperty(PropertyName = "src")]
        public TablatureSource Source { get; set; }

        /// <summary>
        /// Tablature software version
        /// </summary>
        [JsonProperty(PropertyName = "sftvrsn")]
        public SoftwareVersion Versions { get; set; }

        /// <summary>
        /// Effets appliqués ('distorition', 'chorus', 'reverb', ...)
        /// </summary>
        [JsonProperty(PropertyName = "instrument_effects")]
        public IEnumerable<InstrumentEffect> InstrumentEffects { get; set; }

        /// <summary>
        /// Structure de la tablature
        /// </summary>
        /// <example>introx1, coupletx2, refrainx2, coupletx2, ...</example>
        [JsonProperty(PropertyName = "strctr")]
        public IEnumerable<StructureSection> Structure { get; set; }

        /// <summary>
        /// Détails des sections composant la tablature
        /// </summary>
        [JsonProperty(PropertyName = "prts")]
        public IEnumerable<PartSection> PartSections { get; set; }

        /// <summary>
        /// Elements de languages d'une tab (traductions, etc)
        /// </summary>
        [JsonProperty(PropertyName = "lngrsrcs")]
        public IEnumerable<LanguageResource> LanguageResources { get; set; }

        /// <summary>
        /// Instrument de la tablature (guitar, bass guitar, banjo, ...)
        /// </summary>
        [JsonProperty(PropertyName = "instrument")]
        public Instrument Instrument { get; set; }

        /// <summary>
        /// Historique des actions effectuées (création, modifications, migration de version de logiciel, ...)
        /// </summary>
        [JsonProperty(PropertyName = "updts")]
        public IEnumerable<HistoryEntry> History { get; set; }

        /// <summary>
        /// Paroles de la chanson (peut être null)
        /// </summary>
        /// <remarks>Les paroles peuvent être annotés, avec des accords par exemple</remarks>
        [JsonProperty(PropertyName = "lrcs")]
        public Lyrics Lyrics { get; set; }
    }

    /// <summary>
    /// Paroles de la chanson (peut être null)
    /// </summary>
    /// <remarks>Les paroles peuvent être annotés, avec des accords par exemple</remarks>
    [JsonObject(MemberSerialization.OptIn)]
    public class Lyrics
    {
        /// <summary>
        /// Langue des paroles
        /// </summary>
        [JsonProperty(PropertyName = "lng")]
        public string Lang { get; set; }

        /// <summary>
        /// Auteur des paroles
        /// </summary>
        [JsonProperty(PropertyName = "thr")]
        public string Author { get; set; }

        /// <summary>
        /// Phrasé - Combien de phrases affiche-t-on sur une ligne ?
        /// </summary>
        [JsonProperty(PropertyName = "phrs")]
        public int? Phrase { get; set; }

        /// <summary>
        /// Paroles
        /// </summary>
        [JsonProperty(PropertyName = "prts")]
        public IEnumerable<LyricsPart> Parts { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class LyricsPart
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "pid")]
        public Guid PartId { get; set; }

        [JsonProperty(PropertyName = "phrss")]
        public IEnumerable<LyricsPhrase> Phrases { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class LyricsPhrase
    {
        [JsonProperty(PropertyName = "a")]
        public IEnumerable<LyricsPhraseAttribute> Attributes { get; set; }

        [JsonProperty(PropertyName = "c")]
        public IEnumerable<LyricsChord> Chords { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class LyricsPhraseAttribute : BasePropertyItem
    {

    }

    [JsonObject(MemberSerialization.OptIn)]
    public class LyricsChord
    {
        /// <summary>
        /// Position du mot sur lequel l'accord doit être joué
        /// </summary>
        /// <remarks>Si valeur négative, on fait référence à une enum</remarks>
        [JsonProperty(PropertyName = "p")]
        public int Position { get; set; }

        /// <summary>
        /// Accord à jouer
        /// </summary>
        [JsonProperty(PropertyName = "c")]
        public string Chord { get; set; }
    }
}