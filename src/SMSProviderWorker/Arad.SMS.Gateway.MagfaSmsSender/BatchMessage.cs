using System;
using System.Collections.Generic;
using Common;

namespace MagfaSmsSender
{
	public class BatchMessage
	{
		private Guid batchId;
		private string username;
		private string password;
		private string domain;
		private string link;
		private string senderNumber;
		private string smsText;
		private bool isUnicode;
		private bool isFlash;
		private List<InProgressSms> receivers;

		public Guid BatchId
		{
			get { return batchId; }
			set { batchId = value; }
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

		public string SmsText
		{
			get { return smsText; }
			set { smsText = value; }
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
	}
}
