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

using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Data;

namespace Arad.SMS.Gateway.Facade
{
	public class DomainSetting : FacadeEntityBase
	{
		public static bool InsertDomainSetting(Guid domainGuid, DataTable dtSetting)
		{
			Business.DomainSetting domainSettingController = new Business.DomainSetting();
			return domainSettingController.InsertDomainSettings(domainGuid, dtSetting);
		}

		public static DataTable GetDomainSettings(Guid domainGuid)
		{
			Business.DomainSetting domainSettingController = new Business.DomainSetting();
			return domainSettingController.GetDomainSettings(domainGuid);
		}

		//public static string GetSettingValue(Guid domainGuid, string key)
		//{
		//	Business.DomainSetting domainSettingController = new Business.DomainSetting();
		//	DataTable dataTableSetting = domainSettingController.GetSettingValue(domainGuid, key);
		//	if (dataTableSetting.Rows.Count > 0)
		//		return Helper.GetString(dataTableSetting.Rows[0]["Value"]);
		//	return string.Empty;
		//}
	}
}
