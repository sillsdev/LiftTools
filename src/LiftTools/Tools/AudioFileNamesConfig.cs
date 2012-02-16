using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LiftTools.Tools
{
    public partial class AudioFileNamesConfig : UserControl
    {
        public AudioFileNamesConfig()
        {
            InitializeComponent();
        	_lbWritingSystem.Visible = false;
        	_comWritingSystem.Visible = false;
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
			get { return "qaa-x-audio"; } // TODO Sort out the writing systems control CP 2012-02
    	}

    }
}
