namespace Tablator.BusinessModel.Tablature
{
    using System.Collections.Generic;
    using System.Linq;

    public class PartSectionMesureTempsModel
    {
        /// <summary>
        /// Nombre de temps de cette partie de mesure
        /// </summary>
        /// <remarks>Le plus souvent 1, mais peu être 2 si c'est une blanque par exemple, ou même 4 pour une ronde</remarks>
        public int Count { get; set; }

        /// <summary>
        /// Elements (Notes, accords, silences, ...)
        /// </summary>
        public List<PartSectionMesureTempsItemModel> Sons { get; set; }

        public PartSectionMesureTempsModel(DomainModel.PartSectionMesureTemps src)
        {
            Count = src.Count;
            Sons = new List<PartSectionMesureTempsItemModel>();
            if (src.Sons != null && src.Sons.Count() > 0)
                foreach (DomainModel.PartSectionMesureTempsItem s in src.Sons)
                    Sons.Add(new PartSectionMesureTempsItemModel(s));
        }
    }
}