using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Fast_Script.bible_data
{
    public class bible
    {
        public string _bibleVersion { get; set; }
        private List<book> _books;
        private List<string> _booksIndex;
        private bookManipulator _manipulator;
        private data_index.indexBuilder _bibleIndex;
        public data_index.indexBuilder Index
        {get { return _bibleIndex; }set { _bibleIndex = value; }}
        public List<book> getAllBooks()
        {
            return _books;
        }
        public string[] Books
        {
            get
            {
                return _booksIndex.ToArray();
            }
        }
        private BackgroundWorker _indexBuilderWorker;
        public BackgroundWorker indexBuilderWorker { get{return _indexBuilderWorker;} }
        public bible()
        {
            _books = new List<book>();
            _booksIndex = new List<string>();
            _manipulator = new bookManipulator(this);
            _indexBuilderWorker = new BackgroundWorker();
            _indexBuilderWorker.DoWork += new DoWorkEventHandler(indexBuilderWorker_DoWork);
        }
        public void BuildIndex() // uses threading to allow gui to start while indexing happens
        {
            indexBuilderWorker.RunWorkerAsync(this);
            //Index = new data_index.indexBuilder(this);
        }
        public void BuildIndex_withOutThreading()
        {
            Index = new data_index.indexBuilder(this);
        }
        private void indexBuilderWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Index = new data_index.indexBuilder((bible) e.Argument);
        }
        public string getVerse(string book, int chapter, int verse)
        {
            return _manipulator.getVerse(book, chapter, verse);
        }
        public List<data_index.verse> getVerseRange(string startRef, string endRef)
        {
            return _manipulator.getVerseRange(startRef, endRef);
        }
        public bookManipulator getManipulator()
        {
            return _manipulator;
        }
        public string getVersion()
        {
            return _bibleVersion;
        }
        public void addBook(book item)
        {
            _books.Add(item);
            _booksIndex.Add(item.getTitle());
        }
        public void removeBook(string title)
        {
            int index = _booksIndex.IndexOf(title);
            _booksIndex.RemoveAt(index);
            _books.RemoveAt(index);
        }
        public book getBook(string title)
        {
            if (_booksIndex.Contains(title))
            {
                int index = _booksIndex.IndexOf(title);
                return _books[index];
            }
            else
            {
                return new book("null");
            }
        }
    }
}
