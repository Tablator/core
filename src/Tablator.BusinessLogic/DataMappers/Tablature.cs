using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tablator.Infrastructure.Enumerations;

namespace Tablator.BusinessLogic.DataMappers
{
    internal static class TablatureDataMapper
    {
        public static BusinessModel.TablatureModel Map(DomainModel.Tablature tab)
        {
            Dictionary<int, object> _pprts = new Dictionary<int, object>();
            foreach (DomainModel.TablatureProperty tProp in tab.Properties)
                _pprts.Add(tProp.Code, tProp.Value);

            return new BusinessModel.TablatureModel(_pprts);
        }
    }

    public class TablatureBuilder
    {
        private BusinessModel.TablatureModel _tablature { get; set; }
        private DomainModel.Tablature _source { get; set; }

        public TablatureBuilder(DomainModel.Tablature tab)
        {
            _source = tab;
        }

        public BusinessModel.TablatureModel Build()
        {
            PopulateProperties();

            return _tablature;
        }

        private void PopulateProperties()
        {
            if (_source.Properties == null)
                return;

            if (_source.Properties.Count() == 0)
                return;

            foreach (DomainModel.TablatureProperty pprt in _source.Properties)
            {
                switch ((TablaturePropertyEnum)pprt.Code)
                {
                    case TablaturePropertyEnum.Identifier:
                        if (Guid.TryParse(pprt.Value, out Guid id))
                            _tablature.SetId(id);
                        break;
                }
            }
        }
    }
}