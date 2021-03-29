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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using Arad.SMS.Gateway.Common;

namespace Arad.SMS.Gateway.Business
{
	public class Service : BusinessEntityBase
	{
		public Service(DataAccessBase dataAccessProvider = null)
			: base(TableNames.Services.ToString(), dataAccessProvider) { }

		public DataTable GetPagedService(string query, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			DataSet dataSetService = base.FetchSPDataSet("GetPagedService",
																									 "@Query", query,
																									 "@PageNo", pageNo,
																									 "@PageSize", pageSize,
																									 "@SortField", sortField);
			resultCount = Helper.GetInt(dataSetService.Tables[0].Rows[0]["RowCount"]);

			return dataSetService.Tables[1];
		}

		public DataTable GetServiceOfUserForDeterminePrice(Guid userGuid, Guid parentGuid)
		{
			return base.FetchSPDataTable("GetServiceOfUserForDeterminePrice", "@UserGuid", userGuid,
																																				"@ParentGuid", parentGuid);
		}

		public bool UpdateService(Common.Service service)
		{
			return base.ExecuteSPCommand("Update", "@Guid", service.ServiceGuid,
																						"@Title", service.Title,
																						"@IconAddress", service.IconAddress,
																						"@LargeIcon", service.LargeIcon,
																						"@Presentable", service.Presentable,
																						"@ReferencePageKey", service.ReferencePageKey,
																						"@ReferenceServiceKey", service.ReferenceServiceKey,
																						"@Order", service.Order,
																						"@ServiceGroupGuid", service.ServiceGroupGuid);
		}

		public DataTable GetServiceOfUserForDefineGlobalServicePrice(Guid userGuid)
		{
			return FetchSPDataTable("GetServiceOfUserForDefineGlobalServicePrice", "@UserGuid", userGuid);
		}
	}
}
