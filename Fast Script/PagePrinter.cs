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
        private int PageNumber;
        private string _textToPrint;
        public string TextToPrint
        {
            get { return _textToPrint; }
            set { _textToPrint = value; }
        }
        public Font printFont 
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
        private int curChar=0;

        private PrintDocument printDoc = new PrintDocument();
        private PageSettings pgSettings = new PageSettings();
        private PrinterSettings prtSettings = new PrinterSettings();
        private PresenterFolder.IprinterSettings _printerSettings;

        public PagePrinter(PresenterFolder.IprinterSettings printerSettings)
        {
            printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);
            _printerSettings = printerSettings;
        }
        public void filePrintMenuItem_Click(Object sender, EventArgs e)
        {
            PageNumber = 0;

            printDoc.DefaultPageSettings = pgSettings;
            PrintDialog dlg = new PrintDialog();
            dlg.Document = printDoc;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                printDoc.Print();
            }
        }
        private void printDoc_PrintPage(Object sender, PrintPageEventArgs e)
        {
            PageNumber += 1;
            //_textToPrint = ".NET Printing is easyasdd dddd\n23ddddddddddddd\nfive\nsss ddddddddddd\n ddddddddddf \nsdaf\n saf\n sadf\n asdf\n sad\n sda\n fasdsd\n sd\n sad\n sdafsdaf asdfffff sdafffffffff ddddddd \nend\nend\nend\nend\nend\nend\nend\nend\nend\nend\nend\nend\nend\nend\nend\nend\nend\nend\nend\nend\nend\nend\nend\nend\nend\nend\nend\nend\nend\n18\n19\n20\n21\n22\n23\n24\n25\nend\nend\nend\nend\nend\nend\nend\nend\nend\n30\nend\nend\nend\nend\nend\nend\nend\n35\nend\nend\nend\nend\nend\n40\nsdf asldkfj lkjdfl asjlkfj alskdjflksdjlk jalskdjf asldjflksa djlkfsajd lkjf laksjdflkas djlfkjasd lkjfasldkj fkasdjfalsdkjflaksdjf laskjksdlfjflksdjlkasjdlfkj lkdjs";
            //Font printFont = _font;
            //printFont = new Font("Courier New", 12);

            int printHeight = (int)e.MarginBounds.Height;
            int printWidth = (int)e.MarginBounds.Width;
            if (e.PageSettings.Landscape)
            {
                int tmp;
                tmp = e.MarginBounds.Height;
                printHeight = printWidth;
                printWidth = tmp;
            }

            // set number lines per page
            Int32 numLines = (int)printHeight / printFont.Height;

            //Create a rectangle printing are for our document
            RectangleF printArea = new RectangleF(e.MarginBounds.Left, e.MarginBounds.Right, printWidth, printHeight);

            //Use the StringFormat class for the text layout of our document
            StringFormat format = new StringFormat(StringFormatFlags.LineLimit);

            //Fit as many characters as we can into the print area      
            int chars;
            int lines;
            e.Graphics.MeasureString(_textToPrint.Substring(curChar), printFont, new SizeF(printWidth, printHeight), format, out chars, out lines);

            //Print the page
            e.Graphics.DrawString("Page: "+PageNumber, printFont, Brushes.Black, new Point(5, 5));
            e.Graphics.DrawString(_textToPrint.Substring(curChar), printFont, Brushes.Black, e.MarginBounds, format);

            //e.Graphics.DrawString(_textToPrint.Substring(curChar), printFont, Brushes.Black, e.MarginBounds);

            //Increase current char count
            curChar += chars;

            //Detemine if there is more text to print, if
            //there is the tell the printer there is more coming
            if (curChar < _textToPrint.Length)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
                curChar = 0;
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
            pageSetupDialog.PageSettings = pgSettings;
            pageSetupDialog.PrinterSettings = prtSettings;
            pageSetupDialog.AllowOrientation = true;
            pageSetupDialog.AllowMargins = true;
            pageSetupDialog.ShowDialog();
        }
        public void filePrintPreviewMenuItem_Click(Object sender, EventArgs e)
        {
            PageNumber = 0;

            PrintPreviewDialog dlg = new PrintPreviewDialog();
            dlg.Document = printDoc;
            dlg.ShowDialog();
        }
    }
}
