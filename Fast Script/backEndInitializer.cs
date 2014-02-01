using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using Fast_Script.PresenterFolder;

namespace Fast_Script
{
    public class BackEndInitializer
    {
        public PresenterFolder.GUISettings _settings { get; set; }
        public bible_data.Bible Bible
        { get { return _settings.CurrentBible; } 
            set { _settings.CurrentBible = value; } }
        public PagePrinter Printer
        { get { return _printer; } }
        private PagePrinter _printer;
        private WebpageCreator _webpage;
        private Presenter _presenter;
        string _appDataStorageFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Fast_Script");

        public BackEndInitializer(Presenter presenter) //TODO this method requiring presenter should be phased out in favor of requring GUI_Settings
        {
            _presenter = presenter;
            initialize();
        }

        public BackEndInitializer()
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
            _webpage = new WebpageCreator(_settings.DefaultWebPage, "");
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
                _settings = (GUISettings)ObjectSerializing.DeSerializeObjectFromFile("Settings.data", this);
            }
            else if (File.Exists(Path.Combine(_appDataStorageFolder, "Settings.data")))
            {
                try
                {
                    _settings = (GUISettings)ObjectSerializing.DeSerializeObjectFromFile(Path.Combine(_appDataStorageFolder, "Settings.data"), this);
                }
                catch
                {
                    _settings = new GUISettings();
                }
            }
            else
            {
                _settings = new GUISettings();
            }
        }

        // allows for serialization of bible index
        private data_index.IndexBuilder loadIndex(bible_data.Bible Bible)
        {
            string indexFileName = Bible.BibleVersion + ".index";
            data_index.IndexReaderWriter indexStorage = new data_index.IndexReaderWriter();
            data_index.IndexBuilder index = new data_index.IndexBuilder(Bible);
            return index;
        }

        public void SaveWebpage(string page)
        {
            _webpage.writeHTMLPage(page);
        }

        // Index Methods
        public List<string> WordsThatStartWith(string prefix)
        {
            return Bible.Index.WordsThatStartWith(prefix);
        }

        public bool WordExists(string word)
        {
            data_index.Word temp;
            return Bible.Index.TryGetValue(word, out temp);
        }

        public PresenterFolder.ReferenceList searchPhrase(string phrase)
        {
            List<data_index.Verse> verses = Bible.Index.GetVerses(phrase);
            PresenterFolder.ReferenceList newList = new PresenterFolder.ReferenceList();
            if (verses != null)
            {
                foreach (data_index.Verse item in verses)
                {
                    newList.AddReference(item.Book, item.Chapter, item.VerseNumber);
                }
            }
            else
            {
                newList = null;
            }
            return newList;
        }
        // end index methods

        public string[] CurrentBooks
        {
            get
            {
                return Bible.Books;
            }
        }

        public int[] CurrentChapters(string book)
        {
            return Bible.GetBook(book).Chapters;
        }

        public List<string> CurrentVerses(string book, int chapter)
        {
            int numberOfVerses = Bible.GetBook(book).GetChapter(chapter).GetNumberOfVerses();
            List<string> verseList = new List<string>();
            for (int i = 0; i < numberOfVerses; i++)
            {
                verseList.Add(Convert.ToString(i+1));
            }
            return verseList;
        }

        public string GetVerse(string book, int chapter, int verse)
        {
            return Bible.GetVerse(book, chapter, verse);
        }

        public List<data_index.Verse> GetVerseRange(string startRef, string endRef)
        {
            return Bible.GetVerseRange(startRef, endRef);
        }

        // printing methods
        public void PrintText(string text)
        {
            _printer.TextToPrint = text;
            _printer.filePrintMenuItem_Click(new object(), new EventArgs());
        }

        public string GetPrintText()
        {
            return _printer.TextToPrint;
        }

        public void PrintPreview(string text)
        {
            _printer.TextToPrint = text;
            _printer.filePrintPreviewMenuItem_Click(new object(),new EventArgs());
        }

        public void PrintSetup(object sender, EventArgs e)
        {
            _printer.filePageSetupMenuItem_Click(sender, e);
        }
        // end printing methods

        public void SaveSettings()
        {
            ObjectSerializing.SerializeObjectToFile(Path.Combine(_appDataStorageFolder, "Settings.data"), _settings);
        }
    }
}
