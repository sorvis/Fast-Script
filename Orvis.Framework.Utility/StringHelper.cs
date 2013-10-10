using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orvis.Framework.Utility
{
    public class StringHelper
    {
        public static String SplitStringIntoEvenLines(String originalString)
        {
            return SplitStringIntoNumberOfLines(originalString, originalString.Split(' ').Length / 2);
        }

        public static String SplitStringIntoNumberOfLines(String originalString, int numberOfLines)
        {
            String[] orginalStringArray = originalString.Split(' ');
            StringBuilder newString = new StringBuilder((int)(orginalStringArray.Length * 1.5));

            String spaceAfterFirstWord = String.Empty;
            for (int i = 0; i < orginalStringArray.Length; i++)
            {
                newString.Append(spaceAfterFirstWord);
                if (i + 1 == orginalStringArray.Length / numberOfLines)
                {
                    newString.AppendLine(orginalStringArray[i]);
                    spaceAfterFirstWord = String.Empty;
                }
                else
                {
                    newString.Append(orginalStringArray[i]);
                    spaceAfterFirstWord = " ";
                }
            }
            return newString.ToString();
        }
    }
}
