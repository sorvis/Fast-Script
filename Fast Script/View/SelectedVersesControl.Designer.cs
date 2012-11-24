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
            this._checkedVersesCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this._mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this._toolStrip = new System.Windows.Forms.ToolStrip();
            this._mainTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _checkedVersesCheckedListBox
            // 
            this._checkedVersesCheckedListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._checkedVersesCheckedListBox.FormattingEnabled = true;
            this._checkedVersesCheckedListBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this._checkedVersesCheckedListBox.Location = new System.Drawing.Point(3, 28);
            this._checkedVersesCheckedListBox.Name = "_checkedVersesCheckedListBox";
            this._checkedVersesCheckedListBox.Size = new System.Drawing.Size(359, 447);
            this._checkedVersesCheckedListBox.TabIndex = 1;
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
            this._toolStrip.Location = new System.Drawing.Point(0, 0);
            this._toolStrip.Name = "_toolStrip";
            this._toolStrip.Size = new System.Drawing.Size(365, 25);
            this._toolStrip.TabIndex = 2;
            this._toolStrip.Text = "toolStrip1";
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox _checkedVersesCheckedListBox;
        private System.Windows.Forms.TableLayoutPanel _mainTableLayoutPanel;
        private System.Windows.Forms.ToolStrip _toolStrip;
    }
}
