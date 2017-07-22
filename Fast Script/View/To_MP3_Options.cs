using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using System.Speech.Synthesis;

namespace Fast_Script
{
    public partial class To_MP3_Options : Form
    {
        private Presenter _presenter;
        private PresenterFolder.GUISettings _settings;
        public To_MP3_Options(Presenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _settings = presenter.Settings;

            // load avalible voices in to dropdown
            //Installed_Voices_comboBox.Items.AddRange(_presenter.Settings.TTSVoiceNames);

            //Installed_Voices_comboBox.Text = _settings.CurrentTTSVoice.VoiceInfo.Name; // set current voice

            TTS_Rate_trackBar.Value = _settings.TTS_Rate + 10;
        }

        private void Installed_Voices_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //_settings.CurrentTTSVoice = _settings.GetTTSByName(Installed_Voices_comboBox.Text); // set current voice
            updateTTSLabels();
        }
        private void updateTTSLabels()
        {
            /*
            TTS_Voice_Age_textbox.Text = "Age: " + _settings.CurrentTTSVoice.VoiceInfo.Age;
            TTS_Voice_Culture_textbox.Text = "Culture: " + _settings.CurrentTTSVoice.VoiceInfo.Culture;
            TTS_Voice_Gender_textbox.Text = "Gender: " + _settings.CurrentTTSVoice.VoiceInfo.Gender;
*/
            TTS_Rate_label.Text = "Rate: " + _settings.TTS_Rate;
        }

        private void TTS_Rate_trackBar_Scroll(object sender, EventArgs e)
        {
            _settings.TTS_Rate = TTS_Rate_trackBar.Value - 10;
            
            updateTTSLabels();
        }

        private void TestButton_Click(object sender, EventArgs e)
        {
            /*
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.SelectVoice(_settings.CurrentTTSVoice.VoiceInfo.Name);
            synth.Rate = _settings.TTS_Rate;
            synth.SpeakAsync("This is " + _settings.CurrentTTSVoice.VoiceInfo.Description + " Speaking at a rate of " + _settings.TTS_Rate);
            */
        }
    }
}
