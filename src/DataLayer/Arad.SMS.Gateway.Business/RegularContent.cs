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
	public class RegularContent : BusinessEntityBase
	{
		public RegularContent(DataAccessBase dataAccessProvider = null)
			: base(TableNames.RegularContents.ToString(), dataAccessProvider) { }

		public DataTable GetPagedRegularContents(Guid userGuid, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			DataSet dataSetNews = base.FetchSPDataSet("GetPagedRegularContents",
																								"@UserGuid", userGuid,
																								"@PageNo", pageNo,
																								"@PageSize", pageSize,
																								"@SortField", sortField);
			resultCount = Helper.GetInt(dataSetNews.Tables[0].Rows[0]["RowCount"]);

			return dataSetNews.Tables[1];
		}

		public bool UpdateRegularContent(Common.RegularContent regularContent)
		{
			return ExecuteSPCommand("Update",
															"@Guid", regularContent.RegularContentGuid,
															"@Title", regularContent.Title,
															"@Type", regularContent.Type,
															"@Config", regularContent.Config,
															"@IsActive", regularContent.IsActive,
															"@WarningType", regularContent.WarningType,
															"@PrivateNumberGuid", regularContent.PrivateNumberGuid);
		}

		public DataTable GetRegularContent(Guid userGuid)
		{
			return FetchDataTable("SELECT * FROM [RegularContents] WHERE [IsActive] = 1 AND [UserGuid] = @UserGuid", "@UserGuid", userGuid);
		}

		public DataTable GetRegularContentForProcess()
		{
			return FetchSPDataTable("GetRegularContentForProcess");
		}

		public DataTable GetRegularContentFileType()
		{
			return FetchSPDataTable("GetRegularContentFileType");
		}

		public bool SendURLContentToReceiver(Guid regularContentGuid, Guid privateNumberGuid, Guid userGuid, string smsText,
																				 Business.SmsSentPeriodType periodType, int period, DateTime effectiveDateTime)
		{
			return ExecuteSPCommand("SendURLContentToReceiver",
															"@RegularContentGuid", regularContentGuid,
															"@PrivateNumberGuid", privateNumberGuid,
															"@UserGuid", userGuid,
															"@SmsText", smsText,
															"@PeriodType", periodType,
															"@Period", period,
															"@EffectiveDateTime", effectiveDateTime);
		}

		public bool SendDBContentToReceiver(Guid regularContentGuid, Guid privateNumberGuid, Guid userGuid, string smsText,
																				Business.SmsSentPeriodType periodType, int period, DateTime effectiveDateTime)
		{
			return ExecuteSPCommand("SendDBContentToReceiver",
															"@RegularContentGuid", regularContentGuid,
															"@PrivateNumberGuid", privateNumberGuid,
															"@UserGuid", userGuid,
															"@SmsText", smsText,
															"@PeriodType", periodType,
															"@Period", period,
															"@EffectiveDateTime", effectiveDateTime);
		}
	}
}
