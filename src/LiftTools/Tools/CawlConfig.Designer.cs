namespace LiftTools.Tools
{
	partial class CawlConfig
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
			this.components = new System.ComponentModel.Container();
			this._gbCawl = new System.Windows.Forms.GroupBox();
			this._clbCawlWritingSystems = new Palaso.UI.WindowsForms.Widgets.BetterCheckedListBox(this.components);
			this._cbDeleteCawlEntries = new System.Windows.Forms.CheckBox();
			this._cbReportWritingSystemsInUse = new System.Windows.Forms.CheckBox();
			this._gbCawlFile = new System.Windows.Forms.GroupBox();
			this._btnCawlFileChooser = new System.Windows.Forms.Button();
			this._tbCawlFilePath = new System.Windows.Forms.TextBox();
			this._dlgCawlFileChooser = new System.Windows.Forms.OpenFileDialog();
			this._gbCawl.SuspendLayout();
			this._gbCawlFile.SuspendLayout();
			this.SuspendLayout();
			// 
			// _gbCawl
			// 
			this._gbCawl.Controls.Add(this._clbCawlWritingSystems);
			this._gbCawl.Controls.Add(this._cbDeleteCawlEntries);
			this._gbCawl.Controls.Add(this._cbReportWritingSystemsInUse);
			this._gbCawl.Location = new System.Drawing.Point(3, 66);
			this._gbCawl.Name = "_gbCawl";
			this._gbCawl.Size = new System.Drawing.Size(335, 249);
			this._gbCawl.TabIndex = 2;
			this._gbCawl.TabStop = false;
			this._gbCawl.Text = "CAWL Clean Up";
			// 
			// _clbCawlWritingSystems
			// 
			this._clbCawlWritingSystems.FormattingEnabled = true;
			this._clbCawlWritingSystems.Location = new System.Drawing.Point(17, 67);
			this._clbCawlWritingSystems.Name = "_clbCawlWritingSystems";
			this._clbCawlWritingSystems.Size = new System.Drawing.Size(307, 169);
			this._clbCawlWritingSystems.TabIndex = 3;
			// 
			// _cbDeleteCawlEntries
			// 
			this._cbDeleteCawlEntries.AutoSize = true;
			this._cbDeleteCawlEntries.Location = new System.Drawing.Point(17, 43);
			this._cbDeleteCawlEntries.Name = "_cbDeleteCawlEntries";
			this._cbDeleteCawlEntries.Size = new System.Drawing.Size(307, 17);
			this._cbDeleteCawlEntries.TabIndex = 2;
			this._cbDeleteCawlEntries.Text = "Delete gloss and definition with the following writing systems";
			this._cbDeleteCawlEntries.UseVisualStyleBackColor = true;
			// 
			// _cbReportWritingSystemsInUse
			// 
			this._cbReportWritingSystemsInUse.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
			this._cbReportWritingSystemsInUse.AutoSize = true;
			this._cbReportWritingSystemsInUse.Checked = true;
			this._cbReportWritingSystemsInUse.CheckState = System.Windows.Forms.CheckState.Checked;
			this._cbReportWritingSystemsInUse.Location = new System.Drawing.Point(17, 20);
			this._cbReportWritingSystemsInUse.Name = "_cbReportWritingSystemsInUse";
			this._cbReportWritingSystemsInUse.Size = new System.Drawing.Size(219, 17);
			this._cbReportWritingSystemsInUse.TabIndex = 0;
			this._cbReportWritingSystemsInUse.Text = "Report Writing Systems In Use for CAWL";
			this._cbReportWritingSystemsInUse.UseVisualStyleBackColor = true;
			// 
			// _gbCawlFile
			// 
			this._gbCawlFile.Controls.Add(this._btnCawlFileChooser);
			this._gbCawlFile.Controls.Add(this._tbCawlFilePath);
			this._gbCawlFile.Location = new System.Drawing.Point(3, 3);
			this._gbCawlFile.Name = "_gbCawlFile";
			this._gbCawlFile.Size = new System.Drawing.Size(335, 57);
			this._gbCawlFile.TabIndex = 3;
			this._gbCawlFile.TabStop = false;
			this._gbCawlFile.Text = "CAWL File";
			// 
			// _btnCawlFileChooser
			// 
			this._btnCawlFileChooser.Location = new System.Drawing.Point(299, 20);
			this._btnCawlFileChooser.Name = "_btnCawlFileChooser";
			this._btnCawlFileChooser.Size = new System.Drawing.Size(30, 23);
			this._btnCawlFileChooser.TabIndex = 1;
			this._btnCawlFileChooser.Text = "...";
			this._btnCawlFileChooser.UseVisualStyleBackColor = true;
			this._btnCawlFileChooser.Click += new System.EventHandler(this.OnCawlFileChooser_Click);
			// 
			// _tbCawlFilePath
			// 
			this._tbCawlFilePath.Location = new System.Drawing.Point(17, 20);
			this._tbCawlFilePath.Name = "_tbCawlFilePath";
			this._tbCawlFilePath.ReadOnly = true;
			this._tbCawlFilePath.Size = new System.Drawing.Size(276, 20);
			this._tbCawlFilePath.TabIndex = 0;
			// 
			// _dlgCawlFileChooser
			// 
			this._dlgCawlFileChooser.FileName = "SILCawl.lift";
			this._dlgCawlFileChooser.Filter = "Lift Files|*.lift|All Files|*.*";
			// 
			// CawlConfig
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._gbCawlFile);
			this.Controls.Add(this._gbCawl);
			this.Name = "CawlConfig";
			this.Size = new System.Drawing.Size(355, 327);
			this._gbCawl.ResumeLayout(false);
			this._gbCawl.PerformLayout();
			this._gbCawlFile.ResumeLayout(false);
			this._gbCawlFile.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.GroupBox _gbCawl;
		private System.Windows.Forms.CheckBox _cbReportWritingSystemsInUse;
		private System.Windows.Forms.CheckBox _cbDeleteCawlEntries;
		private System.Windows.Forms.GroupBox _gbCawlFile;
		private System.Windows.Forms.Button _btnCawlFileChooser;
		private System.Windows.Forms.TextBox _tbCawlFilePath;
		private Palaso.UI.WindowsForms.Widgets.BetterCheckedListBox _clbCawlWritingSystems;
		private System.Windows.Forms.OpenFileDialog _dlgCawlFileChooser;

    }
}
