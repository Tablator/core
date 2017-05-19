using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tablator.DataAccess.Repositories;
using Tablator.DomainModel;
using System.Linq;
using Tablator.Infrastructure.Enumerations;
using Tablator.Infrastructure.Enumerations.Tablature;
using Newtonsoft.Json.Linq;

namespace Tablator.Core.UnitTests.DataAccessUT
{
    /// <summary>
    /// Test access to a tablature data
    /// </summary>
    [TestClass]
    public class TablatureRepositoryUnitTests
    {
        /// <summary>
        /// Absolute path of the directory who contains files
        /// </summary>
        private static string _fileDirectory;

        /// <summary>
        /// List of tablature files we know as well-formatted, useable as references
        /// </summary>
        private static List<Guid> _GUITAR_WITNESS_TAB_IDS;

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            _fileDirectory = @"D:\Tablator\catalog";

            _GUITAR_WITNESS_TAB_IDS = new List<Guid>()
            {
                new Guid("4bbd4a605edf40b2bf917687e3a94755"),
                new Guid("6d492da3d317403fbb80ae717370ec88")
            };
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            _fileDirectory = null;
            _GUITAR_WITNESS_TAB_IDS = null;
        }

        /// <summary>
        /// WHAT? Read tab file and check if it contains a version information, and if this info is well-formatted
        /// WHY? File's versionning is important to know how load the file
        /// HOW? File contains a section with versionning info (major version number, minor version number, ...)
        /// </summary>
        [TestMethod]
        public void GetTabStorageFormatVersionTestMethod()
        {
            ITablatureRepository _repository;
            StorageFormatVersion sfv;

            try
            {
                _repository = new TablatureRepository(_fileDirectory);
                Assert.IsNotNull(_repository);

                foreach (Guid witnessTabId in _GUITAR_WITNESS_TAB_IDS)
                {
                    sfv = null;

                    sfv = _repository.GetTablatureStorageFormatVersion(witnessTabId);

                    Assert.IsNotNull(sfv);
                    Assert.IsTrue(sfv.Minor > 0);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                sfv = null;
                _repository = null;
            }
        }

        /// <summary>
        /// WHAT? Check if the file has some required properties, like the name of the song
        /// WHY? 
        /// HOW? 
        /// </summary>
        [TestMethod]
        public void TabPropertiesTestMethod()
        {
            ITablatureRepository _repository;
            IEnumerable<TablatureProperty> _tabProperties;

            try
            {
                _repository = new TablatureRepository(_fileDirectory);
                Assert.IsNotNull(_repository);

                foreach (Guid witnessTabId in _GUITAR_WITNESS_TAB_IDS)
                {
                    _tabProperties = _repository.ListTablatureProperties(witnessTabId);

                    Assert.IsNotNull(_tabProperties);
                    Assert.IsFalse(_tabProperties.Count() == 0);

                    Assert.IsNotNull(_tabProperties.Where(x => x.Code == (int)TablaturePropertyEnum.SongName).Select(x => x.Value).FirstOrDefault());
                    Assert.IsNotNull(_tabProperties.Where(x => x.Code == (int)TablaturePropertyEnum.Artist).Select(x => x.Value).FirstOrDefault());
                    Assert.IsNotNull(_tabProperties.Where(x => x.Code == (int)TablaturePropertyEnum.Identifier).Select(x => x.Value).FirstOrDefault());
                    Assert.AreEqual<Guid>(witnessTabId, new Guid(_tabProperties.Where(x => x.Code == (int)TablaturePropertyEnum.Identifier).Select(x => x.Value).First()));
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                _tabProperties = null;
                _repository = null;
            }
        }

        /// <summary>
        /// WHAT? Check if the file has sections declarations, and check the coherence of the data
        /// WHY? 
        /// HOW? 
        /// </summary>
        [TestMethod]
        public void TabSectionDeclarationsTestMethod()
        {
            ITablatureRepository _repository;
            IEnumerable<SectionDeclaration> _tabSectionDeclarations;

            try
            {
                _repository = new TablatureRepository(_fileDirectory);
                Assert.IsNotNull(_repository);

                foreach (Guid witnessTabId in _GUITAR_WITNESS_TAB_IDS)
                {
                    _tabSectionDeclarations = _repository.ListSectionDeclarations(witnessTabId);

                    Assert.IsNotNull(_tabSectionDeclarations);
                    Assert.IsFalse(_tabSectionDeclarations.Count() == 0);

                    // Check that unique sections have not duplicates declarations

                    if (_tabSectionDeclarations.Where(x => x.Type == (int)SectionDeclarationTypeEnum.SongStructure).Count() > 1)
                    {
                        Assert.Fail("multiple song structure section declarations");
                    }
                    else if (_tabSectionDeclarations.Where(x => x.Type == (int)SectionDeclarationTypeEnum.LyricsWithChords).Count() > 1)
                    {
                        Assert.Fail("multiple lyrics&chords section declarations");
                    }

                    // Check the coherence of the data of each section

                    foreach (SectionDeclaration sd in _tabSectionDeclarations)
                    {
                        Assert.IsNotNull(sd.Type);
                        Assert.AreNotEqual<int>(0, sd.Type);
                        Assert.IsNotNull(sd.Id);
                        Assert.AreNotEqual<Guid>(Guid.Empty, sd.Id);

                        if (sd.Type == (int)SectionDeclarationTypeEnum.LanguageResource)
                        {
                            Assert.IsNotNull(sd.Key);
                            Assert.AreNotEqual<string>(string.Empty, sd.Key);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                _tabSectionDeclarations = null;
                _repository = null;
            }
        }

        /// <summary>
        /// WHAT? Check if the file has sections implementations, and check the coherence of the data
        /// WHY? 
        /// HOW? 
        /// </summary>
        [TestMethod]
        public void TabSectionImplementationsTestMethod()
        {
            ITablatureRepository _repository;
            IEnumerable<SectionDeclaration> _tabSectionDeclarations;
            IEnumerable<SectionImplementation> sectionImplementations;

            try
            {
                _repository = new TablatureRepository(_fileDirectory);

                foreach (Guid witnessTabId in _GUITAR_WITNESS_TAB_IDS)
                {
                    _tabSectionDeclarations = _repository.ListSectionDeclarations(witnessTabId);

                    sectionImplementations = _repository.ListSectionImplementations(witnessTabId, _tabSectionDeclarations);

                    Assert.IsNotNull(sectionImplementations);
                    Assert.IsFalse(sectionImplementations.Count() == 0);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                _repository = null;
                sectionImplementations = null;
            }
        }

        /// <summary>
        /// WHAT? Check the coherence between the declarations of the sections and their implementations
        /// WHY? 
        /// HOW? 
        /// </summary>
        [TestMethod]
        public void TabSectionDeclarationAndImplementationsTestMethod()
        {
            ITablatureRepository _repository;
            IEnumerable<Guid> implementationIds;
            IEnumerable<SectionDeclaration> _tabSectionDeclarations;

            try
            {
                _repository = new TablatureRepository(_fileDirectory);

                foreach (Guid witnessTabId in _GUITAR_WITNESS_TAB_IDS)
                {
                    // List section declaration

                    _tabSectionDeclarations = _repository.ListSectionDeclarations(witnessTabId);

                    // List section implementations

                    implementationIds = _repository.ListSectionImplementationIds(witnessTabId);

                    // Check count coherence

                    Assert.IsNotNull(_tabSectionDeclarations);
                    Assert.IsNotNull(implementationIds);

                    Assert.AreNotEqual<int>(0, _tabSectionDeclarations.Count());
                    Assert.AreNotEqual<int>(0, implementationIds.Count());

                    Assert.AreEqual<int>(_tabSectionDeclarations.Count(), implementationIds.Count());

                    // Check ids coherence

                    foreach(Guid sectionId in _tabSectionDeclarations.Select(x => x.Id))
                    {
                        Assert.IsTrue(implementationIds.Contains(sectionId));
                    }
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                _repository = null;
                implementationIds = null;
                _tabSectionDeclarations = null;
            }
        }
    }
}