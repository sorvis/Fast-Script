using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fast_Script.PresenterFolder;

namespace Fast_Script.PresenterFolder.Searching
{
    class SearchParsing
    {
        private IPresenter _presenter;
        private IndexLooker _index;

        public SearchParsing(IPresenter presenter)
        {
            _presenter = presenter;
            _index = new IndexLooker(_presenter.Backend);
        }

        public SearchParsing()
        {

        }

        private void foundWholeBookName(ref string lastFoundBook, ref string[] text, 
            ref int i, ref List<string> suggestionList,  ref IMainWindow _view,
            ref string originalSearch, ref ReferenceList refList)
        {
            lastFoundBook = text[i].CapitalizeWord();
            if (i < text.Count() - 1) //something after book
            {

            }
            else // nothing after book
            {
                // return a list of possible chapters for the book
                suggestionList = _index.GetPossibleChapters(lastFoundBook).AddPrefixToList(originalSearch + " ");
                _view.SearchBoxSuggestions(suggestionList, originalSearch);
            }

            // save book reference
            if (refList.CurrentReference == null || refList.CurrentReference.Range == false)
            {
                refList.AddReference(lastFoundBook);
            }
            else // if expecting another book reference
            {
                refList.CurrentReference.EndBook = lastFoundBook;
                refList.AddReference(null);
            }

            //foundBook(text, originalSearch);
        }
        
        
        public void searchString(string originalSearch, BackEndInitializer _backend, IMainWindow _view)
        {
            PresenterFolder.ReferenceList referenceList = new PresenterFolder.ReferenceList(); // list of references to display
            List<string> suggestionList = new List<string>(); // list of what user might want to type next
            string searchPhrase = "";

            string[] text = originalSearch.Replace(";", " ; ").Split(' ');  // put to space seperated array and seperate the ;'s
            text = PreSearchStringBuilder.FixBookNumberTitlesInSearchArray(text);
            text = PreSearchStringBuilder.CombineHyphenAndDashInArray(text);

            bool foundBook = false; // keeps track of whether this is part way into a reference ie. just found the a book name
            string lastFoundBook = "";
            for (int i = 0; i < text.Count(); i++)
            {
                if (text[i] == ";") // end of reference
                {
                    foundBook = false;
                }
                // look for a whole book name
                else if (!foundBook && _index.ContainsBook(text[i]))
                {
                    foundWholeBookName( ref lastFoundBook, ref text, ref i, ref suggestionList, ref _view, ref originalSearch, ref referenceList);
                    foundBook = true;
                }
                // look for -
                else if (text[i] == "-") // found a book range reference
                {
                    referenceList.CurrentReference.Range = true;
                    foundBook = false;
                }
                // look for part of a book name if last item in text[]
                else if (!foundBook)
                {
                    suggestionList = _backend.CurrentBooks.ToList().StartsWithInList(text[i]);
                    if (suggestionList != null && i == text.Count() - 1) // found part of a book name
                    {
                        _view.SearchBoxSuggestions(suggestionList, originalSearch);
                    }
                    // not a refernce so do a search of the index
                    else
                    {
                        if (i >= text.Count() - 1 && !_backend.WordExists(text[i].ToLower())) // last term
                        {
                            _view.SearchBoxSuggestions(_backend.WordsThatStartWith(text[i].ToLower()),
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
                        if (referenceList.CurrentReference.StartChapter == null) // check if start chap is empty
                        {
                            referenceList.CurrentReference.StartChapter = chapNumber;
                        }
                        else // start chap already has something in it
                        {
                            referenceList.CurrentReference.EndChapter = chapNumber;
                        }
                    }
                    else if (text[i].Contains(':') && !(text[i].Contains('-'))) // just :
                    {
                        if (text[i].EndsWith(":")) // only chap listed return possible verses
                        {
                            // return a list of possible verses for indicated chapter
                            suggestionList = _index.GetPossibleVerses(lastFoundBook, text[i].Substring(0, text[i].Length - 1));
                            suggestionList = suggestionList.AddPrefixToList(originalSearch);
                            _view.SearchBoxSuggestions(suggestionList, originalSearch);
                        }
                        else // chap & verse so store full refence
                        {
                            string[] temp = text[i].Split(':');
                            if (referenceList.CurrentReference.StartChapter == null)
                            {
                                referenceList.CurrentReference.StartChapter = Convert.ToInt32(temp[0]);
                                referenceList.CurrentReference.StartVerse = Convert.ToInt32(temp[1]);
                            }
                            else
                            {
                                referenceList.CurrentReference.EndChapter = Convert.ToInt32(temp[0]);
                                referenceList.CurrentReference.EndVerse = Convert.ToInt32(temp[1]);
                            }
                        }
                    }// end -- // just :
                    else if (text[i].Contains('-') && !(text[i].Contains(':'))) // just - so dealing with chapters only
                    {
                        string[] temp = text[i].Split('-');
                        referenceList.CurrentReference.StartChapter = Convert.ToInt16(temp[0]);
                        referenceList.CurrentReference.EndChapter = Convert.ToInt16(temp[1]);
                        referenceList.CurrentReference.Range = true;
                    } // end  --  // just - so dealing with chapters only
                } // end --// look for reference numbers
            } // end for loop for text[i]  
            if (searchPhrase != "") // do index search if needed
            {
                searchPhrase = searchPhrase.Remove(searchPhrase.Count()-1); // acount for space on end
                referenceList.AppendReferenceList(_backend.searchPhrase(searchPhrase));
            }
            if (referenceList.GetList.Count > 0)
            {
                _presenter.DisplayVersesToWebView(referenceList, searchPhrase);
            }
            else
            {
                _presenter.WriteWebView("No results.");
            }
        } // end search method
    }
}
