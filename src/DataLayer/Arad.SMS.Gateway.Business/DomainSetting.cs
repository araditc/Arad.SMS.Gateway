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
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using Arad.SMS.Gateway.Common;
using System.Data;
using Arad.SMS.Gateway.GeneralLibrary;

namespace Arad.SMS.Gateway.Business
{
	public class DomainSetting : BusinessEntityBase
	{
		public DomainSetting(DataAccessBase dataAccessProvider = null)
			: base(TableNames.DomainSettings.ToString(), dataAccessProvider) { }

		public bool InsertDomainSettings(Guid domainGuid, DataTable dtSetting)
		{
			return ExecuteSPCommand("InsertSetting",
															"@DomainGuid", domainGuid,
															"@Setting", dtSetting);
		}

		//public bool UpdateSetting(Guid domainGuid, string key, string value)
		//{
		//	return base.ExecuteSPCommand("UpdateSetting",
		//																								"@DomainGuid", domainGuid,
		//																								"@Key", key,
		//																								"@Value", value);
		//}

		//public DataTable GetSettingValue(Guid domainGuid, string key)
		//{
		//	return base.FetchSPDataTable("GetSettingValue", "@DomainGuid", domainGuid,
		//																									"@Key", key);
		//}

		public DataTable GetDomainSettings(Guid domainGuid)
		{
			return base.FetchSPDataTable("GetDomainSettings", "@DomainGuid", domainGuid);
		}

	}
}
