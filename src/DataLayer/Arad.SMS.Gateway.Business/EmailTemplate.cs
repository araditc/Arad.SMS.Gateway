using System;
using System.Data;
using Common;
using GeneralLibrary;
using GeneralLibrary.BaseCore;

namespace Business
{
	public class EmailTemplate : BusinessEntityBase
	{
		public EmailTemplate(DataAccessBase dataAccessProvider = null)
			: base(TableNames.EmailTemplates.ToString(), dataAccessProvider) { }

		public DataTable GetPagedEmailTemplates(Guid userGuid, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			DataSet dataSetEmailTemplate = base.FetchSPDataSet("GetPagedEmailTemplates",
																																							"@UserGuid", userGuid,
																																							"@PageNo", pageNo,
																																							"@PageSize", pageSize,
																																							"@SortField", sortField);
			resultCount = Helper.GetInt(dataSetEmailTemplate.Tables[0].Rows[0]["RowCount"]);

			return dataSetEmailTemplate.Tables[1];
		}

		public Guid InsertTemplate(Common.EmailTemplate emailTemplate)
		{
			Guid guid = Guid.NewGuid();
			try
			{
				ExecuteSPCommand("InsertTemplate", "@UserEmailSettingGuid", emailTemplate.UserEmailSettingGuid,
																					"@Guid", guid,
																					"@Subject", emailTemplate.Subject,
																					"@Body", emailTemplate.Body,
																					"@CreateDate", emailTemplate.CreateDate,
																					"@AttachmentList", emailTemplate.AttachmentList);
				return guid;
			}
			catch
			{
				guid = Guid.Empty;
				return guid;
			}
		}

		public bool UpdateTemplate(Common.EmailTemplate emailTemplate)
		{
			return ExecuteSPCommand("UpdateTemplate",
																				 "@Guid", emailTemplate.EmailTemplateGuid,
																				 "@Subject", emailTemplate.Subject,
																				 "@Body", emailTemplate.Body,
																				 "@AttachmentList", emailTemplate.AttachmentList,
																				 "UserEmailSettingGuid", emailTemplate.UserEmailSettingGuid);
		}
	}
}