namespace Tablator.BusinessModel
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;

    [JsonObject(MemberSerialization.OptIn)]
    public class Partie
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "mesures")]
        public List<Mesure> Mesures { get; set; }

        /// <summary>
        /// Liste (dynamique) des accords utilisés dans cette partie
        /// </summary>
        public List<string> ChordList
        {
            get
            {
                if (Mesures == null)
                    return new List<string>();

                if (Mesures.Count == 0)
                    return new List<string>();

                List<string> ret = new List<string>();

                Mesures.Select(x => x.ChordList).ToList().ForEach(delegate (List<string> s)
                {
                    s.ForEach(delegate (string s1)
                    {
                        if (!ret.Contains(s1))
                            ret.Add(s1);
                    });
                });

                return ret;
            }
        }
    }
}