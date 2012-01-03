using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fast_Script.PresenterFolder;

namespace Fast_Script.PresenterFolder.Searching
{
    class searchParsing
    {
        private IPresenter _presenter;
        private indexLooker _index;

        public searchParsing(IPresenter presenter)
        {
            _presenter = presenter;
            _index = new indexLooker(_presenter.Backend);
        }

        public searchParsing()
        {

        }

        private void foundWholeBookName(ref string lastFoundBook, ref string[] text, 
            ref int i, ref List<string> suggestionList,  ref IMainWindow _view,
            ref string originalSearch, ref ReferenceList refList)
        {
            lastFoundBook = text[i].capitalizeWord();
            if (i < text.Count() - 1) //something after book
            {

            }
            else // nothing after book
            {
                // return a list of possible chapters for the book
                suggestionList = _index.getPossibleChapters(lastFoundBook).addPrefixToList(originalSearch + " ");
                _view.searchBoxSuggestions(suggestionList, originalSearch);
            }

            // save book reference
            if (refList.currentRefernce == null || refList.currentRefernce.range == false)
            {
                refList.addReference(lastFoundBook);
            }
            else // if expecting another book reference
            {
                refList.currentRefernce.endBook = lastFoundBook;
                refList.addReference(null);
            }

            //foundBook(text, originalSearch);
        }
        
        
        public void searchString(string originalSearch, backEndInitializer _backend, IMainWindow _view)
        
        {
            PresenterFolder.ReferenceList refList = new PresenterFolder.ReferenceList(); // list of references to display
            List<string> suggestionList = new List<string>(); // list of what user might want to type next
            string searchPhrase = "";

            string[] text = originalSearch.Replace(";", " ; ").Split(' ');  // put to space seperated array and seperate the ;'s
            text = preSearchStringBuilder.fixBookNumberTitlesInSearchArray(text);
            text = preSearchStringBuilder.combineHyphenAndDashInArray(text);

            bool foundBook = false; // keeps track of whether this is part way into a reference ie. just found the a book name
            string lastFoundBook = "";
            for (int i = 0; i < text.Count(); i++)
            {
                if (text[i] == ";") // end of reference
                {
                    foundBook = false;
                }
                // look for a whole book name
                else if (!foundBook && _index.containsBook(text[i]))
                {
                    foundWholeBookName( ref lastFoundBook, ref text, ref i, ref suggestionList, ref _view, ref originalSearch, ref refList);
                    foundBook = true;
                }
                // look for -
                else if (text[i] == "-") // found a book range reference
                {
                    refList.currentRefernce.range = true;
                    foundBook = false;
                }
                // look for part of a book name if last item in text[]
                else if (!foundBook)
                {
                    suggestionList = _backend.currentBooks.ToList().startsWithInList(text[i]);
                    if (suggestionList != null && i == text.Count() - 1) // found part of a book name
                    {
                        _view.searchBoxSuggestions(suggestionList, originalSearch);
                    }
                    // not a refernce so do a search of the index
                    else
                    {
                        if (i >= text.Count() - 1 && !_backend.wordExists(text[i].ToLower())) // last term
                        {
                            _view.searchBoxSuggestions(_backend.wordsThatStartWith(text[i].ToLower()),
                                originalSearch);
                        }
                        else
                        {
                            searchPhrase += text[i].ToLower() + " ";
                        }
                    }
                }
                // look for reference numbers
                else if (foundBook)
                {
                    foundBook = false;
                    int chapNumber;
                    if ((int.TryParse(text[i], out chapNumber))) // just a number
                    {
                        if (refList.currentRefernce.startChapter == null) // check if start chap is empty
                        {
                            refList.currentRefernce.startChapter = chapNumber;
                        }
                        else // start chap already has something in it
                        {
                            refList.currentRefernce.endChapter = chapNumber;
                        }
                    }
                    else if (text[i].Contains(':') && !(text[i].Contains('-'))) // just :
                    {
                        if (text[i].EndsWith(":")) // only chap listed return possible verses
                        {
                            // return a list of possible verses for indicated chapter
                            suggestionList = _index.getPossibleVerses(lastFoundBook, text[i].Substring(0, text[i].Length - 1));
                            suggestionList = suggestionList.addPrefixToList(originalSearch);
                            _view.searchBoxSuggestions(suggestionList, originalSearch);
                        }
                        else // chap & verse so store full refence
                        {
                            string[] temp = text[i].Split(':');
                            if (refList.currentRefernce.startChapter == null)
                            {
                                refList.currentRefernce.startChapter = Convert.ToInt32(temp[0]);
                                refList.currentRefernce.startVerse = Convert.ToInt32(temp[1]);
                            }
                            else
                            {
                                refList.currentRefernce.endChapter = Convert.ToInt32(temp[0]);
                                refList.currentRefernce.endVerse = Convert.ToInt32(temp[1]);
                            }
                        }
                    }// end -- // just :
                    else if (text[i].Contains('-') && !(text[i].Contains(':'))) // just - so dealing with chapters only
                    {
                        string[] temp = text[i].Split('-');
                        refList.currentRefernce.startChapter = Convert.ToInt16(temp[0]);
                        refList.currentRefernce.endChapter = Convert.ToInt16(temp[1]);
                        refList.currentRefernce.range = true;
                    } // end  --  // just - so dealing with chapters only
                } // end --// look for reference numbers
            } // end for loop for text[i]  
            if (searchPhrase != "") // do index search if needed
            {
                searchPhrase = searchPhrase.Remove(searchPhrase.Count()-1); // acount for space on end
                refList.appendReferenceList(_backend.searchPhrase(searchPhrase));
            }
            if (refList.getList.Count > 0)
            {
                _presenter.displayVersesToWebView(refList, searchPhrase);
            }
            else
            {
                _presenter.writeWebView("No results.");
            }
        } // end search method
    }
}
