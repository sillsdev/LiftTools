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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Shell));
            this._chooseLiftButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this._toolChooser = new System.Windows.Forms.ComboBox();
            this._runToolButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this._liftPathDisplay = new System.Windows.Forms.TextBox();
            this._logBox = new Palaso.Progress.LogBox.LogBox();
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
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "LIFT File";
            // 
            // _toolChooser
            // 
            this._toolChooser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._toolChooser.FormattingEnabled = true;
            this._toolChooser.Location = new System.Drawing.Point(92, 56);
            this._toolChooser.Name = "_toolChooser";
            this._toolChooser.Size = new System.Drawing.Size(124, 21);
            this._toolChooser.TabIndex = 2;
            // 
            // _runToolButton
            // 
            this._runToolButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(22, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
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
            // 
            // _logBox
            // 
            this._logBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._logBox.BackColor = System.Drawing.Color.Transparent;
            this._logBox.CancelRequested = false;
            this._logBox.ErrorEncountered = false;
            this._logBox.GetDiagnosticsMethod = null;
            this._logBox.Location = new System.Drawing.Point(25, 117);
            this._logBox.Name = "_logBox";
            this._logBox.Size = new System.Drawing.Size(447, 207);
            this._logBox.TabIndex = 8;
            // 
            // Shell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 336);
            this.Controls.Add(this._logBox);
            this.Controls.Add(this._liftPathDisplay);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._runToolButton);
            this.Controls.Add(this._toolChooser);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._chooseLiftButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Shell";
            this.Text = "Form1";
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
        private Palaso.Progress.LogBox.LogBox _logBox;
    }
}

