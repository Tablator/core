using System;
using System.Collections.Generic;
using System.Text;

namespace Tablator.BusinessModel
{
    public class TablatureModel
    {
        //private Dictionary<int, object> _Properties { get; }

        //private IList<TablatureSectionDeclarationModel> _SectionDeclarations { get; }

        //private IList<TablatureSectionImplementationModel> _SectionImplementations { get; }

        public Guid Id { get; private set; }

        public TablatureModel()
        {

        }

        internal void SetId(Guid g) => Id = g;
    }

    public class TablatureSectionDeclarationModel
    {

    }

    public class TablatureSectionImplementationModel
    {

    }

    public interface ITablatureSectionImplementationContent
    {

    }
}