using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Fast_Script.data_index;

namespace Fast_Script.bible_data
{
    class BibleHashTable
    {
        private Hashtable _BibleHash = new Hashtable();
        private List<Verse> _verseList = new List<Verse>();
        private int _lastVerseIndex = 0;

        //expects exact (case sensitive) reference like 1 John 3:2
        public void AddVerse(string book, int chapterNum, int verseNum, string verse)
        {
            Verse _tempVerse = new Verse(book, chapterNum, verseNum, verse);
            _verseList.Add(_tempVerse);
            _BibleHash.Add(_tempVerse.GetHashCode(), _lastVerseIndex);
            _lastVerseIndex++;
        }
        public Verse GetVerse(string reference)
        {
            return _verseList[Convert.ToInt16(_BibleHash[reference.GetHashCode()])];
        }
        public List<Verse> GetRange(string startRef, string endRef)
        {
            int startRefIndex = Convert.ToInt16(_BibleHash[startRef.GetHashCode()]);
            int endRefIndex =Convert.ToInt16(_BibleHash[endRef.GetHashCode()]);
            if (startRefIndex <= endRefIndex)
            {
                return _verseList.GetRange(startRefIndex, endRefIndex-startRefIndex+1);
            }
            else
            {
                return null;
            }
        }
    }
}
