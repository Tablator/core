using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tablator.DataAccess.Repositories;
using Tablator.DomainModel;
using System.Linq;
using Tablator.Infrastructure.Enumerations;

namespace Tablator.Core.UnitTests.DataAccessUT
{
    [TestClass]
    public class ChordRepositoryUnitTests
    {
        /// <summary>
        /// Absolute path of the directory who contains files
        /// </summary>
        private static string _fileDirectory;

        /// <summary>
        /// List of chords files we know as well-formatted, useable as references
        /// </summary>
        private static List<Guid> _GUITAR_WITNESS_CHORD_IDS;

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            _fileDirectory = @"D:\Tablator\catalog\chords";

            _GUITAR_WITNESS_CHORD_IDS = new List<Guid>()
            {
                new Guid("4A1586EB-80AE-44C9-9BBE-008DDE815382"),
                new Guid("C70C2A4E-98D8-46A1-ABFF-1842953AF132"),
                new Guid("FF42BE7D-8DC7-470B-BE8D-014457EE317F")
            };
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            _fileDirectory = null;
            _GUITAR_WITNESS_CHORD_IDS = null;
        }

        /// <summary>
        /// WHAT? 
        /// WHY? 
        /// HOW? 
        /// </summary>
        [TestMethod]
        public void CheckChordsDataTestMethod()
        {
            IChordRepository _repository;

            try
            {
                _repository = new ChordRepository(_fileDirectory);
                Assert.IsNotNull(_repository);

                foreach (Guid witnessChordId in _GUITAR_WITNESS_CHORD_IDS)
                {
                    Chord c = _repository.Get(witnessChordId);

                    Assert.IsNotNull(c);
                    Assert.IsNotNull(c.Attributes);
                    Assert.IsNotNull(c.Compositions);

                    Assert.AreNotEqual<int>(0, c.Attributes.Count());
                    Assert.AreNotEqual<int>(0, c.Compositions.Count());

                    Assert.AreNotEqual<Guid>(Guid.Empty, c.Id);
                    Assert.AreEqual<Guid>(witnessChordId, c.Id);

                    Assert.AreNotEqual<int>(0, c.Attributes.First().Code);

                    c = null;
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                _repository = null;
            }
        }
    }
}
