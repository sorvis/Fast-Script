using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Fast_Script
{
    public static class ListExtention
    {
        public static List<T> AppendList<T>(this List<T> oldList, List<T> newList)
        {
                foreach (T item in newList)
                {
                    oldList.Add(item);
                }
            return oldList;
        }
        public static List<string> AddPrefixToList(this List<string> list, string prefix)
        {
            List<string> tempList = new List<string>();
            foreach (string item in list)
            {
                tempList.Add(prefix + item);
            }
            return tempList;
        }
        public static List<string> RemoveAll(this List<string> item, string pattern)
        {
            List<string> tempList = new List<string>();
            item.ForEach(delegate(String thing) { tempList.Add(thing); });
            for (int i = 0; i < item.Count; i++)
            {
                if (item[i] == pattern)
                {
                    tempList.Remove(pattern);
                }
            }
            return tempList;
        }
        public static List<string> ToStringList(this List<int> intList)
        {
            List<string> tempList = new List<string>();
            foreach (int item in intList)
            {
                tempList.Add(Convert.ToString(item));
            }
            return tempList;
        }
        public static List<string> StartsWithInList(this List<string> list, string searchString)
        {
            List<string> suggestionList = new List<string>();
            foreach (string item in list)
            {
                if (item.ToLower().StartsWith(searchString.ToLower()))
                {
                    suggestionList.Add(item);
                }
            }
            if (suggestionList.Count >= 1)
            {
                return suggestionList;
            }
            else
            {
                return null;
            }
        }
    }
    public static class StringExtention
    {
        public static string StripPunctuation(this string s)
        {
            var sb = new StringBuilder();
            foreach (char c in s)
            {
                if (!char.IsPunctuation(c))
                    sb.Append(c);
            }
            return sb.ToString();
        }
        public static string ReplaceNotCaseSensitive(this string item, string oldValue, string newValue)
        {
                return Regex.Replace(item, oldValue, newValue, RegexOptions.IgnoreCase);
        }
        public static bool Contains(this string[] list, string item, bool case_sensitive)
        {
            if (case_sensitive)
            {
                return list.Contains(item);
            }
            else
            {
                List<string> tempList = new List<string>();
                foreach (string thing in list)
                {
                    tempList.Add(thing.ToLower());
                }
                item = item.ToLower();
                return tempList.Contains(item);
            }
        }
        public static string RemoveAll(this string item, string remove)
        {
            if (remove.Count() <= item.Count())
            {
                string newString="";
                for (int i = 0; i < item.Count(); i++)
                {
                    if (item.Substring(i, remove.Length) == remove && i <= (item.Count() - remove.Count()))
                    {
                        i += remove.Count();
                    }
                    else
                    {
                        newString += item.Substring(i, 1);
                    }
                }
                return newString;
            }
            else
            {
                return item;
            }
        }
    }
}
