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
	public class LoginStat : BusinessEntityBase
	{
		public LoginStat(DataAccessBase dataAccessProvider = null)
			: base(TableNames.LoginStats.ToString(), dataAccessProvider) { }

		public DataTable GetUserLoginStats(Guid userGuid, string sortField, int pageNo, int pageSize, ref int rowCount)
		{
			DataSet usersInfo = base.FetchSPDataSet("GetUserLoginStats",
																																 "@UserGuid", userGuid,
																																 "@PageNo", pageNo,
																																 "@PageSize", pageSize,
																																 "@SortField", sortField);
			rowCount = Helper.GetInt(usersInfo.Tables[1].Rows[0]["RowCount"]);

			return usersInfo.Tables[0];
		}

	}
}
