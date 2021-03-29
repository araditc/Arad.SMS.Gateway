using Common;
using GeneralLibrary.BaseCore;
using System;
using System.Data;
using GeneralLibrary;

namespace Business
{
	public class EmailSentbox : BusinessEntityBase
	{
		public EmailSentbox() : base(TableNames.EmailSentboxes.ToString()) { }

		public DataTable GetPagedEmailSentbox(Common.EmailSentbox emailSentbox, string fromEffectiveDate, string fromTime, string toEffectiveDate, string toTime, string sender, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			DataSet dataSetEmailSentbox = base.FetchSPDataSet("GetPagedEmailSentbox",
																																							"@UserGuid", emailSentbox.UserGuid,
																																							"@Status", emailSentbox.Status,
																																							"@Reciever", emailSentbox.Reciever,
																																							"@FromCreateDate", fromEffectiveDate,
																																							"@FromTime", fromTime,
																																							"@ToCreateDate", toEffectiveDate,
																																							"@ToTime", toTime,
																																							"@Sender", sender,
																																							"@Subject", emailSentbox.Subject,
																																							"@PageNo", pageNo,
																																							"@PageSize", pageSize,
																																							"@SortField", sortField);
			resultCount = Helper.GetInt(dataSetEmailSentbox.Tables[0].Rows[0]["RowCount"]);
			return dataSetEmailSentbox.Tables[1];
		}
	}
}