namespace HighLighter
{
	public class Bootstrap
	{
		public static void SetupApplication(MainWindow mainApplicationWindow)
		{
			var userTextViewModel = new UserText();
			mainApplicationWindow.DataContext = userTextViewModel;
		}
	}
}