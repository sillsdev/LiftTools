using System.IO;
using Palaso.Code;
using Palaso.Progress.LogBox;

namespace LiftTools.Tools
{
    public class MergeHomographs : Tool
    {
        private IProgress _progress;
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

                progress.WriteMessage("");
                progress.WriteMessage("Starting with {0} entries...", repo.Count);
                progress.WriteMessage("");
                Palaso.DictionaryServices.Processors.HomographMerger.Merge(repo, writingSystemForMatching, progress);
                progress.WriteMessage("Ended with {0} entries...", repo.Count);
            }
            progress.WriteMessageWithColor("blue", "The processed lift is at " + outputLiftPath);

            ValidateFile(progress, outputLiftPath);
        }

        private void ValidateFile(IProgress progress, string path)
        {
            progress.WriteMessage(""); 
            progress.WriteMessage("Validating the processed file...");
            var errors = LiftIO.Validation.Validator.GetAnyValidationErrors(path);
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