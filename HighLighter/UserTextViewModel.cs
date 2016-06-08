using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HighLighter
{
	internal class UserTextViewModel
	{
		private string _text;

		public string Text
		{
			get { return _text; }
			set
			{
				_text = value;
				OnPropertyChanged();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			if (PropertyChanged != null)
				PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
