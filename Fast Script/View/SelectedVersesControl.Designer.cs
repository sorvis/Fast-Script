namespace Fast_Script.View
{
    partial class SelectedVersesControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectedVersesControl));
            this._checkedVersesCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this._mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this._toolStrip = new System.Windows.Forms.ToolStrip();
            this._moveUptoolStripButton = new System.Windows.Forms.ToolStripButton();
            this._moveDownToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._mainTableLayoutPanel.SuspendLayout();
            this._toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _checkedVersesCheckedListBox
            // 
            this._checkedVersesCheckedListBox.AllowDrop = true;
            this._checkedVersesCheckedListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._checkedVersesCheckedListBox.FormattingEnabled = true;
            this._checkedVersesCheckedListBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this._checkedVersesCheckedListBox.Location = new System.Drawing.Point(3, 28);
            this._checkedVersesCheckedListBox.Name = "_checkedVersesCheckedListBox";
            this._checkedVersesCheckedListBox.Size = new System.Drawing.Size(359, 447);
            this._checkedVersesCheckedListBox.TabIndex = 1;
            this._checkedVersesCheckedListBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.checkedVersesCheckedListBox_DragDrop);
            this._checkedVersesCheckedListBox.DragOver += new System.Windows.Forms.DragEventHandler(this.checkedVersesCheckedListBox_DragOver);
            this._checkedVersesCheckedListBox.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.checkedVersesCheckedListBox_GiveFeedback);
            this._checkedVersesCheckedListBox.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.checkedVersesCheckedListBox_QueryContinueDrag);
            this._checkedVersesCheckedListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.checkedVersesCheckedListBox_MouseDown);
            // 
            // _mainTableLayoutPanel
            // 
            this._mainTableLayoutPanel.ColumnCount = 1;
            this._mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._mainTableLayoutPanel.Controls.Add(this._checkedVersesCheckedListBox, 0, 1);
            this._mainTableLayoutPanel.Controls.Add(this._toolStrip, 0, 0);
            this._mainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this._mainTableLayoutPanel.Name = "_mainTableLayoutPanel";
            this._mainTableLayoutPanel.RowCount = 2;
            this._mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._mainTableLayoutPanel.Size = new System.Drawing.Size(365, 478);
            this._mainTableLayoutPanel.TabIndex = 2;
            // 
            // _toolStrip
            // 
            this._toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._moveUptoolStripButton,
            this._moveDownToolStripButton});
            this._toolStrip.Location = new System.Drawing.Point(0, 0);
            this._toolStrip.Name = "_toolStrip";
            this._toolStrip.Size = new System.Drawing.Size(365, 25);
            this._toolStrip.TabIndex = 2;
            this._toolStrip.Text = "toolStrip1";
            // 
            // _moveUptoolStripButton
            // 
            this._moveUptoolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_moveUptoolStripButton.Image")));
            this._moveUptoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._moveUptoolStripButton.Name = "_moveUptoolStripButton";
            this._moveUptoolStripButton.Size = new System.Drawing.Size(75, 22);
            this._moveUptoolStripButton.Text = "Move Up";
            this._moveUptoolStripButton.Click += new System.EventHandler(this.moveUptoolStripButton_Click);
            // 
            // _moveDownToolStripButton
            // 
            this._moveDownToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_moveDownToolStripButton.Image")));
            this._moveDownToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._moveDownToolStripButton.Name = "_moveDownToolStripButton";
            this._moveDownToolStripButton.Size = new System.Drawing.Size(91, 22);
            this._moveDownToolStripButton.Text = "Move Down";
            this._moveDownToolStripButton.Click += new System.EventHandler(this.moveDownToolStripButton_Click);
            // 
            // SelectedVersesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this._mainTableLayoutPanel);
            this.Name = "SelectedVersesControl";
            this.Size = new System.Drawing.Size(365, 478);
            this._mainTableLayoutPanel.ResumeLayout(false);
            this._mainTableLayoutPanel.PerformLayout();
            this._toolStrip.ResumeLayout(false);
            this._toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox _checkedVersesCheckedListBox;
        private System.Windows.Forms.TableLayoutPanel _mainTableLayoutPanel;
        private System.Windows.Forms.ToolStrip _toolStrip;
        private System.Windows.Forms.ToolStripButton _moveUptoolStripButton;
        private System.Windows.Forms.ToolStripButton _moveDownToolStripButton;
    }
}
