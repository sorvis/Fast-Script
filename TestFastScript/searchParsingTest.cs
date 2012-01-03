using Fast_Script.PresenterFolder.Searching;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Fast_Script;
using System.Collections.Generic;

namespace TestFastScript
{
    
    
    /// <summary>
    ///This is a test class for searchParsingTest and is intended
    ///to contain all searchParsingTest Unit Tests
    ///</summary>
    [TestClass()]
    public class searchParsingTest
    {

        backEndInitializer backEnd = new backEndInitializer();

        public searchParsingTest()
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

        private bool areArraysEqual(object[] expected, object[] actual)
        {
            //check length
            if (expected.Length != actual.Length)
            {
                return false;
            }

            //check whole array
            for (int i = 0; i < expected.Length; i++)
            {
                if (expected[i].ToString() != actual[i].ToString())
                {
                    return false;
                }
            }

            return true;    // must be the same
        }

        /// <summary>
        ///A test for capitalizeWord
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Fast Script.exe")]
        public void capitalizeWordTest()
        {
            searchParsing_Accessor target = new searchParsing_Accessor(); 
            Assert.AreEqual("cat".capitalizeWord(), "Cat");
            Assert.AreEqual("CAT".capitalizeWord(), "Cat");
            Assert.AreEqual("cAT".capitalizeWord(), "Cat");
            Assert.AreEqual("Cat".capitalizeWord(), "Cat");
            Assert.AreEqual("1 cat".capitalizeWord(), "1 Cat");
        }

        /// <summary>
        ///A test for searchString in relation to returning a suggestions array
        ///</summary>
        [TestMethod()]
        public void searchStringTest()
        {
            FakeMainWindow fakeView = new FakeMainWindow();
            FakePresenter fakePresenter = new FakePresenter();
            fakePresenter.Backend = backEnd;
            searchParsing target = new searchParsing(fakePresenter);

            // test book suggest
            string originalSearch = "Jud";
            List<string> expected = new List<string>();
            expected.Add("Judges");
            expected.Add("Jude");
            target.searchString(originalSearch, backEnd, fakeView);
            Assert.IsTrue(areArraysEqual(expected.ToArray(), fakeView.getSuggestionsList().ToArray()));
            expected.Clear();
            fakeView.reset();

            // test word suggest
            originalSearch = "Horselea";
            expected.Add("horseleach");
            target.searchString(originalSearch, backEnd, fakeView);
            Assert.IsTrue(areArraysEqual(expected.ToArray(), fakeView.getSuggestionsList().ToArray()));
            expected.Clear();
            fakeView.reset();


            //test chapter suggest
            originalSearch = "Jude";
            expected.Add("Jude 1");
            target.searchString(originalSearch, backEnd, fakeView);
            Assert.IsTrue(areArraysEqual(expected.ToArray(), fakeView.getSuggestionsList().ToArray()));
            expected.Clear();
            fakeView.reset();

            //test verse suggest
            originalSearch = "1 John 1:";
            expected.AddRange(new string[] { "1 John 1:1", "1 John 1:2", "1 John 1:3", "1 John 1:4", "1 John 1:5", "1 John 1:6", "1 John 1:7", "1 John 1:8", "1 John 1:9", "1 John 1:10" });
            target.searchString(originalSearch, backEnd, fakeView);
            Assert.IsTrue(areArraysEqual(expected.ToArray(), fakeView.getSuggestionsList().ToArray()));
            expected.Clear();
            fakeView.reset();

            // tests that break program:

            ////test book range suggest with space before hyphen
            //originalSearch = "1 John -";
            //expected.AddRange(new string[] { "2 John", "3 John", "Jude", "Revelations"});
            //target.searchString(originalSearch, backEnd, fakeView);
            //Assert.IsTrue(areArraysEqual(expected.ToArray(), fakeView.getSuggestionsList().ToArray()));
            //expected.Clear();
            //fakeView.reset();

            ////test chapter range suggest with space before hyphen
            //originalSearch = "1 John 1 -";
            //expected.AddRange(new string[] { "1 John 1:1", "1 John 1:2", "1 John 1:3", "1 John 1:4", "1 John 1:5", "1 John 1:6", "1 John 1:7", "1 John 1:8", "1 John 1:9", "1 John 1:10" });
            //target.searchString(originalSearch, backEnd, fakeView);
            //Assert.IsTrue(areArraysEqual(expected.ToArray(), fakeView.getSuggestionsList().ToArray()));
            //expected.Clear();
            //fakeView.reset();
            
            ////test verse range suggest
            //originalSearch = "1 John 1-";
            //expected.AddRange(new string[] { "1 John 1:1", "1 John 1:2", "1 John 1:3", "1 John 1:4", "1 John 1:5", "1 John 1:6", "1 John 1:7", "1 John 1:8", "1 John 1:9", "1 John 1:10" });
            //target.searchString(originalSearch, backEnd, fakeView);
            //Assert.IsTrue(areArraysEqual(expected.ToArray(), fakeView.getSuggestionsList().ToArray()));
            //expected.Clear();
            //fakeView.reset();
        }
    }
}
