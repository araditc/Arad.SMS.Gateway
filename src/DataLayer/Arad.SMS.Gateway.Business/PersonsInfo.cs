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
using System.Data;

namespace Arad.SMS.Gateway.Business
{
	public class PersonsInfo : BusinessEntityBase
	{
		public PersonsInfo(DataAccessBase dataAccessProvider = null)
			: base(TableNames.PersonsInfo.ToString(), dataAccessProvider) { }

		public int GetCount(Guid zoneGuid, string prefix, string zipcode, NumberType type, int opt)
		{
			DataTable dtCount = FetchSPDataTable("GetCount",
																					 "@ZoneGuid", zoneGuid,
																					 "@Prefix", prefix,
																					 "@ZipCode", zipcode,
																					 "@NumberType", (int)type,
																					 "@Operator", opt);
			return Helper.GetInt(dtCount.Rows[0]["Count"]);
		}

		public DataTable GetPagedBlackListNumbers(string query, int pageNo, int pageSize, string sortField, ref int resultCount)
		{
			DataSet dataSetOutBox = base.FetchSPDataSet("GetPagedBlackListNumbers",
																									"@Query", query,
																									"@PageNo", pageNo,
																									"@PageSize", pageSize,
																									"@SortField", sortField);
			resultCount = Helper.GetInt(dataSetOutBox.Tables[0].Rows[0]["RowCount"]);

			return dataSetOutBox.Tables[1];
		}

		public bool UpdateBlackListStatus(Guid guid, bool isBlackList)
		{
			return ExecuteSPCommand("UpdateBlackListStatus", "@Guid", guid, "@IsBlackList", isBlackList);
		}

		public bool UpdateBlackListStatus(DataTable dtNumbers, bool isBlackList)
		{
			return ExecuteSPCommand("UpdateBlackListTableStatus", "@Numbers", dtNumbers, "@IsBlackList", isBlackList);
		}
	}
}
