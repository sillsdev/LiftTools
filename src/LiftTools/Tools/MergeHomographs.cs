using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Palaso.Code;
using Palaso.Progress;

namespace LiftTools.Tools
{
    public class MergeHomographs : Tool
    {
		public interface IConfigView
		{
			UserControl Control { get; }
			void SetTraits(IEnumerable<string> traits);
			IEnumerable<string> GetSelectedTraits();

		}

    	private IConfigView _config;
        private IProgress _progress;

		public MergeHomographs()
		{
			var config = new MergeHomographsConfig(this);
			_config = config;
			ConfigControl = config;
		}

        public override void Run(string inputLiftPath, string outputLiftPath, IProgress progress)
        {
            _progress = progress;

            //note, we actually just make the output file and open it, and then save any changes on top of it.
            if (inputLiftPath != outputLiftPath)
            {
                File.Copy(inputLiftPath, outputLiftPath, true);
            }
            RequireThat.File(outputLiftPath).Exists();

            using (var repo = new Palaso.DictionaryServices.LiftLexEntryRepository(outputLiftPath))
            {
                var writingSystemForMatching =   Palaso.DictionaryServices.Processors.HomographMerger.GuessPrimarLexicalFormWritingSystem(repo, progress);
                if(writingSystemForMatching==null)
                {
                    progress.WriteError("Sorry, something's different about this dictionary... could not determine a primary writing system for lexical forms.  Does it even have lexical forms, in the first hundred or so words?");
                    return;
                }
                progress.WriteMessage("");
                progress.WriteMessage("Starting with {0} entries...", repo.Count);
                progress.WriteMessage("");
                Palaso.DictionaryServices.Processors.HomographMerger.Merge(repo, writingSystemForMatching, _config.GetSelectedTraits().ToArray(), progress);
                progress.WriteMessage("Ended with {0} entries...", repo.Count);
            }
            progress.WriteMessageWithColor("blue", "The processed lift is at " + outputLiftPath);

            ValidateFile(progress, outputLiftPath);
        }


		public override void OnLiftFilePathChanged(string liftFilePath)
		{
			UpdateTraits(liftFilePath);
		}

    	private void UpdateTraits(string liftFilePath)
    	{
    		var traits = new HashSet<string>();
    		using (var xmlReader = XmlReader.Create(new StreamReader(liftFilePath, Encoding.UTF8)))
    		{
    			while (xmlReader.Read())
    			{
    				if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.LocalName == "entry")
    				{
    					var nodeReader = xmlReader.ReadSubtree();
    					nodeReader.Read(); // The initial read
    					try
    					{
    						var entryNode = XNode.ReadFrom(nodeReader) as XElement;
    						var traitsInEntry = from node in entryNode.Descendants("trait")
    						                    select node.Attribute("name").Value;
    						foreach (var trait in traitsInEntry)
    						{
    							traits.Add(trait);
    						}
    					}
    					catch (Exception e)
    					{
    						_progress.WriteMessageWithColor("red", "Couldn't process entry because: {0}", e.Message);
    					}
    				}
    			}
    		}
    		var knownTraits = new[] {"semantic-domain-ddp4"};
			foreach (var trait in knownTraits.Where(trait => traits.Contains(trait)))
			{
				traits.Remove(trait);
			}

    		_config.SetTraits(traits);
    	}

    	public override string ToString()
        {
            return "Merge Homographs";
        }

        public override string InfoPageName
        {
            get { return "MergeHomographs.htm"; }
        }
    }
}