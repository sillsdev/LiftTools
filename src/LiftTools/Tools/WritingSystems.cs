using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using LiftTools.Tools.Common;
using Palaso.IO;
using Palaso.Lift.Validation;
using Palaso.Progress.LogBox;
using Palaso.WritingSystems;
using Palaso.WritingSystems.Migration.WritingSystemsLdmlV0To1Migration;

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
                }
				foreach (var writingSystem in repository.AllWritingSystems)
				{
					if (!Links.ContainsKey(writingSystem.Bcp47Tag))
					{
						Links.Add(writingSystem.Bcp47Tag, LinkInfo.CreateFromFile(writingSystem.Bcp47Tag));
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
    	    //_writingSystemAudit.LogReport();

    	    if (_config.DoRename)
    	    {
    	        File.Copy(inputLiftPath, outputLiftPath, true);
    	    }

    	    // Create any missing writing systems.
    	    var missingWritingSystems = from info in _writingSystemAudit.Links
    	                                where info.Value.LinkFound && !info.Value.FileFound
    	                                select info.Value;

    	    if (missingWritingSystems.Count() > 0)
            {
                _progress.WriteMessageWithColor("blue", "Creating missing writing systems:");
                foreach (var lang in missingWritingSystems)
    	        {
                    _progress.WriteMessage("  {0}", lang.FileName);
    	            var ws = WritingSystemDefinition.Parse(lang.FileName);
    	            _writingSystems.Set(ws);
    	        }
    	        _writingSystems.Save();
    	    }

            // Report on languages in use, and possible renames
    		_progress.WriteMessageWithColor("blue", "Writing Systems in use:");
			var langInUse = from info in _writingSystemAudit.Links
    		                where info.Value.LinkFound 
    		                select info.Value;

    		var langToRename = new List<string>();
			var langRegex = new Regex(_config.RenameWritingSystemFrom, RegexOptions.IgnoreCase);
			foreach (var lang in langInUse)
			{
				bool matched = false;
				if (!String.IsNullOrEmpty(_config.RenameWritingSystemFrom))
				{
					var match = langRegex.Match(lang.FileName);
					matched = match.Success;
				}
				if (!matched)
				{
					if (_config.DoReportWritingSystemsInUse)
					{
						_progress.WriteMessage("  {0}", lang.FileName);
					}
				}
				else
				{
					langToRename.Add(lang.FileName);
					_progress.WriteMessage(
						_config.DoRename ? "  {0} RENAME TO {1}" : "  {0} COULD RENAME TO {1}", lang.FileName, _config.RenameWritingSystemTo
					);
				}
			}
    		_progress.WriteMessage("\n");

			// Report and / or do renames);
			if (_config.DoRename)
			{
				_progress.WriteMessageWithColor("blue", "Renaming Writing Systems:");
				if (String.IsNullOrEmpty(_config.RenameWritingSystemTo))
				{
					_progress.WriteMessageWithColor("red", "Warning: Replacement 'To' Writing System not set.");
				} else
				{
					foreach (var lang in langToRename)
					{
						_progress.WriteMessage("Renaming {0} to {1}...", lang, _config.RenameWritingSystemTo);
						string search = String.Format("lang=\"{0}\"", lang);
						string replace = String.Format("lang=\"{0}\"", _config.RenameWritingSystemTo);
						FileUtils.GrepFile(outputLiftPath, search, replace);
					}
				}
				if (_config.DoCopyWhenDone)
				{
					File.Copy(outputLiftPath, inputLiftPath, true);
					_progress.WriteMessageWithColor("blue", "Rescanning after rename...");
					_writingSystemAudit.RunAudit(inputLiftPath, _writingSystems, _progress);
					
				}
				_progress.WriteMessage("\n");
			}

			// Report and / or delete unused LDML files
			_progress.WriteMessageWithColor("blue", "Unused Writing Systems:");
    		var unusedWritingSystems = from info in _writingSystemAudit.Links
    		                           where !info.Value.LinkFound && info.Value.FileFound
    		                           select info.Value;
			foreach (var lang in unusedWritingSystems)
			{
				if (_config.DoDeleteUnusedLdmlFiles)
				{
					_writingSystems.Remove(lang.FileName);
					_progress.WriteMessage("  {0} DELETED", lang.FileName);
				} else
				{
					_progress.WriteMessage("  {0}", lang.FileName);
				}
			}
    		_progress.WriteMessage("\n");

			if (_config.DoRename || _config.DoDeleteUnusedLdmlFiles)
			{
				progress.WriteMessageWithColor("blue", "The processed lift is at " + outputLiftPath);
				ValidateFile(progress, outputLiftPath);
			} else
			{
				_progress.WriteMessageWithColor("blue", "Done");
			}
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

        private static void ValidateFile(IProgress progress, string path)
        {
            progress.WriteMessage(""); 
            progress.WriteMessage("Validating the processed file...");
			var errors = Palaso.Lift.Validation.Validator.GetAnyValidationErrors(path, ValidationOptions.All);
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