using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Speech.Synthesis;
using System.Speech.AudioFormat;
using Yeti.MMedia;
using Yeti.MMedia.Mp3;
using WaveLib;
using System.ComponentModel;

namespace Fast_Script
{
    class AudioFileMaker
    {
        // Start Google Text-To-Speech
        static public void googleTTS(string fileNameToCreate, string textToConvert)
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
            while(fileNameCount > 0) // append all files into one then delete the left overs
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
            catch(Exception e)
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

        static public void NetFrameWorkTTS(string fileNameToCreate, string textToConvert, string voiceName, int voiceRate, BackgroundWorker worker)
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.SpeakProgress += delegate(object sender, SpeakProgressEventArgs e) 
                { synth_SpeakProgress(sender, e, worker, textToConvert.Length); };
            Stream audioStream = new MemoryStream();
            synth.SetOutputToWaveStream(audioStream); // set to wave file stream
            //synth.SetOutputToWaveFile(fileNameToCreate+".wav");
            synth.SelectVoice(voiceName);
            synth.Rate = voiceRate;
            //synth.Rate
            synth.Speak(textToConvert); // send data to stream
            //WaveStream waveStream = new WaveStream(audioStream);
            //audioStream.Close();
            synth.Dispose();

            audioStream.Position = 0;   // reset position for audio stream so it can be read

            //WaveStream waveStream = new WaveStream(fileNameToCreate + ".wav");    // read from file
            WaveStream waveStream = new WaveStream(audioStream);                    // read from stream

            string mp3FileName = fileNameToCreate;
            //before convert check for and add .mp3 file extention if nessary
            if (Path.GetExtension(fileNameToCreate).ToLower() != ".mp3".ToLower())
            { mp3FileName += ".mp3"; }

            // convert wav stream to mp3 stream
            Mp3WriterConfig mp3_Config = new Mp3WriterConfig(waveStream.Format);
            try
            {
                Mp3Writer writer = new Mp3Writer(new FileStream(mp3FileName, FileMode.Create), mp3_Config);
                try
                {
                    byte[] buff = new byte[writer.OptimalBufferSize];
                    int read = 0;
                    int actual = 0;
                    long total = waveStream.Length;
                    int progress;
                    try
                    {
                        while ((read = waveStream.Read(buff, 0, buff.Length)) > 0)
                        {
                            writer.Write(buff, 0, read);
                            actual += read;
                            progress = ((int)(((long)actual * 100) / total))/2 +50; // divide by 2 and add 50 so only latter 50% will be used up
                            worker.ReportProgress(progress);
                        }
                    }
                    catch { }
                }
                catch  {}
                finally
                {
                    writer.Close();
                }
            }
            finally
            {
                waveStream.Close();
            }

            //File.Delete(fileNameToCreate + ".wav");       // remove wave file
            audioStream.Close();        // close audio stream
        }

        static private void synth_SpeakProgress(object sender, SpeakProgressEventArgs e, BackgroundWorker worker,
            int charTotal)
        {
            worker.ReportProgress((Convert.ToInt16((int)(((long)e.CharacterPosition*100)/charTotal)))/2); // divide by two so only 50% will be used up
        }
    }
}
