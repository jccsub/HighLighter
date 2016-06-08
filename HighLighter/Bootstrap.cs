namespace HighLighter
{
	public class Bootstrap
	{
		public static void SetupApplication(MainWindow mainApplicationWindow)
		{
			var userTextViewModel = new UserTextViewModel();
			mainApplicationWindow.DataContext = userTextViewModel;
		}
	}
}