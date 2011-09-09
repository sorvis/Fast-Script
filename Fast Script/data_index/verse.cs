using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Fast_Script.data_index
{
    [Serializable()]
    public class verse : ISerializable
    {
        private string _book;

        private Int16 _chapterNum;
        private Int16 _verseNum;
        private string _verseText;
        public verse(string book, int chapter, int verse)
        {
            _book = book;
            _chapterNum = (Int16)chapter;
            _verseNum = (Int16)verse;
        }
        public verse(string book, int chapter, int verse, string verseText)
        {
            _book = book;
            _chapterNum = (Int16)chapter;
            _verseNum = (Int16)verse;
            _verseText = verseText;
        }
        public string getVerseText()
        {
            return _verseText;
        }
        public verse(SerializationInfo info, StreamingContext ctxt)
        {
            _book = (string)info.GetValue("Book", typeof(string));
            _chapterNum = (Int16)info.GetValue("ChapterNum", typeof(int));
            _verseNum = (Int16)info.GetValue("VerseNum", typeof(int));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Book", this._book);
            info.AddValue("ChapterNum", this._chapterNum);
            info.AddValue("VerseNum", this._verseNum);
        }
        public override string ToString()
        {
            return _book + _chapterNum+":"+_verseNum;
        }
        public override int GetHashCode()
        {
            return (_book +" "+ _chapterNum+":" + _verseNum).GetHashCode();
        }
        public String Book
        {
            get
            {
                return _book;
            }
            set
            {
                _book = value;
            }
        }
        public int Chapter
        {
            get
            {
                return _chapterNum;
            }
            set
            {
                _chapterNum = (Int16)value;
            }
        }
        public int Verse
        {
            get
            {
                return _verseNum;
            }
            set
            {
                _verseNum = (Int16)value;
            }
        }
    }
}
