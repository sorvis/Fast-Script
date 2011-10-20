namespace Fast_Script
{
    partial class VerseSelecter
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
            this.verseSelecterListBox = new System.Windows.Forms.ListBox();
            this.AddButton = new System.Windows.Forms.Button();
            this.DoneButton = new System.Windows.Forms.Button();
            this.ReferenceDisplayLabel = new System.Windows.Forms.Label();
            this.ClearButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // verseSelecterListBox
            // 
            this.verseSelecterListBox.ColumnWidth = 90;
            this.verseSelecterListBox.FormattingEnabled = true;
            this.verseSelecterListBox.Location = new System.Drawing.Point(3, 3);
            this.verseSelecterListBox.MultiColumn = true;
            this.verseSelecterListBox.Name = "verseSelecterListBox";
            this.verseSelecterListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.verseSelecterListBox.Size = new System.Drawing.Size(434, 225);
            this.verseSelecterListBox.TabIndex = 0;
            this.verseSelecterListBox.SelectedIndexChanged += new System.EventHandler(this.verseSelecterListBox_SelectedIndexChanged);
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(362, 234);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(75, 23);
            this.AddButton.TabIndex = 2;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // DoneButton
            // 
            this.DoneButton.Location = new System.Drawing.Point(281, 234);
            this.DoneButton.Name = "DoneButton";
            this.DoneButton.Size = new System.Drawing.Size(75, 23);
            this.DoneButton.TabIndex = 3;
            this.DoneButton.Text = "Done";
            this.DoneButton.UseVisualStyleBackColor = true;
            this.DoneButton.Click += new System.EventHandler(this.DoneButton_Click);
            // 
            // ReferenceDisplayLabel
            // 
            this.ReferenceDisplayLabel.AutoSize = true;
            this.ReferenceDisplayLabel.Location = new System.Drawing.Point(3, 234);
            this.ReferenceDisplayLabel.Name = "ReferenceDisplayLabel";
            this.ReferenceDisplayLabel.Size = new System.Drawing.Size(0, 13);
            this.ReferenceDisplayLabel.TabIndex = 4;
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(200, 234);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(75, 23);
            this.ClearButton.TabIndex = 5;
            this.ClearButton.Text = "Clear";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // VerseSelecter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.ReferenceDisplayLabel);
            this.Controls.Add(this.DoneButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.verseSelecterListBox);
            this.Location = new System.Drawing.Point(230, 20);
            this.Name = "VerseSelecter";
            this.Size = new System.Drawing.Size(440, 267);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox verseSelecterListBox;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button DoneButton;
        private System.Windows.Forms.Label ReferenceDisplayLabel;
        private System.Windows.Forms.Button ClearButton;

    }
}
