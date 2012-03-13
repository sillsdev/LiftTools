using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using LiftTools.Tools.Common;
using Palaso.Lift.Validation;
using Palaso.Progress.LogBox;
using Palaso.WritingSystems;
using Palaso.WritingSystems.Migration.WritingSystemsLdmlV0To1Migration;
using Palaso.Xml;

namespace LiftTools.Tools
{
    public class AudioFileNames : Tool
    {
        private LinkAudit _linkAudit;
        private IProgress _progress;
        private readonly AudioFileNamesConfig _config;
    	private IWritingSystemRepository _writingSystems;
    	private IEnumerable<WritingSystemRepositoryProblem> _loadProblems;
    	private IEnumerable<LdmlVersion0MigrationStrategy.MigrationInfo> _migrationInfo;

    	public AudioFileNames()
        {
            _config = new AudioFileNamesConfig();
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
            _linkAudit = new LinkAudit();
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

    		_linkAudit.RunAudit(inputLiftPath, progress);

            var audioFiles = _linkAudit.Links.Where(
                x => x.Value.Type == LinkAudit.LinkInfo.Types.Audio
            );

            if (_config.DoFindFilesFromLinkNumber)
            {
                _progress.WriteMessage("LINK OK and FILE NOT FOUND");
                foreach (var infoPair in audioFiles.Where(x => x.Value.LinkFound && !x.Value.FileFound))
                {
                    var info = infoPair.Value;
                    FindAudioFile(inputLiftPath, info.FileName);
                }
                _progress.WriteMessage("\n");
            }

            if (_config.DoFindLinkFromFile)
            {
                _progress.WriteMessage("FILE OK and LINK NOT FOUND");
                using (var reader = new StreamReader(inputLiftPath))
                {
                    XDocument doc = XDocument.Load(reader);
                    foreach (var infoPair in audioFiles.Where(x => !x.Value.LinkFound && x.Value.FileFound))
                    {
                        var info = infoPair.Value;
                        FindWordForFile(doc, info.FileName);
                    }
                    _progress.WriteMessage("\n");
                    if (_config.DoInsertLinkFromFile)
                    {
                        using (var writer = XmlWriter.Create(
                            new StreamWriter(outputLiftPath, false, Encoding.UTF8),
                            CanonicalXmlSettings.CreateXmlWriterSettings()
                            ))
                        {
                            doc.WriteTo(writer);
                        }
                    }
                }
            }

            _progress.WriteMessage("LINK OK, FILE NOT FOUND. LOOK FOR DUPLICATES");
            // See if the numeric part of the filename exists in the list, in this case the orphan
            // file is a duplicate and can be safely deleted.
            var foundFiles = audioFiles.Where(x => x.Value.FileFound && x.Value.LinkFound);
            foreach (var infoPairDuplicate in audioFiles.Where(x => x.Value.FileFound && !x.Value.LinkFound))
            {
                var infoDuplicate = infoPairDuplicate.Value;
                string[] parts = infoDuplicate.FileName.Split(new[] {'-'}, 2);
                string[] numericParts = parts[1].Split(new[] {'.'}, 2);
                string numericPart = numericParts[0];
                var found = foundFiles.Where(x => x.Value.FileName.Contains(numericPart));
                foreach (var link in found)
                {
                    _progress.WriteMessage(
                        "  FOUND link for '{0}' it matches '{1}'",
                        infoDuplicate.FileName,
                        link.Value.FileName
                        );
                    //infoDuplicate.LinkFound = true;
                    if (_config.DoMoveDuplicates)
                    {
                        string duplicatePath = Path.Combine(LiftProjectInfo.AudioPath(inputLiftPath), "DuplicateFiles");
                        if (!Directory.Exists(duplicatePath))
                        {
                            Directory.CreateDirectory(duplicatePath);
                        }
                        string audioFilePath = LiftProjectInfo.AudioFilePath(inputLiftPath, infoDuplicate.FileName);
                        string duplicateFilePath = Path.Combine(duplicatePath, infoDuplicate.FileName);
                        try
                        {
                            File.Move(audioFilePath, duplicateFilePath);
                            _progress.WriteMessage("    MOVED duplicate file '{0}'", infoDuplicate.FileName);
                        }
                        catch (IOException e)
                        {
                            _progress.WriteMessageWithColor(
                                "red",
                                "    Could not move '{0}' because: {1}",
                                infoDuplicate.FileName,
                                e.Message
                                );
                        }
                    }
                }
            }
            _progress.WriteMessage("\n");

    	    // Final orphan file processing
            _progress.WriteMessage("LINK OK, FILE NOT FOUND. REMAINING ORPHAN FILES");
            string orphanPath = Path.Combine(LiftProjectInfo.AudioPath(inputLiftPath), "OrphanFiles");
			foreach (var infoPair in audioFiles.Where(x => x.Value.FileFound && !x.Value.LinkFound))
			{
				var info = infoPair.Value;
				if (_config.DoMoveRemainingFiles)
				{
					if (!Directory.Exists(orphanPath))
					{
						Directory.CreateDirectory(orphanPath);
					}
					string audioFilePath = LiftProjectInfo.AudioFilePath(inputLiftPath, info.FileName);
					string orphanFilePath = Path.Combine(orphanPath, info.FileName);
					try
					{
						File.Move(audioFilePath, orphanFilePath);
						_progress.WriteMessage("  MOVED ORPHANED FILE '{0}'", info.FileName);
					}
					catch (IOException e)
					{
						_progress.WriteMessageWithColor(
							"red", 
							"  Could not move '{0}' because: {1}", 
							info.FileName, 
							e.Message
						);
					}
				} else
				{
					_progress.WriteMessage("  ORPHANED FILE '{0}'", info.FileName);
				}
			}
			_progress.WriteMessage("\n");

            if (_config.DoReportGoodFiles)
            {
                _progress.WriteMessage("LINK OK and FILE OK");
                foreach (var infoPair in audioFiles.Where(x => x.Value.LinkFound && x.Value.FileFound))
                {
                    var info = infoPair.Value;
                    _progress.WriteMessage("  FOUND '{0}'", info.FileName);
                }
                _progress.WriteMessage("\n");
            }

            if (_config.DoInsertLinkFromFile)
            {
                progress.WriteMessageWithColor("blue", "The processed lift is at " + outputLiftPath);
                ValidateFile(progress, outputLiftPath);
            }
            progress.WriteMessageWithColor("blue", "Done");
        }

