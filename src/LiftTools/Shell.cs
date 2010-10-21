using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using LiftTools.Properties;
using LiftTools.Tools;
using Palaso.IO;

namespace LiftTools
{
    public partial class Shell : Form
    {
        private Tool _currentTool;

        public Shell(IEnumerable<Tool> tools)
        {
            InitializeComponent();

            _liftPathDisplay.Text = Settings.Default.LiftPath;

            foreach (var tool in tools)
            {
                _toolChooser.Items.Add(tool);
            }
            if(_toolChooser.Items.Count>0)
            {
                _toolChooser.SelectedIndex = 0;
            }
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);

            SetWindowText();
        }

        void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            UpdateDisplay();
        }


        private void SetWindowText()
        {
            var ver = Assembly.GetExecutingAssembly().GetName().Version;
            Text = string.Format("LIFT Tools: {0}.{1}.{2}", ver.Major, ver.Minor, ver.Build);
        }

        private void _chooseLiftButton_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "Lexicon Interchange Format (*.LIFT)|*.LIFT";
                dlg.RestoreDirectory = true;
                if (System.Windows.Forms.DialogResult.OK != dlg.ShowDialog())
                    return;
                Settings.Default.LiftPath = dlg.FileName;
                _liftPathDisplay.Text = dlg.FileName;
            }
        }

        private void UpdateDisplay()
        {
            _toolChooser.Enabled = _liftPathDisplay.Enabled = !backgroundWorker1.IsBusy;
            _runToolButton.Enabled = !backgroundWorker1.IsBusy && File.Exists(_liftPathDisplay.Text);
        }

        private void _runToolButton_Click(object sender, EventArgs e)
        {
            _logBox.Clear();
            tabControl1.SelectedTab = _logPage;
            backgroundWorker1.RunWorkerAsync();
        }

        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var processedFile = Path.Combine(Path.GetDirectoryName(_liftPathDisplay.Text), Path.GetFileNameWithoutExtension(_liftPathDisplay.Text)+".processed"+".lift");
            try
            {
                _currentTool.Run(_liftPathDisplay.Text, processedFile, _logBox);
               
            }
            catch (Exception error)
            {
                _logBox.WriteException(error);
            }    
        }

        private void _toolChooser_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentTool = ((Tool)_toolChooser.SelectedItem);
            tabControl1.SelectedTab = _infoPage;
            _infoBrowser.Navigate(FileLocator.GetFileDistributedWithApplication(_currentTool.InfoPageName));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateDisplay();
        }
    }
}
