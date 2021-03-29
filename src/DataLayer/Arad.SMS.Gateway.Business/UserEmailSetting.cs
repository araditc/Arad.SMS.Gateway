using System;
using System.Data;
using GeneralLibrary;
using Common;
using GeneralLibrary.BaseCore;

namespace Business
{
	public class UserEmailSetting : BusinessEntityBase
	{
		public UserEmailSetting(DataAccessBase dataAccessProvider = null)
			: base(TableNames.UserEmailSettings.ToString(), dataAccessProvider) { }

		public System.Data.DataTable GetUserEmailSetting(Guid userGuid)
		{
			return FetchSPDataTable("GetUserEmailSetting", "@UserGuid", userGuid);
		}

		public Guid InsertSetting(Common.UserEmailSetting userEmailSetting)
		{
			try
			{
				DataTable dataTable = base.FetchDataTable(@"SELECT COUNT(*) AS [Count] FROM [UserEmailSettings] WHERE [EmailAddress]=@EmailAddress AND
																																																							[UserGuid] = @UserGuid", "@EmailAddress", userEmailSetting.EmailAddress,
																																																																			 "@UserGuid", userEmailSetting.UserGuid);
				if (Helper.GetInt(dataTable.Rows[0]["Count"]) > 0)
					throw new Exception(Language.GetString("DuplicateEmail"));
				else
					return Insert(userEmailSetting);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public bool UpdateSetting(Common.UserEmailSetting userEmailSetting)
		{
			try
			{
				DataTable dataTable = base.FetchDataTable(@"SELECT COUNT(*) AS [Count] FROM [UserEmailSettings] WHERE [EmailAddress]=@EmailAddress AND
																																																							[UserGuid] = @UserGuid AND
																																																							[Guid]!=@Guid", "@EmailAddress", userEmailSetting.EmailAddress,
																																																															"@UserGuid", userEmailSetting.UserGuid,
																																																															"@Guid", userEmailSetting.EmailSettingGuid);
				if (Helper.GetInt(dataTable.Rows[0]["Count"]) > 0)
					throw new Exception(Language.GetString("DuplicateEmail"));
				else
					return ExecuteSPCommand("UpdateSetting", "@Guid", userEmailSetting.EmailSettingGuid,
																									"@EmailAddress", userEmailSetting.EmailAddress,
																									"@Password", userEmailSetting.Password,
																									"@Type", userEmailSetting.EmailHostType);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
