using System;
using System.Data;
using GeneralLibrary.BaseCore;

namespace Facade
{
	public class EmailOutbox : FacadeEntityBase
	{
		public static Guid InsertSendSingleEmail(Common.EmailOutbox emailOutbox)
		{
			Business.EmailOutbox emailOutboxController = new Business.EmailOutbox();
			return emailOutboxController.InsertSendSingleEmail(emailOutbox);
		}

		public static Guid InsertSendGroupEmail(Common.EmailOutbox emailOutbox)
		{
			Business.EmailOutbox emailOutboxController = new Business.EmailOutbox();
			return emailOutboxController.InsertSendGroupEmail(emailOutbox);
		}

		public static DataTable GetEmailsByPriority()
		{
			Business.EmailOutbox emailOutboxController = new Business.EmailOutbox();
			return emailOutboxController.GetEmailsByPriority();
		}

		public static void UpdateEmailOutboxState(string[] emailOutboxGuidList, Business.EmailOutboxStates state)
		{
			Business.EmailOutbox emailOutboxController = new Business.EmailOutbox();
			string emailOutboxGuids = string.Empty;
			foreach (string emailOutbox in emailOutboxGuidList)
				emailOutboxGuids += "'" + emailOutbox + "',";

			emailOutboxController.UpdateState(emailOutboxGuids.TrimEnd(','), state);
		}

		public static DataTable GetPagedEmailOutbox(Common.EmailOutbox emailOutbox, string fromCreateDate,string fromTime, string toCreateDate,string toTime,string sender, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.EmailOutbox emailOutBox = new Business.EmailOutbox();
			return emailOutBox.GetPagedEmailOutbox(emailOutbox,fromCreateDate,fromTime,toCreateDate,toTime,sender,sortField,pageNo,pageSize,ref resultCount);
		}
	}
}
