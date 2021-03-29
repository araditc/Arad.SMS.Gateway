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
	public class Access : BusinessEntityBase
	{
		public Access(DataAccessBase dataAccessProvider = null)
			: base(TableNames.Accesses.ToString(), dataAccessProvider) { }

		public DataTable GetPagedAccess(Guid serviceGuid, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			DataSet dataSetAccess = base.FetchSPDataSet("GetPagedAccess", "@ServiceGuid", serviceGuid,
																																	 "@PageNo", pageNo,
																																	 "@PageSize", pageSize,
																																	 "@SortField", sortField);
			resultCount = Helper.GetInt(dataSetAccess.Tables[0].Rows[0]["RowCount"]);

			return dataSetAccess.Tables[1];
		}

		public bool UpdateAccess(Common.Access access)
		{
			return base.ExecuteSPCommand("UpdateAccess", "@Guid", access.AccessGuid,
																									"@ServiceGuid", access.ServiceGuid,
																									"@ReferencePermissionsKey", access.ReferencePermissionsKey);
		}
	}
}
