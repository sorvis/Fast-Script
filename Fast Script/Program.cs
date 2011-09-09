using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Fast_Script
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {
            //bible_data.bible testversion = new bible_data.bible();
            //bible_data.bookManipulator testManipulator = testversion.getManipulator();


            //string testverse = testManipulator.getVerse("test book", 2, 3);
            //testManipulator.addVerse("test verse test", "test book", 2, 3, false);
            //string testverse2 = testManipulator.getVerse("test book", 2, 3);
            //testManipulator.addVerse("test verse tes22222t", "test book", 1, 3, false);
            //testManipulator.addVerse("test versesdfasdfasdfsd ftest", "test book", 1, 10, false);
            //testManipulator.addVerse("replacement", "test book", 1, 10, true);

            //XLM_bible_reader.openXML testReader = new XLM_bible_reader.openXML("kjv.xml", testManipulator);
            //data_index.indexBuilder buildIndex = new data_index.indexBuilder(testversion);

            //StopWatch s = new StopWatch();
            //s.Start();

            //List<data_index.verse> testVerseList = buildIndex.getVerses("in and");
            //string testVerseString;
            //if (testVerseList != null)
            //{
            //    testVerseString = testManipulator.getVerse(testVerseList[0].Book, testVerseList[0].Chapter, testVerseList[0].Verse);
            //}

            //foreach (data_index.verse item in testVerseList)
            //{
            //    if (!testManipulator.getVerse(item.Book, item.Chapter, item.Verse).Contains("the"))
            //    {
            //        testVerseString = testManipulator.getVerse(item.Book, item.Chapter, item.Verse);
            //        bool test = true;
            //    }
            //}

            //s.Stop();
            //double time = s.GetElapsedTime();

            //string testString = (testManipulator.getVerse("John", 1, 1) + "\n" +
            //    testManipulator.getVerse("John", 1, 2));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
