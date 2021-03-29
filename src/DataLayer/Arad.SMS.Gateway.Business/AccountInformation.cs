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
using Arad.SMS.Gateway.Common;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;

namespace Arad.SMS.Gateway.Business
{
	public class AccountInformation : BusinessEntityBase
	{
		public AccountInformation(DataAccessBase dataAccessProvider = null)
			: base(TableNames.AccountInformations.ToString(), dataAccessProvider) { }

		//public DataTable GetTypeAccount(Guid userGuid, int type)
		//{
		//	return base.FetchSPDataTable("GetTypeAccount", "@UserGuid", userGuid, "@Type", type);
		//}

		public DataTable GetPagedAccountInformations(Guid userGuid, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			DataSet dataSetAccountInfo = base.FetchSPDataSet("GetPagedAccountInformations", "@UserGuid", userGuid,
																																									 "@SortField", sortField,
																																									 "@PageNo", pageNo,
																																									 "@PageSize", pageSize);
			resultCount = Helper.GetInt(dataSetAccountInfo.Tables[0].Rows[0]["RowCount"]);

			return dataSetAccountInfo.Tables[1];
		}

		public bool UpdateAccount(Common.AccountInformation accountInfo)
		{
			return base.ExecuteSPCommand("UpdateAccount", "@Guid", accountInfo.AccountInfoGuid,
																									 "@Owner", accountInfo.Owner,
																									 "@Branch", accountInfo.Branch,
																									 "@AccountNo", accountInfo.AccountNo,
																									 "@TerminalID", accountInfo.TerminalID,
																									 "@UserName", accountInfo.UserName,
																									 "@Password", accountInfo.Password,
																									 "@PinCode", accountInfo.PinCode,
																									 "@Bank", accountInfo.Bank,
																									 "@CardNo", accountInfo.CardNo,
																									 "@IsActive", accountInfo.IsActive,
																									 "@OnlineGatewayIsActive", accountInfo.OnlineGatewayIsActive);
		}

		public DataTable GetAccountsIsActiveOnline(Guid userGuid)
		{
			return base.FetchSPDataTable("GetAccountsIsActiveOnline", "@UserGuid", userGuid);
		}

		public DataTable GetAccountOfReferenceID(string ReferenceID)
		{
			return base.FetchSPDataTable("GetAccountOfReferenceID", "ReferenceID", ReferenceID);
		}
	}
}
