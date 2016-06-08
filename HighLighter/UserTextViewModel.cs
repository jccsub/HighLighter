using System;
using System.Windows.Input;

namespace HighLighter
{
	internal class UserTextViewModel
	{
		public UserText UserText { get; private set; }

		public UserTextViewModel()
		{
			UserText = new UserText();
		}

		private ICommand _updater;

		public ICommand UpdateCommand
		{
			get { return _updater ?? (_updater = new Updater()); }
			set { _updater = value; }
		}

		private class Updater : ICommand
		{
			public bool CanExecute(object parameter)
			{
				return true;
			}

			public event EventHandler CanExecuteChanged;

			public void Execute(object parameter)
			{
			}
		}
	}
}
