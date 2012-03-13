using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace LiftTools.Tools
{
    public partial class MergeHomographsConfig : UserControl, MergeHomographs.IConfigView
    {
		private readonly MergeHomographs _presenter;

		public MergeHomographsConfig(MergeHomographs presenter)
		{
			_presenter = presenter;
            InitializeComponent();
        }

        public IEnumerable<string> CawlWritingSystemsToRemove()
        {
            var result = from object item in _clbMergeTraits.CheckedItems
                         select item as string;
            return result.ToList();
        }

    	public void SetCawlWritingSystems(IEnumerable<string> result)
    	{
			_clbMergeTraits.Items.Clear();
			foreach (string writingSystem in result)
			{
				_clbMergeTraits.Items.Add(writingSystem);
			}
    	}

    	public void SetDefaultCawlFilePath(string defaultCawlFilePath)
    	{
    		_dlgCawlFileChooser.InitialDirectory = Path.GetDirectoryName(defaultCawlFilePath);
    		_dlgCawlFileChooser.FileName = Path.GetFileName(defaultCawlFilePath);
    	}

    	public UserControl Control
    	{
			get { return this; }
    	}

    	public void SetTraits(IEnumerable<string> traits)
    	{
			_clbMergeTraits.Items.Clear();
			_clbMergeTraits.Items.AddRange(traits.ToArray());
    	}

    	public IEnumerable<string> GetSelectedTraits()
    	{
			var list = (from object item in _clbMergeTraits.CheckedItems select item as string);
    		return list;
    	}
    }
}
