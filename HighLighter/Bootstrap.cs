using System.Collections.Generic;
using System.IO;
using System.Windows.Documents;
using HighLighter.Infrastructure;
using Newtonsoft.Json;

namespace HighLighter
{
	public class Bootstrap
	{
		public static void SetupApplication(MainWindow mainApplicationWindow)
		{

			var fileSystem = new FileSystem();
			var text = fileSystem.ReadAllTextFromFile("LinkTypes.json");

			var links = JsonConvert.DeserializeObject<List<LinkType>>(text);

			var launcher = new AssociateApplicationLauncher();
			launcher.LaunchApplicationFor("https://www.instagram.com/chad.widget");

		}
	}
}