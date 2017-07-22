using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Fast_Script.XLM_bible_reader
{
    class OpenXML_Zefania_XML_Bible_Markup_Language
    {
        private string _fileLocation;
        private XmlTextReader _reader;
        private bible_data.BookManipulator _bible;

        public OpenXML_Zefania_XML_Bible_Markup_Language(string fileLocation, bible_data.BookManipulator manipulator)
        {
            _fileLocation = fileLocation;
            _reader = new XmlTextReader(_fileLocation);
            _bible = manipulator;

            // Create an XmlReader for <format>Zefania XML Bible Markup Language</format>
            using (_reader)
            {
                _reader.ReadToFollowing("title");
                String title = _reader.ReadElementContentAsString();
                _bible.SetVersion(title);

                String book;
                int verseNumber;
                int chapterNumber;
                string verseString;

                while (_reader.ReadToFollowing("BIBLEBOOK")) // read each book name
                {
                    _reader.MoveToAttribute(1);
                    book = _reader.Value;
                    _reader.ReadToFollowing("CHAPTER");
                    while (_reader.Name == "CHAPTER") // read each chapter
                    {
                        _reader.MoveToFirstAttribute();
                        chapterNumber = Convert.ToInt32(_reader.Value);

                        _reader.ReadToFollowing("VERS");
                        while (_reader.Name == "VERS") // read each verse
                        {
                            _reader.MoveToFirstAttribute();
                            verseNumber = Convert.ToInt32(_reader.Value);
                            _reader.Read();
                            verseString = _reader.Value;

                            sendToDataTree(book, verseNumber, chapterNumber, verseString);

                            _reader.Read();
                            _reader.Read();
                            _reader.Read();
                        }//end verse while

                        _reader.Read();
                        _reader.Read();
                    } //end chapter while
                } // end book while
            } //end using statement
        }
        private void sendToDataTree(string book, int verseNum, int chptNum, string verseStrg)
        {
            _bible.AddVerse(verseStrg, book, chptNum, verseNum, true);
        }
    }
}
