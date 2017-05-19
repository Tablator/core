using System;
using System.Collections.Generic;
using System.Text;

namespace Tablator.BusinessModel
{
    public sealed class GuitarChordModel : BaseChordModel
    {
        public IEnumerable<GuitarChordCompositionModel> Composition { get; private set; }
    }

    public sealed class TabGuitarChordModel : BaseChordModel
    {
        public GuitarChordCompositionModel Composition { get; private set; }
    }

    public abstract class BaseChordModel
    {
        public string Name { get; private set; }
        public string ShortName { get; private set; }
    }

    public sealed class GuitarChordCompositionModel : BaseChordCompositionModel
    {

    }

    public abstract class BaseChordCompositionModel
    {
        public string Composition { get; private set; }
    }
}