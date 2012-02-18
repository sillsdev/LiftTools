﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using LiftTools.Tools.Common;
using Palaso.Progress.LogBox;
using Palaso.WritingSystems;
using Palaso.WritingSystems.Migration.WritingSystemsLdmlV0To1Migration;
using Palaso.Xml;

namespace LiftTools.Tools
{
    public class WritingSystems : Tool
    {
		private class WritingSystemAudit
		{
			public class LinkInfo
			{
				private LinkInfo()
				{
				}

				public static LinkInfo CreateFromLink(string link)
				{
					return new LinkInfo
						{
							FileName = link,
							FileFound = false,
							LinkFound = true
						};
				}

				public static LinkInfo CreateFromFile(string fileName)
				{
					return new LinkInfo
					{
						FileName = fileName,
						FileFound = true,
						LinkFound = false
					};
				}

				public string FileName { get; private set; }
				public bool FileFound { get; set; }
				public bool LinkFound { get; set; }

			}

			private IProgress _progress;

			public Dictionary<string, LinkInfo> Links { get; private set; }

			public void RunAudit(string inputLiftPath, IWritingSystemRepository repository, IProgress progress)
			{
				_progress = progress;
				Links = new Dictionary<string, LinkInfo>();

				var langRegEx = new Regex(@"lang=""(.*)""", RegexOptions.IgnoreCase);
				using (var reader = new StreamReader(inputLiftPath))
				{
					while (!reader.EndOfStream)
					{
						string line = reader.ReadLine();
						if (string.IsNullOrEmpty(line)) continue;

						var match = langRegEx.Match(line);
						if (match.Success)
						{
							string langRef = match.Groups[1].Value;
							if (!String.IsNullOrEmpty(langRef))
							{
								// Images may be used more than once.
								if (!Links.ContainsKey(langRef))
								{
									Links.Add(langRef, LinkInfo.CreateFromLink(langRef));
								}
							}
						}
					}
				}
				CheckWritingSystems(inputLiftPath, repository);
			}

			private void CheckWritingSystems(string liftFilePath, IWritingSystemRepository repository)
			{
				foreach (var writingSystemPair in Links)
				{
					var writingSystemInfo = writingSystemPair.Value;
					if (repository.Contains(writingSystemInfo.FileName))
					{
						writingSystemInfo.FileFound = true;
					}
					// Just check the actual file is the same name, which it should be
					string ldmlFilePath = Path.Combine(
						LiftProjectInfo.WritingSystemPath(liftFilePath),
						writingSystemInfo.FileName + ".ldml"
					);
					if (!File.Exists(ldmlFilePath))
					{
						_progress.WriteMessageWithColor("red", "Writing System in repository '{0}', but file not found", writingSystemInfo.FileName);
					}
				}
				foreach (var writingSystem in repository.AllWritingSystems)
				{
					if (!Links.ContainsKey(writingSystem.Bcp47Tag))
					{
						Links.Add(writingSystem.Bcp47Tag, LinkInfo.CreateFromFile(writingSystem.Bcp47Tag));
					}
				}
			}

			public void LogReport()
			{
				_progress.WriteMessage("Writing Systems in data:");
				foreach (var linkInfoPair in Links)
				{
					var linkInfo = linkInfoPair.Value;
					if (linkInfo.LinkFound)
					{
						_progress.WriteMessage("  {0}", linkInfo.FileName);
					}
				}
				_progress.WriteMessage("\n");

				_progress.WriteMessage("Writing Systems in data with no LDML:");
				foreach (var linkInfoPair in Links)
				{
					var linkInfo = linkInfoPair.Value;
					if (linkInfo.LinkFound && !linkInfo.FileFound)
					{
						_progress.WriteMessage("  {0}", linkInfo.FileName);
					}
				}
				_progress.WriteMessage("\n");
				_progress.WriteMessage("Unused Writing Systems:");
				foreach (var linkInfoPair in Links)
				{
					var linkInfo = linkInfoPair.Value;
					if (!linkInfo.LinkFound && linkInfo.FileFound)
					{
						_progress.WriteMessage("  {0}", linkInfo.FileName);
					}
				}
			}
		}

        private WritingSystemAudit _writingSystemAudit;
        private IProgress _progress;
		private readonly WritingSystemsConfig _config;
    	private IWritingSystemRepository _writingSystems;
    	private IEnumerable<WritingSystemRepositoryProblem> _loadProblems;
    	private IEnumerable<LdmlVersion0MigrationStrategy.MigrationInfo> _migrationInfo;

		public WritingSystems()
        {
            _config = new WritingSystemsConfig();
            ConfigControl = _config;
        }

		public override void OnLiftFilePathChanged(string liftFilePath)
		{
			string writingSystemPath = LiftProjectInfo.WritingSystemPath(liftFilePath);
			_writingSystems = LdmlInFolderWritingSystemRepository.Initialize(writingSystemPath, OnMigration, OnLoadProblem);
			_config.SetWritingSystemRepository(_writingSystems);
		}

    	private void OnLoadProblem(IEnumerable<WritingSystemRepositoryProblem> problems)
    	{
    		_loadProblems = problems;
    	}

    	private void OnMigration(IEnumerable<LdmlVersion0MigrationStrategy.MigrationInfo> migrationinfo)
    	{
    		_migrationInfo = migrationinfo;
    	}

    	public override void Run(string inputLiftPath, string outputLiftPath, IProgress progress)
        {
            _writingSystemAudit = new WritingSystemAudit();
            _progress = progress;

            CheckEnvironment(inputLiftPath);

			if (_loadProblems != null)
			{
				foreach (var problem in _loadProblems)
				{
					_progress.WriteMessageWithColor(
						"red", "Writing System Problem: [{0}] {1}", problem.FilePath, problem.Exception.Message);
				}
			}

    		_writingSystemAudit.RunAudit(inputLiftPath, _writingSystems, progress);
			_writingSystemAudit.LogReport();

			progress.WriteMessageWithColor("blue", "DONE");
			//progress.WriteMessageWithColor("blue", "The processed lift is at " + outputLiftPath);
			//ValidateFile(progress, outputLiftPath);
        }

        private static void CheckEnvironment(string liftFilePath)
        {
            string writingSystemPath = LiftProjectInfo.WritingSystemPath(liftFilePath);
            if (!Directory.Exists(writingSystemPath))
            {
                throw new ApplicationException(
                    String.Format("The path for Writing System files could not be found. Expecting '{0}' to exist.", writingSystemPath)
                );
            }
        }

        private void ValidateFile(IProgress progress, string path)
        {
            progress.WriteMessage(""); 
            progress.WriteMessage("Validating the processed file...");
			var errors = Palaso.Lift.Validation.Validator.GetAnyValidationErrors(path);
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
            return "Writing Systems";
        }
        public override string InfoPageName
        {
            get { return "WritingSystems.htm"; }
        }

    }
}