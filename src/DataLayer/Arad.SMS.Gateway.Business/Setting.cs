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

using System.Data;
using Arad.SMS.Gateway.Common;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;

namespace Arad.SMS.Gateway.Business
{
	public class Setting : BusinessEntityBase
	{
		public Setting(DataAccessBase dataAccessProvider = null)
			: base(TableNames.Settings.ToString(), dataAccessProvider) { }

		public string GetValue(int key)
		{
			DataTable settingDataTable = new DataTable();
			settingDataTable = base.FetchDataTable("SELECT * FROM [Settings] WHERE [Key]=@Key", "@Key", key);
			try
			{
				return Helper.GetString(settingDataTable.Rows[0]["Value"]);
			}
			catch
			{
				return string.Empty;
			}
		}

		public DataTable GetSettings(Guid userGuid)
		{
			return FetchSPDataTable("GetSetting", "@UserGuid", userGuid);
		}

		//public bool InsertSetting(DataTable dtSettings)
		//{
		//	return base.ExecuteSPCommand("InsertSetting", "@Settings", dtSettings);
		//}
		public bool InsertSetting(Guid userGuid, DataTable dtSettings)
		{
			return base.ExecuteSPCommand("InsertSetting", "@UserGuid", userGuid, "@Settings", dtSettings);
		}
	}
}
