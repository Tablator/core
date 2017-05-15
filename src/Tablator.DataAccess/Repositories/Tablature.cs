namespace Tablator.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Infrastructure.DataAccess.Bases;
    using System.Threading.Tasks;
    using DomainModel;
    using Newtonsoft;
    using System.Linq;
    using Newtonsoft.Json.Linq;
    using Tablator.Infrastructure.Enumerations.Tablature;

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
            JToken token;

            try
            {
                if (!TryGetTablatureJson(id, out json))
                    throw new Exception();

                if (string.IsNullOrWhiteSpace(json))
                    return null;

                token = JObject.Parse(json).SelectToken("frmtvrsn");
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
            IEnumerable<JToken> tokens;
            IList<TablatureProperty> ret;

            try
            {
                if (!TryGetTablatureJson(id, out json))
                    throw new Exception();

                if (string.IsNullOrWhiteSpace(json))
                    return null;

                tokens = JObject.Parse(json)["pprts"].Children();
                if (tokens == null)
                    return null;

                ret = new List<TablatureProperty>();

                foreach (Newtonsoft.Json.Linq.JToken token in tokens)
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
            IEnumerable<JToken> tokens;
            IList<SectionDeclaration> ret;

            try
            {
                if (!TryGetTablatureJson(id, out json))
                    throw new Exception();

                if (string.IsNullOrWhiteSpace(json))
                    return null;

                tokens = JObject.Parse(json)["dclrtns"].Children();
                if (tokens == null)
                    return null;

                ret = new List<SectionDeclaration>();

                foreach (JToken token in tokens)
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

        /// <summary>
        /// WHAT?
        /// WHY?
        /// </summary>
        /// <param name="id"></param>
        /// <param name="declarations"></param>
        /// <returns></returns>
        public IEnumerable<SectionImplementation> ListSectionImplementations(Guid id, IEnumerable<SectionDeclaration> declarations)
        {
            IEnumerable<JToken> tokens;
            IList<SectionImplementation> ret;
            JObject jo;

            try
            {
                if (!TryGetTablatureJObject(id, out jo))
                    throw new Exception();

                tokens = jo.SelectTokens("sctns").Children();

                ret = new List<SectionImplementation>();

                foreach (JObject _token in tokens)
                {
                    SectionImplementation impl = new SectionImplementation();

                    foreach (JProperty _jprop in _token.Properties())
                    {
                        if (_jprop.Name == "id")
                        {
                            impl.Id = _jprop.Value.ToObject<Guid>();
                        }
                        else if (_jprop.Name == "content")
                        {
                            List<ISectionContent> _implContent = new List<ISectionContent>();

                            switch (declarations.Where(x => x.Id == impl.Id).Select(x => x.Type).First())
                            {
                                case (int)SectionDeclarationTypeEnum.InstrumentSettings:

                                    break;
                                case (int)SectionDeclarationTypeEnum.LyricsWithChords:
                                    _implContent.AddRange(_jprop.Value.ToObject<IEnumerable<LyricsAndChordsSectionContent>>());
                                    break;
                                default:
                                    break;
                            }

                            impl.Content = _implContent;

                            _implContent = null;
                        }
                    }

                    ret.Add(impl);

                    impl = null;
                }

                return ret;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                tokens = null;
                ret = null;
                jo = null;
            }
        }

        /// <summary>
        /// List all identifiers of the section implementations
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Guid> ListSectionImplementationIds(Guid id)
        {
            JObject jo;
            IEnumerable<JToken> tokens;
            IList<Guid> ret;

            try
            {
                if (!TryGetTablatureJObject(id, out jo))
                    throw new Exception();

                tokens = jo.SelectTokens("sctns").Children();

                ret = new List<Guid>();

                foreach (JObject _token in tokens)
                {
                    foreach (JProperty _jprop in _token.Properties())
                    {
                        if (_jprop.Name == "id")
                        {
                            ret.Add(_jprop.Value.ToObject<Guid>());
                        }
                    }
                }

                return ret;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                tokens = null;
                ret = null;
                jo = null;
            }
        }
    }
}