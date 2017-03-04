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

        public TablatureModel()
        { }

        public string GetPartName(Guid id, CultureInfo ci) => LanguageResources.GetPartName(id, ci);
    }

    public interface IInstrumentTablature
    {

    }

    public sealed class GuitarTablatureModel : TablatureModel, IInstrumentTablature
    {
        public int? Capodastre { get; private set; }
        public string Tuning { get; private set; }
        public string[] Chords { get; private set; }
        public GuitarTypeEnum GuitarType { get; private set; }

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
            //ret.Capodastre = tab.InstrumentSettings.Where(x => x.Code == (int)GuitarSettingsEnum.Capodastre).FirstOrDefault() != null ? Convert.ToInt32(tab.InstrumentSettings.Where(x => x.Code == (int)GuitarSettingsEnum.Capodastre).FirstOrDefault().Value) : (int?)null;
            //ret.Tuning = tab.InstrumentSettings.Where(x => x.Code == (int)GuitarSettingsEnum.Tuning).FirstOrDefault() != null ? tab.InstrumentSettings.Where(x => x.Code == (int)GuitarSettingsEnum.Tuning).FirstOrDefault().Value : null;
            //ret.Chords = new List<string>();
            //if (tab.InstrumentSettings.Where(x => x.Code == (int)GuitarSettingsEnum.Chords).FirstOrDefault() != null && !string.IsNullOrWhiteSpace(tab.InstrumentSettings.Where(x => x.Code == (int)GuitarSettingsEnum.Chords).FirstOrDefault().Value) && tab.InstrumentSettings.Where(x => x.Code == (int)GuitarSettingsEnum.Chords).FirstOrDefault().Value.Contains('|'))
            //{
            //    string[] _accords = tab.InstrumentSettings.Where(x => x.Code == (int)GuitarSettingsEnum.Chords).FirstOrDefault().Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            //    if (_accords != null && _accords.Count() > 0)
            //        foreach (string s in _accords)
            //            ret.Chords.Add(s);
            //    _accords = null;
            //}
            if (tab.Instrument.ConfigurationSections.Where(x => x.Code == (int)GuitarConfiguationSectionEnum.Settings).FirstOrDefault() != null && tab.Instrument.ConfigurationSections.Where(x => x.Code == (int)GuitarConfiguationSectionEnum.Settings).FirstOrDefault().Settings != null && tab.Instrument.ConfigurationSections.Where(x => x.Code == (int)GuitarConfiguationSectionEnum.Settings).FirstOrDefault().Settings.Count() > 0)
            {
                if (tab.Instrument.ConfigurationSections.Where(x => x.Code == (int)GuitarConfiguationSectionEnum.Settings).FirstOrDefault().Settings.Where(x => x.Code == (int)GuitarSettingsEnum.Capodastre).FirstOrDefault() != null)
                    ret.Capodastre = Convert.ToInt32(tab.Instrument.ConfigurationSections.Where(x => x.Code == (int)GuitarConfiguationSectionEnum.Settings).FirstOrDefault().Settings.Where(x => x.Code == (int)GuitarSettingsEnum.Capodastre).FirstOrDefault()?.Value);

                if (tab.Instrument.ConfigurationSections.Where(x => x.Code == (int)GuitarConfiguationSectionEnum.Settings).FirstOrDefault().Settings.Where(x => x.Code == (int)GuitarSettingsEnum.Tuning).FirstOrDefault() != null)
                    ret.Tuning = tab.Instrument.ConfigurationSections.Where(x => x.Code == (int)GuitarConfiguationSectionEnum.Settings).FirstOrDefault().Settings.Where(x => x.Code == (int)GuitarSettingsEnum.Tuning).FirstOrDefault().Value;

                if (tab.Instrument.ConfigurationSections.Where(x => x.Code == (int)GuitarConfiguationSectionEnum.Settings).FirstOrDefault().Settings.Where(x => x.Code == (int)GuitarSettingsEnum.Chords).FirstOrDefault() != null && tab.Instrument.ConfigurationSections.Where(x => x.Code == (int)GuitarConfiguationSectionEnum.Settings).FirstOrDefault().Settings.Where(x => x.Code == (int)GuitarSettingsEnum.Chords).FirstOrDefault().Value.Contains('|'))
                    ret.Chords = tab.Instrument.ConfigurationSections.Where(x => x.Code == (int)GuitarConfiguationSectionEnum.Settings).FirstOrDefault().Settings.Where(x => x.Code == (int)GuitarSettingsEnum.Chords).FirstOrDefault().Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }

            return ret;
        }
    }
}