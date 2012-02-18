using System;
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
            var errors = Palaso.Lift.Validation.Validator.GetAnyValidationErrors(inputLiftPath);
            if(string.IsNullOrEmpty(errors))
            {
                progress.WriteMessage("No Errors found.");
            }
            else
            {
                progress.WriteMessageWithColor("red",errors);                
                progress.WriteMessage("Done");
            }
        }

        public override string InfoPageName
        {
            get { return "Validator.htm"; }
        }
    }
}