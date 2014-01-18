using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fast_Script.PresenterFolder.Searching
{
    class IndexLooker
    {
        private BackEndInitializer _backEnd;
        public IndexLooker(BackEndInitializer backEnd)
        {
            _backEnd = backEnd;
        }

        public List<string> GetPossibleChapters(string book)
        {
            return _backEnd.CurrentChapters(book).ToList().ToStringList();
        }

        public List<string> GetPossibleVerses(string book, string chapter)
        {
            return _backEnd.CurrentVerses(book, Convert.ToInt32(chapter));
        }

        public bool ContainsBook(string book)
        {
            return _backEnd.CurrentBooks.Contains(book, false);
        }
    }
}
