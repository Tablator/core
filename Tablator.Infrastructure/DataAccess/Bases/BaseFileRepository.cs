namespace Tablator.Infrastructure.DataAccess.Bases
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Base class to deal with Tablator file storage system
    /// </summary>
    public abstract class BaseFileRepository
    {
        public BaseFileRepository()
        { }

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