using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fast_Script.bible_data
{
    public class bookManipulator
    {
        private bible _bible; // bible as a data tree
        private BibleHashTable _bibleHash; // bible in a hash table

        public bookManipulator(bible item)
        {
            _bible = item;
            _bibleHash = new BibleHashTable();
        }
        public void setVersion(string version)
        {
            _bible._bibleVersion = version;
        }
        public bible getBible()
        {
            return _bible;
        }
        public string getVerse(string book, int chapterNum, int verseNum)
        {
            //return getChapter(book, chapterNum).getVerse(verseNum);
            // hash table expected to be faster
            return _bibleHash.getVerse(book + " " + chapterNum + ":" + verseNum).getVerseText();
        }
        public List<data_index.verse> getVerseRange(string startRef, string endRef)
        { return _bibleHash.getRange(startRef, endRef); }
        private chapter getChapter(string book, int chapterNum)
        {
            return getBook(book).getChapter(chapterNum);
        }
        private book getBook(string book)
        {
            return _bible.getBook(book);
        }
        public void addVerse(string verse, string book, int chapter, int verseNumber, bool replace)
        {
            addChapter(book, chapter); // check to make sure chapter and book is in existance
            chapter currentChapter = _bible.getBook(book).getChapter(chapter);
            if (currentChapter.getVerse(verseNumber) == null || replace)
            {
                currentChapter.addVerse(verse, verseNumber);
                _bibleHash.addVerse(book, chapter, verseNumber, verse); // save also to hash
            }
        }
        private void addChapter(string bookTitle, int chapter)
        {
            addBook(bookTitle);
            book currentBook = _bible.getBook(bookTitle);
            if (currentBook.getChapter(chapter).getChapterNumber() == new chapter(null).getChapterNumber()) // check for null chapter (non existent)
            {
                currentBook.addChapter(new chapter(chapter));
            }
        }
        private void addBook(string bookTitle)
        {
            if (_bible.getBook(bookTitle).getTitle() == new book("null").getTitle() )
            {
                _bible.addBook(new book(bookTitle));
            }
        }

    }
}
