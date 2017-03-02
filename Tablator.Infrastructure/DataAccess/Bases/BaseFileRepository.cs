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

    /// <summary>
    /// Base class to deal with Tablator file storage system
    /// </summary>
    public abstract class BaseFileRepository
    {
        /// <summary>
        /// File system root directory
        /// </summary>
        private readonly string _root_Directory = null;

        private readonly string _file_Extension = "tbltr";

        /// <summary>
        /// Exhaustive list of storage files
        /// </summary>
        protected enum StorageFileEnum
        {
            [Display(Description = "cat_hierarchy.tbltr")]
            CatalogHierarchy,
            [Display(Description = "cat_references.tbltr")]
            CatalogReference
        }

        protected enum TablatureFileSectionEnum
        {
            [Display(Description = "pprts")]
            Properties,
            [Display(Description = "src")]
            Source,
            [Display(Description = "prts")]
            Parts,
            [Display(Description = "lngrsrcs")]
            Structure,
            [Display(Description = "updts")]
            History,
            [Display(Description = "lngrsrcs")]
            LanguageResources,
            [Display(Description = TablatureSerializationConstants.SoftwareVersionConstants.SectionName)]
            SoftwareVersion
        }

        public BaseFileRepository(string rootDirectory)
        {
            if (string.IsNullOrWhiteSpace(rootDirectory))
                throw new ArgumentNullException(nameof(rootDirectory));

            if (!Directory.Exists(rootDirectory))
                throw new ArgumentException(nameof(rootDirectory));

            _root_Directory = rootDirectory;
        }

        /// <summary>
        /// Get the catalog hierarchy file's absolute path
        /// </summary>
        /// <returns>hierarchy file's absolute path or null</returns>
        /// <remarks>Checks if the file exists. If not, returns null.</remarks>
        private string GetCatalogHierarchyFilePath()
        {
            if (!File.Exists(Path.Combine(_root_Directory, StorageFileEnum.CatalogHierarchy.GetDisplayDescription())))
                return null;

            return Path.Combine(_root_Directory, StorageFileEnum.CatalogHierarchy.GetDisplayDescription());
        }

        private string GetFilePath(StorageFileEnum file)
        {
            switch (file)
            {
                case StorageFileEnum.CatalogHierarchy:
                    return GetCatalogHierarchyFilePath();
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Get a tablature file's absolute path
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string GetTablatureFilePath(Guid id)
        {
            if (!File.Exists(Path.Combine(_root_Directory, id.ToString().Replace("-", null) + "." + _file_Extension)))
                return null;

            return Path.Combine(_root_Directory, id.ToString().Replace("-", null) + "." + _file_Extension);
        }

        /// <summary>
        /// Open, read and returns the catalog hierarchy file's content
        /// </summary>
        /// <param name="id"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        protected bool TryGetHierarchyFileContent(out string json)
        {
            json = null;

            return TryGetFileContent(GetCatalogHierarchyFilePath(), out json);
        }

        /// <summary>
        /// Open, read and returns a file's content
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        private bool TryGetFileContent(string filePath, out string json)
        {
            Newtonsoft.Json.Linq.JObject jo;

            try
            {
                using (StreamReader file = File.OpenText(filePath))
                {
                    using (JsonTextReader rdr = new JsonTextReader(file))
                    {
                        jo = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.Linq.JToken.ReadFrom(rdr);
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

        protected bool TryParseFileContent<T>(StorageFileEnum file, out T ret)
        {
            ret = default(T);

            string filePath = null, json = null;

            try
            {
                filePath = GetFilePath(file);

                if (string.IsNullOrWhiteSpace(filePath))
                    return false;

                if (!TryGetFileContent(filePath, out json))
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

        private bool TryParseJson<T>(string json, out T ret)
        {
            ret = JsonConvert.DeserializeObject<T>(json);
            return true;
        }

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

                if (!TryGetFileContent(filePath, out json))
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

                return TryGetFileContent(filePath, out json);
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