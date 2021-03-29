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
	public class Fish : BusinessEntityBase
	{
		public Fish(DataAccessBase dataAccessProvider = null)
			: base(TableNames.Fishes.ToString(), dataAccessProvider) { }

		public bool InsertFishPayment(Common.Fish fish)
		{
			Guid guid = Guid.NewGuid();
			return base.ExecuteSPCommand("InsertFishPayment",
																	 "@Guid", guid,
																	 "@CreateDate", Helper.GetDateTimeForDB(fish.CreateDate),
																	 "@BillNumber", fish.BillNumber,
																	 "@SmsCount", fish.SmsCount,
																	 "@Amount", fish.Amount,
																	 "@PaymentDate", Helper.GetDateTimeForDB(fish.PaymentDate),
																	 "@Description", fish.Description,
																	 "@Type", fish.Type,
																	 "@Status", fish.Status,
																	 "@AccountInformationGuid", fish.AccountInformationGuid,
																	 "@UserGuid", fish.UserGuid);
		}

		public bool IsDuplicateBillNumber(string billNumber)
		{
			return FetchSPDataTable("IsDuplicateBillNumber", "@BillNumber", billNumber).Rows.Count > 0 ? true : false;
		}

		public bool UpdateStatus(Guid fishGuid, FishStatus status, Guid transactionGuid)
		{
			return base.ExecuteSPCommand("UpdateStatus", "@Guid", fishGuid, "@Status", (int)status, "@TransactionGuid", transactionGuid);
		}

		public bool UpdateDescription(Guid fishGuid, string description, FishStatus status)
		{
			return base.ExecuteCommand("UPDATE [dbo].[Fishes] SET [Description] = @Description,[Status] = @Status WHERE [Guid] = @Guid",
																 "@Guid", fishGuid,
																 "@Description", description,
																 "@Status", (int)status);
		}

		public DataTable GetPagedFishesForConfirm(string query, Guid parentGuid, Guid userGuid, string sortField, int pageNo, int pageSize, ref int resultCount, ref int totalSmsCount, ref decimal totalPrice)
		{
			DataSet dataSetUserFish = base.FetchSPDataSet("GetPagedFishesForConfirm",
																										"@Query", query,
																										"@UserGuid", userGuid,
																										"@ParentGuid", parentGuid,
																										"@PageNo", pageNo,
																										"@PageSize", pageSize,
																										"@SortField", sortField);
			resultCount = Helper.GetInt(dataSetUserFish.Tables[0].Rows[0]["RowCount"]);

			if (dataSetUserFish.Tables[2].Rows.Count > 0)
			{
				totalSmsCount = Helper.GetInt(dataSetUserFish.Tables[2].Rows[0]["TotalSmsCount"]);
				totalPrice = Helper.GetDecimal(dataSetUserFish.Tables[2].Rows[0]["TotalPrice"]);
			}

			return dataSetUserFish.Tables[1];
		}

		public DataTable GetPagedUserFishes(Guid userGuid, string query, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			DataSet dataSetUserFish = base.FetchSPDataSet("GetPagedUserFishes",
																										"@UserGuid", userGuid,
																										"@Query", query,
																										"@PageNo", pageNo,
																										"@PageSize", pageSize,
																										"@SortField", sortField);
			resultCount = Helper.GetInt(dataSetUserFish.Tables[0].Rows[0]["RowCount"]);

			return dataSetUserFish.Tables[1];
		}

		public int GetFishStatus(Guid fishGuid)
		{
			return base.GetSPIntFieldValue("GetFishStatus", "@Guid", fishGuid);
		}

		public bool InsertOnlinePayment(Common.Fish fish)
		{
			Guid guid = Guid.NewGuid();
			return base.ExecuteSPCommand("InsertOnlinePayment",
																	 "@Guid", guid,
																	 "@ReferenceID", fish.ReferenceID,
																	 "@CreateDate", Helper.GetDateTimeForDB(fish.CreateDate),
																	 "@PaymentDate", Helper.GetDateTimeForDB(fish.PaymentDate),
																	 "@SmsCount", fish.SmsCount,
																	 "@Amount", fish.Amount,
																	 "@OrderID", fish.OrderID,
																	 "@Description", fish.Description,
																	 "@Type", fish.Type,
																	 "@Status", fish.Status,
																	 "@ReferenceGuid", fish.ReferenceGuid,
																	 "@AccountInformationGuid", fish.AccountInformationGuid,
																	 "@UserGuid", fish.UserGuid);
		}

		public bool UpdateOnlineFish(Guid fishGuid, FishStatus fishStatus, long billNumber, Guid transactionGuid)
		{
			return base.ExecuteSPCommand("UpdateOnlineFish",
																	 "@Guid", fishGuid,
																	 "@Status", (int)fishStatus,
																	 "@BillNumber", billNumber,
																	 "@TransactionGuid", transactionGuid);
		}
	}
}
