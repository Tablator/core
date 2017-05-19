using System;
using System.Collections.Generic;
using System.Text;
using Tablator.Infrastructure.Enumerations;
using System.Linq;

namespace Tablator.BusinessModel.Builders
{
    public sealed class TablatureBuilder
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