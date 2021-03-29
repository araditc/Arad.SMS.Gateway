using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MessagingSystem.SI
{
	[DataContract]
	public class Sms
	{
		private string domain;
		private string messageBody;
		private List<string> recipientsNumber;
		private string senderNumber;
		private Encoding encoding;
		private string udh;
		private MessageType messageType;
		private int priority;
		private int checkingMessageID;
		private DateTime receivedDate;

		[DataMember]
		public string Domain
		{
			get { return domain; }
			set { domain = value; }
		}

		[DataMember]
		public string MessageBody
		{
			get { return messageBody; }
			set { messageBody = value; }
		}

		[DataMember]
		public List<string> RecipientsNumber
		{
			get { return recipientsNumber; }
			set { recipientsNumber = value; }
		}

		[DataMember]
		public string SenderNumber
		{
			get { return senderNumber; }
			set { senderNumber = value; }
		}

		[DataMember]
		public Encoding Encoding
		{
			get { return encoding; }
			set { encoding = value; }
		}

		[DataMember]
		public string UDH
		{
			get { return udh; }
			set { udh = value; }
		}

		[DataMember]
		public MessageType MessageType
		{
			get { return messageType; }
			set { messageType = value; }
		}

		[DataMember]
		public int Priority
		{
			get { return priority; }
			set { priority = value; }
		}

		[DataMember]
		public int CheckingMessageID
		{
			get { return checkingMessageID; }
			set { checkingMessageID = value; }
		}

		[DataMember]
		public DateTime ReceivedDate
		{
			get { return receivedDate; }
			set { receivedDate = value; }
		}

		public Sms()
		{
			recipientsNumber = new List<string>();
		}
	}
}