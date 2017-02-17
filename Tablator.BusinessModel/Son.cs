namespace Tablator.BusinessModel
{
    using Newtonsoft.Json;
    using Tablator.Infrastructure.Enumerations;

    [JsonObject(MemberSerialization.OptIn)]
    public class Son
    {
        [JsonProperty(PropertyName = "chord")]
        public string Chord { get; set; }

        [JsonProperty(PropertyName = "corde")]
        public int Corde { get; set; }

        [JsonProperty(PropertyName = "position")]
        public int Position { get; set; }

        [JsonProperty(PropertyName = "type")]
        public int TypeCode { get; set; }

        public TypeSonEnum Type => (TypeSonEnum)TypeCode;

        [JsonProperty(PropertyName = "dur")]
        public int DurationCode { get; set; }

        public FiguresDeNotes Duration => (FiguresDeNotes)DurationCode;

        private bool? _Mute;

        /// <summary>
        /// Jouée étoufé ou pas
        /// </summary>
        [JsonProperty(PropertyName = "mute")]
        public bool Mute
        {
            get
            {
                if (_Mute.HasValue)
                    return _Mute.Value;
                else
                    return false;
            }
            set { _Mute = value; }
        }

        [JsonProperty(PropertyName = "direction")]
        public int? SensGrattageCode { get; set; }
    }
}