using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fast_Script.PresenterFolder.Searching
{
    static class preSearchStringBuilder
    {
        public static string[] fixBookNumberTitlesInSearchArray(string[] text)
        {
            // fix all books that begin with a number
            int bookNumber;
            List<string> tempList = text.ToList();
            for (int i = 0; i < text.Count() - 1; i++)
            {
                if ((i == 0 || text[i - 1] == "-" || text[i - 1].EndsWith(";")) &&
                        (int.TryParse(text[i], out bookNumber) && text.Count() > i + 1))
                {
                    tempList[1 + i] = tempList[i] + " " + tempList[1 + i];
                    tempList[i] = "";
                }
            }
            return tempList.RemoveAll("").ToArray();
        }

        public static string[] combineHyphenAndDashInArray(string[] tempList)
        {
            // combine numbers, :, and - together
            int bookNumber;
            //List<string> tempList = text.ToList();
            for (int i = 0; i < tempList.Count() - 1; i++)
            {
                if ((int.TryParse(tempList[i], out bookNumber) || int.TryParse(tempList[i + 1], out bookNumber))
                    && (tempList[i].Contains(':') || tempList[i + 1].Contains(':') ||
                    tempList[i].Contains('-') || tempList[i + 1].Contains('-')))
                {
                    tempList[1 + i] = tempList[i] + tempList[1 + i];
                    tempList[i] = "";
                }
            }
            return tempList.ToList().RemoveAll("").ToArray();
        }
    }
}
