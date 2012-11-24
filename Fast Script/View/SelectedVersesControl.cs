using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
            if (index < 0) index = this._checkedVersesCheckedListBox.Items.Count - 1;
            this._checkedVersesCheckedListBox.Items.Remove(e.Data);
            this._checkedVersesCheckedListBox.Items.Insert(index, e.Data);
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

        #endregion Event_Handlers

        #region [--Private_Methods--]

        private void moveCheckedVersesItemOrder(object item, int newIndex)
        {
            if(item != null
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

    }
}
