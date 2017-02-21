using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tablator.Infrastructure.Extensions;
using Tablator.Infrastructure.Enumerations;

namespace Tablator.Infrastructure.UnitTests.Extensions
{
    /// <summary>
    /// Description résumée pour Enumeration
    /// </summary>
    [TestClass]
    public class EnumerationUnitTest
    {
        public EnumerationUnitTest()
        {
            //
            // TODO: ajoutez ici la logique du constructeur
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Obtient ou définit le contexte de test qui fournit
        ///des informations sur la série de tests active, ainsi que ses fonctionnalités.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Attributs de tests supplémentaires
        //
        // Vous pouvez utiliser les attributs supplémentaires suivants lorsque vous écrivez vos tests :
        //
        // Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test de la classe
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Utilisez ClassCleanup pour exécuter du code une fois que tous les tests d'une classe ont été exécutés
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void GetValueFromDisplayDescriptionUnitTest()
        {
            GuitarChordEnum g = EnumerationExtensions.GetValueFromDisplayDescription<GuitarChordEnum>("|0|2|2|1|0|0");
            Assert.AreEqual<GuitarChordEnum>(g, GuitarChordEnum.Am);
        }
    }
}
