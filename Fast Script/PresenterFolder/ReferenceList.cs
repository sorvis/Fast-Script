using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Fast_Script.PresenterFolder
{
    [Serializable()]
    public class ReferenceList : ISerializable
    {
        ReferenceItem _currentRefernce;
        public ReferenceItem currentRefernce
        {
            get { return _currentRefernce; }
            set { _currentRefernce = value; }
        }
        List<ReferenceItem> _referenceList;
        public List<ReferenceItem> getList
        {
            get { return _referenceList; }
        }
        public ReferenceList()
        {
            _referenceList = new List<ReferenceItem>();
            _currentRefernce = null;
        }
        public ReferenceList(List<ReferenceItem> items)
        {
            _referenceList = items;
            _currentRefernce = null;
        }
        public void addReferenceItem(ReferenceItem item)
        {
            _referenceList.Add(item);
        }
        // assumes the ReferenceItem has already been built
        public List<ReferenceItem> buildVerseListFromRange(ReferenceItem item, backEndInitializer backend)
        {
            if (item.range)
            {
                List<ReferenceItem> expandedReferenceList = new List<ReferenceItem>();

                // get list of the books
                int startBookIndex = Array.IndexOf(backend.currentBooks, item.startBook);
                int endBookIndex = Array.IndexOf(backend.currentBooks, item.endBook);
                int startingChapter;
                int endingChapter;
                int startingVerse;
                int endingVerse;
                string book;
                // cycle through the books selected
                for (int bookIndex = startBookIndex; bookIndex <= endBookIndex; bookIndex++)
                {
                    book = backend.currentBooks[bookIndex];

                    if (book == item.startBook) // get starting chapter and verse
                    { startingChapter = (int)item.startChapter; }
                    else
                    { startingChapter = 1; }
                    if (book == item.endBook) // get ending chapter and verse
                    { endingChapter = (int)item.endChapter; }
                    else
                    { endingChapter = backend.currentChapters(book).Count(); }

                    // cycle through chapters for current book
                    for (int chapter = startingChapter; chapter <= endingChapter; chapter++)
                    {
                        if (book == item.startBook && chapter == item.startChapter)
                        { startingVerse = (int)item.startVerse; }
                        else
                        { startingVerse = 1; }
                        if (book == item.endBook && chapter == item.endChapter)
                        { endingVerse = (int)item.endVerse; }
                        else
                        { endingVerse = backend.currentVerses(book, chapter).Count; }

                        // cycle through verses for currect chapter
                        for (int verse = startingVerse; verse <= endingVerse; verse++)
                        {
                            expandedReferenceList.Add(new ReferenceItem(book, chapter, 
                                verse, false, backend.getVerse(book, chapter, verse)));
                        }
                    }
                }

                return expandedReferenceList;
            }
            else 
            {
                return null;
            }
        }
        public ReferenceItem addReference(string startingBook)
        {
            ReferenceItem item = new ReferenceItem();
            _referenceList.Add(item);
            _currentRefernce = item;
            item.startBook = startingBook;
            return item;
        }
        public ReferenceItem addReference(string startingBook, int startChapter, int startVerse)
        {
            ReferenceItem item = new ReferenceItem();
            _referenceList.Add(item);
            _currentRefernce = item;
            item.startBook = startingBook;
            item.startChapter = startChapter;
            item.startVerse = startVerse;
            return item;
        }
        public void appendReferenceList(ReferenceList oldList)
        {
            if (oldList != null)
            {
                foreach (ReferenceItem item in oldList.getList)
                {
                    _referenceList.Add(item);
                }
            }
        }
        public void completeReferences(backEndInitializer backend) // copy over full reference data on each item
        {
            ReferenceItem reff;
            for (int i = 0; i < _referenceList.Count; i++)
            {
                reff = _referenceList[i];
                // handle missing starting references
                if (reff.startChapter == null) // whole book listed
                {
                    reff.startChapter = 1;
                    if (reff.range == false)
                    {
                        reff.endBook = reff.startBook;
                        reff.endChapter = backend.currentChapters(reff.startBook).Last();
                    }
                }
                if (reff.startVerse == null) // whole chapter listed
                {
                    reff.startVerse = 1;
                    if (reff.range == false)
                    {
                        reff.endBook = reff.startBook;
                        if (reff.endChapter == null)
                        {
                            reff.endChapter = reff.startChapter;
                        }
                        reff.endVerse = backend.currentVerses(reff.startBook, (int)reff.endChapter).Count;
                    }
                }

                // handle missing ending references
                if (reff.range == true)
                {
                    if (reff.endBook == null) // no book -- john 3-4
                    {
                        reff.endBook = reff.startBook;
                    }

                    if(reff.endChapter == null && reff.startBook != reff.endBook) // 1 john - 2 john
                    {
                        reff.endChapter = backend.currentChapters(reff.endBook).Last();
                    }
                    else if (reff.endChapter == null) // no chapter --> john 3:3-5
                    {// so same chapter for start as end
                        reff.endChapter = reff.startChapter;
                    }
                    if (reff.endVerse == null) // no verse --> john 3 - john 4 or john 3 - 4
                    {// so last verse of endChapter
                        reff.endVerse = backend.currentVerses(reff.endBook, (int)reff.endChapter).Count;
                    }
                }

                // set reff range to true if range items have been filled in
                if (reff.endBook != null)
                {
                    reff.range = true;
                }
            }
        }
        public ReferenceList(SerializationInfo info, StreamingContext ctxt)
        {
            _currentRefernce = (ReferenceItem)info.GetValue("_currentRefernce",
                typeof(ReferenceItem));
            _referenceList = (List<ReferenceItem>)info.GetValue("_referenceList",
                typeof(List<ReferenceItem>));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("_currentRefernce", _currentRefernce);
            info.AddValue("_referenceList", _referenceList);
        }
    }
}
