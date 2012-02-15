using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Palaso.Code;
using Palaso.Progress.LogBox;

namespace LiftTools.Tools
{
    public class OrphanFiles : Tool
    {
        private class LinkInfo
        {
            public enum Types
            {
                Audio,
                Image
            }

            private LinkInfo()
            {
            }

            public static LinkInfo CreateFromLink(string link, Types type)
            {
                return new LinkInfo
                    {
                        FileName = link,
                        FileFound = false,
                        LinkFound = true,
                        Type = type
                    };
            }

            public static LinkInfo CreateFromFile(string fileName, Types type)
            {
                return new LinkInfo
                {
                    FileName = fileName,
                    FileFound = true,
                    LinkFound = false,
                    Type = type
                };
            }

            public string FileName { get; private set; }
            public Types Type { get; private set; }
            public bool FileFound { get; set; }
            public bool LinkFound { get; set; }

        }

        private Dictionary<string, LinkInfo> _linkinfo;

        private IProgress _progress;
        public override void Run(string inputLiftPath, string outputLiftPath, IProgress progress)
        {
            _progress = progress;
            CheckEnvironment(inputLiftPath);

            _linkinfo = new Dictionary<string, LinkInfo>();

            var audioRegex = new Regex(@"<text>(.*\.wav)</text>", RegexOptions.IgnoreCase);
            var imageRegex = new Regex(@"href=""(.*)""", RegexOptions.IgnoreCase);
            using (var reader = new StreamReader(inputLiftPath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line)) continue;

                    var match = audioRegex.Match(line);
                    if (match.Success)
                    {
                        _linkinfo.Add(match.Groups[1].Value, LinkInfo.CreateFromLink(match.Groups[1].Value, LinkInfo.Types.Audio));
                        continue;
                    }
                    match = imageRegex.Match(line);
                    if (match.Success)
                    {
                        string imageRef = match.Groups[1].Value;
                        imageRef = imageRef.Replace('/', '\\');
                        string imageFile = Path.GetFileName(imageRef);
                        if (!String.IsNullOrEmpty(imageFile))
                        {
                            // Images may be used more than once.
                            if (!_linkinfo.ContainsKey(imageFile))
                            {
                                _linkinfo.Add(imageFile, LinkInfo.CreateFromLink(imageFile, LinkInfo.Types.Image));
                            }
                        }
                    }
                }
            }
            CheckFiles(AudioPath(inputLiftPath), LinkInfo.Types.Audio);
            CheckFiles(ImagePath(inputLiftPath), LinkInfo.Types.Image);
            LogReport();
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

        private static string ImagePath(string lifeFilePath)
        {
            return Path.Combine(ProjectPath(lifeFilePath), "pictures");
        }

        private void CheckEnvironment(string liftFilePath)
        {
            string audioPath = AudioPath(liftFilePath);
            if (Directory.Exists(audioPath))
            {
                _progress.WriteMessage("Audio folder found");
            }
            string imagePath = ImagePath(liftFilePath);
            if (Directory.Exists(imagePath))
            {
                _progress.WriteMessage("Images folder found");
            }
        }

        private void CheckFiles(string path, LinkInfo.Types type)
        {
            var filePathsFound = Directory.GetFiles(path);
            foreach (string filePath in filePathsFound)
            {
                string fileName = Path.GetFileName(filePath);
                if (String.IsNullOrEmpty(fileName)) continue;
                if (_linkinfo.ContainsKey(fileName))
                {
                    _linkinfo[fileName].FileFound = true;
                } else
                {
                    _linkinfo.Add(fileName, LinkInfo.CreateFromFile(fileName, type));
                }
            }
        }

        private void LogReport()
        {
            _progress.WriteMessage("Links with no files:");
            foreach (var linkInfoPair in _linkinfo)
            {
                var linkInfo = linkInfoPair.Value;
                if (linkInfo.LinkFound && !linkInfo.FileFound)
                {
                    _progress.WriteMessage("  {0}", linkInfo.FileName);
                }
            }
            _progress.WriteMessage("Files with no links:");
            foreach (var linkInfoPair in _linkinfo)
            {
                var linkInfo = linkInfoPair.Value;
                if (!linkInfo.LinkFound && linkInfo.FileFound)
                {
                    _progress.WriteMessage("  {0}", linkInfo.FileName);
                }
            }
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
            return "Find Orphan Files";
        }
        public override string InfoPageName
        {
            get { return "OrphanFiles.htm"; }
        }

    }
}