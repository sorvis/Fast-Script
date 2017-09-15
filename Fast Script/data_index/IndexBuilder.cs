using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Collections;

namespace Fast_Script.data_index
{
    [Serializable()]
    public class IndexBuilder : ISerializable
    {
        private bible_data.Bible _bible;
        public bible_data.Bible Bible
        {
            set { _bible = value; }
        }

        private Dictionary<string, Word> _bibleIndex = new Dictionary<string, Word>();
        public Dictionary<string, Word> GetDictionary
        {
            get { return _bibleIndex; }
        }

        private Hashtable _wordHash = new Hashtable();

        public IndexBuilder(bible_data.Bible bible)
        {
            _bible = bible;
            buildIndex();
        }
        public IndexBuilder(SerializationInfo info, StreamingContext ctxt)
        {
            this._bibleIndex = (Dictionary<string, Word>)info.GetValue("Dictionary", typeof(Dictionary<string, Word>));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Dictionary", this._bibleIndex);
        }
        private void buildIndex()
        {
            List<bible_data.Book> bookList = _bible.GetAllBooks();
            List<bible_data.Chapter> chapterList;
            List<string> verseList;
            String[] wordsList;
            int verseNum;

            foreach (bible_data.Book book in bookList)
            {
                chapterList = book.GetAllChapters();
                foreach (bible_data.Chapter chapter in chapterList)
                {
                    verseList = chapter.GetAllVerses();
                    verseNum = 1;
                    foreach (string verse in verseList)
                    {
                        wordsList = verse.Split(' ');
                        wordsList = toLowerAndRemovePunctuation(wordsList);
                        foreach (string word in wordsList)
                        {
                            AddWord(word, new data_index.Verse(book.GetTitle(), chapter.GetChapterNumber(), verseNum));

                            _wordHash[word.GetHashCode()] = word;
                        }
                        verseNum++;
                    }
                }
            }
        } // end methode build index

        public List<string> WordsThatStartWith(string prefix)
        {
            prefix = prefix.ToLower();
            List<string> suggestedWords = new List<string>();
            foreach (DictionaryEntry item in _wordHash)
            {
                string tempString = (string)item.Value;
                if (tempString.StartsWith(prefix))
                {
                    suggestedWords.Add(tempString);
                }
            }
            return suggestedWords;
        }
        public bool TryGetValue(string word, out Word Word)
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
        public void AddWord(string word, Verse Verse)
        {
            Word node;
            if (_bibleIndex.TryGetValue(word, out node)) // check if word is in dictionary
            {
                node.AddVerse(Verse);
            }
            else // word not in dictionary so add it then use recursion
            {
                _bibleIndex.Add(word, new Word(word));
                AddWord(word, Verse);
            }
        }
        public List<Verse> GetVerses(string phrase) // find all common verses from a phrase
        {
            List<Word> allWords = new List<Word>();
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
            Word temp;
            int counter = 0;
            while (counter < allWords.Count - 1)
            {
                counter = 0;
                for (int i = 0; i < allWords.Count - 1; i++)
                {
                    if (allWords[i].GetWord().Length < allWords[i + 1].GetWord().Length)
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

            List<Verse> resault = compareVerses(allWords);

            if (resault.Count < 1) // check for if no matches found
            {
                return null;
            }
            else
            {
                return compareVerses(allWords);
            }
        }
        private static bool isNull(Word item)
        {
            if (item == null)
            { return true; }
            else
            { return false; }
        }
        private Word getWord(string word) // gets all verses for a word from dictionary
        {
            word = word.ToLower();
            Word node;
            if (_bibleIndex.TryGetValue(word, out node))
            {
                return node;
            }
            else
            {
                return null;
            }
        }
        private string getVerse(Verse item)
        {
            return _bible.GetBook(item.Book).GetChapter(item.Chapter).GetVerse(item.VerseNumber);
        }
        private List<Verse> compareVerses(List<Word> wordList) // find all common verses amoung word list
        {
            if (wordList.Count > 1)
            {
                Word twoCombinedWords = new Word();
                foreach(Verse item in wordList[0].GetVerses())
                {
                    if(  getVerse(item).Contains(wordList[1].GetWord()))
                    {
                        twoCombinedWords.AddVerse(item);
                    }
                }
                wordList[0] = twoCombinedWords;
                wordList.RemoveAt(1);
                return compareVerses(wordList);
            }
            else
            {
                return wordList[0].GetVerses();
            }
        }
    }
}
