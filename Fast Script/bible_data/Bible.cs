using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Fast_Script.bible_data
{
    public class Bible
    {
        public string BibleVersion { get; set; }
        private List<Book> _books;
        private List<string> _booksIndex;
        private BookManipulator _manipulator;
        private data_index.IndexBuilder _bibleIndex;
        public data_index.IndexBuilder Index
        {get { return _bibleIndex; }set { _bibleIndex = value; }}
        public List<Book> GetAllBooks()
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
        public BackgroundWorker IndexBuilderWorker { get{return _indexBuilderWorker;} }
        public Bible()
        {
            _books = new List<Book>();
            _booksIndex = new List<string>();
            _manipulator = new BookManipulator(this);
            _indexBuilderWorker = new BackgroundWorker();
            _indexBuilderWorker.DoWork += new DoWorkEventHandler(indexBuilderWorker_DoWork);
        }
        public void BuildIndex() // uses threading to allow gui to start while indexing happens
        {
            IndexBuilderWorker.RunWorkerAsync(this);
            //Index = new data_index.indexBuilder(this);
        }
        public void BuildIndex_withOutThreading()
        {
            Index = new data_index.IndexBuilder(this);
        }
        private void indexBuilderWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Index = new data_index.IndexBuilder((Bible) e.Argument);
        }
        public string GetVerse(string book, int chapter, int verse)
        {
            return _manipulator.GetVerse(book, chapter, verse);
        }
        public List<data_index.Verse> GetVerseRange(string startRef, string endRef)
        {
            return _manipulator.getVerseRange(startRef, endRef);
        }
        public BookManipulator GetManipulator()
        {
            return _manipulator;
        }
        public string GetVersion()
        {
            return BibleVersion;
        }
        public void AddBook(Book item)
        {
            _books.Add(item);
            _booksIndex.Add(item.GetTitle());
        }
        public void RemoveBook(string title)
        {
            int index = _booksIndex.IndexOf(title);
            _booksIndex.RemoveAt(index);
            _books.RemoveAt(index);
        }
        public Book GetBook(string title)
        {
            if (_booksIndex.Contains(title))
            {
                int index = _booksIndex.IndexOf(title);
                return _books[index];
            }
            else
            {
                return new Book("null");
            }
        }
    }
}