        private void FindWordForFile(XDocument doc, string fileName)
        {
            string searchTerm = fileName;
            if (searchTerm.Contains("-"))
            {
                searchTerm = searchTerm.Split(new[] {'-'}, 2)[0];
            }
            var entries = from entry in doc.Descendants("entry")
                          where entry.Descendants("text").Any(x => (x.Value == searchTerm))
                          select entry;
			foreach (var entry in entries)
			{
				var idAttribute = entry.Attribute("id");
				if (idAttribute != null)
				{
					_linkAudit.Links[fileName].LinkFound = true;
					if (_config.DoInsertLinkFromFile)
					{
						_progress.WriteMessage("  ADDED '{0}' to Entry Id '{1}'", fileName, idAttribute.Value);

						var newForm = new XElement("form");
						newForm.Add(new XAttribute("lang", _config.WritingSystemForNewAudioLinks));
						newForm.Add(new XElement("text", fileName));

						var lexicalUnit = entry.Descendants("lexical-unit").First();
						if (lexicalUnit == null)
						{
							_progress.WriteMessageWithColor("red", "  ERROR Entry '{0}' has no lexical-unit", idAttribute.Value);
							continue;
						}
						lexicalUnit.Add(newForm);
					} else
					{
						_progress.WriteMessage("  COULD ADD '{0}' to Entry Id '{1}'", fileName, idAttribute.Value);
					}
				}
				else
				{
					_progress.WriteMessageWithColor("red", "  ERROR Entry with no id attribute");
				}
			}

        }

        private static void CheckEnvironment(string liftFilePath)
        {
			string audioPath = LiftProjectInfo.AudioPath(liftFilePath);
            if (!Directory.Exists(audioPath))
            {
                throw new ApplicationException(
                    String.Format("The path for audio files could not be found. Expecting '{0}' to exist.", audioPath)
                );
            }
        }

        private void FindAudioFile(string liftFilePath, string fileName)
        {
			string audioFilePath = LiftProjectInfo.AudioFilePath(liftFilePath, fileName);
            if (File.Exists(audioFilePath)) // It really shouldn't if LinkAudit is doing it's job.
            {
                return;
            }
            // It doesn't exist. See if we can find a file that matches the numeric part of the filename.
			string audioPath = LiftProjectInfo.AudioPath(liftFilePath);
            var regex = new Regex(".*-(.*)\\.wav", RegexOptions.IgnoreCase);
            var match = regex.Match(fileName);
            if (!match.Success)
            {
                _progress.WriteMessageWithColor("red", "  Could not use filename '{0}' to match", fileName);
                return;
            }
            string searchPattern = String.Format("*{0}.wav", match.Groups[1].Value);
            var fileNamesFound = Directory.GetFiles(audioPath, searchPattern, SearchOption.TopDirectoryOnly);
            if (fileNamesFound.Length == 0)
            {
                _progress.WriteMessageWithColor("red", "  NOT FOUND '{0}'", fileName);
                return;
            }
            if (fileNamesFound.Length > 1)
            {
                _progress.WriteMessageWithColor("red", "  Multiple matches for '{0}'", fileName);
                return;
            }
            string foundFilePath = fileNamesFound[0];
            string foundFileName = Path.GetFileName(foundFilePath);
            if (_config.DoRenameFilesFromLink)
            {
                File.Move(foundFilePath, audioFilePath);
                _progress.WriteMessage("  MOVED '{0}' from '{1}'", fileName, foundFileName);
            } else
            {
                _progress.WriteMessage("  COULD MOVE '{0}' from '{1}'", fileName, foundFileName);
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
            return "Fix Audio File Names";
        }
        public override string InfoPageName
        {
            get { return "FixAudioFileNames.htm"; }
        }

    }
}