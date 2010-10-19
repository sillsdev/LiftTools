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
        }

  
        public override string ToString()
        {
            return "Merge Homographs";
        }
    }
}