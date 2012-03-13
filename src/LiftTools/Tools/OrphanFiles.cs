using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using LiftTools.Tools.Common;
using Palaso.Code;
using Palaso.Lift.Validation;
using Palaso.Progress.LogBox;

namespace LiftTools.Tools
{
    public class OrphanFiles : Tool
    {
        private LinkAudit _linkAudit;
        private IProgress _progress;
        public override void Run(string inputLiftPath, string outputLiftPath, IProgress progress)
        {
            _linkAudit = new LinkAudit();
            _progress = progress;

            _linkAudit.RunAudit(inputLiftPath, progress);
            _linkAudit.LogReport();

            _progress.WriteMessage("DONE");
            //ValidateFile(progress, outputLiftPath);
        }

        private void ValidateFile(IProgress progress, string path)
        {
            progress.WriteMessage(""); 
            progress.WriteMessage("Validating the processed file...");
            var errors = Palaso.Lift.Validation.Validator.GetAnyValidationErrors(path, new ValidationProgress(progress),  ValidationOptions.All);
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
            return "Find Orphan Files";
        }
        public override string InfoPageName
        {
            get { return "OrphanFiles.htm"; }
        }

    }
}