// --------------------------------------------------------------------
// Copyright (c) 2005-2020 Arad ITC.
//
// Author : Ammar Heidari <ammar@arad-itc.org>
// Licensed under the Apache License, Version 2.0 (the "License")
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0 
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// --------------------------------------------------------------------

using System;
using System.Data;
using Arad.SMS.Gateway.Common;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System.Collections.Generic;

namespace Arad.SMS.Gateway.Business
{
	public class UserSetting : BusinessEntityBase
	{
		public UserSetting(DataAccessBase dataAccessProvider = null)
			: base(TableNames.UserSettings.ToString(), dataAccessProvider) { }

		public bool UpdateSetting(Guid userGuid, Business.AccountSetting settingKey, string value)
		{
			return ExecuteSPCommand("UpdateSetting",
															"@UserGuid", userGuid,
															"@Key", (int)settingKey,
															"@Value", value);
		}

		public bool InsertUserSettings(Guid userGuid, DataTable dtSettings)
		{
			return base.ExecuteSPCommand("InsertSetting",
																	 "@UserGuid", userGuid,
																	 "@Settings", dtSettings);
		}

		//public bool UpdateSetting(Guid userGuid, string key, string value)
		//{
		//	return base.ExecuteSPCommand("UpdateSetting",
		//															 "@UserGuid", userGuid,
		//															 "@Key", key,
		//															 "@Value", value);
		//}

		//public bool UpdateShortcut(Guid userGuid, string value)
		//{
		//	return base.ExecuteSPCommand("UpdateShortcut", "@UserGuid", userGuid,
		//																								 "@Value", value);
		//}

		public string GetSettingValue(Guid userGuid, Business.AccountSetting key)
		{
			try
			{
				return GetSPFieldValue("GetSettingValue", "@UserGuid", userGuid, "@Key", (int)key).ToString();
			}
			catch
			{
				return string.Empty;
			}
		}

		public DataTable GetUserShortcutForLoad(Guid userGuid)
		{
			//string strServiceGuid = string.Empty;
			//string[] serviceGuids = GetSettingValue(userGuid, Business.AccountSetting.Shortcut).Split(',');

			//if (serviceGuids.Length > 0)
			//	return new DataTable();

			//foreach (string guid in serviceGuids)
			//	strServiceGuid += string.Format("'{0}',", guid);

			return base.FetchSPDataTable("GetUserShortcutForLoad", "@UserGuid", userGuid);
		}

		public DataTable GetUserSettings(Guid userGuid)
		{
			return base.FetchSPDataTable("GetUserSettings", "@UserGuid", userGuid);
		}

		public DataTable GetUserExpired()
		{
			return FetchSPDataTable("GetUserExpired");
		}

		//public DataTable GetUserLowCredit()
		//{
		//	return FetchSPDataTable("GetUserLowCredit");
		//}

		public void GetUserWebAPIPassword(string username, ref string accountPassword, ref string apiPassword)
		{
			DataTable dt = base.FetchSPDataTable("GetUserWebAPIPassword", "@UserName", username);
			if (dt.Rows.Count > 0)
			{
				accountPassword = dt.Rows[0]["AccountPassword"].ToString();
				apiPassword = dt.Rows[0]["APIPassword"].ToString();
			}
		}
	}
}
