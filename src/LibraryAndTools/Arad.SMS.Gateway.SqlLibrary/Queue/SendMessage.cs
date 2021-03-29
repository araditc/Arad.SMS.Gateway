using System;

namespace SqlLibrary
{
	[Serializable()]
	public class SendMessage
	{
		public string Message { get; set; }
		public int SmsLen { get; set; }
		public int TryCount { get; set; }
		public long SmsIdentifier { get; set; }
		public int SmsPartIndex { get; set; }
		public int IsFlash { get; set; }
		public int IsUnicode { get; set; }
		public string BatchId { get; set; }
		public string ItemId { get; set; }
		public string Sender { get; set; }
		public string ServiceId { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Domain { get; set; }
		public string Link { get; set; }
		public int SmsSenderAgentReference { get; set; }
		public string QueueName { get; set; }
	}
}
