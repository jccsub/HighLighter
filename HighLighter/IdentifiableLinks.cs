using System.Collections.Generic;

namespace HighLighter
{
	public class IdentifiableLinks
	{
		public List<LinkType> linkTypes { get; set; }
	}

	public class LinkType
	{
		public string name { get; set; }
		public string pattern { get; set; }
		public string url { get; set; }
	}
}	