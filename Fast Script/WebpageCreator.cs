using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        }
        public void writeHTMLPage(string pageContent)
        {
            string pageBody = _htmlHead + pageContent + _htmlTail;
            System.IO.File.WriteAllText(_fileName, pageBody);
        }
    }
}
