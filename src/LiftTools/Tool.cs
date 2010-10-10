using System;
using Palaso.Progress.LogBox;

namespace LiftTools
{
    public abstract class  Tool
    {
        public abstract void Run(string path, Palaso.Progress.LogBox.IProgress progress);
    }

    public class Validator : Tool
    {
        public override string ToString()
        {
            return "Validate";
        }

        public override void Run(string path, IProgress progress)
        {
            progress.WriteMessage("Checking..."); 
            progress.WriteMessage(LiftIO.Validation.Validator.GetAnyValidationErrors(path));
            progress.WriteMessage("Done");
        }
    }
}