namespace Tablator.BusinessLogic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Service to handle storage file system
    /// </summary>
    public interface IStorageFileService
    {
        /// <summary>
        /// Get the catalog hierarchy file's absolute path
        /// </summary>
        /// <returns>hierarchy file's absolute path or null</returns>
        /// <remarks>Checks if the file exists. If not, returns null.</remarks>
        Task<string> GetCatalogHierarchyPath();
    }
}