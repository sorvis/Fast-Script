using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Fast_Script.XLM_bible_reader
{
    class BibleBuilder
    {
        OpenXML_Zefania_XML_Bible_Markup_Language _reader;
        bible_data.Bible _bible;
        public BibleBuilder(string fileLocation)
        {
            switch (getFormatName(fileLocation))
            {
                case "Zefania XML Bible Markup Language":
                    _bible = new bible_data.Bible();
                    _reader = new OpenXML_Zefania_XML_Bible_Markup_Language(fileLocation, _bible.GetManipulator());
                    break;
                default:
                    throw new System.ArgumentException("XML format not recognized.", getFormatName(fileLocation));
            }
        }
        private string getFormatName(string fileLocation)
        {
            XmlTextReader XMLreader;
            XMLreader = new XmlTextReader(fileLocation);
            XMLreader.ReadToFollowing("format");
            return XMLreader.ReadElementContentAsString();
        }
        public bible_data.Bible GetBible
        { get { return _bible; } }
    }
}
