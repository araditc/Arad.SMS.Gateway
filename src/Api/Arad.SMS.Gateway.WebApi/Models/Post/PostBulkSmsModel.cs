using System.Collections.Generic;
using System.Xml.Serialization;

namespace WebApi.Models
{
	[XmlRoot(ElementName = "BulkInfo")]
	public class PostBulkSmsModel
	{
		public long Id { get; set; }
		public string SmsText { get; set; }
		public int SmsLen { get; set; }
		public bool IsUnicode { get; set; }
		public List<string> Receivers { get; set; }
	}
}
