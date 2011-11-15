using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fast_Script.PresenterFolder.Searching
{
    class indexLooker
    {
        private backEndInitializer _backEnd;
        public indexLooker(backEndInitializer backEnd)
        {
            _backEnd = backEnd;
        }

        public List<string> getChapters(string book)
        {
            return _backEnd.currentChapters(book).ToList().ToStringList();
        }

        public bool containsBook(string book)
        {
            return _backEnd.currentBooks.Contains(book, false);
        }
    }
}
