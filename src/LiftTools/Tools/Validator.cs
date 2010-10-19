using Palaso.Progress.LogBox;

namespace LiftTools.Tools
{
    public class Validator : Tool
    {
        public override string ToString()
        {
            return "Validate";
        }

        public override void Run(string inputLiftPath, string outputLiftPath, IProgress progress)
        {
            progress.WriteMessage("Checking..."); 
            progress.WriteMessage(LiftIO.Validation.Validator.GetAnyValidationErrors(inputLiftPath));
            progress.WriteMessage("Done");
        }
    }
}