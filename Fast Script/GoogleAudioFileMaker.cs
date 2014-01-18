using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Fast_Script
{
    class GoogleAudioFileMaker
    {
        // Start Google Text-To-Speech
        static public void GoogleTTS(string fileNameToCreate, string textToConvert)
        {
            string[] wordsArray = textToConvert.Split(' ');
            List<string> wordPhrases = new List<string>();

            // put words into under 100 char word phrases
            string tempPhrase = "";
            foreach (string word in wordsArray)
            {
                if (tempPhrase.Count() + word.Count() <= 100) // under 100 char
                {
                    tempPhrase += " " + word;
                }
                else // would be more than 100 char if word is added
                {
                    wordPhrases.Add(tempPhrase);
                    tempPhrase = word;
                }
            }

            // get audio files
            int fileNameCount = 0;
            foreach (string phrase in wordPhrases)
            {
                if (fileNameCount > 0)
                {
                    downloadGoogleFile(fileNameToCreate + fileNameCount, phrase);
                }
                else
                {
                    downloadGoogleFile(fileNameToCreate, phrase);
                }
                fileNameCount++;
            }

            fileNameCount--; // remove last one which does not exist
            while (fileNameCount > 0) // append all files into one then delete the left overs
            {
                if (fileNameCount <= 1)
                {
                    appendFiles(fileNameToCreate + ".mp3", fileNameToCreate + fileNameCount + ".mp3");
                }
                else
                {
                    appendFiles(fileNameToCreate + (fileNameCount - 1) + ".mp3", fileNameToCreate + fileNameCount + ".mp3");
                }
                fileNameCount--;
            }
        }
        /// <summary>
        /// Limited To 100 characters at a time
        /// </summary>
        /// <param name="fileNameToCreate"></param>
        /// <param name="textToConvert"></param>
        static private void downloadGoogleFile(string fileNameToCreate, string textToConvert)
        {
            WebClient web = new WebClient();

            web.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/4.0 (compatible; MSIE 9.0; Windows;)");

            string encstr = string.Empty;

            fileNameToCreate += ".mp3";

            encstr = Uri.EscapeDataString(textToConvert);

            //Console.WriteLine(encstr);
            try
            {
                web.DownloadFile("http://translate.google.com/translate_tts?tl=en&q=" + encstr, fileNameToCreate);
            }
            catch (Exception e)
            {
                string test = e.Message;
            }
            //web.DownloadFile("http://translate.google.com/translate_tts?tl=fr&q=t%C3%A9l%C3%A9phone", ".\\" + fileNameToCreate);
        }
        static private string appendFiles(string firstFile, string secondFile)
        {
            FileStream fs1 = null;
            FileStream fs2 = null;
            try
            {
                fs1 = File.Open(firstFile, FileMode.Append);
                fs2 = File.Open(secondFile, FileMode.Open);
                byte[] fs2Content = new byte[fs2.Length];
                fs2.Read(fs2Content, 0, (int)fs2.Length);
                fs1.Write(fs2Content, 0, (int)fs2.Length);
            }
            finally
            {
                fs1.Close();
                fs2.Close();
                File.Delete(secondFile);
            }
            return firstFile;
        }
        // End Google Text-To-Speech
    }
}
