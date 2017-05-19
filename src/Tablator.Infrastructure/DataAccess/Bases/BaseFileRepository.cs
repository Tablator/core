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

    /// <summary>
    /// Base class to deal with Tablator file storage system
    /// </summary>
    public abstract class BaseFileRepository
    {
        /// <summary>
        /// File system root directory
        /// </summary>
        protected readonly string _root_Directory = null;

        /// <summary>
        /// Files extension
        /// </summary>
        protected readonly string _file_Extension = "tbltr";

        public BaseFileRepository(string rootDirectory, string fileExtension)
        {
            if (string.IsNullOrWhiteSpace(rootDirectory))
                throw new ArgumentNullException(nameof(rootDirectory));

            if (string.IsNullOrWhiteSpace(fileExtension))
                throw new ArgumentNullException(nameof(fileExtension));

            if (!Directory.Exists(rootDirectory))
                throw new ArgumentException(nameof(rootDirectory));

            _root_Directory = rootDirectory;
            _file_Extension = fileExtension;
        }

        /// <summary>
        /// Open, read and returns a file's content
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        protected bool TryGetContent(string filePath, out string json)
        {
            JObject jo;

            try
            {
                using (StreamReader file = File.OpenText(filePath))
                {
                    using (JsonTextReader rdr = new JsonTextReader(file))
                    {
                        jo = (JObject)JToken.ReadFrom(rdr);
                        json = jo.ToString();
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
                jo = null;
            }
        }

        protected bool TryParseJson<T>(string json, out T ret)
        {
            ret = JsonConvert.DeserializeObject<T>(json);
            return true;
        }
    }
}