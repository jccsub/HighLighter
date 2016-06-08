using System.Diagnostics;

namespace HighLighter.Infrastructure
{
	public class AssociateApplicationLauncher : IAssociatedApplicationLauncher
	{
		public void LaunchApplicationFor(string url)
		{
			Process.Start(url);
		}
	}
}