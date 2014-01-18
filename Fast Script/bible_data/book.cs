using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fast_Script.bible_data
{
    public class Book
    {
        private List<Chapter> _chapter;
        private string _bookTitle;

        public List<Chapter> GetAllChapters()
        {
            return _chapter;
        }
        public int[] Chapters
        {
            get
            {
                List<int> chaptersList=new List<int>();
                foreach (Chapter item in _chapter)
                {
                    chaptersList.Add(item.GetChapterNumber());
                }
                return chaptersList.ToArray();
            }
        }
        public Book(string title)
        {
            _bookTitle = title;
            _chapter = new List<Chapter>();
        }
        public Chapter GetChapter(int num)
        {
            if (num > _chapter.Count)
            {
                return new Chapter(null);
            }
            else
            {
                return _chapter[num - 1];
            }
        }
        public void AddChapter(Chapter number)
        {
            int intNumber = number.GetChapterNumber();
            while (_chapter.Count < intNumber)
            {
                _chapter.Add(new Chapter(null));
            }
            _chapter[intNumber-1] = number;
        }
        public string GetTitle()
        {
            return _bookTitle;
        }
    }
}
