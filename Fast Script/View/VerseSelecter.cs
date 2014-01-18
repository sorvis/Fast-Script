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
        private BackEndInitializer _backend;
        private Presenter _presenter;
        private PresenterFolder.ReferenceItem _verseReference;
        private string displaying; // current type displayed: book, chapter, verse
        public VerseSelecter(Presenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _backend = _presenter.Backend;
            _verseReference = new PresenterFolder.ReferenceItem();
        }
        public void resetForm()
        {
            this.Visible = true;
            this.Enabled = true;
            verseSelecterListBox.Items.Clear();
            verseSelecterListBox.Items.AddRange(_backend.CurrentBooks);
            _verseReference = new PresenterFolder.ReferenceItem();
            displaying = "book";
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
            resetForm();
        }
        private void AddToList(PresenterFolder.ReferenceItem item)
        {
            _presenter.addToVerseList(_verseReference);
        }
        private string[] intToString(int[] number)
        {
            string[] tempArray = new string[number.Count()];
            for (int i = 0; i < number.Count(); i++)
            {
                tempArray[i] = Convert.ToString(number[i]);
            }
            return tempArray;
        }
        private void verseSelecterListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox.SelectedObjectCollection selectedItems =
                   verseSelecterListBox.SelectedItems;

            if (selectedItems.Count > 1)
            {
                _verseReference.Range = true;
                switch (displaying)
                {
                    case "book":
                        _verseReference.StartBook = (string)verseSelecterListBox.
                            SelectedItems[0];
                        _verseReference.EndBook = (string)verseSelecterListBox.
                            SelectedItems[selectedItems.Count-1];
                        break;
                    case "chapter":
                        _verseReference.StartChapter = Convert.ToInt32((string)verseSelecterListBox.
                            SelectedItems[0]);
                        _verseReference.EndChapter = Convert.ToInt32((string)verseSelecterListBox.
                            SelectedItems[selectedItems.Count-1]);
                        break;
                    case "verse":
                        _verseReference.StartVerse = Convert.ToInt32((string)verseSelecterListBox.
                            SelectedItems[0]);
                        _verseReference.EndVerse = Convert.ToInt32((string)verseSelecterListBox.
                            SelectedItems[selectedItems.Count-1]);
                        break;
                }

                AddToList(_verseReference);
                resetForm();
            }
            else if (selectedItems.Count == 1)
            {
                switch (displaying)
                {
                    case "book":
                        string bookName = verseSelecterListBox.Text;
                        string[] chapters = intToString(_backend.CurrentChapters(bookName));

                        // update saved verse reference
                        _verseReference.StartBook = bookName;

                        // update ListBox with chapters
                        verseSelecterListBox.Items.Clear();
                        verseSelecterListBox.Items.AddRange(chapters);
                        displaying = "chapter";
                        break;
                    case "chapter":
                        int chapterNumber = Convert.ToInt32(verseSelecterListBox.Text);
                        int numberOfVerses = _backend.CurrentVerses(_verseReference.StartBook, chapterNumber).Count;
                        string[] verses = new string[numberOfVerses];
                        for (int i = 0; i < numberOfVerses; i++)
                        {
                            verses[i] = Convert.ToString(i + 1);
                        }

                        // update saved reference
                        _verseReference.StartChapter = chapterNumber;

                        // update ListBox with verses
                        verseSelecterListBox.Items.Clear();
                        verseSelecterListBox.Items.AddRange(verses);
                        displaying = "verse";
                        break;
                    case "verse": // only one selected
                        _verseReference.StartVerse = Convert.ToInt32(verseSelecterListBox.
                            SelectedItems[0]);

                        //execute add verse
                        AddToList(_verseReference);
                        resetForm();
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
            resetForm();
        }
    }
}
