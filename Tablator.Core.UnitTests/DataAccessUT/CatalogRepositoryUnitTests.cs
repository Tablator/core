using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Tablator.DataAccess.Repositories;
using Tablator.DomainModel;
using System.Linq;

namespace Tablator.Core.UnitTests.DataAccessUT
{
    [TestClass]
    public class CatalogRepositoryUnitTests
    {
        /// <summary>
        /// Absolute path of the directory who contains files
        /// </summary>
        private static string _fileDirectory;

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            _fileDirectory = @"D:\Tablator\catalog";
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            _fileDirectory = null;
        }

        [TestMethod]
        public void CatalogFileExistTestMethod()
        {
            Assert.IsTrue(File.Exists(Path.Combine(_fileDirectory, "cat_hierarchy.tbltr")));
            Assert.IsTrue(File.Exists(Path.Combine(_fileDirectory, "cat_references.tbltr")));
        }

        [TestMethod]
        public void CatalogHierarchyFileTestMethod()
        {
            ICatalogRepository _repository;
            CatalogHierarchyCollectionLevel hierarchyList;

            try
            {
                _repository = new CatalogRepository(_fileDirectory);
                Assert.IsNotNull(_repository);

                hierarchyList = _repository.ListHierarchyLevels();

                Assert.IsNotNull(hierarchyList);
                Assert.IsNotNull(hierarchyList.HierarchyLevels);
                Assert.AreNotEqual<int>(0, hierarchyList.HierarchyLevels.Count());

                foreach (CatalogHierarchyLevel hier in hierarchyList.HierarchyLevels)
                {
                    Assert.AreNotEqual<Guid>(Guid.Empty, hier.Id);
                    Assert.IsNotNull(hier.Name);
                    Assert.AreNotEqual<string>(string.Empty, hier.Name);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                _repository = null;
                hierarchyList = null;
            }
        }

        [TestMethod]
        public void CatalogReferencesFileTestMethod()
        {
            ICatalogRepository _repository;
            CatalogHierarchyTabReferenceCollection refs;

            try
            {
                _repository = new CatalogRepository(_fileDirectory);
                Assert.IsNotNull(_repository);

                refs = _repository.ListReferences();

                Assert.IsNotNull(refs);
                Assert.IsNotNull(refs.Refs);
                Assert.AreNotEqual<int>(0, refs.Refs.Count());

                foreach (CatalogHierarchyTabReference _ref in refs.Refs)
                {
                    Assert.AreNotEqual<Guid>(Guid.Empty, _ref.Id);
                    Assert.IsNotNull(_ref.Name);
                    Assert.AreNotEqual<string>(string.Empty, _ref.Name);
                    Assert.IsNotNull(_ref.UrlPath);
                    Assert.AreNotEqual<string>(string.Empty, _ref.UrlPath);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                refs = null;
                _repository = null;
            }
        }

        [TestMethod]
        public void TablatureIdByReferenceTestMethod()
        {
            ICatalogRepository _repository;
            Dictionary<Guid, string> fileRefs;

            try
            {
                _repository = new CatalogRepository(_fileDirectory);
                Assert.IsNotNull(_repository);

                fileRefs = new Dictionary<Guid, string>();
                fileRefs.Add(new Guid("4BBD4A60-5EDF-40B2-BF91-7687E3A94755"), "francis-cabrel-je-l-aime-a-mourir-guitar-tab");
                fileRefs.Add(new Guid("6d492da3-d317-403f-bb80-ae717370ec88"), "guitar/oasis/dont-look-back-in-anger");

                foreach (KeyValuePair<Guid, string> fileRef in fileRefs)
                {
                    Guid? g = _repository.GetTablatureId(fileRef.Value);

                    Assert.IsNotNull(g);
                    Assert.AreEqual<Guid>(fileRef.Key, g.Value);

                    g = null;
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                fileRefs = null;
                _repository = null;
            }
        }
    }
}