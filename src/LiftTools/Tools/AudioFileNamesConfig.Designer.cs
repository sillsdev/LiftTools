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
            this._cbFindFileFromLinkNumber = new System.Windows.Forms.CheckBox();
            this._cbRenameFile = new System.Windows.Forms.CheckBox();
            this._gbFilesWithoutLinks = new System.Windows.Forms.GroupBox();
            this._cbFindLinkFromFile = new System.Windows.Forms.CheckBox();
            this._cbInsertLinkFromFile = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this._gbFilesWithoutLinks.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._cbRenameFile);
            this.groupBox1.Controls.Add(this._cbFindFileFromLinkNumber);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(256, 75);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Links Without Files";
            // 
            // _cbFindFileFromLinkNumber
            // 
            this._cbFindFileFromLinkNumber.AutoSize = true;
            this._cbFindFileFromLinkNumber.Location = new System.Drawing.Point(17, 19);
            this._cbFindFileFromLinkNumber.Name = "_cbFindFileFromLinkNumber";
            this._cbFindFileFromLinkNumber.Size = new System.Drawing.Size(207, 17);
            this._cbFindFileFromLinkNumber.TabIndex = 1;
            this._cbFindFileFromLinkNumber.Text = "Try to find file name using numeric part";
            this._cbFindFileFromLinkNumber.UseVisualStyleBackColor = true;
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
            // _gbFilesWithoutLinks
            // 
            this._gbFilesWithoutLinks.Controls.Add(this._cbInsertLinkFromFile);
            this._gbFilesWithoutLinks.Controls.Add(this._cbFindLinkFromFile);
            this._gbFilesWithoutLinks.Location = new System.Drawing.Point(3, 84);
            this._gbFilesWithoutLinks.Name = "_gbFilesWithoutLinks";
            this._gbFilesWithoutLinks.Size = new System.Drawing.Size(256, 75);
            this._gbFilesWithoutLinks.TabIndex = 2;
            this._gbFilesWithoutLinks.TabStop = false;
            this._gbFilesWithoutLinks.Text = "Files Without Links";
            // 
            // _cbFindLinkFromFile
            // 
            this._cbFindLinkFromFile.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this._cbFindLinkFromFile.AutoSize = true;
            this._cbFindLinkFromFile.Location = new System.Drawing.Point(17, 20);
            this._cbFindLinkFromFile.Name = "_cbFindLinkFromFile";
            this._cbFindLinkFromFile.Size = new System.Drawing.Size(237, 17);
            this._cbFindLinkFromFile.TabIndex = 0;
            this._cbFindLinkFromFile.Text = "Try to match word using text part of file name";
            this._cbFindLinkFromFile.UseVisualStyleBackColor = true;
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
        private System.Windows.Forms.CheckBox _cbRenameFile;
        private System.Windows.Forms.CheckBox _cbFindFileFromLinkNumber;
        private System.Windows.Forms.GroupBox _gbFilesWithoutLinks;
        private System.Windows.Forms.CheckBox _cbFindLinkFromFile;
        private System.Windows.Forms.CheckBox _cbInsertLinkFromFile;

    }
}
