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
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using Arad.SMS.Gateway.Common;

namespace Arad.SMS.Gateway.Business
{
	public class ServiceGroup : BusinessEntityBase
	{
		public ServiceGroup(DataAccessBase dataAccessProvider = null)
			: base(TableNames.ServiceGroups.ToString(), dataAccessProvider) { }

		public DataTable GetPagedServiceGroup(string title, string sortField, int pageNo, int pageSize, ref int rowCount)
		{
			DataSet dataSetServiceGroup = base.FetchSPDataSet("GetPagedServiceGroup", "@Title", title,
																																						 "@PageNo", pageNo,
																																						 "@PageSize", pageSize,
																																						 "@SortField", sortField);

			rowCount = Helper.GetInt(dataSetServiceGroup.Tables[0].Rows[0]["RowCount"]);

			return dataSetServiceGroup.Tables[1];
		}

		public DataTable GetAllGroupsWithServices(Guid userGuid)
		{
			return base.FetchSPDataTable("GetAllGroupsWithServices", "@UserGuid", userGuid);
		}

		public bool UpdateServiceGroup(Common.ServiceGroup serviceGroup)
		{
			return base.ExecuteSPCommand("UpdateServiceGroup", "@Guid", serviceGroup.ServiceGroupGuid,
																												"@Title", serviceGroup.Title,
																												"@IconAddress", serviceGroup.IconAddress,
																												"@LargeIcon", serviceGroup.LargeIcon,
																												"@ParentGuid",serviceGroup.parentGuid,
																												"@Order", serviceGroup.Order);
		}

		public DataTable GetParentGroups()
		{
			return base.FetchSPDataTable("GetParentGroups");
		}

		public DataTable GetUserGroupsWithServices(Guid userGuid)
		{
			return base.FetchSPDataTable("GetUserGroupsWithServices", "@UserGuid", userGuid);
		}
	}
}
