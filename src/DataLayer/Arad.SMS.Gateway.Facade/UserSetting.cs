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

using Arad.SMS.Gateway.Common;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace Arad.SMS.Gateway.Facade
{
	public class UserSetting : FacadeEntityBase
	{
		//public static bool InsertShortcut(Guid userGuid, string value)
		//{
		//	Business.UserSetting userSettingController = new Business.UserSetting();
		//	return userSettingController.InsertShortcut(userGuid, value);
		//}

		public static bool InsertUserSetting(Guid userGuid, DataTable dtSettings)
		{
			Business.UserSetting userSettingController = new Business.UserSetting();
			return userSettingController.InsertUserSettings(userGuid, dtSettings);
		}

		//public static DataTable GetUserShortcut(Guid UserGuid,string key)
		//{
		//	Business.UserSetting userSettingController = new Business.UserSetting();
		//	return userSettingController.GetSettingValue(UserGuid,key);
		//}

		private static List<Common.Menu> UserShortcuts = new List<Common.Menu>();
		public static List<Menu> GetUserShortcut(Guid userGuid)
		{
			UserShortcuts.Clear();
			Business.UserSetting userSettingController = new Business.UserSetting();
			DataTable dtServiceInfo = userSettingController.GetUserShortcutForLoad(userGuid);
			foreach (DataRow row in dtServiceInfo.Rows)
			{
				var menu = new Common.Menu
						{
							Title = row["Title"].ToString(),
							Path = "/PageLoader.aspx?c=" + Helper.Encrypt(row["ReferencePageKey"], HttpContext.Current.Session),
							SmallIcon = row["IconAddress"].ToString(),
							LargeIcon = row["LargeIcon"].ToString(),
						};

				UserShortcuts.Add(menu);
			}
			return UserShortcuts;
		}

		public static DataTable GetUserShortcutForLoad(Guid userGuid)
		{
			Business.UserSetting userSettingController = new Business.UserSetting();
			return userSettingController.GetUserShortcutForLoad(userGuid);
		}

		public static DataTable GetUserSettings(Guid userGuid)
		{
			Business.UserSetting userSettingController = new Business.UserSetting();
			return userSettingController.GetUserSettings(userGuid);
		}

		public static string GetSettingValue(Guid userGuid, Business.AccountSetting key)
		{
			Business.UserSetting userSettingController = new Business.UserSetting();
			return userSettingController.GetSettingValue(userGuid, key);
		}

		//public static bool UpdateSetting(Guid userGuid, string key, string value)
		//{
		//	Business.UserSetting userSettingController = new Business.UserSetting();
		//	return userSettingController.UpdateSetting(userGuid, key, value);
		//}

		public static DataTable GetUserExpired()
		{
			Business.UserSetting userSettingController = new Business.UserSetting();
			return userSettingController.GetUserExpired();
		}

		//public static DataTable GetUserLowCredit()
		//{
		//	Business.UserSetting userSettingController = new Business.UserSetting();
		//	return userSettingController.GetUserLowCredit();
		//}

		public static bool UpdateSetting(Guid userGuid, Business.AccountSetting accountSetting, string value)
		{
			Business.UserSetting userSettingController = new Business.UserSetting();
			return userSettingController.UpdateSetting(userGuid, accountSetting, value);
		}

		public static void GetUserWebAPIPassword(string username, ref string accountPassword, ref string apiPassword)
		{
			Business.UserSetting userSettingController = new Business.UserSetting();
			userSettingController.GetUserWebAPIPassword(username, ref accountPassword, ref apiPassword);
		}
	}
}
