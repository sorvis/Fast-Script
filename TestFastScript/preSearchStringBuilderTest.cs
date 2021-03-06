﻿using Fast_Script.PresenterFolder.Searching;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestFastScript
{
    
    
    /// <summary>
    ///This is a test class for preSearchStringBuilderTest and is intended
    ///to contain all preSearchStringBuilderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class preSearchStringBuilderTest
    {


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
        ///A test for fixBookNumberTitlesInSearchArray
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Fast Script.exe")]
        public void fixBookNumberTitlesInSearchArrayTest()
        {
            SearchParsing_Accessor target = new SearchParsing_Accessor();
            string[] text = { "1", "John", "-", "2", "John", ";", "3", "John", "4", "FakeBook" };
            string[] expected = { "1 John", "-", "2 John", ";", "3 John", "4", "FakeBook" };
            string[] actual;
            actual = PreSearchStringBuilder.FixBookNumberTitlesInSearchArray(text);
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        /// <summary>
        ///A test for combineHyphenAndDashInArray
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Fast Script.exe")]
        public void combineHyphenAndDashInArrayTest()
        {
            string[] tempList = { "John", "5", "-", "6", "Jude", "5", ":", "4" };
            string[] expected = { "John", "5-6", "Jude", "5:4" };
            string[] actual;
            actual = PreSearchStringBuilder.CombineHyphenAndDashInArray(tempList);

            Assert.IsTrue(actual.areArraysEqual(expected));
        }
    }
}
