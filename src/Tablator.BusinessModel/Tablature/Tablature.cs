using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Tablator.Infrastructure.Enumerations;
using System.Globalization;

namespace Tablator.BusinessModel.Tablature
{
    public class TablatureModel
    {
        public Guid Id { get; protected set; }
        public string SongName { get; protected set; }
        public string ArtistName { get; protected set; }
        public int? Tempo { get; protected set; }
        public List<StructureSectionModel> Structure { get; protected set; }
        public LanguageResourceCollectionModel LanguageResources { get; protected set; }
        public InstrumentEnum Instrument { get; protected set; }
        public List<PartSectionModel> PartSections { get; protected set; }
        public LyricsModel Lyrics { get; protected set; }

        public TablatureModel()
        { }

        public string GetPartName(Guid id, CultureInfo ci) => LanguageResources.GetPartName(id, ci);
    }

    public class LyricsModel
    {
        public string Language { get; protected set; }
        public string Author { get; protected set; }
        public int Phrase { get; protected set; }
        public List<LyricsPartModel> Parts { get; protected set; }

        public LyricsModel(DomainModel.Lyrics lyrics)
        {
            if (lyrics == null)
                return;

            Language = lyrics.Lang;
            Author = lyrics.Author;
            Phrase = lyrics.Phrase.HasValue ? lyrics.Phrase.Value : 1;
            Parts = new List<LyricsPartModel>();

            if (lyrics.Parts == null)
                return;

            foreach (DomainModel.LyricsPart p in lyrics.Parts)
                Parts.Add(new LyricsPartModel(p));
        }
    }

    public class LyricsPartModel
    {
        public Guid Id { get; protected set; }
        public Guid PartId { get; protected set; }
        public List<LyricsSentenceModel> Sentences { get; protected set; }

        public LyricsPartModel(DomainModel.LyricsPart part)
        {
            Id = part.Id;
            PartId = part.PartId;
            Sentences = new List<LyricsSentenceModel>();

            foreach (DomainModel.LyricsPhrase p in part.Phrases)
                Sentences.Add(new LyricsSentenceModel(p));
        }
    }

    public class LyricsSentenceModel
    {
        public string Sentence { get; protected set; }

        public List<LyricsSentenceChordModel> Chords { get; set; }

        public LyricsSentenceModel(DomainModel.LyricsPhrase p)
        {
            Sentence = p?.Attributes?.Where(x => x.Code == (int)LyricsSentenceAttributeEnum.SentenceContent).Select(x => x.Value).FirstOrDefault();

            Chords = new List<LyricsSentenceChordModel>();

            if (p.Chords == null || p.Chords.Count() == 0)
                return;

            foreach (DomainModel.LyricsChord c in p.Chords)
                Chords.Add(new LyricsSentenceChordModel(c.Position, c.Chord));
        }
    }

    public class LyricsSentenceChordModel
    {
        public int Position { get; protected set; }
        public string Chord { get; protected set; }

        public LyricsSentenceChordModel(int position, string chord)
        {
            Position = position;
            Chord = chord;
        }
    }

    public interface IInstrumentTablature
    {

    }

    public sealed class GuitarTablatureModel : TablatureModel, IInstrumentTablature
    {
        public int? Capodastre { get; private set; }
        public string Tuning { get; private set; }
        public GuitarTypeEnum GuitarType { get; private set; }
        private string[] _Chords { get; set; }
        public List<TabGuitarChordModel> Chords { get; private set; }

        public GuitarTablatureModel()
            : base()
        { }

