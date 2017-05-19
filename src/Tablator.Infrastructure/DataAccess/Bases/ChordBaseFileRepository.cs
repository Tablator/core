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

    public abstract class ChordBaseFileRepository : IdFileBaseRepository
    {
        /// <summary>
        /// New instance of a file's chord repository
        /// </summary>
        /// <param name="dir">Files root directory path</param>
        public ChordBaseFileRepository(string dir)
            : base(dir, FileExtensionConfiguration.Chord)
        {

        }
    }
}