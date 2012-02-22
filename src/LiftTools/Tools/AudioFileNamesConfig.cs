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
    public partial class AudioFileNamesConfig : UserControl
    {
    	private IWritingSystemRepository _repository;
    	private WritingSystemSetupModel _model;

        public AudioFileNamesConfig()
        {
            InitializeComponent();
			EnableWritingSystemControls(false);

        }

		private void EnableWritingSystemControls(bool state)
		{
        	_cbWritingSystem.Enabled = state;
        	_lbWritingSystem.Enabled = false; // Don't allow the dialog up yet CP 2012-02			
		}

		public void SetWritingSystemRepository(IWritingSystemRepository writingSystems)
		{
			_repository = writingSystems;
			_model = new WritingSystemSetupModel(_repository);
			_cbWritingSystem.BindToModel(_model);
			EnableWritingSystemControls(true);
		}
		
		public bool DoFindFilesFromLinkNumber
        {
            get { return _cbFindFileFromLinkNumber.Checked; }
        }

        public bool DoRenameFilesFromLink
        {
            get { return _cbRenameFile.Checked; }
        }

        public bool DoFindLinkFromFile
        {
            get { return _cbFindLinkFromFile.Checked; }
        }

        public bool DoInsertLinkFromFile
        {
            get { return _cbInsertLinkFromFile.Checked; }
        }

        public bool DoReportGoodFiles
        {
            get { return _cbReportGoodFiles.Checked; }
        }

    	public bool DoMoveRemainingFiles
    	{
			get { return _cbMoveRemainingOrphanFiles.Checked; }
    	}

    	public string WritingSystemForNewAudioLinks
    	{
			get { return _model.CurrentRFC4646; }
    	}

        public bool DoMoveDuplicates
        {
            get { return _cbDoMoveDuplicateFiles.Checked; }
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
