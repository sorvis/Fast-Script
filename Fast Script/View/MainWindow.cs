using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing.Text;
using Fast_Script.PresenterFolder.Searching;

namespace Fast_Script
{
    public partial class MainWindow : Form, IMainWindow
    {
        private List<FontFamily> _avalibleFonts=null;
        // give credit to http://dryicons.com for icons
        private Presenter _presenter;
        public Presenter presenter { get { return _presenter; } }
        public BackgroundWorker Get_MakeMP3backgroundWorker { get { return MakeMP3backgroundWorker; } }
        private VerseSelecter _selectVerse;
        public MainWindow()
        {
            InitializeComponent();
            _presenter = new Presenter(this);
            _selectVerse = new VerseSelecter(_presenter);

            // verse Selecter
            Controls.Add(_selectVerse);
            _selectVerse.Visible = false;
            _selectVerse.BringToFront();

            if (_presenter.Settings.VerseSelecterEnabled)
            { _selectVerse.resetForm(); }

            //disable search bar while index is building
            if (_presenter.Settings.CurrentBible.indexBuilderWorker.IsBusy)
            {
                searchBox.Enabled = false;

                //add event to re-enable searchBox
                _presenter.Settings.CurrentBible.indexBuilderWorker.RunWorkerCompleted += 
                    new RunWorkerCompletedEventHandler(indexBuilderWorker_RunWorkerCompleted);
            }
        }

        // the index of the current bible has been built so re-enable search box
        private void indexBuilderWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            searchBox.Enabled = true;
        }

        public CheckedListBox verseListBox
        {get { return checkedVerses; }}

