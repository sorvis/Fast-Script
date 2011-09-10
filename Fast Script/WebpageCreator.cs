using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Fast_Script
{
    class WebpageCreator
    {
        private string _fileName;
        private string _htmlHead;
        private string _CSS_imports; //<link href="css/style.css" type="text/css" rel="stylesheet">
        private string _htmlTail;

        public WebpageCreator(string fileName, string cssImports)
        {
            _fileName = fileName;
            _CSS_imports = cssImports;
            _htmlHead = "<html><head>" + _CSS_imports +
                "<script src=\"script.js\" type=\"text/javascript\"></script>" + 
                "</head><body>";
            _htmlTail = "</body></html>";

            // check for HTML Directory (installer does not seem to create empty folder)
            string folderPath = Path.GetDirectoryName(fileName); 
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            if (!File.Exists(fileName)) // check to see if need to create the file
            {
                File.Create(fileName);
            }
        }
        public void writeHTMLPage(string pageContent)
        {
            string pageBody = _htmlHead + pageContent + _htmlTail;
            File.WriteAllText(_fileName, pageBody);
        }
    }
}
