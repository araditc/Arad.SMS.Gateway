using System;

namespace Common
{
	[Serializable()]
	public class InProgressSms
	{
		private Guid outboxGuid;
		private string fileName;
		private string recipientNumber;
		private SendStatus sendStatus;
		private DeliveryStatus deliveryStatus;
		private string returnID;
		private string checkID;

		public Guid OutboxGuid
		{
			get { return outboxGuid; }
			set { outboxGuid = value; }
		}

		public string FileName
		{
			get { return fileName; }
			set { fileName = value; }
		}

		public string RecipientNumber
		{
			get { return recipientNumber; }
			set { recipientNumber = value; }
		}

		public SendStatus SendStatus
		{
			get { return sendStatus; }
			set { sendStatus = value; }
		}

		public DeliveryStatus DeliveryStatus
		{
			get { return deliveryStatus; }
			set { deliveryStatus = value; }
		}

		public string ReturnID
		{
			get { return returnID; }
			set { returnID = value; }
		}

		public string CheckID
		{
			get { return checkID; }
			set { checkID = value; }
		}
	}
}