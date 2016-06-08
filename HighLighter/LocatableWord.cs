using System;
using System.Windows.Documents;

namespace HighLighter
{
	public class LocatableWord
	{
		private readonly TextPointer _startPosition;
		private readonly TextPointer _endPosition;
		private readonly string _word;

		public LocatableWord(TextPointer startPosition, TextPointer endPosition, string word)
		{
			_startPosition = startPosition;
			_endPosition = endPosition;
			_word = word;
		}


		public string Word
		{
			get { return _word; }
		}

		public TextPointer StartPosition
		{
			get { return _startPosition; }
		}

		public TextPointer EndPosition
		{
			get { return _endPosition; }
		}
	}
}