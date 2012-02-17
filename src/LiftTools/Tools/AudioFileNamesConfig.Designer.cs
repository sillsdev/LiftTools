namespace LiftTools.Tools
{
    partial class AudioFileNamesConfig
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this._cbRenameFile = new System.Windows.Forms.CheckBox();
			this._cbFindFileFromLinkNumber = new System.Windows.Forms.CheckBox();
			this._gbFilesWithoutLinks = new System.Windows.Forms.GroupBox();
			this._lbWritingSystem = new System.Windows.Forms.LinkLabel();
			this._cbMoveRemainingOrphanFiles = new System.Windows.Forms.CheckBox();
			this._cbInsertLinkFromFile = new System.Windows.Forms.CheckBox();
			this._cbFindLinkFromFile = new System.Windows.Forms.CheckBox();
			this._gbGoodFiles = new System.Windows.Forms.GroupBox();
			this._cbReportGoodFiles = new System.Windows.Forms.CheckBox();
			this._cbWritingSystem = new Palaso.UI.WindowsForms.WritingSystems.WSPickerUsingComboBox();
			this.groupBox1.SuspendLayout();
			this._gbFilesWithoutLinks.SuspendLayout();
			this._gbGoodFiles.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this._cbRenameFile);
			this.groupBox1.Controls.Add(this._cbFindFileFromLinkNumber);
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(289, 75);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Links Without Files";
			// 
			// _cbRenameFile
			// 
			this._cbRenameFile.AutoSize = true;
			this._cbRenameFile.Location = new System.Drawing.Point(17, 43);
			this._cbRenameFile.Name = "_cbRenameFile";
			this._cbRenameFile.Size = new System.Drawing.Size(87, 17);
			this._cbRenameFile.TabIndex = 2;
			this._cbRenameFile.Text = "Rename files";
			this._cbRenameFile.UseVisualStyleBackColor = true;
			// 
			// _cbFindFileFromLinkNumber
			// 
			this._cbFindFileFromLinkNumber.AutoSize = true;
			this._cbFindFileFromLinkNumber.Checked = true;
			this._cbFindFileFromLinkNumber.CheckState = System.Windows.Forms.CheckState.Checked;
			this._cbFindFileFromLinkNumber.Location = new System.Drawing.Point(17, 19);
			this._cbFindFileFromLinkNumber.Name = "_cbFindFileFromLinkNumber";
			this._cbFindFileFromLinkNumber.Size = new System.Drawing.Size(207, 17);
			this._cbFindFileFromLinkNumber.TabIndex = 1;
			this._cbFindFileFromLinkNumber.Text = "Try to find file name using numeric part";
			this._cbFindFileFromLinkNumber.UseVisualStyleBackColor = true;
			// 
			// _gbFilesWithoutLinks
			// 
			this._gbFilesWithoutLinks.Controls.Add(this._cbWritingSystem);
			this._gbFilesWithoutLinks.Controls.Add(this._lbWritingSystem);
			this._gbFilesWithoutLinks.Controls.Add(this._cbMoveRemainingOrphanFiles);
			this._gbFilesWithoutLinks.Controls.Add(this._cbInsertLinkFromFile);
			this._gbFilesWithoutLinks.Controls.Add(this._cbFindLinkFromFile);
			this._gbFilesWithoutLinks.Location = new System.Drawing.Point(3, 84);
			this._gbFilesWithoutLinks.Name = "_gbFilesWithoutLinks";
			this._gbFilesWithoutLinks.Size = new System.Drawing.Size(289, 119);
			this._gbFilesWithoutLinks.TabIndex = 2;
			this._gbFilesWithoutLinks.TabStop = false;
			this._gbFilesWithoutLinks.Text = "Files Without Links";
			// 
			// _lbWritingSystem
			// 
			this._lbWritingSystem.AutoSize = true;
			this._lbWritingSystem.Location = new System.Drawing.Point(37, 67);
			this._lbWritingSystem.Name = "_lbWritingSystem";
			this._lbWritingSystem.Size = new System.Drawing.Size(102, 13);
			this._lbWritingSystem.TabIndex = 3;
			this._lbWritingSystem.TabStop = true;
			this._lbWritingSystem.Text = "With Writing System";
			this._lbWritingSystem.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.WritingSystem_LinkClicked);
			// 
			// _cbMoveRemainingOrphanFiles
			// 
			this._cbMoveRemainingOrphanFiles.AutoSize = true;
			this._cbMoveRemainingOrphanFiles.Location = new System.Drawing.Point(16, 91);
			this._cbMoveRemainingOrphanFiles.Name = "_cbMoveRemainingOrphanFiles";
			this._cbMoveRemainingOrphanFiles.Size = new System.Drawing.Size(222, 17);
			this._cbMoveRemainingOrphanFiles.TabIndex = 2;
			this._cbMoveRemainingOrphanFiles.Text = "Move remaining files to OrphanFiles folder";
			this._cbMoveRemainingOrphanFiles.UseVisualStyleBackColor = true;
			// 
			// _cbInsertLinkFromFile
			// 
			this._cbInsertLinkFromFile.AutoSize = true;
			this._cbInsertLinkFromFile.Location = new System.Drawing.Point(17, 44);
			this._cbInsertLinkFromFile.Name = "_cbInsertLinkFromFile";
			this._cbInsertLinkFromFile.Size = new System.Drawing.Size(71, 17);
			this._cbInsertLinkFromFile.TabIndex = 1;
			this._cbInsertLinkFromFile.Text = "Insert link";
			this._cbInsertLinkFromFile.UseVisualStyleBackColor = true;
			// 
			// _cbFindLinkFromFile
			// 
			this._cbFindLinkFromFile.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
			this._cbFindLinkFromFile.AutoSize = true;
			this._cbFindLinkFromFile.Checked = true;
			this._cbFindLinkFromFile.CheckState = System.Windows.Forms.CheckState.Checked;
			this._cbFindLinkFromFile.Location = new System.Drawing.Point(17, 20);
			this._cbFindLinkFromFile.Name = "_cbFindLinkFromFile";
			this._cbFindLinkFromFile.Size = new System.Drawing.Size(237, 17);
			this._cbFindLinkFromFile.TabIndex = 0;
			this._cbFindLinkFromFile.Text = "Try to match word using text part of file name";
			this._cbFindLinkFromFile.UseVisualStyleBackColor = true;
			// 
			// _gbGoodFiles
			// 
			this._gbGoodFiles.Controls.Add(this._cbReportGoodFiles);
			this._gbGoodFiles.Location = new System.Drawing.Point(3, 209);
			this._gbGoodFiles.Name = "_gbGoodFiles";
			this._gbGoodFiles.Size = new System.Drawing.Size(289, 51);
			this._gbGoodFiles.TabIndex = 3;
			this._gbGoodFiles.TabStop = false;
			this._gbGoodFiles.Text = "Links With Files";
			// 
			// _cbReportGoodFiles
			// 
			this._cbReportGoodFiles.AutoSize = true;
			this._cbReportGoodFiles.Checked = true;
			this._cbReportGoodFiles.CheckState = System.Windows.Forms.CheckState.Checked;
			this._cbReportGoodFiles.Location = new System.Drawing.Point(16, 20);
			this._cbReportGoodFiles.Name = "_cbReportGoodFiles";
			this._cbReportGoodFiles.Size = new System.Drawing.Size(109, 17);
			this._cbReportGoodFiles.TabIndex = 0;
			this._cbReportGoodFiles.Text = "Report files found";
			this._cbReportGoodFiles.UseVisualStyleBackColor = true;
			// 
			// _cbWritingSystem
			// 
			this._cbWritingSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cbWritingSystem.FormattingEnabled = true;
			this._cbWritingSystem.Location = new System.Drawing.Point(146, 64);
			this._cbWritingSystem.Name = "_cbWritingSystem";
			this._cbWritingSystem.Size = new System.Drawing.Size(137, 21);
			this._cbWritingSystem.TabIndex = 4;
			// 
			// AudioFileNamesConfig
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._gbGoodFiles);
			this.Controls.Add(this._gbFilesWithoutLinks);
			this.Controls.Add(this.groupBox1);
			this.Name = "AudioFileNamesConfig";
			this.Size = new System.Drawing.Size(305, 275);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this._gbFilesWithoutLinks.ResumeLayout(false);
			this._gbFilesWithoutLinks.PerformLayout();
			this._gbGoodFiles.ResumeLayout(false);
			this._gbGoodFiles.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox _cbRenameFile;
        private System.Windows.Forms.CheckBox _cbFindFileFromLinkNumber;
        private System.Windows.Forms.GroupBox _gbFilesWithoutLinks;
        private System.Windows.Forms.CheckBox _cbFindLinkFromFile;
        private System.Windows.Forms.CheckBox _cbInsertLinkFromFile;
        private System.Windows.Forms.GroupBox _gbGoodFiles;
        private System.Windows.Forms.CheckBox _cbReportGoodFiles;
		private System.Windows.Forms.CheckBox _cbMoveRemainingOrphanFiles;
		private System.Windows.Forms.LinkLabel _lbWritingSystem;
		private Palaso.UI.WindowsForms.WritingSystems.WSPickerUsingComboBox _cbWritingSystem;

    }
}
