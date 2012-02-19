using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Palaso.Progress.LogBox;

namespace LiftTools.Tools
{
    public class Cawl : Tool
    {
		public interface IConfigView
		{
			UserControl Control { get; }
			string CawlFilePath { get; }
			void SetCawlWritingSystems(IEnumerable<string> result);
			void SetDefaultCawlFilePath(string defaultCawlFilePath);
			void SetTextCawlFilePath(string cawlFilePath);
		}

        private IProgress _progress;
		private readonly IConfigView _config;
    	private List<string> _cawlWritingSystems;

    	public Cawl()
        {
            _config = new CawlConfig(this);
            ConfigControl = _config.Control;
			_config.SetDefaultCawlFilePath(defaultCawlFilePath);
        }

    	public string defaultCawlFilePath
    	{
    		get
    		{
    			string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "WeSay");
    			path = Path.Combine(path, "Common");
    			path = Path.Combine(path, "WordPacks");
    			path = Path.Combine(path, "SILCAWL");
    			path = Path.Combine(path, "SILCAWL.lift");
    			return path;
    		}
    	}

    	public override void Run(string inputLiftPath, string outputLiftPath, IProgress progress)
        {
            _progress = progress;

            CheckEnvironment();

			using (var readStream = new StreamReader(inputLiftPath, Encoding.UTF8))
			using (var writeStream = new StreamWriter(outputLiftPath, false, Encoding.UTF8))
			{
				var xmlReader = XmlReader.Create(readStream);
				var xmlWriter = XmlWriter.Create(writeStream, Palaso.Xml.CanonicalXmlSettings.CreateXmlWriterSettings());
				// TODO read and write same, checking for gloss / definition along the way
			}


        }

        private void CheckEnvironment()
        {
			if (_cawlWritingSystems == null)
			{
				throw new ApplicationException(
					"CAWL file name has not been set. Check out the config tab, and select a CAWL file."
				);
			}
        }

        private static void ValidateFile(IProgress progress, string path)
        {
            progress.WriteMessage(""); 
            progress.WriteMessage("Validating the processed file...");
			var errors = Palaso.Lift.Validation.Validator.GetAnyValidationErrors(path);
            if (string.IsNullOrEmpty(errors))
            {
                progress.WriteMessage("No Errors found.");
            }
            else
            {
                progress.WriteMessageWithColor("red", errors);
                progress.WriteMessage("Done");
            }
        }

    	public void OnCawlFilePathChanged()
    	{
    		string cawlFilePath = _config.CawlFilePath;
    		XDocument doc = XDocument.Load(cawlFilePath);
    		var result = from form in doc.Descendants("form")
    		             group form by form.Attribute("lang").Value into lang
    		             where true
    		             select lang.Key;

    		_cawlWritingSystems = result.ToList();
    		_config.SetTextCawlFilePath(cawlFilePath);
			_config.SetCawlWritingSystems(_cawlWritingSystems);
    	} 
		
		public override string ToString()
        {
            return "CAWL Remove Gloss and Definition";
        }
        public override string InfoPageName
        {
            get { return "Cawl.htm"; }
        }


    }
}