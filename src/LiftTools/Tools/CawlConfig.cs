using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace LiftTools.Tools
{
    public partial class CawlConfig : UserControl, Cawl.IConfigView
    {
    	private readonly Cawl _presenter;

    	public CawlConfig(Cawl presenter)
		{
			_presenter = presenter;
            InitializeComponent();
        }

        public bool DoReportWritingSystemsInUse
        {
            get { return _cbReportWritingSystemsInUse.Checked; }
        }

    	public bool DoDeleteCawlEntries
    	{
			get { return _cbDeleteCawlEntries.Checked; }
    	}

		private void OnCawlFileChooser_Click(object sender, EventArgs e)
		{
			var result = _dlgCawlFileChooser.ShowDialog();
			if (result == DialogResult.OK)
			{
				CawlFilePath = _dlgCawlFileChooser.FileName;
				_presenter.OnCawlFilePathChanged();
			}

		}

    	public string CawlFilePath { get; private set; }
    	public void SetCawlWritingSystems(IEnumerable<string> result)
    	{
			_clbCawlWritingSystems.Items.Clear();
			foreach (string writingSystem in result)
			{
				_clbCawlWritingSystems.Items.Add(writingSystem);
			}
    	}

    	public void SetDefaultCawlFilePath(string defaultCawlFilePath)
    	{
    		_dlgCawlFileChooser.InitialDirectory = Path.GetDirectoryName(defaultCawlFilePath);
    		_dlgCawlFileChooser.FileName = Path.GetFileName(defaultCawlFilePath);
    	}

    	public void SetTextCawlFilePath(string cawlFilePath)
    	{
    		_tbCawlFilePath.Text = cawlFilePath;
    	}

    	public UserControl Control
    	{
			get { return this; }
    	}
    }
}
