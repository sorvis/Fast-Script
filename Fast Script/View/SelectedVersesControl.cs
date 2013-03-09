using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fast_Script.PresenterFolder;
using System.Runtime.InteropServices;

namespace Fast_Script.View
{
    public partial class SelectedVersesControl : UserControl
    {
        public SelectedVersesControl()
        {
            InitializeComponent();
        }

        #region [--Properties--]

        public CheckedListBox SelectedVersesCheckedListBox
        {
            get
            {
                return this._checkedVersesCheckedListBox;
            }
        }

        #endregion Properties

        #region [--Private_Methods--]

        private void moveCheckedVersesItemOrder(object item, int newIndex)
        {
            if (item != null
                && _checkedVersesCheckedListBox.Items.Contains(item)
                && _checkedVersesCheckedListBox.Items.Count > newIndex
                && newIndex >= 0)
            {
                bool itemIsChecked = _checkedVersesCheckedListBox.CheckedItems.Contains(item);
                _checkedVersesCheckedListBox.Items.Remove(item);
                _checkedVersesCheckedListBox.Items.Insert(newIndex, item);
                _checkedVersesCheckedListBox.SetItemChecked(newIndex, itemIsChecked);
                _checkedVersesCheckedListBox.SetSelected(newIndex, true);
            }
        }

        #endregion Private_Methods

        #region [--Event_Handlers--]

        private void checkedVersesCheckedListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (_checkedVersesCheckedListBox.SelectedItem != null)
            {
                _checkedVersesCheckedListBox.DoDragDrop(_checkedVersesCheckedListBox.SelectedItem, DragDropEffects.Move);
            }
        }

        private void checkedVersesCheckedListBox_DragDrop(object sender, DragEventArgs e)
        {
            Point point = _checkedVersesCheckedListBox.PointToClient(new Point(e.X, e.Y));
            int index = this._checkedVersesCheckedListBox.IndexFromPoint(point);
            moveCheckedVersesItemOrder(e.Data.GetData(typeof(ReferenceItemWrapper)), index);
        }

        private void checkedVersesCheckedListBox_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void moveUptoolStripButton_Click(object sender, EventArgs e)
        {
            moveCheckedVersesItemOrder(_checkedVersesCheckedListBox.SelectedItem, _checkedVersesCheckedListBox.SelectedIndex - 1);
        }

        private void moveDownToolStripButton_Click(object sender, EventArgs e)
        {
            moveCheckedVersesItemOrder(_checkedVersesCheckedListBox.SelectedItem, _checkedVersesCheckedListBox.SelectedIndex + 1);
        }

        private void checkedVersesCheckedListBox_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            if (sender != _checkedVersesCheckedListBox || e.EscapePressed)
            {
                e.Action = DragAction.Cancel;
            }
        }

        private void checkedVersesCheckedListBox_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            if (e.Effect == DragDropEffects.Move)
            {
                Cursor.Current = Cursors.Hand;
            }
            else
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void deleteVerseToolStripButton_Click(object sender, EventArgs e)
        {
            _checkedVersesCheckedListBox.Items.Remove(_checkedVersesCheckedListBox.SelectedItem);
        }

        private void checkedVersesCheckedListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                _checkedVersesCheckedListBox.Items.Remove(_checkedVersesCheckedListBox.SelectedItem);
            }
        }

        #endregion Event_Handlers

        private void _checkedVersesCheckedListBox_DragLeave(object sender, EventArgs e)
        {
            //DataObject dataObject = new DataObject();
            //dataObject.SetData(DataFormats.Serializable, textToPast);
            //dataObject.SetData(DataFormats.Text, textToPast);
            //this.ParentForm.DoDragDrop(dataObject, DragDropEffects.Copy);

            //DoDragDrop(new DataObject(DataFormats.Text, "test paste"), DragDropEffects.Copy);
            // Copy the text in the datafield to Clipboard
            
        }
    }
}