        public static explicit operator GuitarTablatureModel(DomainModel.Tablature tab)
        {
            GuitarTablatureModel ret = new GuitarTablatureModel();

            // Common stuff

            ret.Instrument = (InstrumentEnum)tab.Instrument.Code;
            ret.Id = new Guid(tab.Properties.Where(x => x.Code == (int)TablaturePropertyEnum.Identifier).FirstOrDefault().Value);
            ret.SongName = tab.Properties.Where(x => x.Code == (int)TablaturePropertyEnum.SongName).FirstOrDefault().Value;
            ret.ArtistName = tab.Properties.Where(x => x.Code == (int)TablaturePropertyEnum.Artist).FirstOrDefault().Value;
            ret.Tempo = !string.IsNullOrWhiteSpace(tab.Properties.Where(x => x.Code == (int)TablaturePropertyEnum.Tempo).FirstOrDefault()?.Value) ? Convert.ToInt32(tab.Properties.Where(x => x.Code == (int)TablaturePropertyEnum.Tempo).FirstOrDefault()?.Value) : (int?)null;

            ret.Structure = new List<StructureSectionModel>();
            if (tab.Structure != null && tab.Structure.Count() > 0)
                foreach (DomainModel.StructureSection sec in tab.Structure)
                    ret.Structure.Add(new StructureSectionModel(sec.PartId, sec.Repeat));

            ret.LanguageResources = new LanguageResourceCollectionModel(tab.LanguageResources);

            // Guitar stuff

            if (tab.Instrument.InstrumentType.HasValue && tab.Instrument.InstrumentType.Value > 0)
                ret.GuitarType = (GuitarTypeEnum)tab.Instrument.InstrumentType.Value;

            if (tab.Instrument.ConfigurationSections.Where(x => x.Code == (int)GuitarConfiguationSectionEnum.Settings).FirstOrDefault() != null && tab.Instrument.ConfigurationSections.Where(x => x.Code == (int)GuitarConfiguationSectionEnum.Settings).FirstOrDefault().Settings != null && tab.Instrument.ConfigurationSections.Where(x => x.Code == (int)GuitarConfiguationSectionEnum.Settings).FirstOrDefault().Settings.Count() > 0)
            {
                if (tab.Instrument.ConfigurationSections.Where(x => x.Code == (int)GuitarConfiguationSectionEnum.Settings).FirstOrDefault().Settings.Where(x => x.Code == (int)GuitarSettingsEnum.Capodastre).FirstOrDefault() != null)
                    ret.Capodastre = Convert.ToInt32(tab.Instrument.ConfigurationSections.Where(x => x.Code == (int)GuitarConfiguationSectionEnum.Settings).FirstOrDefault().Settings.Where(x => x.Code == (int)GuitarSettingsEnum.Capodastre).FirstOrDefault()?.Value);

                if (tab.Instrument.ConfigurationSections.Where(x => x.Code == (int)GuitarConfiguationSectionEnum.Settings).FirstOrDefault().Settings.Where(x => x.Code == (int)GuitarSettingsEnum.Tuning).FirstOrDefault() != null)
                    ret.Tuning = tab.Instrument.ConfigurationSections.Where(x => x.Code == (int)GuitarConfiguationSectionEnum.Settings).FirstOrDefault().Settings.Where(x => x.Code == (int)GuitarSettingsEnum.Tuning).FirstOrDefault().Value;

                if (tab.Instrument.ConfigurationSections.Where(x => x.Code == (int)GuitarConfiguationSectionEnum.Settings).FirstOrDefault().Settings.Where(x => x.Code == (int)GuitarSettingsEnum.Chords).FirstOrDefault() != null && tab.Instrument.ConfigurationSections.Where(x => x.Code == (int)GuitarConfiguationSectionEnum.Settings).FirstOrDefault().Settings.Where(x => x.Code == (int)GuitarSettingsEnum.Chords).FirstOrDefault().Value.Contains('|'))
                    ret._Chords = tab.Instrument.ConfigurationSections.Where(x => x.Code == (int)GuitarConfiguationSectionEnum.Settings).FirstOrDefault().Settings.Where(x => x.Code == (int)GuitarSettingsEnum.Chords).FirstOrDefault().Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }

            // Chords stuff

            if (ret._Chords != null && ret._Chords.Length > 0)
            {
                ret.Chords = new List<TabGuitarChordModel>();

                //foreach (string ch in ret._Chords)
                    //ret.Chords.Add()//TODO: fetch chord details by chordservice
            }

            ret.PartSections = new List<PartSectionModel>();
            if (tab.PartSections != null && tab.PartSections.Count() > 0)
                foreach (DomainModel.PartSection sec in tab.PartSections)
                    ret.PartSections.Add(new PartSectionModel(sec));

            // Lyrics stuff

            if (tab.Lyrics != null)
                ret.Lyrics = new LyricsModel(tab.Lyrics);

            return ret;
        }
    }
}