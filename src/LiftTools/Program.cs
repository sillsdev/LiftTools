using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LiftTools.Properties;
using LiftTools.Tools;
using Palaso.Reporting;

namespace LiftTools
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SetupErrorHandling();
            Application.Run(new Shell(new List<Tool>(new Tool[]
                                                     {
                                                         new MergeHomographs(), 
                                                         new Validator(),
                                                         new AudioFileNames(),
														 new WritingSystems(),
                                                         new OrphanFiles()
                                                     })));
            Settings.Default.Save();
        }

        private static void SetupErrorHandling()
        {
            ErrorReport.EmailAddress = "hide@gmail.com".Replace("hide","hattonjohn");
            ErrorReport.AddStandardProperties();
            ExceptionHandler.Init();

            //we don't have a google analytics yet. UsageReporter.Init();
        }

    }
}
