namespace Tablator.BusinessLogic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using Infrastructure.Extensions;

    /// <summary>
    /// Service to handle storage file system
    /// </summary>
    public sealed class StorageFileService : IStorageFileService
    {
        /// <summary>
        /// File system root directory
        /// </summary>
        private readonly string _root_Directory = null;

        /// <summary>
        /// Exhaustive list of storage files
        /// </summary>
        private enum StorageFileEnum
        {
            [Display(Description = "cat_hierarchy.tbltr")]
            CatalogHierarchy,
            [Display(Description = "cat_references.tbltr")]
            CatalogReference
        }

        /// <summary>
        /// Creates new instance of the service
        /// </summary>
        /// <param name="rootDirectory">File system root directory</param>
        public StorageFileService(string rootDirectory)
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
        public async Task<string> GetCatalogHierarchyPath()
        {
            if (!File.Exists(Path.Combine(_root_Directory, StorageFileEnum.CatalogHierarchy.GetDisplayDescription())))
                return null;

            return Path.Combine(_root_Directory, StorageFileEnum.CatalogHierarchy.GetDisplayDescription());
        }
    }
}