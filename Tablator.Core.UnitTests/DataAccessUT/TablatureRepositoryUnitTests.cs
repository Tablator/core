using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Tablator.DataAccess.Repositories;
using Tablator.DomainModel;
using System.Linq;
using Tablator.Infrastructure.Enumerations;

namespace Tablator.Core.UnitTests.DataAccessUT
{
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
        public void GetGuitarTabStorageFormatVersionTestMethod()
        {
            TablatureRepository _repository;
            StorageFormatVersion sfv;

            try
            {
                _repository = new TablatureRepository(_fileDirectory);

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
        /// WHAT? 
        /// WHY? 
        /// HOW? 
        /// </summary>
        [TestMethod]
        public void GetGuitarTabPropertiesTestMethod()
        {
            TablatureRepository _repository;
            IEnumerable<TablatureProperty> _tabProperties;

            try
            {
                _repository = new TablatureRepository(_fileDirectory);

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
        /// WHAT? 
        /// WHY? 
        /// HOW? 
        /// </summary>
        [TestMethod]
        public void GetGuitarTabSectionDeclarationsTestMethod()
        {
            TablatureRepository _repository;
            IEnumerable<SectionDeclaration> _tabSectionDeclarations;

            try
            {
                _repository = new TablatureRepository(_fileDirectory);

                foreach (Guid witnessTabId in _GUITAR_WITNESS_TAB_IDS)
                {
                    _tabSectionDeclarations = _repository.ListSectionDeclarations(witnessTabId);

                    Assert.IsNotNull(_tabSectionDeclarations);
                    Assert.IsFalse(_tabSectionDeclarations.Count() == 0);

                    
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
    }
}