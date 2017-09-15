using System;
namespace Fast_Script
{
    public interface IAudioGenerator
    {
        void MakeFileFromText(string fileNameToCreate, string textToConvert,
                         string voiceName, int voiceRate, 
                         System.ComponentModel.BackgroundWorker worker);
    }
}