        private void webResualts_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            string pageURL = e.Url.ToString();
            if (!pageURL.Contains("res://ieframe.dll/unknownprotocol.htm"))
            {
                if (pageURL.Contains("addReference"))
                {
                    string[] link = e.Url.ToString().Split('=');
                    checkedVerses.Items.Add(new PresenterFolder.ReferenceItemWrapper(
                        _presenter.ItemstInWebView[Convert.ToInt32(link[1])]), true);

                    //reload webview with last displayed verses
                    _presenter.displayVersesToWebView();
                }
            }
            else
            {
                webResualts.Url = new Uri(_presenter.Settings.DefaultWebPage);
            }
        }
        public bool verseListContains(string item)
        {
            List<string> items = new List<string>();
            foreach(PresenterFolder.ReferenceItemWrapper thing in checkedVerses.Items)
            {
                 items.Add(thing.getItem.ToString());
            }
            if(items.Contains(item))
            {return true;}
            else
            {return false;}
        }
        public void loadWebPage(string link)
        {
            webResualts.Url = new Uri(link);
        }

        private void searchBox_Enter(object sender, EventArgs e)
        {
            _presenter.searchString(searchBox.Text);
            //string[] items = searchBox.Text.Split(' ');
            //if (items[0] != "")
            //{
            //    _presenter.searchString(items, searchBox.Text);
            //}
        }

        private void searchBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 32) // space pressed
            {
                // TODO make current selected value be picked
                //searchBox.Text = searchBox.
            }
            else if (e.KeyValue != 8) //don't do anything for backspace(8)
            {
                _presenter.searchString(searchBox.Text);
            }
        }
        public void searchBoxSuggestions(object list, string currentText)
        {
            searchBox.DataSource = list;
            searchBox.Text=currentText;
            searchBox.DroppedDown = true;
            searchBox.SelectionStart = currentText.Length;
        }

        private void SelectVersesButton_Click(object sender, EventArgs e)
        {
            _selectVerse.resetForm();
            _presenter.Settings.VerseSelecterEnabled = true;
        }

        private void SendToWebViewtoolStripButton_Click(object sender, EventArgs e)
        {
            PresenterFolder.ReferenceList refList = new PresenterFolder.ReferenceList();
            foreach (PresenterFolder.ReferenceItemWrapper item in verseListBox.Items)
            {
                refList.addReferenceItem(item.getItem);
            }
            _presenter.displayVersesToWebView(refList, "");
        }

        private void checkedVerses_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckedListBox clb = (CheckedListBox)sender;
            int index = clb.SelectedIndex;

            // When you remove an item from the Items collection, it fires the SelectedIndexChanged
            // event again, with SelectedIndex = -1.  Hence the check for index != -1 first, 
            // to prevent an invalid selectedindex error
            if (index != -1 && clb.GetItemCheckState(index) == CheckState.Unchecked)
            {
                clb.Items.RemoveAt(index);
            }
        }
        private void buttonCopySelectedVersesToClipboard_Click(object sender, EventArgs e)
        {
            PresenterFolder.ReferenceList refList = new PresenterFolder.ReferenceList();
            foreach (PresenterFolder.ReferenceItemWrapper item in verseListBox.Items)
            {
                refList.addReferenceItem(item.getItem);
            }
            if (refList.getList.Count > 0)
            {
                _presenter.putVersesToClipBoard(refList);
            }
        }
        private void toolStripQuickPrint_Click(object sender, EventArgs e)
        {
            PresenterFolder.ReferenceList refList = new PresenterFolder.ReferenceList();
            foreach (PresenterFolder.ReferenceItemWrapper item in verseListBox.Items)
            {
                refList.addReferenceItem(item.getItem);
            }
            if (refList.getList.Count > 0)
            {
                _presenter.Backend.PrintText(_presenter.putVersesForPlainText(refList));
            }
        }
        private void bibleToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            ToolStripMenuItem[] menuItems = new ToolStripMenuItem[_presenter.Settings.Bibles.Count];
            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItems[i] = new ToolStripMenuItem();
                menuItems[i].Name = "Bible:"+i.ToString();
                menuItems[i].Tag = _presenter.Settings.Bibles[i];
                menuItems[i].Text = _presenter.Settings.Bibles[i]._bibleVersion;
                menuItems[i].Click += new EventHandler(BibleMenuItemClickHandler);

                if (_presenter.Settings.CurrentBible._bibleVersion == 
                    _presenter.Settings.Bibles[i]._bibleVersion)
                {
                    menuItems[i].Checked = true;
                }
            }
            bibleToolStripMenuItem.DropDownItems.Clear();
            bibleToolStripMenuItem.DropDownItems.AddRange(menuItems);

            ToolStripMenuItem AddBibleToolStripItem = new ToolStripMenuItem("Add Bible");
            AddBibleToolStripItem.Click += new EventHandler(BibleMenuItemClickHandler);
            bibleToolStripMenuItem.DropDownItems.Add(AddBibleToolStripItem);
        }
        private void BibleMenuItemClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;

            if(clickedItem.Name.Contains("Bible:")) // check for a bible item
            {
                _presenter.Settings.CurrentBible.indexBuilderWorker.RunWorkerCompleted -= indexBuilderWorker_RunWorkerCompleted; // remove old event handler for re-enableing search box
                
                _presenter.Settings.CurrentBible = (bible_data.bible)clickedItem.Tag; // set bible a current bible

                //check to see if bible index is still being built
                if (_presenter.Settings.CurrentBible.indexBuilderWorker.IsBusy)
                {
                    searchBox.Enabled = false;

                    //add event to re-enable searchBox
                    _presenter.Settings.CurrentBible.indexBuilderWorker.RunWorkerCompleted +=
                        new RunWorkerCompletedEventHandler(indexBuilderWorker_RunWorkerCompleted);
                }
            }
            else if (clickedItem.Text == "Add Bible")
            {
                if (openFileDialog.ShowDialog() != DialogResult.Cancel)
                {
                    _presenter.Settings.addBible(openFileDialog.FileName);
                }   
            }
        }

        private void menuFontSize_TextChanged(object sender, EventArgs e)
        {
            ToolStripTextBox textBox = (ToolStripTextBox)sender;
            try
            {
                _presenter.Settings.PrinterFont = new Font(
                    _presenter.Settings.PrinterFont.FontFamily.Name, 
                    Convert.ToInt16(textBox.Text));
            }
            catch
            {
                if (textBox.Text != "")
                {
                    textBox.Text = Convert.ToString(_presenter.Backend._settings.PrinterFont.Size);
                }
            }
        }
        private List<FontFamily> getAvalibleFonts()
        {
            if (_avalibleFonts == null)
            {
                FontFamily[] allFonts = new InstalledFontCollection().Families;
                //weed out all the weird fonts that don't work with normal settings
                List<FontFamily> normalFonts = new List<FontFamily>();
                foreach (FontFamily font in allFonts)
                {
                    try
                    {
                        new Font(font, 12);
                        normalFonts.Add(font);
                    }
                    catch { }
                }
                _avalibleFonts = normalFonts;
            }
            return _avalibleFonts;
        }
        private void fontFamilyToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            List<FontFamily> allFonts = getAvalibleFonts();

            ToolStripMenuItem[] menuItems = new ToolStripMenuItem[allFonts.Count];

            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItems[i] = new ToolStripMenuItem();
                menuItems[i].Name = "Font:" + i.ToString();
                menuItems[i].Tag = allFonts[i];
                menuItems[i].Text = allFonts[i].Name;
                menuItems[i].Font = new Font(allFonts[i], 12);
                menuItems[i].Click += new EventHandler(fontFamilyToolStripMenuItem_ClickHandler);

                if (_presenter.Settings.PrinterFont.Name == allFonts[i].Name)
                {
                    menuItems[i].Checked = true;
                }
            }
            fontFamilyToolStripMenuItem.DropDownItems.Clear();
            fontFamilyToolStripMenuItem.DropDownItems.AddRange(menuItems);
        }
        private void fontFamilyToolStripMenuItem_ClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            FontFamily clickedFont = (FontFamily)clickedItem.Tag;
            _presenter.Settings.PrinterFont = new Font(clickedFont.Name,
                _presenter.Settings.PrinterFont.Size);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripQuickPrint_Click(sender, e);
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PresenterFolder.ReferenceList refList = new PresenterFolder.ReferenceList();
            foreach (PresenterFolder.ReferenceItemWrapper item in verseListBox.Items)
            {
                refList.addReferenceItem(item.getItem);
            }
            
            if (refList.getList.Count > 0)
            {
                _presenter.Backend.printPreview(_presenter.putVersesForPlainText(refList));
            }
        }
        private PresenterFolder.ReferenceList getCurrentVerseList()
        {
            PresenterFolder.ReferenceList refList = new PresenterFolder.ReferenceList();
            foreach (PresenterFolder.ReferenceItemWrapper item in verseListBox.Items)
            {
                refList.addReferenceItem(item.getItem);
            }
            return refList;
        }
        private void printSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _presenter.Backend.printSetup(sender, e);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Close();
            Application.Exit();
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            _presenter.saveSettings();

            if (MakeMP3backgroundWorker.IsBusy)
            {
                DialogResult StopMP3_Generating = MessageBox.Show("A MP3 File is currently being generated. Do you want to cancel the generation and exit?",
                    "Still Generating MP3 File!",
                    MessageBoxButtons.YesNo);
                switch (StopMP3_Generating)
                {
                    case DialogResult.No:
                        {
                            MakeMP3backgroundWorker.CancelAsync();
                            e.Cancel = true;
                            break;
                        }
                    case DialogResult.Yes:
                        {
                            break;
                        }
                }
            }
        }

        private void fontSizeToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            menuFontSize.Text = Convert.ToString(_presenter.Settings.PrinterFont.Size);
        }

        private void SavetoolStripButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                _presenter.saveVerseListToFile(getCurrentVerseList(),
                    saveFileDialog.FileName); 
            }  
        }

        private void saveVerseListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                _presenter.saveVerseListToFile(getCurrentVerseList(),
                    saveFileDialog.FileName);
            } 
        }

        private void loadVerseListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                _presenter.setNewVerseList( _presenter.getSavedVerseListFromFile(
                    openFileDialog.FileName));
            }
        }

        private void saveListToMP3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveToMP3Dialog.ShowDialog() != DialogResult.Cancel)
            {
                MakeMP3backgroundWorker.RunWorkerAsync(saveToMP3Dialog.FileName);
                //_presenter.putVersesToAudioMP3(saveFileDialog.FileName, getCurrentVerseList());
            }
        }

        private void MakeMP3backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (MainprogressBar.InvokeRequired)
            {
                MainprogressBar.Invoke(new MethodInvoker(delegate
                {
                    MainprogressBar.Value = e.ProgressPercentage;
                }));
            }
            else
            {
                MainprogressBar.Value = e.ProgressPercentage;
            }
        }

        private void MakeMP3backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (MainprogressBar.InvokeRequired)
            {
                MainprogressBar.Invoke(new MethodInvoker(delegate
                    {
                        MainprogressBar.Visible = true;
                        MainprogressBar.Value = 0;
                        saveListToMP3ToolStripMenuItem.Enabled = false;
                    }));
            }

            string fileName = (string) e.Argument;
            PresenterFolder.ReferenceList tempRefList = getCurrentVerseList();
            AudioFileMaker.MakeFileFromText(fileName, _presenter.putVersesToStringForTTS( tempRefList),
                _presenter.Settings.CurrentTTSVoice.VoiceInfo.Name, _presenter.Settings.TTS_Rate, (BackgroundWorker) sender);
        }

        private void MakeMP3backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
                MainprogressBar.Visible = false;
                MainprogressBar.Value = 0;
                saveListToMP3ToolStripMenuItem.Enabled = true;
        }

        private void saveToMP3OptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            To_MP3_Options mp3Options = new To_MP3_Options(_presenter);
            mp3Options.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }
    }
}
