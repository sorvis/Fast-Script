using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fast_Script.PresenterFolder.Searching
{
    static class Extentions
    {
        public static string CapitalizeWord(this string word)
        {
            char[] tempArray = word.ToCharArray();

            // capitalize first char if not a number
            // if number then probably a numbered book
            int bookNumber;
            if (int.TryParse(Convert.ToString(tempArray[0]), out bookNumber)) // first char is number
            {
                // assume spot 2 is what needs capitalized.
                tempArray[2] = char.ToUpper(tempArray[2]);
                for (int i = 3; i < word.Count(); i++)
                {
                    tempArray[i] = char.ToLower(tempArray[i]);
                }
            }
            else // first char is not number so capitalize it
            {
                tempArray[0] = char.ToUpper(tempArray[0]);
                for (int i = 1; i < word.Count(); i++)
                {
                    tempArray[i] = char.ToLower(tempArray[i]);
                }
            }

            string tempString = "";
            foreach (char i in tempArray)
            {
                tempString += i;
            }
            return tempString;
        }
    }
}
