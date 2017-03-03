using System;
using System.Collections.Generic;
using System.Text;

namespace Tablator.BusinessModel.Tablature
{
    public sealed class StructureSectionModel
    {
        public Guid PartId { get; }
        public int Repeat { get; }

        public StructureSectionModel(Guid partId, int repeat)
        {
            PartId = partId;
            Repeat = repeat;
        }
    }
}