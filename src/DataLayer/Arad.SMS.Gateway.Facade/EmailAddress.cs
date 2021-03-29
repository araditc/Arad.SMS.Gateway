using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralLibrary.BaseCore;
using System.Data;

namespace Facade
{
	public class EmailAddress : FacadeEntityBase
	{
		public static long GetCountEmailOfEmailBook(Guid emailBookGuid)
		{
			Business.EmailAddress emailController = new Business.EmailAddress();
			return emailController.GetCountEmailOfEmailBook(emailBookGuid);
		}

		public static DataTable GetPagedEmails(Common.EmailAddress email, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.EmailAddress emailController = new Business.EmailAddress();
			return emailController.GetPagedEmails(email, sortField, pageNo, pageSize, ref resultCount);
		}

		public static bool CheckDuplicateEmailStatus(Guid userGuid, Guid emailBookGuid, Business.CheckEmailScope scope, string email)
		{
			Business.EmailAddress emailController = new Business.EmailAddress();
			return emailController.CheckDuplicateEmailStatus(userGuid, emailBookGuid, scope, email);
		}

		public static bool InsertEmail(Guid emailBookGuid, string email)
		{
			Business.EmailAddress emailController = new Business.EmailAddress();
			return emailController.InsertEmail(emailBookGuid, email);
		}

		public static DataTable GetEmailsOfGroup(Guid guid)
		{
			Business.EmailAddress emailController = new Business.EmailAddress();
			return emailController.GetEmailsOfGroup(guid);
		}
	}
}
