using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Palaso.UI.WindowsForms.WritingSystems;
using Palaso.WritingSystems;

namespace LiftTools.Tools
{
    public partial class WritingSystemsConfig : UserControl
    {
    	private IWritingSystemRepository _repository;
    	private WritingSystemSetupModel _model;

		public WritingSystemsConfig()
        {
            InitializeComponent();
			EnableWritingSystemControls(false);
        }

		private void EnableWritingSystemControls(bool state)
		{
        	_tbWritingSystemFrom.Enabled = state;
			_cbWritingSystemTo.Enabled = state;
        	_lbWritingSystem.Enabled = false; // Don't allow the dialog up yet CP 2012-02			
		}

		public void SetWritingSystemRepository(IWritingSystemRepository writingSystems)
		{
			_repository = writingSystems;
			_model = new WritingSystemSetupModel(_repository);
			_cbWritingSystemTo.BindToModel(_model);
			EnableWritingSystemControls(true);
		}

        public bool DoReportWritingSystemsInUse
        {
            get { return _cbReportWritingSystemsInUse.Checked; }
        }

        public bool DoRename
        {
            get { return _cbDoRename.Checked; }
        }

    	public bool DoDeleteUnusedLdmlFiles
    	{
			get { return _cbDeleteUnusedWritingSystems.Checked; }
    	}

    	public string RenameWritingSystemFrom
    	{
			get { return _tbWritingSystemFrom.Text; }
    	}

    	public string RenameWritingSystemTo
    	{
			get { return _model.CurrentRFC4646; }
    	}

		private void WritingSystem_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			var dlg = new WritingSystemSetupDialog(_model);
			var result = dlg.ShowDialog();
			if (result == DialogResult.OK)
			{
				_repository.Save();
			}
		}

    }
}
