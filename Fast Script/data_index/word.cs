using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Fast_Script.data_index
{
    [Serializable()]
    public class word : ISerializable
    {
        private List<verse> _verses = new List<verse>();
        private string _word;

        public word(string word)
        {
            _word = word;
        }
        public word()
        {
        }
        public word(SerializationInfo info, StreamingContext ctxt)
        {
            this._verses = (List<verse>)info.GetValue("Verses", typeof(List<verse>));
            this._word = (string)info.GetValue("Word", typeof(string));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Verses", this._verses);
            info.AddValue("Word", this._word);
        }
        public string getWord()
        {
            return _word;
        }
        public void addVerse(verse newVerse)
        {
            _verses.Add(newVerse);
        }
        public List<verse> getVerses()
        {
            return _verses;
        }
    }
}
