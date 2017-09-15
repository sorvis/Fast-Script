using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fast_Script.bible_data
{
    public class Chapter
    {
        private int? _chapter_number;
        private List<string> _verse;

        public List<string> GetAllVerses()
        {
            return _verse;
        }
        public int GetNumberOfVerses()
        {
            return _verse.Count;
        }
        public Chapter(int? number)
        {
            _chapter_number = number;
            _verse = new List<string>();
        }
        public string GetVerse(int num)
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
        public void AddVerse(string verse, int number)
        {
            while (_verse.Count < number)
            {
                _verse.Add(null);
            }
            _verse[number-1] = verse;
        }
        public int GetChapterNumber()
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
