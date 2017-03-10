namespace Tablator.BusinessModel.Tablature
{
    using System.Collections.Generic;
    using System.Linq;

    public class PartSectionMesureTempsItemModel
    {
        public List<PartSectionMesureTempsItemPropertyModel> Pprts { get; set; }

        public PartSectionMesureTempsItemModel(DomainModel.PartSectionMesureTempsItem src)
        {
            Pprts = new List<PartSectionMesureTempsItemPropertyModel>();
            if (src.Properties != null && src.Properties.Count() > 0)
                foreach (DomainModel.PartSectionMesureTempsItemProperty p in src.Properties)
                    Pprts.Add(new PartSectionMesureTempsItemPropertyModel(p.Code, p.Value));
        }
    }
}