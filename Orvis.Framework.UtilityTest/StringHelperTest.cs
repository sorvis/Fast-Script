using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orvis.Framework.Utility;
using System.Diagnostics;
using System.Linq;

namespace Orvis.Framework.UtilityTest
{
    [TestClass]
    public class StringHelperTest
    {
        [TestMethod]
        public void SplitStringIntoEvenLines_should_make_at_least_two_lines_given_two_words()
        {
            String text = "test word";
            String actual = StringHelper.SplitStringIntoEvenLines(text);
            Assert.IsTrue(actual.Contains(Environment.NewLine));
            Trace.Write(actual);
        }

        [TestMethod]
        public void SplitStringIntoEvenLines_should_keep_the_first_two_spaces_between_four_words()
        {
            String text = "test word with one";
            String expected = "test word";
            String actual = StringHelper.SplitStringIntoEvenLines(text);
            Trace.Write(actual);
            Assert.IsTrue(actual.StartsWith(expected));
        }

        [TestMethod]
        public void SplitStringIntoEvenLines_should_not_put_a_space_at_the_begining_of_new_lines()
        {
            String text = "test word with one";
            String expected = Environment.NewLine+"with one";
            String actual = StringHelper.SplitStringIntoEvenLines(text);
            Trace.Write(actual);
            Assert.IsTrue(actual.EndsWith(expected));
        }

        [TestMethod]
        public void SplitStringIntoEvenLines_should_have_no_more_lines_then_the_longest_line()
        {
            String text = "test word with one another thing is that I don't always spell things right and I don't really care what I am typing I just need a lot of words";
            String actual = StringHelper.SplitStringIntoEvenLines(text);
            String longestLine = actual.Split(Environment.NewLine.ToCharArray()).OrderByDescending(item => item.Length).First();
            Trace.WriteLine(actual);
            Trace.WriteLine("Longest Line: "+longestLine);
            int numberOfLines = actual.Count(character => character == Environment.NewLine.ToCharArray()[0]);
            Assert.IsTrue(longestLine.Length >= numberOfLines);
        }

        [TestMethod]
        public void SplitStringIntoEvenLines_should_have_more_lines_then_the_shortest_line()
        {
            String text = "test word with one another thing is that I don't always spell things right and I don't really care what I am typing I just need a lot of words";
            String actual = StringHelper.SplitStringIntoEvenLines(text);
            String shortestLine = actual.Split(Environment.NewLine.ToCharArray()).OrderBy(item => item.Length).First(item=>item!=String.Empty);
            Trace.WriteLine(actual);
            Trace.WriteLine("Shortest Line: " + shortestLine);
            int numberOfLines = actual.Count(character => character == Environment.NewLine.ToCharArray()[0])+1;
            Assert.IsTrue(shortestLine.Length <= numberOfLines);
        }
    }
}
