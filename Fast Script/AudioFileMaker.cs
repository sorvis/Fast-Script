﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
