namespace LiftTools
{
    partial class Shell
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.WindowsFormsSynchronizationContext windowsFormsSynchronizationContext1 = new System.Windows.Forms.WindowsFormsSynchronizationContext();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Shell));
			this._chooseLiftButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this._toolChooser = new System.Windows.Forms.ComboBox();
			this._runToolButton = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this._liftPathDisplay = new System.Windows.Forms.TextBox();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this._tabControl = new System.Windows.Forms.TabControl();
			this._infoPage = new System.Windows.Forms.TabPage();
			this._infoBrowser = new System.Windows.Forms.WebBrowser();
			this._logPage = new System.Windows.Forms.TabPage();
			this._logBox = new Palaso.UI.WindowsForms.Progress.LogBox();
			this._configPage = new System.Windows.Forms.TabPage();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this._tabControl.SuspendLayout();
			this._infoPage.SuspendLayout();
			this._logPage.SuspendLayout();
			this.SuspendLayout();
			// 
			// _chooseLiftButton
			// 
			this._chooseLiftButton.Location = new System.Drawing.Point(414, 21);
			this._chooseLiftButton.Name = "_chooseLiftButton";
			this._chooseLiftButton.Size = new System.Drawing.Size(31, 23);
			this._chooseLiftButton.TabIndex = 0;
			this._chooseLiftButton.Text = "...";
			this._chooseLiftButton.UseVisualStyleBackColor = true;
			this._chooseLiftButton.Click += new System.EventHandler(this._chooseLiftButton_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(22, 26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "LIFT File";
			// 
			// _toolChooser
			// 
			this._toolChooser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._toolChooser.FormattingEnabled = true;
			this._toolChooser.Location = new System.Drawing.Point(92, 56);
			this._toolChooser.Name = "_toolChooser";
			this._toolChooser.Size = new System.Drawing.Size(241, 21);
			this._toolChooser.TabIndex = 2;
			this._toolChooser.SelectedIndexChanged += new System.EventHandler(this._toolChooser_SelectedIndexChanged);
			// 
			// _runToolButton
			// 
			this._runToolButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._runToolButton.Location = new System.Drawing.Point(414, 50);
			this._runToolButton.Name = "_runToolButton";
			this._runToolButton.Size = new System.Drawing.Size(58, 30);
			this._runToolButton.TabIndex = 3;
			this._runToolButton.Text = "Run";
			this._runToolButton.UseVisualStyleBackColor = true;
			this._runToolButton.Click += new System.EventHandler(this._runToolButton_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(22, 64);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(28, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Tool";
			// 
			// _liftPathDisplay
			// 
			this._liftPathDisplay.Location = new System.Drawing.Point(92, 21);
			this._liftPathDisplay.Name = "_liftPathDisplay";
			this._liftPathDisplay.ReadOnly = true;
			this._liftPathDisplay.Size = new System.Drawing.Size(296, 20);
			this._liftPathDisplay.TabIndex = 7;
			this._liftPathDisplay.TextChanged += new System.EventHandler(this._liftPathDisplay_TextChanged);
			// 
			// _tabControl
			// 
			this._tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._tabControl.Controls.Add(this._infoPage);
			this._tabControl.Controls.Add(this._logPage);
			this._tabControl.Controls.Add(this._configPage);
			this._tabControl.Location = new System.Drawing.Point(25, 103);
			this._tabControl.Name = "_tabControl";
			this._tabControl.SelectedIndex = 0;
			this._tabControl.Size = new System.Drawing.Size(568, 349);
			this._tabControl.TabIndex = 9;
			// 
			// _infoPage
			// 
			this._infoPage.Controls.Add(this._infoBrowser);
			this._infoPage.Location = new System.Drawing.Point(4, 22);
			this._infoPage.Name = "_infoPage";
			this._infoPage.Padding = new System.Windows.Forms.Padding(3);
			this._infoPage.Size = new System.Drawing.Size(560, 323);
			this._infoPage.TabIndex = 0;
			this._infoPage.Text = "Info";
			this._infoPage.UseVisualStyleBackColor = true;
			// 
			// _infoBrowser
			// 
			this._infoBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
			this._infoBrowser.Location = new System.Drawing.Point(3, 3);
			this._infoBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this._infoBrowser.Name = "_infoBrowser";
			this._infoBrowser.Size = new System.Drawing.Size(554, 317);
			this._infoBrowser.TabIndex = 0;
			// 
			// _logPage
			// 
			this._logPage.Controls.Add(this._logBox);
			this._logPage.Location = new System.Drawing.Point(4, 22);
			this._logPage.Name = "_logPage";
			this._logPage.Padding = new System.Windows.Forms.Padding(3);
			this._logPage.Size = new System.Drawing.Size(560, 273);
			this._logPage.TabIndex = 1;
			this._logPage.Text = "Log";
			this._logPage.UseVisualStyleBackColor = true;
			// 
			// _logBox
			// 
			this._logBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this._logBox.BackColor = System.Drawing.Color.Transparent;
			this._logBox.CancelRequested = false;
			this._logBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this._logBox.ErrorEncountered = false;
			this._logBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._logBox.GetDiagnosticsMethod = null;
			this._logBox.Location = new System.Drawing.Point(3, 3);
			this._logBox.Name = "_logBox";
			this._logBox.ProgressIndicator = null;
			this._logBox.ShowCopyToClipboardMenuItem = false;
			this._logBox.ShowDetailsMenuItem = false;
			this._logBox.ShowDiagnosticsMenuItem = false;
			this._logBox.ShowFontMenuItem = false;
			this._logBox.ShowMenu = false;
			this._logBox.Size = new System.Drawing.Size(554, 267);
			this._logBox.SyncContext = windowsFormsSynchronizationContext1;
			this._logBox.TabIndex = 9;
			// 
			// _configPage
			// 
			this._configPage.Location = new System.Drawing.Point(4, 22);
			this._configPage.Name = "_configPage";
			this._configPage.Size = new System.Drawing.Size(560, 273);
			this._configPage.TabIndex = 2;
			this._configPage.Text = "Config";
			this._configPage.UseVisualStyleBackColor = true;
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 300;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// Shell
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
			this.ClientSize = new System.Drawing.Size(625, 475);
			this.Controls.Add(this._tabControl);
			this.Controls.Add(this._liftPathDisplay);
			this.Controls.Add(this.label3);
			this.Controls.Add(this._runToolButton);
			this.Controls.Add(this._toolChooser);
			this.Controls.Add(this.label1);
			this.Controls.Add(this._chooseLiftButton);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Shell";
			this.Text = "Form1";
			this._tabControl.ResumeLayout(false);
			this._infoPage.ResumeLayout(false);
			this._logPage.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _chooseLiftButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox _toolChooser;
        private System.Windows.Forms.Button _runToolButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox _liftPathDisplay;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TabControl _tabControl;
        private System.Windows.Forms.TabPage _infoPage;
        private System.Windows.Forms.TabPage _logPage;
        private Palaso.UI.WindowsForms.Progress.LogBox _logBox;
        private System.Windows.Forms.WebBrowser _infoBrowser;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabPage _configPage;
    }
}

