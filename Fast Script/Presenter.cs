using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fast_Script.PresenterFolder;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;

namespace Fast_Script
{
    public class Presenter : Fast_Script.PresenterFolder.Searching.IPresenter
    {
        private BackEndInitializer _backend;
        public BackEndInitializer Backend {get { return _backend; }}
        private MainWindow _view;
        private PresenterFolder.Searching.SearchParsing _ParseSearch;
        private ReferenceItem[] _currentItemsInWebview;
        public ReferenceItem[] ItemstInWebView
        {get{return _currentItemsInWebview;}}
        private string _currentBoldWord;
        public string CurrentBoldWord
        { get { return _currentBoldWord; } }
        public GUISettings Settings
        { get { return _backend._settings; } }
        private string _appDataStorageFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Fast_Script");
        public Presenter(MainWindow view)
        {
            _backend = new BackEndInitializer(this);
            _view = view;
            _ParseSearch = new PresenterFolder.Searching.SearchParsing(this);
        }
        public void saveVerseListToFile(ReferenceList list, string fileName)
        {
            ObjectSerializing.SerializeObjectToFile(fileName, list);
        }
        public ReferenceList getSavedVerseListFromFile(string fileName)
        {
            return (ReferenceList)ObjectSerializing.DeSerializeObjectFromFile(fileName);
        }
        public void saveSettings()
        {
             ObjectSerializing.SerializeObjectToFile(Path.Combine(_appDataStorageFolder, "Settings.data"), Settings);
        }
        public void addToVerseList(ReferenceItem item)
        {
            if (item.ToString() != "")
            {
                ReferenceItemWrapper refItem = new ReferenceItemWrapper(item);
                _view.VerseListBox.Items.Add(refItem, true);
                _view.VerseListBox.SelectedItem = refItem;  // select it so scroll bar will go down
            }
        }
        public void setNewVerseList(ReferenceList list)
        {
            _view.VerseListBox.Items.Clear();
            foreach (ReferenceItem item in list.GetList)
            {
                addToVerseList(item);
            }
        }
        public void searchString(string originalSearch)
        {
            _ParseSearch.searchString(originalSearch, _backend, _view);
        }
        public void displayVersesToWebView() // reloading current verses
        {
            DisplayVersesToWebView(new ReferenceList(_currentItemsInWebview.ToList()), _currentBoldWord);
        }
        private string putVersesToStringGeneric(string[] verseArray)
        {
            StringBuilder finalText = new StringBuilder();
            foreach (string item in verseArray)
            {
                finalText.AppendLine().AppendLine(item);
            }
            return finalText.ToString();
        }
        public string putVersesForPlainText(ReferenceList list)
        {
            return putVersesToStringGeneric(verseListToText(list));
        }
        public string putVersesToStringForTTS(ReferenceList list)
        {
            return putVersesToStringGeneric(verseListToTextForTTS(list));
        }
        public void putVersesToClipBoard(ReferenceList list)
        {
            Clipboard.SetText(putVersesToStringGeneric(verseListToText(list)));
        }

