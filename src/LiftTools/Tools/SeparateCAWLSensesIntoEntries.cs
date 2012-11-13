using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Palaso.Code;
using Palaso.Lift.Validation;
using Palaso.Progress;

namespace LiftTools.Tools
{
    public class SeparateCAWLSensesIntoEntries : Tool
    {
        private IProgress _progress;

		public SeparateCAWLSensesIntoEntries()
		{
			ConfigControl = null;
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
                progress.WriteMessage("");
                progress.WriteMessage("Starting with {0} entries...", repo.Count);
                progress.WriteMessage("");
				Palaso.DictionaryServices.Processors.EntrySplitter.Run(repo, progress);
                progress.WriteMessage("Ended with {0} entries...", repo.Count);
            }
            progress.WriteMessageWithColor("blue", "The processed lift is at " + outputLiftPath);

            ValidateFile(progress, outputLiftPath);
        }


    	public override string ToString()
        {
			return "Separate CAWL Senses into their own Entries";
        }

        public override string InfoPageName
        {
			get { return "SeparateCAWLSensesIntoEntries.htm"; }
        }

    }
}