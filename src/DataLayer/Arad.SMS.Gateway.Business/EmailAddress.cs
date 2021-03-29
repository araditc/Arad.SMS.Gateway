using System;
using GeneralLibrary.BaseCore;
using Common;
using GeneralLibrary;
using System.Data;

namespace Business
{
	public class EmailAddress : BusinessEntityBase
	{
		public EmailAddress(DataAccessBase dataAccessProvider = null)
			: base(TableNames.EmailAddresses.ToString(), dataAccessProvider) { }

		public long GetCountEmailOfEmailBook(Guid emailBookGuid)
		{
			return Helper.GetLong(base.FetchDataTable("SELECT COUNT(*) AS [RowCount] FROM [EmailAddresses] WHERE [EmailBookGuid]=@EmailBookGuid", "@EmailBookGuid", emailBookGuid).Rows[0]["RowCount"]);
		}

		public DataTable GetPagedEmails(Common.EmailAddress email, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			DataSet emailsInfo = base.FetchSPDataSet("GetPagedEmails",
																										 "@EmailBookGuid", email.EmailBookGuid,
																										 "@Email", email.Email,
																										 "@PageNo", pageNo,
																										 "@PageSize", pageSize,
																										 "@SortField", sortField);
			resultCount = Helper.GetInt(emailsInfo.Tables[0].Rows[0]["RowCount"]);
			return emailsInfo.Tables[1];
		}

		public bool CheckDuplicateEmailStatus(Guid userGuid, Guid emailBookGuid, CheckEmailScope scope, string email)
		{
			DataTable dataTableEmailStatus = base.FetchSPDataTable("EmailStatus", "@Scope", (int)scope,
																																		 "@Email", email,
																																		 "@EmailBookGuid", emailBookGuid,
																																		 "@UserGuid", userGuid);
			return dataTableEmailStatus.Rows.Count > 0 ? false : true;
		}

		public bool InsertEmail(Guid emailBookGuid, string email)
		{
			return base.ExecuteSPCommand("InsertEmail", "@Guid", Guid.NewGuid(),
																									 "@CreateDate", DateTime.Now,
																									 "@Email", email,
																									 "@EmailBookGuid", emailBookGuid);
		}

		public DataTable GetEmailsOfGroup(Guid guid)
		{
			return FetchSPDataTable("GetEmailsOfGroup", "@Guid", guid);
		}
	}
}
