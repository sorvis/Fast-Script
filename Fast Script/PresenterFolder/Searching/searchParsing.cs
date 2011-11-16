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

        private void foundWholeBookName(ref bool foundBook, ref string lastFoundBook, ref string[] text, 
            ref int i, ref List<string> suggestionList,  ref IMainWindow _view,
            ref string originalSearch, ref ReferenceList refList)
        {
            foundBook = true;
            lastFoundBook = capitalizeWord(text[i]);
            if (i < text.Count() - 1) //something after book
            {

            }
            else // nothing after book
            {
                // return a list of possible chapters for the book
                suggestionList = _index.getChapters(lastFoundBook).addPrefixToList(originalSearch + " ");
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
        private string[] fixBookNumberTitlesInSearchArray(string[] text)
        {
            // fix all books that begin with a number
            int bookNumber;
            List<string> tempList = text.ToList();
            for (int i = 0; i < text.Count() - 1; i++)
            {
                if ((i == 0 || text[i - 1] == "-" || text[i - 1].EndsWith(";")) &&
                        (int.TryParse(text[i], out bookNumber) && text.Count() > i + 1))
                {
                    tempList[1 + i] = tempList[i] + " " + tempList[1 + i];
                    tempList[i] = "";
                }
            }
            return tempList.RemoveAll("").ToArray();
        }
        private string[] combineHyphenAndDashInArray(string[] tempList)
        {
            // combine numbers, :, and - together
            int bookNumber;
            //List<string> tempList = text.ToList();
            for (int i = 0; i < tempList.Count() - 1; i++)
            {
                if ((int.TryParse(tempList[i], out bookNumber) || int.TryParse(tempList[i + 1], out bookNumber))
                    && (tempList[i].Contains(':') || tempList[i + 1].Contains(':')||
                    tempList[i].Contains('-') || tempList[i + 1].Contains('-')))
                {
                    tempList[1 + i] = tempList[i] + tempList[1 + i];
                    tempList[i] = "";
                }
            }
            return tempList.ToList().RemoveAll("").ToArray(); 
        }
        public void searchString(string originalSearch, backEndInitializer _backend, IMainWindow _view)
        
        {
            PresenterFolder.ReferenceList refList = new PresenterFolder.ReferenceList(); // list of references to display
            List<string> suggestionList = new List<string>(); // list of what user might want to type next
            string searchPhrase = "";

            string[] text = originalSearch.Replace(";", " ; ").Split(' ');  // put to space seperated array and seperate the ;'s
            text = fixBookNumberTitlesInSearchArray(text);
            text = combineHyphenAndDashInArray(text);

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
                    foundWholeBookName(ref foundBook, ref lastFoundBook, ref text, ref i, ref suggestionList, ref _view, ref originalSearch, ref refList);
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
                            suggestionList = _backend.currentVerses(lastFoundBook, Convert.ToInt32(text[i].Substring(0, text[i].Length - 1)));
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
        } // end search methode
        private string capitalizeWord(string word)
        {
            char[] tempArray = word.ToCharArray();

            // capitalize first char if not a number
            // if number then probably a numbered book
            int bookNumber;
            if (int.TryParse(Convert.ToString(tempArray[0]), out bookNumber)) // first char is number
            {
                // assume spot 2 is what needs capitalized.
                tempArray[2] = char.ToUpper(tempArray[2]);
                for (int i = 3; i < word.Count(); i++)
                {
                    tempArray[i] = char.ToLower(tempArray[i]);
                }
            }
            else // first char is not number so capitalize it
            {
                tempArray[0] = char.ToUpper(tempArray[0]);
                for (int i = 1; i < word.Count(); i++)
                {
                    tempArray[i] = char.ToLower(tempArray[i]);
                }
            }

            string tempString = "";
            foreach (char i in tempArray)
            {
                tempString += i;
            }
            return tempString;
        }
    }
}
