using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Collections;

namespace Fast_Script.data_index
{
    [Serializable()]
    public class indexBuilder : ISerializable
    {
        private bible_data.bible _bible;
        public bible_data.bible Bible
        {
            set { _bible = value; }
        }

        private Dictionary<string, word> _bibleIndex = new Dictionary<string, word>();
        public Dictionary<string, word> GetDictionary
        {
            get { return _bibleIndex; }
        }

        private Hashtable wordHash = new Hashtable();

        public indexBuilder(bible_data.bible bible)
        {
            _bible = bible;
            buildIndex();
        }
        public indexBuilder(SerializationInfo info, StreamingContext ctxt)
        {
            this._bibleIndex = (Dictionary<string, word>)info.GetValue("Dictionary", typeof(Dictionary<string, word>));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Dictionary", this._bibleIndex);
        }
        private void buildIndex()
        {
            List<bible_data.book> bookList = _bible.getAllBooks();
            List<bible_data.chapter> chapterList;
            List<string> verseList;
            String[] wordsList;
            int verseNum;

            foreach (bible_data.book book in bookList)
            {
                chapterList = book.getAllChapters();
                foreach (bible_data.chapter chapter in chapterList)
                {
                    verseList = chapter.getAllVerses();
                    verseNum = 1;
                    foreach (string verse in verseList)
                    {
                        wordsList = verse.Split(' ');
                        wordsList = toLowerAndRemovePunctuation(wordsList);
                        foreach (string word in wordsList)
                        {
                            addWord(word, new data_index.verse(book.getTitle(), chapter.getChapterNumber(), verseNum));

                            wordHash[word.GetHashCode()] = word;
                        }
                        verseNum++;
                    }
                }
            }
        } // end methode build index
        public List<string> wordsThatStartWith(string prefix)
        {
            prefix = prefix.ToLower();
            List<string> suggestedWords = new List<string>();
            foreach (DictionaryEntry item in wordHash)
            {
                string tempString = (string)item.Value;
                if (tempString.StartsWith(prefix))
                {
                    suggestedWords.Add(tempString);
                }
            }
            return suggestedWords;
        }
        public bool TryGetValue(string word, out word Word)
        {
            return _bibleIndex.TryGetValue(word, out Word);
        }
        private string[] toLowerAndRemovePunctuation(string[] words)
        {
            List<string> temp = new List<string>();
            foreach (string item in words)
            {
                temp.Add(item.ToLower().StripPunctuation());
            }
            return temp.ToArray();
        }
        public void addWord(string word, verse Verse)
        {
            word node;
            if (_bibleIndex.TryGetValue(word, out node)) // check if word is in dictionary
            {
                node.addVerse(Verse);
            }
            else // word not in dictionary so add it then use recursion
            {
                _bibleIndex.Add(word, new word(word));
                addWord(word, Verse);
            }
        }
        public List<verse> getVerses(string phrase) // find all common verses from a phrase
        {
            List<word> allWords = new List<word>();
            foreach (string item in phrase.Split(' '))
            {
                allWords.Add(getWord(item));
            }
            if (allWords.Contains(null))
            {
                return null;
            }

            // sort list of words with the biggest word first to littlest word last
            // this will help efficency somewhat
            word temp;
            int counter = 0;
            while (counter < allWords.Count - 1)
            {
                counter = 0;
                for (int i = 0; i < allWords.Count - 1; i++)
                {
                    if (allWords[i].getWord().Length < allWords[i + 1].getWord().Length)
                    {
                        temp = allWords[i];
                        allWords[i] = allWords[i + 1];
                        allWords[i + 1] = temp;
                    }
                    else
                    {
                        counter++;
                    }
                }
            }

            List<verse> resault = compareVerses(allWords);

            if (resault.Count < 1) // check for if no matches found
            {
                return null;
            }
            else
            {
                return compareVerses(allWords);
            }
        }
        private static bool isNull(word item)
        {
            if (item == null)
            { return true; }
            else
            { return false; }
        }
        private word getWord(string word) // gets all verses for a word from dictionary
        {
            word = word.ToLower();
            word node;
            if (_bibleIndex.TryGetValue(word, out node))
            {
                return node;
            }
            else
            {
                return null;
            }
        }
        private string getVerse(verse item)
        {
            return _bible.getBook(item.Book).getChapter(item.Chapter).getVerse(item.Verse);
        }
        private List<verse> compareVerses(List<word> wordList) // find all common verses amoung word list
        {
            if (wordList.Count > 1)
            {
                word twoCombinedWords = new word();
                foreach(verse item in wordList[0].getVerses())
                {
                    if(  getVerse(item).Contains(wordList[1].getWord()))
                    {
                        twoCombinedWords.addVerse(item);
                    }
                }
                wordList[0] = twoCombinedWords;
                wordList.RemoveAt(1);
                return compareVerses(wordList);
            }
            else
            {
                return wordList[0].getVerses();
            }
        }
    }
}
