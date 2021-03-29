using System;

namespace MagfaSmsSender
{
	public class InProgressSms
	{
		private string recipientNumber;
		private string returnID;
		private string checkID;
		private Business.SendStatus sendStatus;
		private Business.DeliveryStatus deliveryStatus;

		public string RecipientNumber
		{
			get { return recipientNumber; }
			set { recipientNumber = value; }
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

		public Business.SendStatus SendStatus
		{
			get { return sendStatus; }
			set { sendStatus = value; }
		}

		public Business.DeliveryStatus DeliveryStatus
		{
			get { return deliveryStatus; }
			set { deliveryStatus = value; }
		}
	}
}