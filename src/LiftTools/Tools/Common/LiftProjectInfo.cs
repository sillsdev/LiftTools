using System.IO;

namespace LiftTools.Tools.Common
{
	public class LiftProjectInfo
	{

		public static string WritingSystemPath(string liftFilePath)
		{
			return Path.Combine(ProjectPath(liftFilePath), "WritingSystems");
		}

		public static string ProjectPath(string liftFilePath)
		{
			return Path.GetDirectoryName(liftFilePath);
		}

		public static string AudioPath(string liftFilePath)
		{
			return Path.Combine(ProjectPath(liftFilePath), "audio");
		}

		public static string AudioFilePath(string liftFilePath, string fileName)
		{
			return Path.Combine(AudioPath(liftFilePath), fileName);
		}

		public static string ImagePath(string lifeFilePath)
		{
			return Path.Combine(ProjectPath(lifeFilePath), "pictures");
		}

	}
}