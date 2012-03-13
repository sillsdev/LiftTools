namespace LiftTools.Tools
{
	partial class MergeHomographsConfig
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
			this._gbDataConfiguration = new System.Windows.Forms.GroupBox();
			this._clbMergeTraits = new Palaso.UI.WindowsForms.Widgets.BetterCheckedListBox(this.components);
			this._dlgCawlFileChooser = new System.Windows.Forms.OpenFileDialog();
			this._lblDataConfiguration = new System.Windows.Forms.Label();
			this._gbDataConfiguration.SuspendLayout();
			this.SuspendLayout();
			// 
			// _gbDataConfiguration
			// 
			this._gbDataConfiguration.Controls.Add(this._lblDataConfiguration);
			this._gbDataConfiguration.Controls.Add(this._clbMergeTraits);
			this._gbDataConfiguration.Location = new System.Drawing.Point(3, 3);
			this._gbDataConfiguration.Name = "_gbDataConfiguration";
			this._gbDataConfiguration.Size = new System.Drawing.Size(335, 291);
			this._gbDataConfiguration.TabIndex = 2;
			this._gbDataConfiguration.TabStop = false;
			this._gbDataConfiguration.Text = "Data Configuration";
			// 
			// _clbMergeTraits
			// 
			this._clbMergeTraits.FormattingEnabled = true;
			this._clbMergeTraits.Location = new System.Drawing.Point(17, 43);
			this._clbMergeTraits.Name = "_clbMergeTraits";
			this._clbMergeTraits.Size = new System.Drawing.Size(307, 229);
			this._clbMergeTraits.TabIndex = 3;
			// 
			// _dlgCawlFileChooser
			// 
			this._dlgCawlFileChooser.FileName = "SILCawl.lift";
			this._dlgCawlFileChooser.Filter = "Lift Files|*.lift|All Files|*.*";
			// 
			// _lblDataConfiguration
			// 
			this._lblDataConfiguration.AutoSize = true;
			this._lblDataConfiguration.Location = new System.Drawing.Point(17, 24);
			this._lblDataConfiguration.Name = "_lblDataConfiguration";
			this._lblDataConfiguration.Size = new System.Drawing.Size(169, 13);
			this._lblDataConfiguration.TabIndex = 4;
			this._lblDataConfiguration.Text = "Set which traits should be merged.";
			// 
			// MergeHomographsConfig
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._gbDataConfiguration);
			this.Name = "MergeHomographsConfig";
			this.Size = new System.Drawing.Size(355, 308);
			this._gbDataConfiguration.ResumeLayout(false);
			this._gbDataConfiguration.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.GroupBox _gbDataConfiguration;
		private Palaso.UI.WindowsForms.Widgets.BetterCheckedListBox _clbMergeTraits;
		private System.Windows.Forms.OpenFileDialog _dlgCawlFileChooser;
		private System.Windows.Forms.Label _lblDataConfiguration;

    }
}
