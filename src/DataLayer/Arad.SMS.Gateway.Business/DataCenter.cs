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

namespace Arad.SMS.Gateway.Business
{
	public class DataCenter : BusinessEntityBase
	{
		public DataCenter(DataAccessBase dataAccessProvider = null)
			: base(TableNames.DataCenters.ToString(), dataAccessProvider) { }

		public DataTable GetUserDataCenter(Guid userGuid, DataCenterType dataCenterType)
		{
			return base.FetchSPDataTable("GetUserDataCenter", "@UserGuid", userGuid,
																												"@DataCenterType",(int)dataCenterType);
		}

		public DataTable GetDomainMenu(Guid domainGuid, Business.DataLocation location,Business.Desktop desktop)
		{
			return base.FetchSPDataTable("GetDomainMenu",
																	 "@DomainGuid", domainGuid,
																	 "@Location", (int)location,
																	 "@Desktop",(int)desktop);
		}

		public bool UpdateDataCenter(Common.DataCenter dataCenter)
		{
			return base.ExecuteSPCommand("Update",
																	 "@Guid", dataCenter.DataCenterGuid,
																	 "@Title", dataCenter.Title,
																	 "@Type", dataCenter.Type,
																	 "@IsArchived", dataCenter.IsArchived);
		}

		public bool UpdateLocation(Common.DataCenter dataCenter)
		{
			return base.ExecuteSPCommand("UpdateLocation",
																	 "@Guid", dataCenter.DataCenterGuid,
																	 "@Location", dataCenter.Location,
																	 "@Desktop", dataCenter.Desktop);
		}
	}
}
