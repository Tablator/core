using System;
using System.Collections.Generic;
using System.Text;

namespace Tablator.BusinessModel.Tablature
{
    public class PartSectionModel
    {
        public Guid Id { get; set; }

        public List<PartSectionMesureModel> Mesures { get; set; }

        public PartSectionModel(DomainModel.PartSection src)
        {
            Id = src.Id;
            Mesures = new List<PartSectionMesureModel>();
            foreach (DomainModel.PartSectionMesure ms in src.Mesures)
                Mesures.Add(new PartSectionMesureModel(ms));
        }
    }

    public class PartSectionMesureModel
    {
        public List<PartSectionMesureTempsModel> Temps { get; set; }

        public List<PartSectionMesurePropertyModel> Pprts { get; set; }

        public PartSectionMesureModel(DomainModel.PartSectionMesure src)
        {
            Temps = new List<PartSectionMesureTempsModel>();
            Pprts = new List<PartSectionMesurePropertyModel>();

            if (src.Temps != null)
                foreach (DomainModel.PartSectionMesureTemps tmps in src.Temps)
                    Temps.Add(new PartSectionMesureTempsModel(tmps));

            if (src.Properties != null)
                foreach (DomainModel.PartSectionMesureProperty pprt in src.Properties)
                    Pprts.Add(new PartSectionMesurePropertyModel(pprt.Code, pprt.Value));
        }
    }
}