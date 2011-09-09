using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Drawing;
using System.Reflection;
using System.Speech.Synthesis;

namespace Fast_Script.PresenterFolder
{
    [Serializable()]
    public class GUI_Settings : ISerializable
    {
        public bool VerseSelecterEnabled{ get; set; }

        // print settings
        public Font PrinterFont
        {
            get { return _backend.Printer.PrinterFont; }
            set { _backend.Printer.PrinterFont = value; }
        }
        // end print settings

        // bibles
        private string _pathToXMLBibles
        {
            get
            {
                return System.IO.Path.Combine(System.IO.Path.GetDirectoryName(
                   Assembly.GetExecutingAssembly().Location), "XML_Bibles");
            }
        }
        private List<bible_data.bible> _bibles;
        public List<bible_data.bible> Bibles
        { get { return _bibles; } set { _bibles = value; } }
        private bible_data.bible _currentBible;
        public bible_data.bible CurrentBible
        { get { return _currentBible; } set { _currentBible = value; } }
        // end bibles

        // text to speech
        private List<InstalledVoice> _TTSvoices = null;
        public List<InstalledVoice> TTS_GetVoices
        {
            get
            {
                if (_TTSvoices == null)
                {
                    _TTSvoices = new List<InstalledVoice>();
                    foreach (InstalledVoice voice in new SpeechSynthesizer().GetInstalledVoices())
                    {
                        _TTSvoices.Add(voice);
                    }
                }
                return _TTSvoices;
            }
        }
        public InstalledVoice CurrentTTSVoice { get; set; }
        public string[] Get_All_TTS_VoiceNames
        {
            get
            {
                string[] tempVoiceList = new string[TTS_GetVoices.Count];
                for(int i = 0; i < TTS_GetVoices.Count; i++)
                {
                    tempVoiceList[i] =  TTS_GetVoices[i].VoiceInfo.Name;
                }
                return tempVoiceList;
            }
        }
        public InstalledVoice Get_TTS_byName(string name)
        {
            foreach (InstalledVoice voice in TTS_GetVoices)
            {
                if (voice.VoiceInfo.Name == name)
                { return voice; }
            }
            return null;
        }
        public int TTS_Rate { get; set; }
        // end text to speech

        private backEndInitializer _backend;

        public GUI_Settings(backEndInitializer backend)
        {
            _backend = backend;

            //load in some defaults since a settings file was not found

            PrinterFont = new Font("Times New Roman", 12);

            _bibles = new List<bible_data.bible>();
            loadAllBiblesInFolder(); // load in all default bibles

            loadDefaultBibleIfNone();

            VerseSelecterEnabled = false;

            CurrentTTSVoice = Get_TTS_byName( new SpeechSynthesizer().Voice.Name); // loads in default voice
            TTS_Rate = new SpeechSynthesizer().Rate; //load default rate for TTS
        }
        private void loadDefaultBibleIfNone()
        {
            if (CurrentBible == null)// to prevent errors load first bible in list
            {
                try
                {
                    CurrentBible = _bibles.First();
                }
                catch
                {
                    //TODO: send message to GUI demanding a bible be loaded
                }
            }
        }
        private bool setBible(string versionName)
        {
            foreach(bible_data.bible item in _bibles)
            {
                if(item._bibleVersion == versionName)
                {
                    _currentBible = item;
                    return true;
                }
            }
            return false;
        }
        public string addBible(string fileName)
        {
            bible_data.bible tempBible = new XLM_bible_reader.BibleBuilder(fileName).GetBible;
            tempBible.BuildIndex();
            if (!listContainsBible(tempBible._bibleVersion))
            {
                _bibles.Add(tempBible);

                string newFileName = tempBible._bibleVersion+System.IO.Path.GetExtension(fileName);
                if (!System.IO.File.Exists(System.IO.Path.Combine(_pathToXMLBibles, newFileName)))
                {
                    // move a copy to the XML_Bibles folder for next time program runs if it does not exist
                    new FileCopier(fileName, _pathToXMLBibles, newFileName);
                }
            }
            return tempBible._bibleVersion;
        }
        private bool listContainsBible(string versionName) // keep list from getting dups
        {
            foreach (bible_data.bible bible in _bibles)
            {
                if (bible._bibleVersion == versionName)
                { return true; }
            }
            return false;
        }
        public void loadAllBiblesInFolder()
        {
            if (System.IO.Directory.Exists(_pathToXMLBibles))
            {
                string[] files = System.IO.Directory.GetFiles(_pathToXMLBibles);
                foreach (string file in files)
                {
                    if (System.IO.Path.GetExtension(file).ToLower() == ".xml")
                    {
                        addBible(file);
                    }
                }
            }
        }
        public GUI_Settings(SerializationInfo info, StreamingContext ctxt)
        {
            _backend = (backEndInitializer) ctxt.Context;
            _bibles = new List<bible_data.bible>();
            loadAllBiblesInFolder(); // load in all bibles in folder
            setBible( (string)info.GetValue("CurrentBible", typeof(string)));
            loadDefaultBibleIfNone(); // load first one incase

            PrinterFont = (Font)info.GetValue("_printFont", typeof(Font));
            VerseSelecterEnabled = (bool)info.GetValue("VerseSelecterEnabled", typeof(bool));
            CurrentTTSVoice = Get_TTS_byName((string)info.GetValue("CurrentTTSVoice", typeof(string)));
            TTS_Rate = (int)info.GetValue("TTS_Rate", typeof(int));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("_printFont", PrinterFont);
            info.AddValue("CurrentBible", CurrentBible._bibleVersion);
            info.AddValue("VerseSelecterEnabled", VerseSelecterEnabled);
            info.AddValue("CurrentTTSVoice", CurrentTTSVoice.VoiceInfo.Name);
            info.AddValue("TTS_Rate", TTS_Rate);
        }
    }
}
