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

    public abstract class IdFileBaseRepository : BaseFileRepository
    {
        /// <summary>
        /// New instance of a base repository for files with ids
        /// </summary>
        /// <param name="dir">Files root directory path</param>
        /// <param name="dir">Files extensions</param>
        public IdFileBaseRepository(string dir, string ext)
            : base(dir, ext)
        {

        }

        protected string GetPath(Guid id)
        {
            if (!File.Exists(Path.Combine(_root_Directory, id.ToString().Replace("-", null) + "." + _file_Extension)))
                return null;

            return Path.Combine(_root_Directory, id.ToString().Replace("-", null) + "." + _file_Extension);
        }

        protected bool TryGetJObject(Guid id, out JObject ret)
        {
            ret = null;

            string filePath = null;

            try
            {
                filePath = GetPath(id);

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

        protected bool TryGetJson(Guid id, out string ret)
        {
            ret = null;

            string filePath = null, json = null;

            try
            {
                filePath = GetPath(id);

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

        protected bool TryParseContent<T>(Guid id, out T ret)
        {
            ret = default(T);

            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            string filePath = null, json = null;

            try
            {
                filePath = GetPath(id);

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

        protected bool TryGetContent(Guid id, out string json)
        {
            json = null;

            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            string filePath = null;

            try
            {
                filePath = GetPath(id);

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