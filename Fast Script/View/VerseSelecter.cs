using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fast_Script
{
    public partial class VerseSelecter : UserControl
    {
        private const string BOOK = "book";
        private const string CHAPTER = "chapter";
        private const string VERSE = "verse";
        private BackEndInitializer _backend;
        private Presenter _presenter;
        private PresenterFolder.ReferenceItem _verseReference;
        private string _displaying; // current type displayed: book, chapter, verse
        public VerseSelecter(Presenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _backend = _presenter.Backend;
            _verseReference = new PresenterFolder.ReferenceItem();
        }
        public void ResetForm()
        {
            Visible = true;
            this.Enabled = true;
            _verseSelecterListBox.Items.Clear();
            _verseSelecterListBox.Items.AddRange(_backend.CurrentBooks);
            _verseReference = new PresenterFolder.ReferenceItem();
            _displaying = BOOK;
            ReferenceDisplayLabel.Text = "";
        }
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            this.Enabled = false;
        }
        private void AddButton_Click(object sender, EventArgs e)
        {
            AddToList(_verseReference);
            ResetForm();
        }
        private void AddToList(PresenterFolder.ReferenceItem item)
        {
            _presenter.addToVerseList(_verseReference);
        }
        private string[] intToString(int[] number)
        {
            return number.Select(x => Convert.ToString(x)).ToArray();
        }

        private void readFromVerseSelecter(Action<string> setStart, Action<string> setEnd)
        {
            var start = (string)_verseSelecterListBox.SelectedItems[0];
            setStart(start);

            var count = _verseSelecterListBox.SelectedItems.Count;
            var end = (string)_verseSelecterListBox.SelectedItems[count -1];
            setEnd(end);
        }

        private void verseSelecterListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MouseButtons != MouseButtons.Left) return;
            ListBox.SelectedObjectCollection selectedItems =
                   _verseSelecterListBox.SelectedItems;

            if (selectedItems.Count > 1)
            {
                _verseReference.Range = true;
                switch (_displaying)
                {
                    case BOOK:
                        readFromVerseSelecter(x => _verseReference.StartBook = x, x => _verseReference.EndBook = x);
                        break;
                    case CHAPTER:
                        readFromVerseSelecter(x => _verseReference.StartChapter = Convert.ToInt32(x), x => _verseReference.EndChapter = Convert.ToInt32(x));
                        break;
                    case VERSE:
						readFromVerseSelecter(x => _verseReference.StartVerse = Convert.ToInt32(x), x => _verseReference.EndVerse = Convert.ToInt32(x));
                        break;
                }

                AddToList(_verseReference);
                ResetForm();
            }
            else if (selectedItems.Count == 1)
            {
                switch (_displaying)
                {
                    case BOOK:
                        string bookName = _verseSelecterListBox.Text;
                        string[] chapters = intToString(_backend.CurrentChapters(bookName));

                        // update saved verse reference
                        _verseReference.StartBook = bookName;

                        // update ListBox with chapters
                        _verseSelecterListBox.Items.Clear();
                        _verseSelecterListBox.Items.AddRange(chapters);
                        _displaying = CHAPTER;
                        break;
                    case CHAPTER:
                        int chapterNumber = Convert.ToInt32(_verseSelecterListBox.Text);
                        int numberOfVerses = _backend.CurrentVerses(_verseReference.StartBook, chapterNumber).Count;
                        string[] verses = new string[numberOfVerses];
                        for (int i = 0; i < numberOfVerses; i++)
                        {
                            verses[i] = Convert.ToString(i + 1);
                        }

                        // update saved reference
                        _verseReference.StartChapter = chapterNumber;

                        // update ListBox with verses
                        _verseSelecterListBox.Items.Clear();
                        _verseSelecterListBox.Items.AddRange(verses);
                        _displaying = VERSE;
                        break;
                    case VERSE: // only one selected
                        _verseReference.StartVerse = Convert.ToInt32(_verseSelecterListBox.
                            SelectedItems[0]);

                        //execute add verse
                        AddToList(_verseReference);
                        ResetForm();
                        break;
                }
            }
            // update reference display
            ReferenceDisplayLabel.Text = _verseReference.ToString();
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
            _presenter.Settings.VerseSelecterEnabled = false;
            this.Visible = false;
            this.Enabled = false;
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            ResetForm();
        }
    }
}
