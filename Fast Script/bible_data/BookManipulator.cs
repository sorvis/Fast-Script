using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fast_Script.bible_data
{
    public class BookManipulator
    {
        private Bible _bible; // bible as a data tree
        private BibleHashTable _bibleHash; // bible in a hash table

        public BookManipulator(Bible item)
        {
            _bible = item;
            _bibleHash = new BibleHashTable();
        }
        public void SetVersion(string version)
        {
            _bible.BibleVersion = version;
        }
        public Bible GetBible()
        {
            return _bible;
        }
        public string GetVerse(string book, int chapterNum, int verseNum)
        {
            //return getChapter(book, chapterNum).getVerse(verseNum);
            // hash table expected to be faster
            return _bibleHash.GetVerse(book + " " + chapterNum + ":" + verseNum).GetVerseText();
        }
        public List<data_index.Verse> getVerseRange(string startRef, string endRef)
        { return _bibleHash.GetRange(startRef, endRef); }
        private Chapter getChapter(string book, int chapterNum)
        {
            return getBook(book).GetChapter(chapterNum);
        }
        private Book getBook(string book)
        {
            return _bible.GetBook(book);
        }
        public void AddVerse(string verse, string book, int chapter, int verseNumber, bool replace)
        {
            addChapter(book, chapter); // check to make sure chapter and book is in existance
            Chapter currentChapter = _bible.GetBook(book).GetChapter(chapter);
            if (currentChapter.GetVerse(verseNumber) == null || replace)
            {
                currentChapter.AddVerse(verse, verseNumber);
                _bibleHash.AddVerse(book, chapter, verseNumber, verse); // save also to hash
            }
        }
        private void addChapter(string bookTitle, int chapter)
        {
            addBook(bookTitle);
            Book currentBook = _bible.GetBook(bookTitle);
            if (currentBook.GetChapter(chapter).GetChapterNumber() == new Chapter(null).GetChapterNumber()) // check for null chapter (non existent)
            {
                currentBook.AddChapter(new Chapter(chapter));
            }
        }
        private void addBook(string bookTitle)
        {
            if (_bible.GetBook(bookTitle).GetTitle() == new Book("null").GetTitle() )
            {
                _bible.AddBook(new Book(bookTitle));
            }
        }

    }
}
