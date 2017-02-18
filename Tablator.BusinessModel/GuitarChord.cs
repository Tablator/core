namespace Tablator.BusinessModel
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using Tablator.Infrastructure.Constants;

    public class GuitarChord : Chord
    {
        [JsonProperty(PropertyName = "capo")]
        public int Capo { get; }

        private string _Composition;

        public override string Composition
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_Composition))
                {
                    _Composition = string.Empty;
                    foreach (KeyValuePair<int, int?> kvp in Positions.OrderBy(x => x.Key))
                        _Composition += kvp.Value + ChordConstants.CompositionSeparator;
                    _Composition += Capo;
                }

                return _Composition;
            }
        }

        public GuitarChord()
        {

        }
    }

    public class GuitarChordCollection : List<GuitarChord>
    {
        public GuitarChordCollection()
        { }
    }
}