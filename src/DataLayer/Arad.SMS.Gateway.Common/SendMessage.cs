using System;

namespace Common
{
	[Serializable()]
	public class SendMessage
	{
		public string Message { get; set; }
		public bool isFlash { get; set; }
		public bool isUnicode { get; set; }
		public string BatchId { get; set; }
		public string ItemId { get; set; }
		public string Sender { get; set; }
		public string userName { get; set; }
		public string password { get; set; }
		public string domain { get; set; }
		public string link { get; set; }
	}
}
