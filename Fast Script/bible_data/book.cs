using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fast_Script.bible_data
{
    public class book
    {
        private List<chapter> _chapter;
        private string _bookTitle;

        public List<chapter> getAllChapters()
        {
            return _chapter;
        }
        public int[] Chapters
        {
            get
            {
                List<int> chaptersList=new List<int>();
                foreach (chapter item in _chapter)
                {
                    chaptersList.Add(item.getChapterNumber());
                }
                return chaptersList.ToArray();
            }
        }
        public book(string title)
        {
            _bookTitle = title;
            _chapter = new List<chapter>();
        }
        public chapter getChapter(int num)
        {
            if (num > _chapter.Count)
            {
                return new chapter(null);
            }
            else
            {
                return _chapter[num - 1];
            }
        }
        public void addChapter(chapter number)
        {
            int intNumber = number.getChapterNumber();
            while (_chapter.Count < intNumber)
            {
                _chapter.Add(new chapter(null));
            }
            _chapter[intNumber-1] = number;
        }
        public string getTitle()
        {
            return _bookTitle;
        }
    }
}
