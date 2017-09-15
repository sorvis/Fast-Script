using System;
using System.ComponentModel;
using System.IO;
using VoiceRSS_SDK;

namespace Fast_Script
{
    public class VoiceRssAudioFileMaker : IAudioGenerator
    {
		string lang = Languages.English_UnitedStates;
        private string apiKey = Environment.GetEnvironmentVariable("VoiceRssApiKey");

        public void MakeFileFromText(string fileNameToCreate, string textToConvert, 
                                     string voiceName, int voiceRate, BackgroundWorker worker)
        {
			var voiceParams = new VoiceParameters(textToConvert, lang)
			{
				AudioCodec = AudioCodec.MP3,
				AudioFormat = AudioFormat.Format_44KHZ.AF_44khz_16bit_stereo,
				IsBase64 = false,
				IsSsml = false,
				SpeedRate = 0
			};

			var voiceProvider = new VoiceProvider(apiKey, false);
			var voice = voiceProvider.Speech<byte[]>(voiceParams);

			File.WriteAllBytes(fileNameToCreate, voice);
        }
    }
}
