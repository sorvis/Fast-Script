namespace Fast_Script
{
    partial class MainWindow
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.MainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveListToMP3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveVerseListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._openVerseListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fontFamilyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFontFamily = new System.Windows.Forms.ToolStripComboBox();
            this.fontSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFontSize = new System.Windows.Forms.ToolStripTextBox();
            this.saveToMP3OptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bibleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._selectedVersesGroupBox = new System.Windows.Forms.GroupBox();
            this._selectedVerseOperationToolStrip = new System.Windows.Forms.ToolStrip();
            this.SavetoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.buttonCopySelectedVersesToClipboard = new System.Windows.Forms.ToolStripButton();
            this._pasteToProgramToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.SelectVersesButton = new System.Windows.Forms.ToolStripButton();
            this.SendToWebViewtoolStripButton = new System.Windows.Forms.ToolStripButton();
            this._selectedVersesControl = new Fast_Script.View.SelectedVersesControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.searchBox = new System.Windows.Forms.ComboBox();
            this.webResualts = new System.Windows.Forms.WebBrowser();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.MakeMP3backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.MainprogressBar = new System.Windows.Forms.ProgressBar();
            this.saveToMP3Dialog = new System.Windows.Forms.SaveFileDialog();
            this._copyReferencesToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.MainMenuStrip.SuspendLayout();
            this._selectedVersesGroupBox.SuspendLayout();
            this._selectedVerseOperationToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenuStrip
            // 
            this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.bibleToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.MainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MainMenuStrip.Name = "MainMenuStrip";
            this.MainMenuStrip.Size = new System.Drawing.Size(698, 24);
            this.MainMenuStrip.TabIndex = 1;
            this.MainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printToolStripMenuItem,
            this.printPreviewToolStripMenuItem,
            this.printSetupToolStripMenuItem,
            this.saveListToMP3ToolStripMenuItem,
            this.saveVerseListToolStripMenuItem,
            this._openVerseListToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.printToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.printToolStripMenuItem.Text = "Print";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // printPreviewToolStripMenuItem
            // 
            this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.printPreviewToolStripMenuItem.Text = "Print Preview";
            this.printPreviewToolStripMenuItem.Click += new System.EventHandler(this.printPreviewToolStripMenuItem_Click);
            // 
            // printSetupToolStripMenuItem
            // 
            this.printSetupToolStripMenuItem.Name = "printSetupToolStripMenuItem";
            this.printSetupToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.printSetupToolStripMenuItem.Text = "Print Setup";
            this.printSetupToolStripMenuItem.Click += new System.EventHandler(this.printSetupToolStripMenuItem_Click);
            // 
            // saveListToMP3ToolStripMenuItem
            // 
            this.saveListToMP3ToolStripMenuItem.Name = "saveListToMP3ToolStripMenuItem";
            this.saveListToMP3ToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.saveListToMP3ToolStripMenuItem.Text = "Save List To MP3";
            this.saveListToMP3ToolStripMenuItem.Click += new System.EventHandler(this.saveListToMP3ToolStripMenuItem_Click);
            // 
            // saveVerseListToolStripMenuItem
            // 
            this.saveVerseListToolStripMenuItem.Name = "saveVerseListToolStripMenuItem";
            this.saveVerseListToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveVerseListToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.saveVerseListToolStripMenuItem.Text = "Save Verse List";
            this.saveVerseListToolStripMenuItem.Click += new System.EventHandler(this.saveVerseListToolStripMenuItem_Click);
            // 
            // _openVerseListToolStripMenuItem
            // 
            this._openVerseListToolStripMenuItem.Name = "_openVerseListToolStripMenuItem";
            this._openVerseListToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this._openVerseListToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this._openVerseListToolStripMenuItem.Text = "Open Verse List";
            this._openVerseListToolStripMenuItem.Click += new System.EventHandler(this.loadVerseListToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printOptionsToolStripMenuItem,
            this.saveToMP3OptionsToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // printOptionsToolStripMenuItem
            // 
            this.printOptionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fontFamilyToolStripMenuItem,
            this.fontSizeToolStripMenuItem});
            this.printOptionsToolStripMenuItem.Name = "printOptionsToolStripMenuItem";
            this.printOptionsToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.printOptionsToolStripMenuItem.Text = "Verse Printing Options";
            // 
            // fontFamilyToolStripMenuItem
            // 
            this.fontFamilyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFontFamily});
            this.fontFamilyToolStripMenuItem.Name = "fontFamilyToolStripMenuItem";
            this.fontFamilyToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.fontFamilyToolStripMenuItem.Text = "Font Family";
            this.fontFamilyToolStripMenuItem.DropDownOpening += new System.EventHandler(this.fontFamilyToolStripMenuItem_DropDownOpening);
            // 
            // menuFontFamily
            // 
            this.menuFontFamily.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.menuFontFamily.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.menuFontFamily.MaxDropDownItems = 20;
            this.menuFontFamily.Name = "menuFontFamily";
            this.menuFontFamily.Size = new System.Drawing.Size(121, 23);
            this.menuFontFamily.Sorted = true;
            // 
            // fontSizeToolStripMenuItem
            // 
            this.fontSizeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFontSize});
            this.fontSizeToolStripMenuItem.Name = "fontSizeToolStripMenuItem";
            this.fontSizeToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.fontSizeToolStripMenuItem.Text = "Font Size";
            this.fontSizeToolStripMenuItem.DropDownOpening += new System.EventHandler(this.fontSizeToolStripMenuItem_DropDownOpening);
            // 
            // menuFontSize
            // 
            this.menuFontSize.Name = "menuFontSize";
            this.menuFontSize.Size = new System.Drawing.Size(100, 23);
            this.menuFontSize.Text = "12";
            this.menuFontSize.TextChanged += new System.EventHandler(this.menuFontSize_TextChanged);
            // 
            // saveToMP3OptionsToolStripMenuItem
            // 
            this.saveToMP3OptionsToolStripMenuItem.Name = "saveToMP3OptionsToolStripMenuItem";
            this.saveToMP3OptionsToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.saveToMP3OptionsToolStripMenuItem.Text = "Save To MP3 Options";
            this.saveToMP3OptionsToolStripMenuItem.Click += new System.EventHandler(this.saveToMP3OptionsToolStripMenuItem_Click);
            // 
            // bibleToolStripMenuItem
            // 
            this.bibleToolStripMenuItem.Name = "bibleToolStripMenuItem";
            this.bibleToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.bibleToolStripMenuItem.Text = "Bible";
            this.bibleToolStripMenuItem.DropDownOpening += new System.EventHandler(this.bibleToolStripMenuItem_DropDownOpening);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // _selectedVersesGroupBox
            // 
            this._selectedVersesGroupBox.Controls.Add(this._selectedVerseOperationToolStrip);
            this._selectedVersesGroupBox.Controls.Add(this._selectedVersesControl);
            this._selectedVersesGroupBox.Controls.Add(this.panel1);
            this._selectedVersesGroupBox.Location = new System.Drawing.Point(13, 27);
            this._selectedVersesGroupBox.Name = "_selectedVersesGroupBox";
            this._selectedVersesGroupBox.Size = new System.Drawing.Size(214, 526);
            this._selectedVersesGroupBox.TabIndex = 2;
            this._selectedVersesGroupBox.TabStop = false;
            this._selectedVersesGroupBox.Text = "Selected Verses";
            // 
            // _selectedVerseOperationToolStrip
            // 
            this._selectedVerseOperationToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this._selectedVerseOperationToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SavetoolStripButton,
            this.buttonCopySelectedVersesToClipboard,
            this._pasteToProgramToolStripButton,
            this.SelectVersesButton,
            this.SendToWebViewtoolStripButton,
            this._copyReferencesToolStripButton});
            this._selectedVerseOperationToolStrip.Location = new System.Drawing.Point(6, 460);
            this._selectedVerseOperationToolStrip.Name = "_selectedVerseOperationToolStrip";
            this._selectedVerseOperationToolStrip.Size = new System.Drawing.Size(181, 25);
            this._selectedVerseOperationToolStrip.TabIndex = 0;
            this._selectedVerseOperationToolStrip.Text = "toolStrip2";
            // 
            // SavetoolStripButton
            // 
            this.SavetoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SavetoolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("SavetoolStripButton.Image")));
            this.SavetoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SavetoolStripButton.Name = "SavetoolStripButton";
            this.SavetoolStripButton.Size = new System.Drawing.Size(23, 22);
            this.SavetoolStripButton.Text = "Save list to file.";
            this.SavetoolStripButton.Click += new System.EventHandler(this.SavetoolStripButton_Click);
            // 
            // buttonCopySelectedVersesToClipboard
            // 
            this.buttonCopySelectedVersesToClipboard.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonCopySelectedVersesToClipboard.Image = ((System.Drawing.Image)(resources.GetObject("buttonCopySelectedVersesToClipboard.Image")));
            this.buttonCopySelectedVersesToClipboard.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonCopySelectedVersesToClipboard.Name = "buttonCopySelectedVersesToClipboard";
            this.buttonCopySelectedVersesToClipboard.Size = new System.Drawing.Size(23, 22);
            this.buttonCopySelectedVersesToClipboard.Text = "Copy Selected Verse Text To Clipboard";
            this.buttonCopySelectedVersesToClipboard.Click += new System.EventHandler(this.buttonCopySelectedVersesToClipboard_Click);
            // 
            // _pasteToProgramToolStripButton
            // 
            this._pasteToProgramToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._pasteToProgramToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_pasteToProgramToolStripButton.Image")));
            this._pasteToProgramToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._pasteToProgramToolStripButton.Name = "_pasteToProgramToolStripButton";
            this._pasteToProgramToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._pasteToProgramToolStripButton.Text = "Paste To Program";
            this._pasteToProgramToolStripButton.ToolTipText = "Pastes to last open program";
            this._pasteToProgramToolStripButton.Click += new System.EventHandler(this.pasteToProgramToolStripButton_Click);
            // 
            // SelectVersesButton
            // 
            this.SelectVersesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SelectVersesButton.Image = ((System.Drawing.Image)(resources.GetObject("SelectVersesButton.Image")));
            this.SelectVersesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SelectVersesButton.Name = "SelectVersesButton";
            this.SelectVersesButton.Size = new System.Drawing.Size(23, 22);
            this.SelectVersesButton.Text = "Manually Select Verses";
            this.SelectVersesButton.Click += new System.EventHandler(this.SelectVersesButton_Click);
            // 
            // SendToWebViewtoolStripButton
            // 
            this.SendToWebViewtoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SendToWebViewtoolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("SendToWebViewtoolStripButton.Image")));
            this.SendToWebViewtoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SendToWebViewtoolStripButton.Name = "SendToWebViewtoolStripButton";
            this.SendToWebViewtoolStripButton.Size = new System.Drawing.Size(23, 22);
            this.SendToWebViewtoolStripButton.Text = "Display Verses";
            this.SendToWebViewtoolStripButton.Click += new System.EventHandler(this.SendToWebViewtoolStripButton_Click);
            // 
            // _selectedVersesControl
            // 
            this._selectedVersesControl.AutoSize = true;
            this._selectedVersesControl.IsDirty = false;
            this._selectedVersesControl.Location = new System.Drawing.Point(6, 19);
            this._selectedVersesControl.Name = "_selectedVersesControl";
            this._selectedVersesControl.Size = new System.Drawing.Size(202, 448);
            this._selectedVersesControl.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(3, 473);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(122, 24);
            this.panel1.TabIndex = 1;
            // 
            // searchBox
            // 
            this.searchBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.searchBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.searchBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.searchBox.FormattingEnabled = true;
            this.searchBox.Location = new System.Drawing.Point(233, 27);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(453, 21);
            this.searchBox.TabIndex = 3;
            this.searchBox.Enter += new System.EventHandler(this.searchBox_Enter);
            this.searchBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.searchBox_KeyUp);
            // 
            // webResualts
            // 
            this.webResualts.Location = new System.Drawing.Point(233, 54);
            this.webResualts.MinimumSize = new System.Drawing.Size(20, 20);
            this.webResualts.Name = "webResualts";
            this.webResualts.Size = new System.Drawing.Size(453, 458);
            this.webResualts.TabIndex = 4;
            this.webResualts.Url = new System.Uri("", System.UriKind.Relative);
            this.webResualts.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webResualts_Navigating);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // MakeMP3backgroundWorker
            // 
            this.MakeMP3backgroundWorker.WorkerReportsProgress = true;
            this.MakeMP3backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.MakeMP3backgroundWorker_DoWork);
            this.MakeMP3backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.MakeMP3backgroundWorker_ProgressChanged);
            this.MakeMP3backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.MakeMP3backgroundWorker_RunWorkerCompleted);
            // 
            // MainprogressBar
            // 
            this.MainprogressBar.Location = new System.Drawing.Point(233, 526);
            this.MainprogressBar.Name = "MainprogressBar";
            this.MainprogressBar.Size = new System.Drawing.Size(453, 23);
            this.MainprogressBar.TabIndex = 5;
            this.MainprogressBar.Visible = false;
            // 
            // saveToMP3Dialog
            // 
            this.saveToMP3Dialog.DefaultExt = "mp3";
            this.saveToMP3Dialog.Filter = "MP3 Audio|*.mp3|All files|*.*";
            // 
            // _copyReferencesToolStripButton
            // 
            this._copyReferencesToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._copyReferencesToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_copyReferencesToolStripButton.Image")));
            this._copyReferencesToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._copyReferencesToolStripButton.Name = "_copyReferencesToolStripButton";
            this._copyReferencesToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._copyReferencesToolStripButton.Text = "Copy References To Clip Board";
            this._copyReferencesToolStripButton.Click += new System.EventHandler(this.copyReferencesToolStripButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 565);
            this.Controls.Add(this.MainprogressBar);
            this.Controls.Add(this.webResualts);
            this.Controls.Add(this.searchBox);
            this.Controls.Add(this._selectedVersesGroupBox);
            this.Controls.Add(this.MainMenuStrip);
            this.Name = "MainWindow";
            this.Text = "Fast Script";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.MainMenuStrip.ResumeLayout(false);
            this.MainMenuStrip.PerformLayout();
            this._selectedVersesGroupBox.ResumeLayout(false);
            this._selectedVersesGroupBox.PerformLayout();
            this._selectedVerseOperationToolStrip.ResumeLayout(false);
            this._selectedVerseOperationToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printSetupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fontFamilyToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox menuFontFamily;
        private System.Windows.Forms.ToolStripMenuItem fontSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox menuFontSize;
        private System.Windows.Forms.GroupBox _selectedVersesGroupBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip _selectedVerseOperationToolStrip;
        private System.Windows.Forms.ToolStripButton buttonCopySelectedVersesToClipboard;
        private System.Windows.Forms.ComboBox searchBox;
        private System.Windows.Forms.WebBrowser webResualts;
        private System.Windows.Forms.ToolStripButton SelectVersesButton;
        private System.Windows.Forms.ToolStripButton SendToWebViewtoolStripButton;
        private System.Windows.Forms.ToolStripMenuItem bibleToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripButton SavetoolStripButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem saveVerseListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _openVerseListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveListToMP3ToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker MakeMP3backgroundWorker;
        private System.Windows.Forms.ProgressBar MainprogressBar;
        private System.Windows.Forms.ToolStripMenuItem saveToMP3OptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveToMP3Dialog;
        private View.SelectedVersesControl _selectedVersesControl;
        private System.Windows.Forms.ToolStripButton _pasteToProgramToolStripButton;
        private System.Windows.Forms.ToolStripButton _copyReferencesToolStripButton;

    }
}