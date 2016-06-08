using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace HighLighter
{
	public class SocialMediaHighlighter : RichTextBox
	{
		bool _updating = false;

		protected override void OnTextChanged(TextChangedEventArgs e)
		{
			if (!_updating)
			{
				_updating = true;
				try
				{
					ClearProperties();
					var words = FindWords();
					base.OnTextChanged(e);
				}
				finally
				{
					_updating = false;
				}
			}
		}

		private IQueryable<LocatableWord> FindWords()
		{
			var foundWords = new List<LocatableWord>();
			var textPointer = Document.ContentStart;
			Run run = null;
			int startPosition = 0;

			while (textPointer.CompareTo(Document.ContentEnd) < 0)
			{
				run = textPointer.Parent as Run;

				if (run != null)
				{
					var text = run.Text + ' ';
					for (var i = 0; i < text.Length; i++)
					{
						if (char.IsWhiteSpace(text[i]))
						{
							if (startPosition < i)
							{
								var foundWord = new LocatableWord(
									run.ContentStart.GetPositionAtOffset(startPosition),
									run.ContentStart.GetPositionAtOffset(i),
									text.Substring(startPosition, i-startPosition));


								if (foundWord.Word == "light")
								{
									var link = new Hyperlink(foundWord.StartPosition, foundWord.EndPosition)
									{
										NavigateUri = new Uri("http://www.google.com"),

									};
								}

								foundWords.Add(foundWord);
							}
							startPosition = i + 1;
						}
					}
				}
				textPointer = textPointer.GetNextContextPosition(LogicalDirection.Forward);
			}

			if (run != null)
			{
				var lastText = run.Text;
				var foundLastWord = new LocatableWord(
					run.ContentStart.GetPositionAtOffset(startPosition),
					run.ContentStart.GetPositionAtOffset(lastText.Length - 1),
					lastText.Substring(startPosition, lastText.Length - startPosition));

				foundWords.Add(foundLastWord);
			}

			return foundWords.AsQueryable();
		}



		void ClearProperties()
		{
			var documentRange = new TextRange(this.Document.ContentStart,
				this.Document.ContentEnd);
			documentRange.ClearAllProperties();
		}

		private void AddHyperlinkText(string linkURL, string linkName,
		   string TextBeforeLink, string TextAfterLink)
		{
			Paragraph para = new Paragraph();
			para.Margin = new Thickness(0); // remove indent between paragraphs

			Hyperlink link = new Hyperlink();
			link.IsEnabled = true;
			link.Inlines.Add(linkName);
			link.NavigateUri = new Uri(linkURL);
			link.RequestNavigate += (sender, args) => Process.Start(args.Uri.ToString());

			para.Inlines.Add(new Run("[" + DateTime.Now.ToLongTimeString() + "]: "));
			para.Inlines.Add(TextBeforeLink);
			para.Inlines.Add(link);
			para.Inlines.Add(new Run(TextAfterLink));

			this.Document.Blocks.Add(para);
		}
	}

	internal class Location
	{
		public object StartPosition { get; set; }
		public object EndPosition { get; set; }
		public object Word { get; set; }
	}
}