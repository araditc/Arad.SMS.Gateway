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
	public class Transaction : BusinessEntityBase
	{
		public Transaction(DataAccessBase dataAccessProvider = null) :
			base(TableNames.Transactions.ToString(), dataAccessProvider)
		{ }

		public DataTable GetUserTransaction(Guid userGuid)
		{
			return base.FetchSPDataTable("GetUserTransactions", "@UserGuid", userGuid);
		}

		public DataTable GetPagedUserTransaction(Guid userGuid, Guid referenceGuid, string query, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			DataSet dataSetTransaction = base.FetchSPDataSet("GetPagedUserTransactions",
																												"@UserGuid", userGuid,
																												"@ReferenceGuid", referenceGuid,
																												"@Query", query,
																												"@PageNo", pageNo,
																												"@PageSize", pageSize,
																												"@SortField", sortField);

			resultCount = Helper.GetInt(dataSetTransaction.Tables[0].Rows[0]["RowCount"]);

			return dataSetTransaction.Tables[1];
		}

		public DataTable GetPagedUsersTransaction(Guid userGuid, Guid domainGuid, string username, string query, string sortField, int pageNo, int pageSize, ref int resultCount,ref decimal totalCount)
		{
			DataSet dataSetTransaction = base.FetchSPDataSet("GetPagedUsersTransactions",
																											 "@UserGuid", userGuid,
																											 "@DomainGuid",domainGuid,
																											 "@UserName",username,
																											 "@Query", query,
																											 "@PageNo", pageNo,
																											 "@PageSize", pageSize,
																											 "@SortField", sortField);

			resultCount = Helper.GetInt(dataSetTransaction.Tables[0].Rows[0]["RowCount"]);

			if (dataSetTransaction.Tables[2].Rows.Count > 0)
				totalCount = Helper.GetInt(dataSetTransaction.Tables[2].Rows[0]["TotalCount"]);

			return dataSetTransaction.Tables[1];
		}

		public bool CalculateBenefit(Guid userGuid, decimal smsCount, Guid transactionGuid)
		{
			return base.ExecuteSPCommand("CalculateBenefit",
																	 "@UserGuid", userGuid,
																	 "@SmsCount", smsCount,
																	 "@TransactionGuid", transactionGuid);
		}
	}
}
