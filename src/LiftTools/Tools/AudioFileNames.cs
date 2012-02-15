using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Palaso.Code;
using Palaso.Progress.LogBox;

namespace LiftTools.Tools
{
    public class AudioFileNames : Tool
    {
        private IProgress _progress;
        public override void Run(string inputLiftPath, string outputLiftPath, IProgress progress)
        {
            CheckEnvironment(inputLiftPath);
            _progress = progress;

            var regex = new Regex(@"<text>(.*\.wav)</text>", RegexOptions.IgnoreCase);
            using (var reader = new StreamReader(inputLiftPath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line)) continue;

                    var match = regex.Match(line);
                    if (match.Success)
                    {
                        FindAudioFile(inputLiftPath, match.Groups[1].Value);
                    }

                }
            }
            _progress.WriteMessage("DONE");
            //ValidateFile(progress, outputLiftPath);
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
            if (File.Exists(audioFilePath))
            {
                _progress.WriteMessage("FOUND '{0}'", fileName);
                return;
            }
            // It doesn't exist. See if we can find a file that matches the numeric part of the filename.
            string audioPath = AudioPath(liftFilePath);
            var regex = new Regex(".*-(.*)\\.wav", RegexOptions.IgnoreCase);
            var match = regex.Match(fileName);
            if (!match.Success)
            {
                _progress.WriteMessageWithColor("red", "Could not use filename '{0}' to match", fileName);
                return;
            }
            string searchPattern = String.Format("*{0}.wav", match.Groups[1].Value);
            var fileNamesFound = Directory.GetFiles(audioPath, searchPattern, SearchOption.TopDirectoryOnly);
            if (fileNamesFound.Length == 0)
            {
                _progress.WriteMessageWithColor("red", "NOT FOUND '{0}'", fileName);
                return;
            }
            if (fileNamesFound.Length > 1)
            {
                _progress.WriteMessageWithColor("red", "Multiple matches for '{0}'", fileName);
                return;
            }
            string foundFilePath = fileNamesFound[0];
            string foundFileName = Path.GetFileName(foundFilePath);
            File.Move(foundFilePath, audioFilePath);
            _progress.WriteMessage("MOVED '{0}' from '{1}'", fileName, foundFileName);
        }

        private void ValidateFile(IProgress progress, string path)
        {
            progress.WriteMessage(""); 
            progress.WriteMessage("Validating the processed file...");
            var errors = LiftIO.Validation.Validator.GetAnyValidationErrors(path);
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