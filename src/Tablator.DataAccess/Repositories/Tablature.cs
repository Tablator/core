namespace Tablator.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Infrastructure.DataAccess.Bases;
    using System.Threading.Tasks;
    using DomainModel;
    using Newtonsoft;

    /// <summary>
    /// Repository to deal with tablatures data
    /// </summary>
    public sealed class TablatureRepository : BaseFileRepository, ITablatureRepository
    {
        public TablatureRepository(string catalogRootDirectory)
               : base(catalogRootDirectory)
        { }

        public Tablature Get(Guid id)
        {
            Tablature ret = null;
            if (!TryParseTablatureFileContent<Tablature>(id, out ret))
                return null;

            return ret;
        }

        /// <summary>
        /// WHAT? Returns the version of the storage format of the tablature
        /// </summary>
        /// <param name="id">tablature's identifier</param>
        /// <returns>The sotrage format's version, or null if a problem occurred</returns>
        public StorageFormatVersion GetTablatureStorageFormatVersion(Guid id)
        {
            string json;
            Newtonsoft.Json.Linq.JToken token;

            try
            {
                if (!TryGetTablatureJson(id, out json))
                    throw new Exception();

                if (string.IsNullOrWhiteSpace(json))
                    return null;
                
                token = Newtonsoft.Json.Linq.JObject.Parse(json).SelectToken("frmtvrsn");
                if (token == null)
                    return null;

                return token.ToObject<StorageFormatVersion>();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                json = null;
                token = null;
            }
        }

        /// <summary>
        /// WHAT? List the properties of the tablature, like the name of the song, or the name of the artist.
        /// It's the common properties for all the instruments
        /// WHY? To get the main information of the tab
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<TablatureProperty> ListTablatureProperties(Guid id)
        {
            string json;
            IEnumerable<Newtonsoft.Json.Linq.JToken> tokens;
            IList<TablatureProperty> ret;

            try
            {
                if (!TryGetTablatureJson(id, out json))
                    throw new Exception();

                if (string.IsNullOrWhiteSpace(json))
                    return null;

                tokens = Newtonsoft.Json.Linq.JObject.Parse(json)["pprts"].Children();
                if (tokens == null)
                    return null;

                ret = new List<TablatureProperty>();

                foreach(Newtonsoft.Json.Linq.JToken token in tokens)
                    ret.Add(token.ToObject<TablatureProperty>());

                return ret;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                json = null;
                tokens = null;
                ret = null;
            }
        }

        /// <summary>
        /// WHAT? List the declarations of the sections what the file contains
        /// WHY? 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<SectionDeclaration> ListSectionDeclarations(Guid id)
        {
            string json;
            IEnumerable<Newtonsoft.Json.Linq.JToken> tokens;
            IList<SectionDeclaration> ret;

            try
            {
                if (!TryGetTablatureJson(id, out json))
                    throw new Exception();

                if (string.IsNullOrWhiteSpace(json))
                    return null;

                tokens = Newtonsoft.Json.Linq.JObject.Parse(json)["dclrtns"].Children();
                if (tokens == null)
                    return null;

                ret = new List<SectionDeclaration>();

                foreach (Newtonsoft.Json.Linq.JToken token in tokens)
                    ret.Add(token.ToObject<SectionDeclaration>());

                return ret;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                json = null;
                tokens = null;
                ret = null;
            }
        }
    }
}