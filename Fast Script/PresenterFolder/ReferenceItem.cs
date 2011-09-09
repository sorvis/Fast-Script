using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Fast_Script.PresenterFolder
{
    [Serializable()]
    public class ReferenceItem : ISerializable
    {
        private string _startBook;
        public string startBook
        {
            get
            {
                return _startBook;
            }
            set
            {
                _startBook = value;
            }
        }
        private string _endBook;
        public string endBook
        {
            get
            {
                return _endBook;
            }
            set
            {
                _endBook = value;
            }
        }
        private int? _startChapter;
        public int? startChapter
        {
            get
            {
                return _startChapter;
            }
            set
            {
                _startChapter = value;
            }
        }
        private int? _endChapter;
        public int? endChapter
        {
            get
            {
                return _endChapter;
            }
            set
            {
                _endChapter = value;
            }
        }
        private int? _startVerse;
        public int? startVerse
        {
            get
            {
                return _startVerse;
            }
            set
            {
                _startVerse = value;
            }
        }
        private int? _endVerse;
        public int? endVerse
        {
            get
            {
                return _endVerse;
            }
            set
            {
                _endVerse = value;
            }
        }
        private bool _range = false;
        public bool range
        {
            get{return _range;}
            set { _range = value; }
        }

        string _text;
        public string text
        {
            get{return _text;}
            set { _text = value; }
        }

        public ReferenceItem()
        {
        }
        public ReferenceItem(string startBook, int startChapter, int startVerse, bool range, string verseText)
        {
            _startBook = startBook;
            _startChapter = startChapter;
            _startVerse = startVerse;
            _range = range;
            _text = verseText;
        }
        public override string ToString()
        {
            string reference = "";
            if (range == false)
            {
                if (startBook != null)
                {
                    reference += startBook;
                    if (startChapter != null)
                    {
                        reference += " " + startChapter;
                        if (startVerse != null)
                        {
                            reference += ":" + startVerse;
                        }
                    }
                }
            }
            else // range of verses
            {
                string secondHalfReference = "";
                if (startBook != null) // book
                {
                    reference += startBook;
                    if (startBook != endBook && endBook != null)
                    {
                        secondHalfReference += " - " + endBook;
                    }

                    if (startChapter != null) // chapter
                    {
                        reference += " " + startChapter;
                        if (secondHalfReference == "" && startChapter != endChapter && endChapter!=null)
                        {
                            secondHalfReference += "-" + endChapter;
                        }
                        else if (secondHalfReference != "" && endChapter != null) // don't check for different chapters because two references are in different books
                        {
                            secondHalfReference += " " + endChapter;
                        }

                        if (startVerse != null) // verse
                        {
                            reference += ":" + startVerse;
                            if (endVerse != null&&secondHalfReference!="")
                            {
                                secondHalfReference += ":" + endVerse;
                            }
                            else if (endVerse != null)
                            {
                                secondHalfReference += "-" + endVerse;
                            }
                        }
                    }
                }
                reference = reference + secondHalfReference;
            }
            return reference;
        }
        public ReferenceItem(SerializationInfo info, StreamingContext ctxt)
        {
            _startBook = (string)info.GetValue("_startBook",
                typeof(string));
            _endBook = (string)info.GetValue("_endBook",
                typeof(string));
            _startChapter = (int?)info.GetValue("_startChapter",
                typeof(int?));
            _endChapter = (int?)info.GetValue("_endChapter",
                typeof(int?));
            _startVerse = (int?)info.GetValue("_startVerse",
                typeof(int?));
            _endVerse = (int?)info.GetValue("_endVerse",
                typeof(int?));
            _range = (bool)info.GetValue("_range",
                typeof(bool));
            _text = (string)info.GetValue("_text",
                typeof(string));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("_startBook", _startBook);
            info.AddValue("_endBook", _endBook);
            info.AddValue("_startChapter", _startChapter);
            info.AddValue("_endChapter", _endChapter);
            info.AddValue("_startVerse", _startVerse);
            info.AddValue("_endVerse", _endVerse);
            info.AddValue("_range", _range);
            info.AddValue("_text", _text);
        }
    }
    class ReferenceItemWrapper
    {
        private ReferenceItem _item;
        public ReferenceItemWrapper(ReferenceItem item)
        {
            _item = item;
        }
        public ReferenceItem getItem
        {get {return _item;}}
        public override string ToString()
        {
            return _item.ToString();
        }
    }
}
