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

            SetWindowText();
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

        private void _runToolButton_Click(object sender, EventArgs e)
        {
            _currentTool = ((Tool)_toolChooser.SelectedItem);
            _logBox.Clear();
            backgroundWorker1.RunWorkerAsync();
        }

        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var processedFile = Path.Combine(Path.GetDirectoryName(_liftPathDisplay.Text), Path.GetFileNameWithoutExtension(_liftPathDisplay.Text)+".processed"+".lift");
            try
            {
                _currentTool.Run(_liftPathDisplay.Text, processedFile, _logBox);
                _logBox.WriteMessage("The processed lift is at " + processedFile);
            }
            catch (Exception error)
            {
                _logBox.WriteException(error);
            }    
        }
    }
}
