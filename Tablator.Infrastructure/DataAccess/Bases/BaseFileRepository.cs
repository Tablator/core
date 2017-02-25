﻿namespace Tablator.Infrastructure.DataAccess.Bases
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using Infrastructure.Extensions;

    /// <summary>
    /// Base class to deal with Tablator file storage system
    /// </summary>
    public abstract class BaseFileRepository
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
        protected async Task<string> GetCatalogHierarchyPath()
        {
            if (!File.Exists(Path.Combine(_root_Directory, StorageFileEnum.CatalogHierarchy.GetDisplayDescription())))
                return null;

            return Path.Combine(_root_Directory, StorageFileEnum.CatalogHierarchy.GetDisplayDescription());
        }

        protected bool IsSectionExist(string filePath)
        {
            throw new NotImplementedException();
        }

        protected bool TryGetFileSectionContent(string filePath, out string json)
        {
            throw new NotImplementedException();
        }
    }
}