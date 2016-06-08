using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;

namespace HighLighter
{

	/// <summary>
	/// Most of this code comes from here: http://stackoverflow.com/a/2641774. Only minor
	/// aesthetic changes.
	/// 
	/// The problem that I needed solved was that the Document property of the RichTextBox
	/// was not bindable. It would have been easy enough to handle the RichTextBox.TextChanged
	/// event; however, that would lead to handling a change event in the code-behind, violating
	/// my mvvm model.
	/// </summary>
	public class RichTextBoxHelper : DependencyObject
	{
		private static readonly HashSet<Thread> RecursionProtection = new HashSet<Thread>();

		public static string GetDocumentXaml(DependencyObject dependencyObject)
		{
			return (string)dependencyObject.GetValue(DocumentXamlProperty);
		}

		public static void SetDocumentXaml(DependencyObject obj, string value)
		{
			RecursionProtection.Add(Thread.CurrentThread);
			obj.SetValue(DocumentXamlProperty, value);
			RecursionProtection.Remove(Thread.CurrentThread);
		}

		public static readonly DependencyProperty DocumentXamlProperty = DependencyProperty.RegisterAttached(
			"DocumentXaml",
			typeof(string),
			typeof(RichTextBoxHelper),
			new FrameworkPropertyMetadata(
				"",
				FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
				(obj, e) =>
				{
					if (RecursionProtection.Contains(Thread.CurrentThread))
						return;

					var richTextBox = (RichTextBox)obj;

				// Parse the XAML to a document (or use XamlReader.Parse())

				try
					{
						var stream = new MemoryStream(Encoding.UTF8.GetBytes(GetDocumentXaml(richTextBox)));
						var doc = (FlowDocument)XamlReader.Load(stream);

					// Set the document
					richTextBox.Document = doc;
					}
					catch (Exception)
					{
						richTextBox.Document = new FlowDocument();
					}

				// When the document changes update the source
				richTextBox.TextChanged += (obj2, e2) =>
					{
						RichTextBox richTextBox2 = obj2 as RichTextBox;
						if (richTextBox2 != null)
						{
							SetDocumentXaml(richTextBox, XamlWriter.Save(richTextBox2.Document));
						}
					};
				}
			)
		);
	}
}
