namespace Tablator.Infrastructure.DataAccess.Bases
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using Infrastructure.Extensions;
    using DomainModel.Constants;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Tablator.Infrastructure.Constants.FileStorageSystem;

    public abstract class TablatureBaseFileRepository : IdFileBaseRepository
    {
        /// <summary>
        /// New instance of a file's tablature repository
        /// </summary>
        /// <param name="dir">Files root directory path</param>
        public TablatureBaseFileRepository(string dir)
            : base(dir, FileExtensionConfiguration.Tablature)
        {

        }

        /// <summary>
        /// Get a tablature file's absolute path
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Obsolete("Please use generic method -> see IdFileBaseRepository.cs", false)]
        protected string GetTablatureFilePath(Guid id)
        {
            if (!File.Exists(Path.Combine(_root_Directory, id.ToString().Replace("-", null) + "." + _file_Extension)))
                return null;

            return Path.Combine(_root_Directory, id.ToString().Replace("-", null) + "." + _file_Extension);
        }

        [Obsolete("Please use generic method -> see IdFileBaseRepository.cs", false)]
        protected bool TryGetTablatureJObject(Guid id, out JObject ret)
        {
            ret = null;

            string filePath = null;

            try
            {
                filePath = GetTablatureFilePath(id);

                if (string.IsNullOrWhiteSpace(filePath))
                    return false;

                using (StreamReader file = File.OpenText(filePath))
                {
                    using (JsonTextReader rdr = new JsonTextReader(file))
                    {
                        ret = (JObject)JToken.ReadFrom(rdr);
                    }
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                filePath = null;
            }
        }

        [Obsolete("Please use generic method -> see IdFileBaseRepository.cs", false)]
        protected bool TryGetTablatureJson(Guid id, out string ret)
        {
            ret = null;

            string filePath = null, json = null;

            try
            {
                filePath = GetTablatureFilePath(id);

                if (string.IsNullOrWhiteSpace(filePath))
                    return false;

                if (!TryGetContent(filePath, out json))
                    return false;

                if (string.IsNullOrWhiteSpace(json))
                    return false;

                ret = json;
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                filePath = null;
                json = null;
            }
        }

        [Obsolete("Please use generic method -> see IdFileBaseRepository.cs", false)]
        protected bool TryParseTablatureFileContent<T>(Guid id, out T ret)
        {
            ret = default(T);

            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            string filePath = null, json = null;

            try
            {
                filePath = GetTablatureFilePath(id);

                if (string.IsNullOrWhiteSpace(filePath))
                    return false;

                if (!TryGetContent(filePath, out json))
                    return false;

                if (string.IsNullOrWhiteSpace(json))
                    return false;

                return TryParseJson<T>(json, out ret);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                filePath = null;
                json = null;
            }
        }

        /// <summary>
        /// Open, read and returns a tablature file's content
        /// </summary>
        /// <param name="id"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        [Obsolete("Please use generic method -> see IdFileBaseRepository.cs", false)]
        protected bool TryGetTablatureFileContent(Guid id, out string json)
        {
            json = null;

            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            string filePath = null;

            try
            {
                filePath = GetTablatureFilePath(id);

                if (string.IsNullOrWhiteSpace(filePath))
                    return false;

                return TryGetContent(filePath, out json);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                filePath = null;
            }
        }
    }
}