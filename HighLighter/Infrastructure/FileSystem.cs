using System.IO;

namespace HighLighter.Infrastructure
{
	public class FileSystem : IFileSystem
	{
		public string ReadAllTextFromFile(string fileName)
		{
			return File.ReadAllText("LinkTypes.json");
		}
	}
}