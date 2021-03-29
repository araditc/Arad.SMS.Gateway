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
using System.Collections.Generic;

namespace Arad.SMS.Gateway.Business
{
	public class Outbox : BusinessEntityBase
	{
		public Outbox(DataAccessBase dataAccessProvider = null)
			: base(TableNames.Outboxes.ToString(), dataAccessProvider) { }

		public bool UpdateStatus(Guid guid, SendStatus status, long id = 0)
		{
			return ExecuteSPCommand("UpdateStatus",
															"@Guid", guid,
															"@Id", id,
															"@Status", status);
		}

		public DataTable GetPagedSmses(Guid userGuid, Guid referenceGuid, string query, int pageNo, int pageSize, string sortField, ref int resultCount, ref int totalReceiverCount, ref decimal totalPrice)
		{
			DataSet dataSetOutBox = base.FetchSPDataSet("GetPagedSmses",
																									"@UserGuid", userGuid,
																									"@ReferenceGuid", referenceGuid,
																									"@Query", query,
																									"@PageNo", pageNo,
																									"@PageSize", pageSize,
																									"@SortField", sortField);
			resultCount = Helper.GetInt(dataSetOutBox.Tables[0].Rows[0]["RowCount"]);

			if (dataSetOutBox.Tables[2].Rows.Count > 0)
			{
				totalReceiverCount = Helper.GetInt(dataSetOutBox.Tables[2].Rows[0]["TotalReceiverCount"]);
				totalPrice = Helper.GetDecimal(dataSetOutBox.Tables[2].Rows[0]["TotalPrice"]);
			}

			return dataSetOutBox.Tables[1];
		}

		public DataTable GetPagedUserSmses(Guid userGuid, Guid domainGuid, string query,
																			 int pageNo, int pageSize, string sortField, ref int resultCount,
																			 ref int totalReceiverCount, ref decimal totalPrice,
																			 ref Dictionary<DeliveryStatus, Tuple<int, int>> dictionaryDelivery)
		{
			DataSet dataSetOutBox = base.FetchSPDataSet("GetPagedUserSmses",
																									"@UserGuid", userGuid,
																									"@DomainGuid", domainGuid,
																									"@Query", query,
																									"@PageNo", pageNo,
																									"@PageSize", pageSize,
																									"@SortField", sortField);
			resultCount = Helper.GetInt(dataSetOutBox.Tables[0].Rows[0]["RowCount"]);

			if (dataSetOutBox.Tables[2].Rows.Count > 0)
			{
				totalReceiverCount = Helper.GetInt(dataSetOutBox.Tables[2].Rows[0]["TotalReceiverCount"]);
				totalPrice = Helper.GetDecimal(dataSetOutBox.Tables[2].Rows[0]["TotalPrice"]);
				dictionaryDelivery.Add(DeliveryStatus.SentAndReceivedbyPhone, new Tuple<int, int>(Helper.GetInt(dataSetOutBox.Tables[2].Rows[0]["TotalDeliveredCount"]), Helper.GetInt(dataSetOutBox.Tables[2].Rows[0]["TotalSmsDeliveredCount"])));
				dictionaryDelivery.Add(DeliveryStatus.NotSent, new Tuple<int, int>(Helper.GetInt(dataSetOutBox.Tables[2].Rows[0]["TotalFailedCount"]), Helper.GetInt(dataSetOutBox.Tables[2].Rows[0]["TotalSmsFailedCount"])));
				dictionaryDelivery.Add(DeliveryStatus.SentToItc, new Tuple<int, int>(Helper.GetInt(dataSetOutBox.Tables[2].Rows[0]["TotalSentToICTCount"]), Helper.GetInt(dataSetOutBox.Tables[2].Rows[0]["TotalSmsSentToICTCount"])));
				dictionaryDelivery.Add(DeliveryStatus.ReceivedByItc, new Tuple<int, int>(Helper.GetInt(dataSetOutBox.Tables[2].Rows[0]["TotalDeliveredICTCount"]), Helper.GetInt(dataSetOutBox.Tables[2].Rows[0]["TotalSmsDeliveredICTCount"])));
				dictionaryDelivery.Add(DeliveryStatus.BlackList, new Tuple<int, int>(Helper.GetInt(dataSetOutBox.Tables[2].Rows[0]["TotalBlackListCount"]), Helper.GetInt(dataSetOutBox.Tables[2].Rows[0]["TotalSmsBlackListCount"])));
			}
			return dataSetOutBox.Tables[1];
		}

