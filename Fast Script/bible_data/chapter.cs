using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fast_Script.bible_data
{
    public class chapter
    {
        private int? _chapter_number;
        private List<string> _verse;

        public List<string> getAllVerses()
        {
            return _verse;
        }
        public int getNumberOfVerses()
        {
            return _verse.Count;
        }
        public chapter(int? number)
        {
            _chapter_number = number;
            _verse = new List<string>();
        }
        public string getVerse(int num)
        {
            if (num > _verse.Count)
            {
                return null;
            }
            else
            {
                return _verse[num - 1];
            }
        }
        public void addVerse(string verse, int number)
        {
            while (_verse.Count < number)
            {
                _verse.Add(null);
            }
            _verse[number-1] = verse;
        }
        public int getChapterNumber()
        {
            if (_chapter_number != null)
            {
                return Convert.ToInt32(_chapter_number.ToString());
            }
            else
            {
                return 0;
            }
        }
    }
}
