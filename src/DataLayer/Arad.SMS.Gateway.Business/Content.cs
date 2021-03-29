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
	public class Content : BusinessEntityBase
	{
		public Content(DataAccessBase dataAccessProvider = null)
			: base(TableNames.Contents.ToString(), dataAccessProvider) { }

		public DataTable GetPagedContents(Guid regularContentGuid, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			DataSet dataSetNews = base.FetchSPDataSet("GetPagedContents",
																								"@Guid", regularContentGuid,
																								"@PageNo", pageNo,
																								"@PageSize", pageSize,
																								"@SortField", sortField);
			resultCount = Helper.GetInt(dataSetNews.Tables[0].Rows[0]["RowCount"]);

			return dataSetNews.Tables[1];
		}

		public bool InsertContents(Guid regularContentGuid, DataTable dtContents)
		{
			return ExecuteSPCommand("InsertContents",
															"@RegularContentGuid", regularContentGuid,
															"@Contents", dtContents);
		}

		public bool Delete(string guids)
		{
			return ExecuteSPCommand("Delete", "@Guids", guids);
		}

		public bool SendContentToReceiver(Guid regularContentGuid, Guid privateNumberGuid,
																			Guid userGuid, Business.SmsSentPeriodType periodType, int period,
																			DateTime effectiveDateTime)
		{
			return ExecuteSPCommand("SendContentToReceiver",
															"@RegularContentGuid", regularContentGuid,
															"@PrivateNumberGuid", privateNumberGuid,
															"@UserGuid", userGuid,
															"@PeriodType", periodType,
															"@Period", period,
															"@EffectiveDateTime", effectiveDateTime);
		}
	}
}
