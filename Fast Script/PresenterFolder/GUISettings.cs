using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Drawing;
using System.Reflection;
//using System.Speech.Synthesis;
using System.IO;
using Fast_Script.Properties;

namespace Fast_Script.PresenterFolder
{
    [Serializable()]
    public class GUISettings : ISerializable, IPrinterSettings
    {
        public bool VerseSelecterEnabled{ get; set; }

        public string DefaultWebPage { get; set; }

        public Font PrinterFont{get; set;}

        // bibles
        private string pathToXMLBibles
        {
            get
            {
                return System.IO.Path.Combine(System.IO.Path.GetDirectoryName(
                   Assembly.GetExecutingAssembly().Location), "XML_Bibles");
            }
        }
        private List<bible_data.Bible> _bibles;
        public List<bible_data.Bible> Bibles
        { get { return _bibles; } set { _bibles = value; } }
        private bible_data.Bible _currentBible;
        public bible_data.Bible CurrentBible
        { get { return _currentBible; } set { _currentBible = value; } }
        // end bibles

        /*
        // text to speech
        private List<InstalledVoice> _TTSvoices = null;
        public List<InstalledVoice> TTSVoices
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
        public string[] TTSVoiceNames
        {
            get
            {
                string[] tempVoiceList = new string[TTSVoices.Count];
                for(int i = 0; i < TTSVoices.Count; i++)
                {
                    tempVoiceList[i] =  TTSVoices[i].VoiceInfo.Name;
                }
                return tempVoiceList;
            }
        }
        public InstalledVoice GetTTSByName(string name)
        {
            foreach (InstalledVoice voice in TTSVoices)
            {
                if (voice.VoiceInfo.Name == name)
                { return voice; }
            }
            return null;
        }
        */
        public int TTS_Rate { get; set; }
        // end text to speech

        /// <summary>
        /// Create then load settings with default values.
        /// </summary>
        public GUISettings()
        {

            //load in some defaults since a settings file was not found

            PrinterFont = new Font("Times New Roman", 12);

            _bibles = new List<bible_data.Bible>();
            LoadAllBiblesInFolder(); // load in all default bibles

            loadDefaultBibleIfNone();

            VerseSelecterEnabled = false;

            //CurrentTTSVoice = GetTTSByName( new SpeechSynthesizer().Voice.Name); // loads in default voice
            //TTS_Rate = new SpeechSynthesizer().Rate; //load default rate for TTS
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
            foreach(bible_data.Bible item in _bibles)
            {
                if(item.BibleVersion == versionName)
                {
                    _currentBible = item;
                    return true;
                }
            }
            return false;
        }
        public string AddBible(string fileName)
        {
            bible_data.Bible tempBible = new XLM_bible_reader.BibleBuilder(fileName).GetBible;
            tempBible.BuildIndex(); // build the index
            string newFileName = tempBible.BibleVersion + System.IO.Path.GetExtension(fileName);
            //ObjectSerializing.deepCopyToFileFromObject(tempBible, System.IO.Path.GetFileNameWithoutExtension(newFileName) + ".bib"); // save a binary copy of built bible
            if (!listContainsBible(tempBible.BibleVersion))
            {
                _bibles.Add(tempBible);

                if (!System.IO.File.Exists(System.IO.Path.Combine(pathToXMLBibles, newFileName)))
                {
                    // move a copy to the XML_Bibles folder for next time program runs if it does not exist
                    new FileCopier(fileName, pathToXMLBibles, newFileName);
                }
            }
            return tempBible.BibleVersion;
        }
        private bool listContainsBible(string versionName) // keep list from getting dups
        {
            foreach (bible_data.Bible bible in _bibles)
            {
                if (bible.BibleVersion == versionName)
                { return true; }
            }
            return false;
        }
        public void LoadAllBiblesInFolder()
        {
            if (System.IO.Directory.Exists(pathToXMLBibles))
            {
                string[] files = System.IO.Directory.GetFiles(pathToXMLBibles);
                foreach (string file in files)
                {
                    if (System.IO.Path.GetExtension(file).ToLower() == ".bib")
                    {
                        loadDataBible(file);
                    }
                    else if (System.IO.Path.GetExtension(file).ToLower() == ".xml")
                    {
                        AddBible(file);
                    }
                }
            }
        }
        private void loadDataBible(string file)
        {
            bible_data.Bible bible = (bible_data.Bible)ObjectSerializing.deepCopyFromFileToObject(file);

            if (!listContainsBible(bible.BibleVersion))
            {
                _bibles.Add(bible);
            }
        }
        public GUISettings(SerializationInfo info, StreamingContext ctxt)
        {
            _bibles = new List<bible_data.Bible>();
            LoadAllBiblesInFolder(); // load in all bibles in folder
            setBible( (string)info.GetValue("CurrentBible", typeof(string)));
            loadDefaultBibleIfNone(); // load first one incase

            PrinterFont = (Font)info.GetValue("_printFont", typeof(Font));
            VerseSelecterEnabled = (bool)info.GetValue("VerseSelecterEnabled", typeof(bool));
            //CurrentTTSVoice = GetTTSByName((string)info.GetValue("CurrentTTSVoice", typeof(string)));
            TTS_Rate = (int)info.GetValue("TTS_Rate", typeof(int));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("_printFont", PrinterFont);
            info.AddValue("CurrentBible", CurrentBible.BibleVersion);
            info.AddValue("VerseSelecterEnabled", VerseSelecterEnabled);
            //info.AddValue("CurrentTTSVoice", CurrentTTSVoice.VoiceInfo.Name);
            info.AddValue("TTS_Rate", TTS_Rate);
        }
    }
}
