using System;
using System.Runtime.Serialization;

namespace MessagingSystem.SI
{
	[DataContract]
	public class SendResult
	{
		private long messageID;
		private SmsStatus status;

		[DataMember]
		public long MessageID
		{
			get { return messageID; }
			set { messageID = value; }
		}

		[DataMember]
		public SmsStatus Status
		{
			get { return status; }
			set { status = value; }
		}
	}
}