using System;
using System.Windows.Forms;
using Palaso.Lift.Validation;
using Palaso.Progress;

namespace LiftTools.Tools
{
    /// <summary>
    /// Tools are things which process a dictionary.  They are all exposed via the "Lift Tools"
    /// application (http://projects.palaso.org/projects/show/lifttools), but live here
    /// because other clients may wish to offer them, too.
    /// </summary>
    public abstract class Tool
    {
        public abstract void Run(string inputLiftPath, string outputLiftPath, IProgress progress);

        public abstract string InfoPageName { get; }

		public virtual void OnLiftFilePathChanged(string liftFilePath)
		{
		}
		
		public UserControl ConfigControl { get; protected set; }


    	public static void ValidateFile(IProgress progress, string path)
    	{
    		progress.WriteMessage("");
    		progress.WriteMessage("Validating the processed file...");
    		var errors = Palaso.Lift.Validation.Validator.GetAnyValidationErrors(path, new ValidationProgress(progress), ValidationOptions.All);
    		if (String.IsNullOrEmpty(errors))
    		{
    			progress.WriteMessage("No Errors found.");
    		}
    		else
    		{
    			progress.WriteMessageWithColor("red", errors);
    			progress.WriteMessage("Done");
    		}
    	}
    }
}