namespace Tablator.Infrastructure.DomainModel.Constants
{
    public static class BasePropertyItemSerializationConstants
    {
        public const string Code = "cod";
        public const string Value = "val";
    }

    public static class TablatureSerializationConstants
    {
        public const string Instrument = "instrument";
        public const string TypeInstrument = "instrument_type";

        public static class SoftwareVersionConstants
        {
            public const string SectionName = "sftvrsns";

            public static class SectionPropertiesConstants
            {
                public const string Major = "mjr";
                public const string Minor = "mnr";
                public const string Revision = "rev";
            }
        }
    }
}