using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tablator.BusinessLogic.Services;
using Tablator.DataAccess.Repositories;
using Tablator.BusinessModel;

namespace Tablator.Core.UnitTests.BusinessLogicUT
{
    [TestClass]
    public class TablatureServiceUnitTests
    {
        /// <summary>
        /// Absolute path of the directory who contains files
        /// </summary>
        private static string _fileDirectory;

        /// <summary>
        /// List of tablature files we know as well-formatted, useable as references
        /// </summary>
        private static List<Guid> _WITNESS_TAB_IDS;

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            _fileDirectory = @"D:\Tablator\catalog";

            _WITNESS_TAB_IDS = new List<Guid>()
            {
                new Guid("4bbd4a605edf40b2bf917687e3a94755"),
                new Guid("6d492da3d317403fbb80ae717370ec88")
            };
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            _fileDirectory = null;
            _WITNESS_TAB_IDS = null;
        }

        [TestMethod]
        public void TablatureAttributesDataCheckTestMethod()
        {
            ITablatureRepository _repository;
            TablatureService _service;
            TablatureModel tab;

            try
            {
                _repository = new TablatureRepository(_fileDirectory);
                Assert.IsNotNull(_repository);

                _service = new TablatureService(_repository);
                Assert.IsNotNull(_service);

                foreach (Guid witnessId in _WITNESS_TAB_IDS)
                {
                    tab = _service.Get(witnessId);
                    Assert.IsNotNull(tab);

                    Assert.AreNotEqual<Guid>(Guid.Empty, tab.Id);

                    tab = null;
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message, ex);
            }
            finally
            {
                _repository = null;
                _service = null;
                tab = null;
            }
        }
    }
}