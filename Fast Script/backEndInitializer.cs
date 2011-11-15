using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using Fast_Script.PresenterFolder;

namespace Fast_Script
{
    public class backEndInitializer
    {
        public PresenterFolder.GUI_Settings _settings { get; set; }
        public bible_data.bible Bible
        { get { return _settings.CurrentBible; } 
            set { _settings.CurrentBible = value; } }
        public PagePrinter Printer
        { get { return _printer; } }
        private PagePrinter _printer;
        private WebpageCreator webpage;
        private Presenter _presenter;
        string _appDataStorageFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Fast_Script");
        public backEndInitializer(Presenter presenter) //TODO this method requiring presenter should be phased out in favor of requring GUI_Settings
        {
            _presenter = presenter;
            //_bible = new XLM_bible_reader.BibleBuilder("kjv.xml").GetBible;
            //_bible.BuildIndex();
            initialize();
        }
        public backEndInitializer()
        {
            initialize();
        }
        private void initialize()
        {
            checkAppDataStorageFolder();
            loadSettings();
            _settings.DefaultWebPage = Path.Combine(_appDataStorageFolder, "HTML\\page.html"); ;
            _printer = new PagePrinter(_settings);
            _settings.PrinterFont = new Font("Times New Roman", 12);
            webpage = new WebpageCreator(_settings.DefaultWebPage, "");
        }
        private void checkAppDataStorageFolder()
        {
            // create a program path in ApplicationData folder if needed
            if (!Directory.Exists(_appDataStorageFolder))
            {
                Directory.CreateDirectory(_appDataStorageFolder);
            }
        }
        private void loadSettings()
        {
            if (File.Exists("Settings.data"))
            {
                _settings = (GUI_Settings)ObjectSerializing.DeSerializeObject("Settings.data", this);
            }
            else if (File.Exists(Path.Combine(_appDataStorageFolder, "Settings.data")))
            {
                _settings = (GUI_Settings)ObjectSerializing.DeSerializeObject(Path.Combine(_appDataStorageFolder, "Settings.data"), this);
            }
            else
            {
                _settings = new GUI_Settings();
            }
        }
        // allows for serialization of bible index
        private data_index.indexBuilder loadIndex(bible_data.bible Bible)
        {
            string indexFileName = Bible._bibleVersion + ".index";
            data_index.indexReaderWriter indexStorage = new data_index.indexReaderWriter();
            if (false && File.Exists(indexFileName))
            {
                return indexStorage.DeSerializeObject(indexFileName, Bible);
            }
            else
            {
                data_index.indexBuilder index = new data_index.indexBuilder(Bible);
                //indexStorage.SerializeObject(indexFileName, index);
                return index;
            }
        }
        public void saveWebpage(string page)
        {
            webpage.writeHTMLPage(page);
        }

        // Index Methods
        public List<string> wordsThatStartWith(string prefix)
        {
            return Bible.Index.wordsThatStartWith(prefix);
        }
        public bool wordExists(string word)
        {
            data_index.word temp;
            return Bible.Index.TryGetValue(word, out temp);
        }
        public PresenterFolder.ReferenceList searchPhrase(string phrase)
        {
            List<data_index.verse> verses = Bible.Index.getVerses(phrase);
            PresenterFolder.ReferenceList newList = new PresenterFolder.ReferenceList();
            if (verses != null)
            {
                foreach (data_index.verse item in verses)
                {
                    newList.addReference(item.Book, item.Chapter, item.Verse);
                }
            }
            else
            {
                newList = null;
            }
            return newList;
        }
        // end index methods

        public string[] currentBooks
        {
            get
            {
                return Bible.Books;
            }
        }
        public int[] currentChapters(string book)
        {
            return Bible.getBook(book).Chapters;
        }
        public List<string> currentVerses(string book, int chapter)
        {
            int numberOfVerses = Bible.getBook(book).getChapter(chapter).getNumberOfVerses();
            List<string> verseList = new List<string>();
            for (int i = 0; i < numberOfVerses; i++)
            {
                verseList.Add(Convert.ToString(i+1));
            }
            return verseList;
        }
        public string getVerse(string book, int chapter, int verse)
        {
            return Bible.getVerse(book, chapter, verse);
        }
        public List<data_index.verse> getVerseRange(string startRef, string endRef)
        {
            return Bible.getVerseRange(startRef, endRef);
        }

        // printing methods
        public void PrintText(string text)
        {
            _printer.TextToPrint = text;
            _printer.filePrintMenuItem_Click(new object(), new EventArgs());
        }
        public string getPrintText()
        {
            return _printer.TextToPrint;
        }
        public void printPreview(string text)
        {
            _printer.TextToPrint = text;
            _printer.filePrintPreviewMenuItem_Click(new object(),new EventArgs());
        }
        public void printSetup(object sender, EventArgs e)
        {
            _printer.filePageSetupMenuItem_Click(sender, e);
        }
        // end printing methods

    }
}
