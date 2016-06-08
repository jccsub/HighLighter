using System.Runtime.Serialization;

namespace HighLighter
{
	[DataContract]
	public class LinkPattern
	{

		[DataMember]
		public string Pattern { get; set; }

		[DataMember]
		public string Url { get; set; }
	}
}