        private string[] verseListToTextForTTS(ReferenceList list)
        {
            string[] verseText = new string[list.GetList.Count];
            string tempVerse = "";
            string tempTitle = "";
            list.CompleteReferences(_backend);
            int counter = 0;
            int verseNumber;
            foreach (ReferenceItem refItem in list.GetList)
            {
                tempTitle = refItem.StartBook + " Chapter " + refItem.StartChapter + " verse " +
                    refItem.StartVerse;
                if (refItem.Range == false) // just one verse
                {
                    tempVerse = _backend.GetVerse(refItem.StartBook,
                        (int)refItem.StartChapter, (int)refItem.StartVerse);
                }
                else // range of verses in one refItem
                {
                    tempTitle += "; To " + refItem.EndBook + " Chapter " + refItem.EndChapter + " Verse " +
                        refItem.EndVerse;
                    tempVerse = "";//clear it just in case

                    // get range of verses and format the sting
                    foreach (data_index.Verse verseItem in _backend.GetVerseRange(
                        refItem.StartBook + " " + refItem.StartChapter + ":" +
                        refItem.StartVerse, refItem.EndBook + " " + refItem.EndChapter +
                        ":" + refItem.EndVerse))
                    {
                        verseNumber = verseItem.VerseNumber;
                        if (verseNumber == 1) // at first verse print book and chapter num
                        {
                            tempVerse += ". Chapter " + verseItem.Chapter + " of the book of " + verseItem.Book +". .";
                        }
                        tempVerse += ". " + verseItem.GetVerseText();   // apend that verses text
                    }// end foreach
                }

                verseText[counter] = tempTitle + '\n' + tempVerse;
                counter++;
            }
            return verseText;
        }
        private string[] verseListToText(ReferenceList list)
        {
            string[] verseText = new string[list.GetList.Count];
            string tempVerse="";
            string tempTitle ="";
            list.CompleteReferences(_backend);
            int counter = 0;
            int verseNumber;
            foreach (ReferenceItem refItem in list.GetList)
            {
                tempTitle = refItem.StartBook + " " + refItem.StartChapter + ":" +
                    refItem.StartVerse;
                if (refItem.Range == false)
                {
                    tempVerse = _backend.GetVerse(refItem.StartBook,
                        (int)refItem.StartChapter, (int)refItem.StartVerse);
                }
                else // range of verses in one refItem
                {
                    tempTitle += " - " + refItem.EndBook + " " + refItem.EndChapter + ":" +
                        refItem.EndVerse;
                    tempVerse = "";//clear it just in case

                    // get range of verses and format the sting
                    foreach (data_index.Verse verseItem in _backend.GetVerseRange(
                        refItem.StartBook + " " + refItem.StartChapter + ":" +
                        refItem.StartVerse, refItem.EndBook + " " + refItem.EndChapter +
                        ":" + refItem.EndVerse))
                    {
                        verseNumber = verseItem.VerseNumber;
                        if (verseNumber == 1) // at first verse pring book and chapter num
                        {
                            tempVerse += verseItem.Book + " " + verseItem.Chapter;
                        }
                        tempVerse += "\n" + verseItem.VerseNumber + " " + verseItem.GetVerseText();
                    }
                }

                verseText[counter] = tempTitle+'\n'+tempVerse;
                counter++;
            }
            return verseText;
        }
        public void DisplayVersesToWebView(ReferenceList list, string boldWords)
        {
            // save verses in cas a refresh is needed later
            _currentItemsInWebview = list.GetList.ToArray();
            _currentBoldWord = boldWords;

            list.CompleteReferences(_backend);

            string finalPage="<ul>";
            string title="";
            string verses="";
            string tempVerse = "";

            int limit = 20;
            ReferenceItem item;
            for (int i = 0; i < list.GetList.Count() && i < limit; i++)
            {
                item = list.GetList[i];

                title = "";
                verses = "";

                title += item.StartBook + " " + item.StartChapter + ":" + item.StartVerse;

                if (item.Range == false) // get verse for single reference
                {
                    tempVerse = _backend.GetVerse(item.StartBook, (int)item.StartChapter, (int)item.StartVerse);
                    foreach (string word in boldWords.Split(' '))
                    {
                        tempVerse = Regex.Replace(tempVerse, (word + @"\b"), // @"\b" forces whole case
                            ("<b>" + word + "</b>"), RegexOptions.IgnoreCase);
                    }
                    verses +="</br>"+tempVerse;
                }
                else // the range of verses
                {
                    // add title
                    title += " - " + item.EndBook + " " + item.EndChapter + ":" + item.EndVerse;

                    string currentBook = "";
                    int currentChapter = 0;
                    foreach (data_index.Verse verseItem in _backend.GetVerseRange(
                        item.StartBook + " " + item.StartChapter + ":" + item.StartVerse,
                        item.EndBook + " " + item.EndChapter + ":" + item.EndVerse))
                    {
                        if (verseItem.Book != currentBook)
                        {
                            verses += "</br></br><b><h1>" + verseItem.Book + "</h1>"
                                + "Chapter " + verseItem.Chapter + "</b>";
                            currentBook = verseItem.Book;
                            currentChapter = (int)verseItem.Chapter;
                        }
                        else if (verseItem.Chapter != currentChapter)
                        {
                            verses += "</br></br><b>Chapter " + verseItem.Chapter + "</b>";
                            currentChapter = (int)verseItem.Chapter;
                        }
                        // print the verse itself
                        verses += "</br><b>" + verseItem.VerseNumber + "</b> " 
                            + verseItem.GetVerseText();
                    }

                }

                // write html list item
                finalPage += "<li><b><p>" + title;
                if(!_view.verseListContains(item.ToString())) // look to see if add reference button needed
                {
                    finalPage += "<A HREF=\"HTML\\page.html:addReference="+i+"\"> Add</A>";
                }
                else
                {finalPage += " Added";}
                finalPage += "</b>" + verses + "</p></li>";
            }

            finalPage += "</ul>"; // put html end of list

            if (list.GetList.Count() > limit)
            { 
                finalPage += "<b>Plus " + (list.GetList.Count() - limit) + " more</b>"; 
            }

            WriteWebView(finalPage);
        }
        public void WriteWebView(string page)
        {
            _backend.SaveWebpage(page);
            _view.loadWebPage(Settings.DefaultWebPage);
        }
    }
}
