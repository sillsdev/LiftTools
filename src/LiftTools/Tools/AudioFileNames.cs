using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using LiftTools.Tools.Common;
using Palaso.Code;
using Palaso.Progress.LogBox;
using Palaso.Xml;

namespace LiftTools.Tools
{
    public class AudioFileNames : Tool
    {
        private LinkAudit _linkAudit;
        private IProgress _progress;
        private readonly AudioFileNamesConfig _config;

        public AudioFileNames()
        {
            _config = new AudioFileNamesConfig();
            ConfigControl = _config;
        }

        public override void Run(string inputLiftPath, string outputLiftPath, IProgress progress)
        {
            _linkAudit = new LinkAudit();
            _progress = progress;

            CheckEnvironment(inputLiftPath);
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
        		
				string orphanPath = Path.Combine(AudioPath(inputLiftPath), "OrphanFiles");
				foreach (var infoPair in audioFiles.Where(x => x.Value.FileFound && !x.Value.LinkFound))
				{
					var info = infoPair.Value;
					if (_config.DoMoveRemainingFiles)
					{
						if (!Directory.Exists(orphanPath))
						{
							Directory.CreateDirectory(orphanPath);
						}
						File.Move(AudioFilePath(inputLiftPath, info.FileName), orphanPath);
						_progress.WriteMessage("  MOVED ORPHANED FILE '{0}'", info.FileName);
					} else
					{
						_progress.WriteMessage("  ORPHANED FILE '{0}'", info.FileName);
					}
				}
				_progress.WriteMessage("\n");
			}

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
			
			progress.WriteMessageWithColor("blue", "The processed lift is at " + outputLiftPath);
			ValidateFile(progress, outputLiftPath);
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

        private static string ProjectPath(string liftFilePath)
        {
            return Path.GetDirectoryName(liftFilePath);
        }

        private static string AudioPath(string liftFilePath)
        {
            return Path.Combine(ProjectPath(liftFilePath), "audio");
        }

        private static string AudioFilePath(string liftFilePath, string fileName)
        {
            return Path.Combine(AudioPath(liftFilePath), fileName);
        }

        private static void CheckEnvironment(string liftFilePath)
        {
            string audioPath = AudioPath(liftFilePath);
            if (!Directory.Exists(audioPath))
            {
                throw new ApplicationException(
                    String.Format("The path for audio files could not be found. Expecting '{0}' to exist.", audioPath)
                );
            }
        }

        private void FindAudioFile(string liftFilePath, string fileName)
        {
            string audioFilePath = AudioFilePath(liftFilePath, fileName);
            if (File.Exists(audioFilePath)) // It really shouldn't if LinkAudit is doing it's job.
            {
                return;
            }
            // It doesn't exist. See if we can find a file that matches the numeric part of the filename.
            string audioPath = AudioPath(liftFilePath);
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
            return "Fix Audio File Names";
        }
        public override string InfoPageName
        {
            get { return "FixAudioFileNames.htm"; }
        }

    }
}