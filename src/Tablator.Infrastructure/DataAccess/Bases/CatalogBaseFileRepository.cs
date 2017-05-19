namespace Tablator.Infrastructure.DataAccess.Bases
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using Infrastructure.Extensions;
    using Tablator.Infrastructure.Constants.FileStorageSystem;

    public abstract class CatalogBaseFileRepository : BaseFileRepository
    {
        /// <summary>
        /// New instance of a file's catalog repository
        /// </summary>
        /// <param name="dir">Files root directory path</param>
        public CatalogBaseFileRepository(string dir)
            : base(dir, FileExtensionConfiguration.Catalog)
        {

        }

        /// <summary>
        /// Exhaustive list of storage files
        /// </summary>
        protected enum StorageFileEnum
        {
            [Display(Description = "cat_hierarchy")]
            CatalogHierarchy,
            [Display(Description = "cat_references")]
            CatalogReference
        }

        /// <summary>
        /// Get the catalog hierarchy file's absolute path
        /// </summary>
        /// <returns>hierarchy file's absolute path or null</returns>
        /// <remarks>Checks if the file exists. If not, returns null.</remarks>
        private string GetCatalogHierarchyPath()
        {
            if (!File.Exists(Path.Combine(_root_Directory, StorageFileEnum.CatalogHierarchy.GetDisplayDescription() + "." + _file_Extension)))
                return null;

            return Path.Combine(_root_Directory, StorageFileEnum.CatalogHierarchy.GetDisplayDescription() + "." + _file_Extension);
        }

        private string GetCatalogReferencePath()
        {
            if (!File.Exists(Path.Combine(_root_Directory, StorageFileEnum.CatalogReference.GetDisplayDescription() + "." + _file_Extension)))
                return null;

            return Path.Combine(_root_Directory, StorageFileEnum.CatalogReference.GetDisplayDescription() + "." + _file_Extension);
        }

        private string GetFilePath(StorageFileEnum file)
        {
            switch (file)
            {
                case StorageFileEnum.CatalogHierarchy:
                    return GetCatalogHierarchyPath();
                case StorageFileEnum.CatalogReference:
                    return GetCatalogReferencePath();
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Open, read and returns the catalog hierarchy file's content
        /// </summary>
        /// <param name="id"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        protected bool TryGetHierarchyContent(out string json)
        {
            json = null;

            return TryGetContent(GetCatalogHierarchyPath(), out json);
        }

        protected bool TryParseContent<T>(StorageFileEnum file, out T ret)
        {
            ret = default(T);

            string filePath = null, json = null;

            try
            {
                filePath = GetFilePath(file);

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
    }
}