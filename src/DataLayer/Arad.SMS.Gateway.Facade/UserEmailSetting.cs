using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralLibrary;
using GeneralLibrary.BaseCore;
using System.Data;

namespace Facade
{
	public class UserEmailSetting : FacadeEntityBase
	{
		public static DataTable GetUserEmailSetting(Guid userGuid)
		{
			Business.UserEmailSetting userEmailSettingController = new Business.UserEmailSetting();
			return userEmailSettingController.GetUserEmailSetting(userGuid);
		}

		public static bool InsertSetting(Common.UserEmailSetting userEmailSetting)
		{
			Business.UserEmailSetting userEmailSettingController = new Business.UserEmailSetting();
			return userEmailSettingController.InsertSetting(userEmailSetting) != Guid.Empty ? true : false;
		}

		public static bool UpdateSetting(Common.UserEmailSetting userEmailSetting)
		{
			Business.UserEmailSetting userEmailSettingController = new Business.UserEmailSetting();
			return userEmailSettingController.UpdateSetting(userEmailSetting);
		}

		public static Common.UserEmailSetting LoadEmailSetting(Guid settingGuid)
		{
			Business.UserEmailSetting userEmailSettingController = new Business.UserEmailSetting();
			Common.UserEmailSetting userEmailSetting = new Common.UserEmailSetting();
			userEmailSettingController.Load(settingGuid, userEmailSetting);
			return userEmailSetting;
		}

		public static bool DeleteSetting(Guid guid)
		{
			Business.UserEmailSetting userEmailSettingController = new Business.UserEmailSetting();
			return userEmailSettingController.Delete(guid);
		}
	}
}
