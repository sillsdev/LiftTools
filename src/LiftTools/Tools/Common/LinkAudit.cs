using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Palaso.Progress.LogBox;

namespace LiftTools.Tools.Common
{
    public class LinkAudit
    {
        public class LinkInfo
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

        private IProgress _progress;

        public string LiftFilePath { get; private set; }

        public Dictionary<string, LinkInfo> Links { get; private set; }

        public void RunAudit(string inputLiftPath, IProgress progress)
        {
            _progress = progress;
            Links = new Dictionary<string, LinkInfo>();

            CheckEnvironment(inputLiftPath);

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
                        Links.Add(match.Groups[1].Value, LinkInfo.CreateFromLink(match.Groups[1].Value, LinkInfo.Types.Audio));
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
                            if (!Links.ContainsKey(imageFile))
                            {
                                Links.Add(imageFile, LinkInfo.CreateFromLink(imageFile, LinkInfo.Types.Image));
                            }
                        }
                    }
                }
            }
            CheckFiles(AudioPath(inputLiftPath), LinkInfo.Types.Audio);
            CheckFiles(ImagePath(inputLiftPath), LinkInfo.Types.Image);
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
                if (Links.ContainsKey(fileName))
                {
                    Links[fileName].FileFound = true;
                } else
                {
                    Links.Add(fileName, LinkInfo.CreateFromFile(fileName, type));
                }
            }
        }

        public void LogReport()
        {
            _progress.WriteMessage("Links with no files:");
            foreach (var linkInfoPair in Links)
            {
                var linkInfo = linkInfoPair.Value;
                if (linkInfo.LinkFound && !linkInfo.FileFound)
                {
                    _progress.WriteMessage("  {0}", linkInfo.FileName);
                }
            }
            _progress.WriteMessage("\n");
            _progress.WriteMessage("Files with no links:");
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
}