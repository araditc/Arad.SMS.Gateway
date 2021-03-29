using System;
using System.Collections.Generic;

namespace Common
{
	[Serializable()]
	public class BatchMessage
	{
		private SmsSenderAgentReference smsSenderAgentReference;
		private Guid userGuid;
		private Guid batchId;
		private string itemId;
		private string username;
		private string password;
		private string domain;
		private string link;
		private string senderNumber;
		private string serviceId;
		private string smsText;
		private int smsLen;
		private long smsIdentifier;
		private int smsPartIndex;
		private bool isUnicode;
		private bool isFlash;
		private List<InProgressSms> receivers;
		private string queueName;

		public SmsSenderAgentReference SmsSenderAgentReference
		{
			get { return smsSenderAgentReference; }
			set { smsSenderAgentReference = value; }
		}

		public Guid UserGuid
		{
			get { return userGuid; }
			set { userGuid = value; }
		}

		public Guid BatchId
		{
			get { return batchId; }
			set { batchId = value; }
		}

		public string ItemId
		{
			get { return itemId; }
			set { itemId = value; }
		}

		public string Username
		{
			get { return username; }
			set { username = value; }
		}

		public string Password
		{
			get { return password; }
			set { password = value; }
		}

		public string Domain
		{
			get { return domain; }
			set { domain = value; }
		}

		public string Link
		{
			get { return link; }
			set { link = value; }
		}

		public string SenderNumber
		{
			get { return senderNumber; }
			set { senderNumber = value; }
		}

		public string ServiceId
		{
			get { return serviceId; }
			set { serviceId = value; }
		}

		public string SmsText
		{
			get { return smsText; }
			set { smsText = value; }
		}

		public int SmsLen
		{
			get { return smsLen; }
			set { smsLen = value; }
		}

		public long SmsIdentifier
		{
			get { return smsIdentifier; }
			set { smsIdentifier = value; }
		}

		public int SmsPartIndex 
		{
			get { return smsPartIndex; }
			set { smsPartIndex = value; }
		}

		public bool IsUnicode
		{
			get { return isUnicode; }
			set { isUnicode = value; }
		}

		public bool IsFlash
		{
			get { return isFlash; }
			set { isFlash = value; }
		}

		public List<InProgressSms> Receivers
		{
			get { return receivers; }
			set { receivers = value; }
		}

		public string QueueName
		{
			get { return queueName; }
			set { queueName = value; }
		}
	}
}
