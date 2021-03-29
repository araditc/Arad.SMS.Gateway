using Common;
using GeneralLibrary.BaseCore;
using System;
using System.Data;
using GeneralLibrary;

namespace Business
{
	public class EmailOutbox : BusinessEntityBase
	{
		public EmailOutbox() : base(TableNames.EmailOutboxes.ToString()) { }

		public Guid InsertSendSingleEmail(Common.EmailOutbox emailOutbox)
		{
			Guid guid = Guid.NewGuid();
			try
			{
				ExecuteSPCommand("InsertSendSingleEmail", "@Guid", guid,
																									"@UserEmailSettingGuid", emailOutbox.UserEmailSettingGuid,
																									"@Reciever", emailOutbox.Reciever,
																									"@Body", emailOutbox.Body,
																									"@Subject", emailOutbox.Subject,
																									"@AttachmentList", emailOutbox.AttachmentList,
																									"@TypeSend", emailOutbox.TypeSend,
																									"@State", emailOutbox.State,
																									"@CreateDate", emailOutbox.CreateDate,
																									"@UserGuid", emailOutbox.UserGuid);
				return guid;
			}
			catch
			{
				guid = Guid.Empty;
				return guid;
			}
		}

		public Guid InsertSendGroupEmail(Common.EmailOutbox emailOutbox)
		{
			Guid guid = Guid.NewGuid();
			try
			{
				ExecuteSPCommand("InsertSendGroupEmail", "@Guid", guid,
																									"@UserEmailSettingGuid", emailOutbox.UserEmailSettingGuid,
																									"@Reciever", emailOutbox.Reciever,
																									"@Body", emailOutbox.Body,
																									"@Subject", emailOutbox.Subject,
																									"@AttachmentList", emailOutbox.AttachmentList,
																									"@GroupGuid", emailOutbox.GroupGuid,
																									"@TypeSend", emailOutbox.TypeSend,
																									"@State", emailOutbox.State,
																									"@CreateDate", emailOutbox.CreateDate,
																									"@UserGuid", emailOutbox.UserGuid);
				return guid;
			}
			catch
			{
				guid = Guid.Empty;
				return guid;
			}
		}

		public System.Data.DataTable GetEmailsByPriority()
		{
			return FetchSPDataTable("GetEmailByPriority");
		}

		public void UpdateState(string emailOutboxGuids, EmailOutboxStates state)
		{
			ExecuteSPCommand("UpdateEmailOutboxState", "@EmailOutboxGuids", emailOutboxGuids,
																							"@State", (int)state);
		}

		public System.Data.DataTable GetPagedEmailOutbox(Common.EmailOutbox emailOutbox, string fromCreateDate, string fromTime, string toCreateDate, string toTime, string sender, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			DataSet dataSetEmailOutbox = base.FetchSPDataSet("GetPagedEmailOutbox",
																																							"@UserGuid", emailOutbox.UserGuid,
																																							"@State", emailOutbox.State,
																																							"@Reciever", emailOutbox.Reciever,
																																							"@FromCreateDate",fromCreateDate,
																																							"@FromTime",fromTime,
																																							"@ToCreateDate",toCreateDate,
																																							"@ToTime",toTime,
																																							"@TypeSend", emailOutbox.TypeSend,
																																							"@Subject", emailOutbox.Subject,
																																							"@PageNo", pageNo,
																																							"@PageSize", pageSize,
																																							"@SortField", sortField);
			resultCount = Helper.GetInt(dataSetEmailOutbox.Tables[0].Rows[0]["RowCount"]);
			return dataSetEmailOutbox.Tables[1];
		}
	}
}

