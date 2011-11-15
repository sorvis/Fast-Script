using Fast_Script.PresenterFolder.Searching;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Fast_Script;
using System.Collections.Generic;

namespace TestFastScript
{
    
    
    /// <summary>
    ///This is a test class for indexLookerTest and is intended
    ///to contain all indexLookerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class indexLookerTest
    {
        backEndInitializer backEnd = new backEndInitializer();

        public indexLookerTest()
        {
            backEnd.Bible.BuildIndex_withOutThreading();
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
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

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for getChapters
        ///</summary>
        [TestMethod()]
        public void getChaptersTest()
        {
            indexLooker target = new indexLooker(backEnd);
            string book = "Jude";
            List<string> expected = new List<string>(new string[]
            {
                "1"
            });
            List<string> actual;
            actual = target.getChapters(book);

            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }
    }
}