		public DataTable GetPagedUserManualSmses(string query, int pageNo, int pageSize, string sortField, ref int resultCount, ref int totalReceiverCount, ref decimal totalPrice)
		{
			DataSet dataSetOutBox = base.FetchSPDataSet("GetPagedUserManualSmses",
																									"@Query", query,
																									"@PageNo", pageNo,
																									"@PageSize", pageSize,
																									"@SortField", sortField);
			resultCount = Helper.GetInt(dataSetOutBox.Tables[0].Rows[0]["RowCount"]);

			if (dataSetOutBox.Tables[2].Rows.Count > 0)
			{
				totalReceiverCount = Helper.GetInt(dataSetOutBox.Tables[2].Rows[0]["TotalReceiverCount"]);
				totalPrice = Helper.GetDecimal(dataSetOutBox.Tables[2].Rows[0]["TotalPrice"]);
			}

			return dataSetOutBox.Tables[1];
		}

		public DataTable GetSendQueue(SmsSenderAgentReference smsSenderAgentRefrence, int agentQueueSize, int threadCount)
		{
			return FetchSPDataTable("GetSendQueue", "@SmsSenderAgentRefrence", smsSenderAgentRefrence,
																							"@Count", agentQueueSize,
																							"@ThreadCount", threadCount);
		}

		public DataTable GetOutboxReport(Guid referenceGuid)
		{
			return FetchSPDataTable("GetOutboxReport", "@OutboxGuid", referenceGuid);
		}

		public DataTable GetExportDataRequest(int recordCount)
		{
			return FetchSPDataTable("GetExportDataRequest", "@RecordCount", recordCount);
		}

		public bool UpdateExportDataRequest(DataTable dtSaveRequest)
		{
			return ExecuteSPCommand("UpdateExportDataRequest", "@Requests", dtSaveRequest);
		}

		public DataTable GetPagedExportData(Guid guid, int pageNo, int pageSize)
		{
			return FetchSPDataTable("GetPagedExportData",
															"@Guid", guid,
															"@PageNo", pageNo,
															"@PageSize", pageSize);
		}

		public DataTable GetPagedExportText(Guid guid, int pageNo, int pageSize)
		{
			return FetchSPDataTable("GetPagedExportText",
															"@Guid", guid,
															"@PageNo", pageNo,
															"@PageSize", pageSize);
		}

		public bool SetOutboxExportDataStatus(Guid outboxGuid)
		{
			return ExecuteCommand("UPDATE [Outboxes] SET [ExportDataStatus] = 2 WHERE [Guid] = @Guid", "@Guid", outboxGuid);
		}

		public bool SetOutboxExportTxtStatus(Guid outboxGuid)
		{
			return ExecuteCommand("UPDATE [Outboxes] SET [ExportTxtStatus] = 2 WHERE [Guid] = @Guid", "@Guid", outboxGuid);
		}

		public DataTable GetOutboxForGiveBackCredit()
		{
			return FetchSPDataTable("GetOutboxForGiveBackCredit");
		}

		public bool GiveBackBlackListAndFailedSend(Guid guid)
		{
			return ExecuteSPCommand("GiveBackBlackListAndFailedSend", "@Guid", guid);
		}

		public bool AddBlackListNumbersToTable(Guid guid)
		{
			return ExecuteSPCommand("AddBlackListNumbersToTable", "@OutboxGuid", guid);
		}

		public bool ResendFailedSms(Guid outboxGuid)
		{
			return ExecuteSPCommand("ResendFailedSms", "@Guid", outboxGuid);
		}

		public bool ArchiveNumbers(Guid guid)
		{
			return ExecuteSPCommand("ArchiveNumbers", "@OutboxGuid", guid);
		}

		public bool CheckReceiverCount(Guid guid)
		{
			return ExecuteSPCommand("CheckReceiverCount", "@OutboxGuid", guid);
		}
	}
}
