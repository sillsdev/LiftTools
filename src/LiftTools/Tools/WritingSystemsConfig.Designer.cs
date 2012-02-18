namespace LiftTools.Tools
{
	partial class WritingSystemsConfig
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
			this._gbWritingSystems = new System.Windows.Forms.GroupBox();
			this._lbOr = new System.Windows.Forms.Label();
			this._cbWritingSystemFrom = new Palaso.UI.WindowsForms.WritingSystems.WSPickerUsingComboBox();
			this._tbWritingSystemFrom = new System.Windows.Forms.TextBox();
			this._lbTo = new System.Windows.Forms.Label();
			this._lbFrom = new System.Windows.Forms.Label();
			this._cbWritingSystemTo = new Palaso.UI.WindowsForms.WritingSystems.WSPickerUsingComboBox();
			this._lbWritingSystem = new System.Windows.Forms.LinkLabel();
			this._cbDeleteUnusedWritingSystems = new System.Windows.Forms.CheckBox();
			this._cbDoRename = new System.Windows.Forms.CheckBox();
			this._cbReportWritingSystemsInUse = new System.Windows.Forms.CheckBox();
			this._cbCopyWhenDone = new System.Windows.Forms.CheckBox();
			this._gbWritingSystems.SuspendLayout();
			this.SuspendLayout();
			// 
			// _gbWritingSystems
			// 
			this._gbWritingSystems.Controls.Add(this._cbCopyWhenDone);
			this._gbWritingSystems.Controls.Add(this._lbOr);
			this._gbWritingSystems.Controls.Add(this._cbWritingSystemFrom);
			this._gbWritingSystems.Controls.Add(this._tbWritingSystemFrom);
			this._gbWritingSystems.Controls.Add(this._lbTo);
			this._gbWritingSystems.Controls.Add(this._lbFrom);
			this._gbWritingSystems.Controls.Add(this._cbWritingSystemTo);
			this._gbWritingSystems.Controls.Add(this._lbWritingSystem);
			this._gbWritingSystems.Controls.Add(this._cbDeleteUnusedWritingSystems);
			this._gbWritingSystems.Controls.Add(this._cbDoRename);
			this._gbWritingSystems.Controls.Add(this._cbReportWritingSystemsInUse);
			this._gbWritingSystems.Location = new System.Drawing.Point(3, 3);
			this._gbWritingSystems.Name = "_gbWritingSystems";
			this._gbWritingSystems.Size = new System.Drawing.Size(289, 249);
			this._gbWritingSystems.TabIndex = 2;
			this._gbWritingSystems.TabStop = false;
			this._gbWritingSystems.Text = "Writing Systems";
			// 
			// _lbOr
			// 
			this._lbOr.AutoSize = true;
			this._lbOr.Location = new System.Drawing.Point(35, 93);
			this._lbOr.Name = "_lbOr";
			this._lbOr.Size = new System.Drawing.Size(18, 13);
			this._lbOr.TabIndex = 9;
			this._lbOr.Text = "Or";
			// 
			// _cbWritingSystemFrom
			// 
			this._cbWritingSystemFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cbWritingSystemFrom.FormattingEnabled = true;
			this._cbWritingSystemFrom.Location = new System.Drawing.Point(71, 90);
			this._cbWritingSystemFrom.Name = "_cbWritingSystemFrom";
			this._cbWritingSystemFrom.Size = new System.Drawing.Size(212, 21);
			this._cbWritingSystemFrom.TabIndex = 8;
			// 
			// _tbWritingSystemFrom
			// 
			this._tbWritingSystemFrom.Location = new System.Drawing.Point(71, 64);
			this._tbWritingSystemFrom.Name = "_tbWritingSystemFrom";
			this._tbWritingSystemFrom.Size = new System.Drawing.Size(212, 20);
			this._tbWritingSystemFrom.TabIndex = 7;
			this._tbWritingSystemFrom.Text = "Some Regular Expression";
			// 
			// _lbTo
			// 
			this._lbTo.AutoSize = true;
			this._lbTo.Location = new System.Drawing.Point(35, 120);
			this._lbTo.Name = "_lbTo";
			this._lbTo.Size = new System.Drawing.Size(20, 13);
			this._lbTo.TabIndex = 6;
			this._lbTo.Text = "To";
			// 
			// _lbFrom
			// 
			this._lbFrom.AutoSize = true;
			this._lbFrom.Location = new System.Drawing.Point(35, 67);
			this._lbFrom.Name = "_lbFrom";
			this._lbFrom.Size = new System.Drawing.Size(30, 13);
			this._lbFrom.TabIndex = 5;
			this._lbFrom.Text = "From";
			// 
			// _cbWritingSystemTo
			// 
			this._cbWritingSystemTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cbWritingSystemTo.FormattingEnabled = true;
			this._cbWritingSystemTo.Location = new System.Drawing.Point(71, 117);
			this._cbWritingSystemTo.Name = "_cbWritingSystemTo";
			this._cbWritingSystemTo.Size = new System.Drawing.Size(212, 21);
			this._cbWritingSystemTo.TabIndex = 4;
			// 
			// _lbWritingSystem
			// 
			this._lbWritingSystem.AutoSize = true;
			this._lbWritingSystem.Location = new System.Drawing.Point(14, 219);
			this._lbWritingSystem.Name = "_lbWritingSystem";
			this._lbWritingSystem.Size = new System.Drawing.Size(130, 13);
			this._lbWritingSystem.TabIndex = 3;
			this._lbWritingSystem.TabStop = true;
			this._lbWritingSystem.Text = "Configure Writing Systems";
			this._lbWritingSystem.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.WritingSystem_LinkClicked);
			// 
			// _cbDeleteUnusedWritingSystems
			// 
			this._cbDeleteUnusedWritingSystems.AutoSize = true;
			this._cbDeleteUnusedWritingSystems.Location = new System.Drawing.Point(17, 144);
			this._cbDeleteUnusedWritingSystems.Name = "_cbDeleteUnusedWritingSystems";
			this._cbDeleteUnusedWritingSystems.Size = new System.Drawing.Size(189, 17);
			this._cbDeleteUnusedWritingSystems.TabIndex = 2;
			this._cbDeleteUnusedWritingSystems.Text = "Delete unused Writing System files";
			this._cbDeleteUnusedWritingSystems.UseVisualStyleBackColor = true;
			// 
			// _cbDoRename
			// 
			this._cbDoRename.AutoSize = true;
			this._cbDoRename.Location = new System.Drawing.Point(17, 44);
			this._cbDoRename.Name = "_cbDoRename";
			this._cbDoRename.Size = new System.Drawing.Size(185, 17);
			this._cbDoRename.TabIndex = 1;
			this._cbDoRename.Text = "Rename Writing System (in lift file)";
			this._cbDoRename.UseVisualStyleBackColor = true;
			// 
			// _cbReportWritingSystemsInUse
			// 
			this._cbReportWritingSystemsInUse.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
			this._cbReportWritingSystemsInUse.AutoSize = true;
			this._cbReportWritingSystemsInUse.Checked = true;
			this._cbReportWritingSystemsInUse.CheckState = System.Windows.Forms.CheckState.Checked;
			this._cbReportWritingSystemsInUse.Location = new System.Drawing.Point(17, 20);
			this._cbReportWritingSystemsInUse.Name = "_cbReportWritingSystemsInUse";
			this._cbReportWritingSystemsInUse.Size = new System.Drawing.Size(170, 17);
			this._cbReportWritingSystemsInUse.TabIndex = 0;
			this._cbReportWritingSystemsInUse.Text = "Report Writing Systems In Use";
			this._cbReportWritingSystemsInUse.UseVisualStyleBackColor = true;
			// 
			// _cbCopyWhenDone
			// 
			this._cbCopyWhenDone.AutoSize = true;
			this._cbCopyWhenDone.Location = new System.Drawing.Point(17, 168);
			this._cbCopyWhenDone.Name = "_cbCopyWhenDone";
			this._cbCopyWhenDone.Size = new System.Drawing.Size(224, 17);
			this._cbCopyWhenDone.TabIndex = 10;
			this._cbCopyWhenDone.Text = "Copy processed file over input when done";
			this._cbCopyWhenDone.UseVisualStyleBackColor = true;
			// 
			// WritingSystemsConfig
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._gbWritingSystems);
			this.Name = "WritingSystemsConfig";
			this.Size = new System.Drawing.Size(305, 265);
			this._gbWritingSystems.ResumeLayout(false);
			this._gbWritingSystems.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.GroupBox _gbWritingSystems;
        private System.Windows.Forms.CheckBox _cbReportWritingSystemsInUse;
		private System.Windows.Forms.CheckBox _cbDoRename;
		private System.Windows.Forms.CheckBox _cbDeleteUnusedWritingSystems;
		private System.Windows.Forms.LinkLabel _lbWritingSystem;
		private System.Windows.Forms.Label _lbTo;
		private System.Windows.Forms.Label _lbFrom;
		private Palaso.UI.WindowsForms.WritingSystems.WSPickerUsingComboBox _cbWritingSystemTo;
		private System.Windows.Forms.TextBox _tbWritingSystemFrom;
		private System.Windows.Forms.Label _lbOr;
		private Palaso.UI.WindowsForms.WritingSystems.WSPickerUsingComboBox _cbWritingSystemFrom;
		private System.Windows.Forms.CheckBox _cbCopyWhenDone;

    }
}
