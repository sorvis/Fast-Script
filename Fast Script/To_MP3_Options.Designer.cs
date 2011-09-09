namespace Fast_Script
{
    partial class To_MP3_Options
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.Tab_TTS_Voice_Options = new System.Windows.Forms.TabPage();
            this.TTS_Voice_Age_textbox = new System.Windows.Forms.Label();
            this.Installed_Voices_comboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Tab_MP3_Encoding = new System.Windows.Forms.TabPage();
            this.TTS_Voice_Gender_textbox = new System.Windows.Forms.Label();
            this.TTS_Voice_Culture_textbox = new System.Windows.Forms.Label();
            this.TTS_Rate_trackBar = new System.Windows.Forms.TrackBar();
            this.TTS_Rate_label = new System.Windows.Forms.Label();
            this.TestButton = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.Tab_TTS_Voice_Options.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TTS_Rate_trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.Tab_TTS_Voice_Options);
            this.tabControl.Controls.Add(this.Tab_MP3_Encoding);
            this.tabControl.Location = new System.Drawing.Point(-3, 1);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(288, 261);
            this.tabControl.TabIndex = 0;
            // 
            // Tab_TTS_Voice_Options
            // 
            this.Tab_TTS_Voice_Options.Controls.Add(this.TestButton);
            this.Tab_TTS_Voice_Options.Controls.Add(this.TTS_Rate_label);
            this.Tab_TTS_Voice_Options.Controls.Add(this.TTS_Rate_trackBar);
            this.Tab_TTS_Voice_Options.Controls.Add(this.TTS_Voice_Culture_textbox);
            this.Tab_TTS_Voice_Options.Controls.Add(this.TTS_Voice_Gender_textbox);
            this.Tab_TTS_Voice_Options.Controls.Add(this.TTS_Voice_Age_textbox);
            this.Tab_TTS_Voice_Options.Controls.Add(this.Installed_Voices_comboBox);
            this.Tab_TTS_Voice_Options.Controls.Add(this.label1);
            this.Tab_TTS_Voice_Options.Location = new System.Drawing.Point(4, 22);
            this.Tab_TTS_Voice_Options.Name = "Tab_TTS_Voice_Options";
            this.Tab_TTS_Voice_Options.Size = new System.Drawing.Size(280, 235);
            this.Tab_TTS_Voice_Options.TabIndex = 0;
            this.Tab_TTS_Voice_Options.Text = "Voice Options";
            this.Tab_TTS_Voice_Options.UseVisualStyleBackColor = true;
            // 
            // TTS_Voice_Age_textbox
            // 
            this.TTS_Voice_Age_textbox.AutoSize = true;
            this.TTS_Voice_Age_textbox.Location = new System.Drawing.Point(4, 48);
            this.TTS_Voice_Age_textbox.Name = "TTS_Voice_Age_textbox";
            this.TTS_Voice_Age_textbox.Size = new System.Drawing.Size(32, 13);
            this.TTS_Voice_Age_textbox.TabIndex = 3;
            this.TTS_Voice_Age_textbox.Text = "Age: ";
            // 
            // Installed_Voices_comboBox
            // 
            this.Installed_Voices_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Installed_Voices_comboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Installed_Voices_comboBox.FormattingEnabled = true;
            this.Installed_Voices_comboBox.Location = new System.Drawing.Point(7, 21);
            this.Installed_Voices_comboBox.Name = "Installed_Voices_comboBox";
            this.Installed_Voices_comboBox.Size = new System.Drawing.Size(121, 21);
            this.Installed_Voices_comboBox.TabIndex = 2;
            this.Installed_Voices_comboBox.SelectedIndexChanged += new System.EventHandler(this.Installed_Voices_comboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Installed Microsoft Voices:";
            // 
            // Tab_MP3_Encoding
            // 
            this.Tab_MP3_Encoding.Location = new System.Drawing.Point(4, 22);
            this.Tab_MP3_Encoding.Name = "Tab_MP3_Encoding";
            this.Tab_MP3_Encoding.Size = new System.Drawing.Size(280, 235);
            this.Tab_MP3_Encoding.TabIndex = 1;
            this.Tab_MP3_Encoding.Text = "MP3 Encoding";
            this.Tab_MP3_Encoding.UseVisualStyleBackColor = true;
            // 
            // TTS_Voice_Gender_textbox
            // 
            this.TTS_Voice_Gender_textbox.AutoSize = true;
            this.TTS_Voice_Gender_textbox.Location = new System.Drawing.Point(4, 61);
            this.TTS_Voice_Gender_textbox.Name = "TTS_Voice_Gender_textbox";
            this.TTS_Voice_Gender_textbox.Size = new System.Drawing.Size(48, 13);
            this.TTS_Voice_Gender_textbox.TabIndex = 4;
            this.TTS_Voice_Gender_textbox.Text = "Gender: ";
            // 
            // TTS_Voice_Culture_textbox
            // 
            this.TTS_Voice_Culture_textbox.AutoSize = true;
            this.TTS_Voice_Culture_textbox.Location = new System.Drawing.Point(4, 74);
            this.TTS_Voice_Culture_textbox.Name = "TTS_Voice_Culture_textbox";
            this.TTS_Voice_Culture_textbox.Size = new System.Drawing.Size(46, 13);
            this.TTS_Voice_Culture_textbox.TabIndex = 5;
            this.TTS_Voice_Culture_textbox.Text = "Culture: ";
            // 
            // TTS_Rate_trackBar
            // 
            this.TTS_Rate_trackBar.Location = new System.Drawing.Point(7, 119);
            this.TTS_Rate_trackBar.Maximum = 20;
            this.TTS_Rate_trackBar.Name = "TTS_Rate_trackBar";
            this.TTS_Rate_trackBar.Size = new System.Drawing.Size(264, 45);
            this.TTS_Rate_trackBar.TabIndex = 6;
            this.TTS_Rate_trackBar.Scroll += new System.EventHandler(this.TTS_Rate_trackBar_Scroll);
            // 
            // TTS_Rate_label
            // 
            this.TTS_Rate_label.AutoSize = true;
            this.TTS_Rate_label.Location = new System.Drawing.Point(4, 103);
            this.TTS_Rate_label.Name = "TTS_Rate_label";
            this.TTS_Rate_label.Size = new System.Drawing.Size(30, 13);
            this.TTS_Rate_label.TabIndex = 7;
            this.TTS_Rate_label.Text = "Rate";
            // 
            // TestButton
            // 
            this.TestButton.Location = new System.Drawing.Point(7, 171);
            this.TestButton.Name = "TestButton";
            this.TestButton.Size = new System.Drawing.Size(75, 23);
            this.TestButton.TabIndex = 8;
            this.TestButton.Text = "Test Voice";
            this.TestButton.UseVisualStyleBackColor = true;
            this.TestButton.Click += new System.EventHandler(this.TestButton_Click);
            // 
            // To_MP3_Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.tabControl);
            this.Name = "To_MP3_Options";
            this.Text = "Send To MP3 Options";
            this.tabControl.ResumeLayout(false);
            this.Tab_TTS_Voice_Options.ResumeLayout(false);
            this.Tab_TTS_Voice_Options.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TTS_Rate_trackBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage Tab_TTS_Voice_Options;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage Tab_MP3_Encoding;
        private System.Windows.Forms.ComboBox Installed_Voices_comboBox;
        private System.Windows.Forms.Label TTS_Voice_Age_textbox;
        private System.Windows.Forms.Label TTS_Voice_Culture_textbox;
        private System.Windows.Forms.Label TTS_Voice_Gender_textbox;
        private System.Windows.Forms.Label TTS_Rate_label;
        private System.Windows.Forms.TrackBar TTS_Rate_trackBar;
        private System.Windows.Forms.Button TestButton;
    }
}