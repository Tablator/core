namespace Tablator.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DomainModel;

    /// <summary>
    /// Repository to deal with tablatures data
    /// </summary>
    public interface ITablatureRepository
    {
        Tablature Get(Guid id);

        /// <summary>
        /// WHAT? Returns the version of the storage format of the tablature
        /// </summary>
        /// <param name="id">tablature's identifier</param>
        /// <returns>The sotrage format's version, or null if a problem occurred</returns>
        StorageFormatVersion GetTablatureStorageFormatVersion(Guid id);

        /// <summary>
        /// WHAT? List the properties of the tablature, like the name of the song, or the name of the artist.
        /// It's the common properties for all the instruments
        /// WHY? To get the main information of the tab
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<TablatureProperty> ListTablatureProperties(Guid id);

        /// <summary>
        /// WHAT?
        /// WHY?
        /// </summary>
        /// <param name="id"></param>
        /// <param name="declarations"></param>
        /// <returns></returns>
        IEnumerable<SectionImplementation> ListSectionImplementations(Guid id, IEnumerable<SectionDeclaration> declarations);

        /// <summary>
        /// List all identifiers of the section implementations
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<Guid> ListSectionImplementationIds(Guid id);
    }
}