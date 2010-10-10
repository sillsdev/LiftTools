using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LiftTools.Properties;

namespace LiftTools
{
    public partial class Shell : Form
    {
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
            ((Tool) _toolChooser.SelectedItem).Run(_liftPathDisplay.Text, _logBox);
        }
    }
}
