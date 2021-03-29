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
	public class Inbox : BusinessEntityBase
	{
		public Inbox(DataAccessBase dataAccessProvider = null)
			: base(TableNames.Inboxes.ToString(), dataAccessProvider) { }

		public DataTable GetPagedSmses(Guid userGuid, Guid inboxGroupGuid, string query, int pageNo, int pageSize, string sortField, ref int resultCount)
		{
			DataSet dataSetSmses = base.FetchSPDataSet("GetPagedSmses",
																								 "@UserGuid", userGuid,
																								 "@InboxGroupGuid", inboxGroupGuid,
																								 "@Query", query,
																								 "@PageNo", pageNo,
																								 "@PageSize", pageSize,
																								 "@SortField", sortField);
			resultCount = Helper.GetInt(dataSetSmses.Tables[0].Rows[0]["RowCount"]);
			return dataSetSmses.Tables[1];
		}

		public DataTable GetPagedUserSmses(Guid userGuid, string query, int pageNo, int pageSize, string sortField, ref int resultCount)
		{
			DataSet dataSetSmses = base.FetchSPDataSet("GetPagedUserSmses",
																								 "@UserGuid", userGuid,
																								 "@Query", query,
																								 "@PageNo", pageNo,
																								 "@PageSize", pageSize,
																								 "@SortField", sortField);
			resultCount = Helper.GetInt(dataSetSmses.Tables[0].Rows[0]["RowCount"]);
			return dataSetSmses.Tables[1];
		}

		public DataTable GetChartDetailsAtSpecificDate(Guid userGuid, DateTime fromDateTime, DateTime toDateTime, int pageNo, int pageSize, ref int rowCount)
		{
			DataSet dataSetChartDetails = base.FetchSPDataSet("GetChartDetailsAtSpecificDate",
																												"@UserGuid", userGuid,
																												"@FromDateTime", fromDateTime,
																												"@ToDateTime", toDateTime,
																												"@PageNo", pageNo,
																												"@PageSize", pageSize);

			rowCount = Helper.GetInt(dataSetChartDetails.Tables[0].Rows[0]["RowCount"]);

			return dataSetChartDetails.Tables[1];
		}

		public DataTable InsertReceiveSms(Common.ReceiveMessage receiveSms)
		{
			//var encoding = System.Text.Encoding.GetEncoding("UTF-8");
			//receiveSms.SmsText = encoding.GetString(Convert.FromBase64String(receiveSms.SmsText));

			return FetchSPDataTable("InsertReceiveSms",
															"@SmsText", receiveSms.SmsText,
															"@Sender", receiveSms.Sender,
															"@ReceiveDateTime", receiveSms.ReceiveDateTime,
															"@Receiver", receiveSms.Receiver,
															"@Udh", receiveSms.UDH,
															"@InboxGroupGuid", Guid.Empty);
		}

		public long GetCountNumberOfGroup(Guid groupGuid)
		{
			return Helper.GetLong(base.FetchDataTable("SELECT COUNT(*) AS [RowCount] FROM [Inboxes] WHERE [IsDeleted] = 0 AND [InboxGroupGuid] = @GroupGuid", "@GroupGuid", groupGuid).Rows[0]["RowCount"]);
		}

		public bool ChangeInboxGroup(Guid inboxGuid, Guid inboxGroupGuid)
		{
			return ExecuteCommand("UPDATE [Inboxes] SET [InboxGroupGuid] = @InboxGroupGuid WHERE [Guid] = @Guid", "@InboxGroupGuid", inboxGroupGuid, "@Guid", inboxGuid);
		}

		public bool DeleteMultipleRow(string guids)
		{
			return ExecuteSPCommand("DeleteMultipleRow", "@Guids", guids);
		}

		public DataTable GetPagedParserSms(Guid parserGuid, Guid formulaGuid, int lottery, string sender, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			DataSet ds = FetchSPDataSet("GetPagedParserSms",
																	"@ParserGuid", parserGuid,
																	"@FormulaGuid", formulaGuid,
																	"@Lottery", lottery,
																	"@Sender", sender,
																	"@SortField", sortField,
																	"@PageNo", pageNo,
																	"@PageSize", pageSize);

			resultCount = Helper.GetInt(ds.Tables[0].Rows[0]["RowCount"]);
			return ds.Tables[1];
		}

		public DataTable GetParserSmsReport(Guid ParserGuid)
		{
			return FetchSPDataTable("GetParserSmsReport", "@ParserGuid", ParserGuid);
		}
	}
}
