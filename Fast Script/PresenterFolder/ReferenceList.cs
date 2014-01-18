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
        private ReferenceItem _currentReference;
        public ReferenceItem CurrentReference
        {
            get { return _currentReference; }
            set { _currentReference = value; }
        }
        List<ReferenceItem> _referenceList;
        public List<ReferenceItem> GetList
        {
            get { return _referenceList; }
        }
        public ReferenceList()
        {
            _referenceList = new List<ReferenceItem>();
            _currentReference = null;
        }
        public ReferenceList(List<ReferenceItem> items)
        {
            _referenceList = items;
            _currentReference = null;
        }
        public void AddReferenceItem(ReferenceItem item)
        {
            _referenceList.Add(item);
        }
        // assumes the ReferenceItem has already been built
        public List<ReferenceItem> BuildVerseListFromRange(ReferenceItem item, BackEndInitializer backend)
        {
            if (item.Range)
            {
                List<ReferenceItem> expandedReferenceList = new List<ReferenceItem>();

                // get list of the books
                int startBookIndex = Array.IndexOf(backend.CurrentBooks, item.StartBook);
                int endBookIndex = Array.IndexOf(backend.CurrentBooks, item.EndBook);
                int startingChapter;
                int endingChapter;
                int startingVerse;
                int endingVerse;
                string book;
                // cycle through the books selected
                for (int bookIndex = startBookIndex; bookIndex <= endBookIndex; bookIndex++)
                {
                    book = backend.CurrentBooks[bookIndex];

                    if (book == item.StartBook) // get starting chapter and verse
                    { startingChapter = (int)item.StartChapter; }
                    else
                    { startingChapter = 1; }
                    if (book == item.EndBook) // get ending chapter and verse
                    { endingChapter = (int)item.EndChapter; }
                    else
                    { endingChapter = backend.CurrentChapters(book).Count(); }

                    // cycle through chapters for current book
                    for (int chapter = startingChapter; chapter <= endingChapter; chapter++)
                    {
                        if (book == item.StartBook && chapter == item.StartChapter)
                        { startingVerse = (int)item.StartVerse; }
                        else
                        { startingVerse = 1; }
                        if (book == item.EndBook && chapter == item.EndChapter)
                        { endingVerse = (int)item.EndVerse; }
                        else
                        { endingVerse = backend.CurrentVerses(book, chapter).Count; }

                        // cycle through verses for currect chapter
                        for (int verse = startingVerse; verse <= endingVerse; verse++)
                        {
                            expandedReferenceList.Add(new ReferenceItem(book, chapter, 
                                verse, false, backend.GetVerse(book, chapter, verse)));
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
        public ReferenceItem AddReference(string startingBook)
        {
            ReferenceItem item = new ReferenceItem();
            _referenceList.Add(item);
            _currentReference = item;
            item.StartBook = startingBook;
            return item;
        }
        public ReferenceItem AddReference(string startingBook, int startChapter, int startVerse)
        {
            ReferenceItem item = new ReferenceItem();
            _referenceList.Add(item);
            _currentReference = item;
            item.StartBook = startingBook;
            item.StartChapter = startChapter;
            item.StartVerse = startVerse;
            return item;
        }
        public void AppendReferenceList(ReferenceList oldList)
        {
            if (oldList != null)
            {
                foreach (ReferenceItem item in oldList.GetList)
                {
                    _referenceList.Add(item);
                }
            }
        }
        public void CompleteReferences(BackEndInitializer backend) // copy over full reference data on each item
        {
            ReferenceItem reference;
            for (int i = 0; i < _referenceList.Count; i++)
            {
                reference = _referenceList[i];
                // handle missing starting references
                if (reference.StartChapter == null) // whole book listed
                {
                    reference.StartChapter = 1;
                    if (reference.Range == false)
                    {
                        reference.EndBook = reference.StartBook;
                        reference.EndChapter = backend.CurrentChapters(reference.StartBook).Last();
                    }
                }
                if (reference.StartVerse == null) // whole chapter listed
                {
                    reference.StartVerse = 1;
                    if (reference.Range == false)
                    {
                        reference.EndBook = reference.StartBook;
                        if (reference.EndChapter == null)
                        {
                            reference.EndChapter = reference.StartChapter;
                        }
                        reference.EndVerse = backend.CurrentVerses(reference.StartBook, (int)reference.EndChapter).Count;
                    }
                }

                // handle missing ending references
                if (reference.Range == true)
                {
                    if (reference.EndBook == null) // no book -- john 3-4
                    {
                        reference.EndBook = reference.StartBook;
                    }

                    if(reference.EndChapter == null && reference.StartBook != reference.EndBook) // 1 john - 2 john
                    {
                        reference.EndChapter = backend.CurrentChapters(reference.EndBook).Last();
                    }
                    else if (reference.EndChapter == null) // no chapter --> john 3:3-5
                    {// so same chapter for start as end
                        reference.EndChapter = reference.StartChapter;
                    }
                    if (reference.EndVerse == null) // no verse --> john 3 - john 4 or john 3 - 4
                    {// so last verse of endChapter
                        reference.EndVerse = backend.CurrentVerses(reference.EndBook, (int)reference.EndChapter).Count;
                    }
                }

                // set reff range to true if range items have been filled in
                if (reference.EndBook != null)
                {
                    reference.Range = true;
                }
            }
        }
        public ReferenceList(SerializationInfo info, StreamingContext ctxt)
        {
            _currentReference = (ReferenceItem)info.GetValue("_currentRefernce",
                typeof(ReferenceItem));
            _referenceList = (List<ReferenceItem>)info.GetValue("_referenceList",
                typeof(List<ReferenceItem>));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("_currentRefernce", _currentReference);
            info.AddValue("_referenceList", _referenceList);
        }
    }
}
