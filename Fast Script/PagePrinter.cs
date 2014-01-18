using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Drawing;
using System.Windows.Forms;

namespace Fast_Script
{
    public class PagePrinter
    {
        private int _pageNumber;
        private string _textToPrint;
        public string TextToPrint
        {
            get { return _textToPrint; }
            set { _textToPrint = value; }
        }
        public Font PrintFont 
        {
            get
            {
                return _printerSettings.PrinterFont;
            }
            set
            {
                _printerSettings.PrinterFont = value;
            }
        }
        private int _curChar=0;

        private PrintDocument _printDoc = new PrintDocument();
        private PageSettings _pageSettings = new PageSettings();
        private PrinterSettings _printSettings = new PrinterSettings();
        private PresenterFolder.IPrinterSettings _printerSettings;

        public PagePrinter(PresenterFolder.IPrinterSettings printerSettings)
        {
            _printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);
            _printerSettings = printerSettings;
        }
        public void filePrintMenuItem_Click(Object sender, EventArgs e)
        {
            _pageNumber = 0;

            _printDoc.DefaultPageSettings = _pageSettings;
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = _printDoc;
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                _printDoc.Print();
            }
        }
        private void printDoc_PrintPage(Object sender, PrintPageEventArgs eventArgs)
        {
            _pageNumber += 1;

            int printHeight = (int)eventArgs.MarginBounds.Height;
            int printWidth = (int)eventArgs.MarginBounds.Width;
            if (eventArgs.PageSettings.Landscape)
            {
                int tmp;
                tmp = eventArgs.MarginBounds.Height;
                printHeight = printWidth;
                printWidth = tmp;
            }

            // set number lines per page
            Int32 numLines = (int)printHeight / PrintFont.Height;

            //Create a rectangle printing are for our document
            RectangleF printArea = new RectangleF(eventArgs.MarginBounds.Left, eventArgs.MarginBounds.Right, printWidth, printHeight);

            //Use the StringFormat class for the text layout of our document
            StringFormat format = new StringFormat(StringFormatFlags.LineLimit);

            //Fit as many characters as we can into the print area      
            int chars;
            int lines;
            eventArgs.Graphics.MeasureString(_textToPrint.Substring(_curChar), PrintFont, new SizeF(printWidth, printHeight), format, out chars, out lines);

            //Print the page
            eventArgs.Graphics.DrawString("Page: "+_pageNumber, PrintFont, Brushes.Black, new Point(5, 5));
            eventArgs.Graphics.DrawString(_textToPrint.Substring(_curChar), PrintFont, Brushes.Black, eventArgs.MarginBounds, format);

            //e.Graphics.DrawString(_textToPrint.Substring(curChar), printFont, Brushes.Black, e.MarginBounds);

            //Increase current char count
            _curChar += chars;

            //Detemine if there is more text to print, if
            //there is the tell the printer there is more coming
            if (_curChar < _textToPrint.Length)
            {
                eventArgs.HasMorePages = true;
            }
            else
            {
                eventArgs.HasMorePages = false;
                _curChar = 0;
            }
        }
        private int RemoveZeros(int value)
        {
            //Check the value passed into the function,
            //if the value is a 0 (zero) then return a 1,
            //otherwise return the value passed in
            switch (value)
            {
                case 0:
                    return 1;
                default:
                    return value;
            }
        }
        public void filePageSetupMenuItem_Click(Object sender, EventArgs e)
        {
            PageSetupDialog pageSetupDialog = new PageSetupDialog();
            pageSetupDialog.PageSettings = _pageSettings;
            pageSetupDialog.PrinterSettings = _printSettings;
            pageSetupDialog.AllowOrientation = true;
            pageSetupDialog.AllowMargins = true;
            pageSetupDialog.ShowDialog();
        }
        public void filePrintPreviewMenuItem_Click(Object sender, EventArgs e)
        {
            _pageNumber = 0;

            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.Document = _printDoc;
            printPreviewDialog.ShowDialog();
        }
    }
}
