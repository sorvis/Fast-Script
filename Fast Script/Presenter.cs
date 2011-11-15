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
    public class Presenter
    {
        private backEndInitializer _backend;
        public backEndInitializer Backend {get { return _backend; }}
        private MainWindow _view;
        private PresenterFolder.Searching.searchParsing _ParseSearch;
        private string _defaultPageLocation;
        public string DefaultWebPage
        { get { return _defaultPageLocation; } }
        private ReferenceItem[] _currentItemsInWebview;
        public ReferenceItem[] ItemstInWebView
        {get{return _currentItemsInWebview;}}
        private string _currentBoldWord;
        public string CurrentBoldWord
        { get { return _currentBoldWord; } }
        public GUI_Settings Settings
        { get { return _backend._settings; } }
        private string _appDataStorageFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Fast_Script");
        public Presenter(MainWindow view)
        {
            _defaultPageLocation = Path.Combine(_appDataStorageFolder, "HTML\\page.html");
            _backend = new backEndInitializer(this);
            _view = view;
            _ParseSearch = new PresenterFolder.Searching.searchParsing(this);
        }
        public void saveVerseListToFile(ReferenceList list, string fileName)
        {
            ObjectSerializing.SerializeObject(fileName, list);
        }
        public ReferenceList getSavedVerseListFromFile(string fileName)
        {
            return (ReferenceList)ObjectSerializing.DeSerializeObject(fileName);
        }
        public void saveSettings()
        {
             ObjectSerializing.SerializeObject(Path.Combine(_appDataStorageFolder, "Settings.data"), Settings);
        }
        public void addToVerseList(ReferenceItem item)
        {
            if (item.ToString() != "")
            {
                _view.verseListBox.Items.Add(new ReferenceItemWrapper(item), true);
            }
        }
        public void setNewVerseList(ReferenceList list)
        {
            _view.verseListBox.Items.Clear();
            foreach (ReferenceItem item in list.getList)
            {
                _view.verseListBox.Items.Add(new ReferenceItemWrapper(item), true);
            }
        }
        public void searchString(string originalSearch)
        {
            _ParseSearch.searchString(originalSearch, _backend, _view);
        }
        public void displayVersesToWebView() // reloading current verses
        {
            displayVersesToWebView(new ReferenceList(_currentItemsInWebview.ToList()), _currentBoldWord);
        }
        private string putVersesToStringGeneric(string[] verseArray)
        {
            string finalText = "";
            foreach (string item in verseArray)
            {
                finalText += item + '\n' + '\n';
            }
            finalText = finalText.Substring(0, finalText.Count() - 2);//drop off last two \n
            return finalText;
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
            string finalText = "";
            foreach (string item in verseListToText(list))
            {
                finalText += item + '\n'+'\n';
            }
            finalText = finalText.Substring(0,finalText.Count() - 2);//drop off last two \n
            Clipboard.SetText(finalText);
        }
        private string[] verseListToTextForTTS(ReferenceList list)
        {
            string[] verseText = new string[list.getList.Count];
            string tempVerse = "";
            string tempTitle = "";
            list.completeReferences(_backend);
            int counter = 0;
            int verseNumber;
            foreach (ReferenceItem refItem in list.getList)
            {
                tempTitle = refItem.startBook + " Chapter " + refItem.startChapter + " verse " +
                    refItem.startVerse;
                if (refItem.range == false) // just one verse
                {
                    tempVerse = _backend.getVerse(refItem.startBook,
                        (int)refItem.startChapter, (int)refItem.startVerse);
                }
                else // range of verses in one refItem
                {
                    tempTitle += "; To " + refItem.endBook + " Chapter " + refItem.endChapter + " Verse " +
                        refItem.endVerse;
                    tempVerse = "";//clear it just in case

                    // get range of verses and format the sting
                    foreach (data_index.verse verseItem in _backend.getVerseRange(
                        refItem.startBook + " " + refItem.startChapter + ":" +
                        refItem.startVerse, refItem.endBook + " " + refItem.endChapter +
                        ":" + refItem.endVerse))
                    {
                        verseNumber = verseItem.Verse;
                        if (verseNumber == 1) // at first verse print book and chapter num
                        {
                            tempVerse += ". Chapter " + verseItem.Chapter + " of the book of " + verseItem.Book +". .";
                        }
                        tempVerse += ". " + verseItem.getVerseText();   // apend that verses text
                    }// end foreach
                }

                verseText[counter] = tempTitle + '\n' + tempVerse;
                counter++;
            }
            return verseText;
        }
        private string[] verseListToText(ReferenceList list)
        {
            string[] verseText = new string[list.getList.Count];
            string tempVerse="";
            string tempTitle ="";
            list.completeReferences(_backend);
            int counter = 0;
            int verseNumber;
            foreach (ReferenceItem refItem in list.getList)
            {
                tempTitle = refItem.startBook + " " + refItem.startChapter + ":" +
                    refItem.startVerse;
                if (refItem.range == false)
                {
                    tempVerse = _backend.getVerse(refItem.startBook,
                        (int)refItem.startChapter, (int)refItem.startVerse);
                }
                else // range of verses in one refItem
                {
                    tempTitle += " - " + refItem.endBook + " " + refItem.endChapter + ":" +
                        refItem.endVerse;
                    tempVerse = "";//clear it just in case

                    // get range of verses and format the sting
                    foreach (data_index.verse verseItem in _backend.getVerseRange(
                        refItem.startBook + " " + refItem.startChapter + ":" +
                        refItem.startVerse, refItem.endBook + " " + refItem.endChapter +
                        ":" + refItem.endVerse))
                    {
                        verseNumber = verseItem.Verse;
                        if (verseNumber == 1) // at first verse pring book and chapter num
                        {
                            tempVerse += verseItem.Book + " " + verseItem.Chapter;
                        }
                        tempVerse+="\n"+verseItem.Verse+" "+verseItem.getVerseText();
                    }
                }

                verseText[counter] = tempTitle+'\n'+tempVerse;
                counter++;
            }
            return verseText;
        }
        public void displayVersesToWebView(ReferenceList list, string boldWords)
        {
            // save verses in cas a refresh is needed later
            _currentItemsInWebview = list.getList.ToArray();
            _currentBoldWord = boldWords;

            list.completeReferences(_backend);

            string finalPage="<ul>";
            string title="";
            string verses="";
            string tempVerse = "";

            int limit = 20;
            ReferenceItem item;
            for (int i = 0; i < list.getList.Count() && i < limit; i++)
            {
                item = list.getList[i];

                title = "";
                verses = "";

                title += item.startBook + " " + item.startChapter + ":" + item.startVerse;

                if (item.range == false) // get verse for single reference
                {
                    tempVerse = _backend.getVerse(item.startBook, (int)item.startChapter, (int)item.startVerse);
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
                    title += " - " + item.endBook + " " + item.endChapter + ":" + item.endVerse;

                    string currentBook = "";
                    int currentChapter = 0;
                    foreach (data_index.verse verseItem in _backend.getVerseRange(
                        item.startBook + " " + item.startChapter + ":" + item.startVerse,
                        item.endBook + " " + item.endChapter + ":" + item.endVerse))
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
                        verses += "</br><b>" + verseItem.Verse + "</b> " 
                            + verseItem.getVerseText();
                    }

                    //List<ReferenceItem> itemList = new ReferenceList().buildVerseListFromRange(item, _backend);
                    
                    //foreach (ReferenceItem Refitem in itemList)
                    //{
                        
                    //}
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

            if (list.getList.Count() > limit)
            { 
                finalPage += "<b>Plus " + (list.getList.Count() - limit) + " more</b>"; 
            }

            writeWebView(finalPage);
        }
        public void writeWebView(string page)
        {
            _backend.saveWebpage(page);
            _view.loadWebPage(_defaultPageLocation);
        }
    }
}
