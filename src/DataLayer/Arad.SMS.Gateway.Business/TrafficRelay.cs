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
using System.Threading.Tasks;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using Arad.SMS.Gateway.Common;
using System.Data;
using Arad.SMS.Gateway.GeneralLibrary;

namespace Arad.SMS.Gateway.Business
{
	public class TrafficRelay : BusinessEntityBase
	{
		public TrafficRelay(DataAccessBase dataAccessProvider = null)
			: base(TableNames.TrafficRelays.ToString(), dataAccessProvider) { }

		public DataTable GetPagedTrafficRelays(Guid userGuid, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			DataSet dataSetSmsParser = base.FetchSPDataSet("GetPagedTrafficRelays",
																										 "@UserGuid", userGuid,
																										 "@PageNo", pageNo,
																										 "@PageSize", pageSize,
																										 "@SortField", sortField);

			resultCount = Helper.GetInt(dataSetSmsParser.Tables[0].Rows[0]["RowCount"]);

			return dataSetSmsParser.Tables[1];
		}

		public bool InsertUrl(Common.TrafficRelay trafficRelay)
		{
			return ExecuteSPCommand("InsertUrl",
															"@Guid", Guid.NewGuid(),
															"@Title", trafficRelay.Title,
															"@Url", trafficRelay.Url,
															"@TryCount", trafficRelay.TryCount,
															"@IsActive", trafficRelay.IsActive,
															"@UserGuid", trafficRelay.UserGuid);
		}

		public bool UpdateUrl(Common.TrafficRelay trafficRelay)
		{
			return ExecuteSPCommand("UpdateUrl",
															"@Guid", trafficRelay.TrafficRelayGuid,
															"@Title", trafficRelay.Title,
															"@Url", trafficRelay.Url,
															"@TryCount", trafficRelay.TryCount,
															"@IsActive", trafficRelay.IsActive);
		}
	}
}
