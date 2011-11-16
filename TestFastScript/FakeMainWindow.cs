using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestFastScript
{
    class FakeMainWindow:Fast_Script.PresenterFolder.Searching.IMainWindow
    {
        private List<string> suggestions = new List<string>();
        public string originalSearch { get; set; }
        public void searchBoxSuggestions(object thing, string text)
        {
            suggestions = (List<string>) thing;
            originalSearch = text;
        }
        public List<string> getSuggestionsList()
        {
            return suggestions;
        }
        public void reset()
        {
            suggestions.Clear();
            originalSearch = null;
        }
    }
}
