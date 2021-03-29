using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralLibrary.BaseCore;

namespace Facade
{
	public class EmailSentbox : FacadeEntityBase
	{
		public static void InsertEmail(Common.EmailSentbox emailSentbox)
		{
			Business.EmailSentbox emailSentboxController = new Business.EmailSentbox();
			emailSentboxController.Insert(emailSentbox);
		}

		public static System.Data.DataTable GetPagedEmailSentbox(Common.EmailSentbox emailSentbox, string fromEffectiveDate, string fromTime, string toEffectiveDate, string toTime, string sender, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.EmailSentbox emailSentBoxController = new Business.EmailSentbox();
			return emailSentBoxController.GetPagedEmailSentbox(emailSentbox, fromEffectiveDate, fromTime, toEffectiveDate, toTime, sender, sortField, pageNo, pageSize, ref resultCount);
		}
	}
}
