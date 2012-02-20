using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Palaso.Progress.LogBox;
using Palaso.Xml;

namespace LiftTools.Tools
{
    public class Cawl : Tool
    {
		public interface IConfigView
		{
			UserControl Control { get; }
		    bool DoDeleteCawlEntries { get; }
		    void SetCawlWritingSystems(IEnumerable<string> result);
			void SetDefaultCawlFilePath(string defaultCawlFilePath);
			void SetTextCawlFilePath(string cawlFilePath);
		    IEnumerable<string> CawlWritingSystemsToRemove();
		}

        private IProgress _progress;
		private readonly IConfigView _config;
    	private List<string> _cawlWritingSystems;

    	public Cawl()
        {
            _config = new CawlConfig(this);
            ConfigControl = _config.Control;
			SetDefaultCawlFilePath();
        }

        private void SetDefaultCawlFilePath()
        {
            string defaultCawlFilePath = DefaultCawlFilePath;
            _config.SetDefaultCawlFilePath(defaultCawlFilePath);
            if (File.Exists(defaultCawlFilePath))
            {
                OnCawlFilePathChanged(defaultCawlFilePath);
            }
        }

        public string DefaultCawlFilePath
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

            using (var xmlReader = XmlReader.Create(new StreamReader(inputLiftPath, Encoding.UTF8)))
            using (var xmlWriter = XmlWriter.Create(new StreamWriter(outputLiftPath, false, Encoding.UTF8), CanonicalXmlSettings.CreateXmlWriterSettings()))
            {
			    var formsToRemove = _config.CawlWritingSystemsToRemove();
                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.LocalName == "entry")
                    {
                        var nodeReader = xmlReader.ReadSubtree();
                        nodeReader.Read(); // The initial read
                        try
                        {
                            var entryNode = XNode.ReadFrom(nodeReader) as XElement;
                            var definitionsToRemove = from node in entryNode.Descendants("form")
                                                      where
                                                          node.Attributes("lang").Any(
                                                              a => formsToRemove.Contains(a.Value)
                                                          )
                                                      select node;
                            if (_config.DoDeleteCawlEntries)
                            {
                                definitionsToRemove.Remove();
                            }
                            var glossesToRemove = from node in entryNode.Descendants("gloss")
                                                  where
                                                      node.Attributes("lang").Any(
                                                          a => formsToRemove.Contains(a.Value)
                                                      )
                                                  select node;
                            if (_config.DoDeleteCawlEntries)
                            {
                                glossesToRemove.Remove();
                            }
                            entryNode.WriteTo(xmlWriter);
                        } catch (Exception e)
                        {
                            _progress.WriteMessageWithColor("red", "Couldn't process entry because: {0}", e.Message);
                        }
                    }
                    else
                    {
                        xmlWriter.WriteNodeShallow(xmlReader);
                    }
                }
			}
            progress.WriteMessageWithColor("blue", "The processed lift is at " + outputLiftPath);
            ValidateFile(progress, outputLiftPath);
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

    	public void OnCawlFilePathChanged(string cawlFilePath)
    	{
    	    CawlFilePath = cawlFilePath;
    		XDocument doc = XDocument.Load(CawlFilePath);
    		var result = from form in doc.Descendants("form")
    		             group form by form.Attribute("lang").Value into lang
    		             where true
    		             select lang.Key;

    		_cawlWritingSystems = result.ToList();
    		_config.SetTextCawlFilePath(CawlFilePath);
			_config.SetCawlWritingSystems(_cawlWritingSystems);
    	}

        private string CawlFilePath { get; set; }

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