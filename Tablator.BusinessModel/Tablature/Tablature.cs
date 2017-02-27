using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Tablator.Infrastructure.Enumerations;

namespace Tablator.BusinessModel.Tablature
{
    public class TablatureModel
    {
        public Guid Id { get; protected set; }
        public string SongName { get; protected set; }
        public string ArtistName { get; protected set; }

        public TablatureModel()
        { }
    }

    public interface IInstrumentTablature
    {

    }

    public sealed class GuitarTablatureModel : TablatureModel, IInstrumentTablature
    {
        public GuitarTablatureModel()
            : base()
        {

        }

        public static explicit operator GuitarTablatureModel(DomainModel.Tablature tab)
        {
            GuitarTablatureModel ret = new GuitarTablatureModel();

            ret.Id = new Guid(tab.Properties.Where(x => x.Code == (int)TablaturePropertyEnum.Identifier).FirstOrDefault().Value);
            ret.SongName = tab.Properties.Where(x => x.Code == (int)TablaturePropertyEnum.SongName).FirstOrDefault().Value;
            ret.ArtistName = tab.Properties.Where(x => x.Code == (int)TablaturePropertyEnum.Artist).FirstOrDefault().Value;

            return ret;
        }
    }
}