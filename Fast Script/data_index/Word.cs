using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Fast_Script.data_index
{
    [Serializable()]
    public class Word : ISerializable
    {
        private List<Verse> _verses = new List<Verse>();
        private string _word;

        public Word(string word)
        {
            _word = word;
        }
        public Word()
        {
        }
        public Word(SerializationInfo info, StreamingContext ctxt)
        {
            this._verses = (List<Verse>)info.GetValue("Verses", typeof(List<Verse>));
            this._word = (string)info.GetValue("Word", typeof(string));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Verses", this._verses);
            info.AddValue("Word", this._word);
        }
        public string GetWord()
        {
            return _word;
        }
        public void AddVerse(Verse newVerse)
        {
            _verses.Add(newVerse);
        }
        public List<Verse> GetVerses()
        {
            return _verses;
        }
    }
}
