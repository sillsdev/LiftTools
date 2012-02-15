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
            this._cbFindByNumber = new System.Windows.Forms.CheckBox();
            this._cbRenameFiles = new System.Windows.Forms.CheckBox();
            this._gbFilesWithoutLinks = new System.Windows.Forms.GroupBox();
            this._cbFindLinkByWord = new System.Windows.Forms.CheckBox();
            this._cbInsertLink = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this._gbFilesWithoutLinks.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._cbRenameFiles);
            this.groupBox1.Controls.Add(this._cbFindByNumber);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(256, 75);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Links Without Files";
            // 
            // _cbFindByNumber
            // 
            this._cbFindByNumber.AutoSize = true;
            this._cbFindByNumber.Location = new System.Drawing.Point(17, 19);
            this._cbFindByNumber.Name = "_cbFindByNumber";
            this._cbFindByNumber.Size = new System.Drawing.Size(207, 17);
            this._cbFindByNumber.TabIndex = 1;
            this._cbFindByNumber.Text = "Try to find file name using numeric part";
            this._cbFindByNumber.UseVisualStyleBackColor = true;
            // 
            // _cbRenameFiles
            // 
            this._cbRenameFiles.AutoSize = true;
            this._cbRenameFiles.Location = new System.Drawing.Point(17, 43);
            this._cbRenameFiles.Name = "_cbRenameFiles";
            this._cbRenameFiles.Size = new System.Drawing.Size(87, 17);
            this._cbRenameFiles.TabIndex = 2;
            this._cbRenameFiles.Text = "Rename files";
            this._cbRenameFiles.UseVisualStyleBackColor = true;
            // 
            // _gbFilesWithoutLinks
            // 
            this._gbFilesWithoutLinks.Controls.Add(this._cbInsertLink);
            this._gbFilesWithoutLinks.Controls.Add(this._cbFindLinkByWord);
            this._gbFilesWithoutLinks.Location = new System.Drawing.Point(3, 84);
            this._gbFilesWithoutLinks.Name = "_gbFilesWithoutLinks";
            this._gbFilesWithoutLinks.Size = new System.Drawing.Size(256, 75);
            this._gbFilesWithoutLinks.TabIndex = 2;
            this._gbFilesWithoutLinks.TabStop = false;
            this._gbFilesWithoutLinks.Text = "Files Without Links";
            // 
            // _cbFindLinkByWord
            // 
            this._cbFindLinkByWord.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this._cbFindLinkByWord.AutoSize = true;
            this._cbFindLinkByWord.Location = new System.Drawing.Point(17, 20);
            this._cbFindLinkByWord.Name = "_cbFindLinkByWord";
            this._cbFindLinkByWord.Size = new System.Drawing.Size(237, 17);
            this._cbFindLinkByWord.TabIndex = 0;
            this._cbFindLinkByWord.Text = "Try to match word using text part of file name";
            this._cbFindLinkByWord.UseVisualStyleBackColor = true;
            // 
            // _cbInsertLink
            // 
            this._cbInsertLink.AutoSize = true;
            this._cbInsertLink.Location = new System.Drawing.Point(17, 44);
            this._cbInsertLink.Name = "_cbInsertLink";
            this._cbInsertLink.Size = new System.Drawing.Size(71, 17);
            this._cbInsertLink.TabIndex = 1;
            this._cbInsertLink.Text = "Insert link";
            this._cbInsertLink.UseVisualStyleBackColor = true;
            // 
            // AudioFileNamesConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._gbFilesWithoutLinks);
            this.Controls.Add(this.groupBox1);
            this.Name = "AudioFileNamesConfig";
            this.Size = new System.Drawing.Size(274, 177);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this._gbFilesWithoutLinks.ResumeLayout(false);
            this._gbFilesWithoutLinks.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox _cbRenameFiles;
        private System.Windows.Forms.CheckBox _cbFindByNumber;
        private System.Windows.Forms.GroupBox _gbFilesWithoutLinks;
        private System.Windows.Forms.CheckBox _cbFindLinkByWord;
        private System.Windows.Forms.CheckBox _cbInsertLink;

    }
}
