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
	public class ScheduledSms : BusinessEntityBase
	{
		public ScheduledSms(DataAccessBase dataAccessProvider = null)
			: base(TableNames.ScheduledSmses.ToString(), dataAccessProvider)
		{ }

		public bool InsertSms(Common.ScheduledSms scheduledSms, string recievers)
		{
			//scheduledSms.ScheduledSmsGuid = Guid.NewGuid();
			return ExecuteSPCommand("InsertSms",
															"@Guid", scheduledSms.ScheduledSmsGuid,
															"@PrivateNumberGuid", scheduledSms.PrivateNumberGuid,
															"@Reciever", recievers,
															"@SmsText", scheduledSms.SmsText,
															"@SmsLen", scheduledSms.SmsLen,
															"@PresentType", scheduledSms.PresentType,
															"@Encoding", scheduledSms.Encoding,
															"@TypeSend", scheduledSms.TypeSend,
															"@Status", scheduledSms.Status,
															"@DateTimeFuture", scheduledSms.DateTimeFuture,
															"@UserGuid", scheduledSms.UserGuid,
															"@IPAddress", scheduledSms.IP,
															"@Browser", scheduledSms.Browser);
		}

		public bool InsertGroupSms(Common.ScheduledSms scheduledSms)
		{
			//scheduledSms.ScheduledSmsGuid = Guid.NewGuid();
			return ExecuteSPCommand("InsertGroupSms",
															"@Guid", scheduledSms.ScheduledSmsGuid,
															"@PrivateNumberGuid", scheduledSms.PrivateNumberGuid,
															"@ReferenceGuid", scheduledSms.ReferenceGuid,
															"@SmsText", scheduledSms.SmsText,
															"@SmsLen", scheduledSms.SmsLen,
															"@PresentType", scheduledSms.PresentType,
															"@Encoding", scheduledSms.Encoding,
															"@TypeSend", scheduledSms.TypeSend,
															"@Status", scheduledSms.Status,
															"@DateTimeFuture", scheduledSms.DateTimeFuture,
															"@UserGuid", scheduledSms.UserGuid,
															"@IPAddress", scheduledSms.IP,
															"@Browser", scheduledSms.Browser);
		}

		public bool InsertPeriodSms(Common.ScheduledSms scheduledSms, string recievers)
		{
			return ExecuteSPCommand("InsertPeriodSms",
															"@Guid", scheduledSms.ScheduledSmsGuid,
															"@PrivateNumberGuid", scheduledSms.PrivateNumberGuid,
															"@Reciever", recievers,
															"@ReferenceGuid", scheduledSms.ReferenceGuid,
															"@SmsText", scheduledSms.SmsText,
															"@SmsLen", scheduledSms.SmsLen,
															"@PresentType", scheduledSms.PresentType,
															"@Encoding", scheduledSms.Encoding,
															"@TypeSend", scheduledSms.TypeSend,
															"@Status", scheduledSms.Status,
															"@Period", scheduledSms.Period,
															"@PeriodType", scheduledSms.PeriodType,
															"@StartDateTime", scheduledSms.StartDateTime,
															"@EndDateTime", scheduledSms.EndDateTime,
															"@UserGuid", scheduledSms.UserGuid,
															"@IPAddress", scheduledSms.IP,
															"@Browser", scheduledSms.Browser);
		}

		public bool GarbageCollector()
		{
			return ExecuteSPCommand("GarbageCollector");
		}

		public bool InsertGradualSms(Common.ScheduledSms scheduledSms, string recievers)
		{
			return ExecuteSPCommand("InsertGradualSms",
															"@Guid", scheduledSms.ScheduledSmsGuid,
															"@PrivateNumberGuid", scheduledSms.PrivateNumberGuid,
															"@Reciever", recievers,
															"@ReferenceGuid", scheduledSms.ReferenceGuid,
															"@SmsText", scheduledSms.SmsText,
															"@SmsLen", scheduledSms.SmsLen,
															"@PresentType", scheduledSms.PresentType,
															"@Encoding", scheduledSms.Encoding,
															"@TypeSend", scheduledSms.TypeSend,
															"@Status", scheduledSms.Status,
															"@DateTimeFuture", scheduledSms.DateTimeFuture,
															"@Period", scheduledSms.Period,
															"@PeriodType", scheduledSms.PeriodType,
															"@SendPageNo", scheduledSms.SendPageNo,
															"@SendPageSize", scheduledSms.SendPageSize,
															"@UserGuid", scheduledSms.UserGuid,
															"@IPAddress", scheduledSms.IP,
															"@Browser", scheduledSms.Browser);
		}

		public bool InsertFormatSms(Common.ScheduledSms scheduledSms)
		{
			return ExecuteSPCommand("InsertFormatSms",
															"@Guid", scheduledSms.ScheduledSmsGuid,
															"@PrivateNumberGuid", scheduledSms.PrivateNumberGuid,
															"@ReferenceGuid", scheduledSms.ReferenceGuid,
															"@TypeSend", scheduledSms.TypeSend,
															"@Status", scheduledSms.Status,
															"@DateTimeFuture", scheduledSms.DateTimeFuture,
															"@UserGuid", scheduledSms.UserGuid,
															"@IPAddress", scheduledSms.IP,
															"@Browser", scheduledSms.Browser);
		}

		public DataTable GetUserQueue(Guid userGuid, string query, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			DataSet dataSetSms = base.FetchSPDataSet("GetUserQueue",
																							 "@UserGuid", userGuid,
																							 "@Query", query,
																							 "@PageNo", pageNo,
																							 "@PageSize", pageSize,
																							 "@SortField", sortField);
			resultCount = Helper.GetInt(dataSetSms.Tables[0].Rows[0]["RowCount"]);

			return dataSetSms.Tables[1];
		}

		public DataTable GetPagedUsersQueue(Guid userGuid, string query, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			DataSet dataSetSms = base.FetchSPDataSet("GetPagedUsersQueue",
																							 "@UserGuid", userGuid,
																							 "@Query", query,
																							 "@PageNo", pageNo,
																							 "@PageSize", pageSize,
																							 "@SortField", sortField);
			resultCount = Helper.GetInt(dataSetSms.Tables[0].Rows[0]["RowCount"]);

			return dataSetSms.Tables[1];
		}

		public bool UpdateStatus(Guid scheduledSmsGuid, ScheduledSmsStatus status)
		{
			return ExecuteSPCommand("UpdateStatus", "@Guid", scheduledSmsGuid, "@Status", (int)status);
		}

		public bool ResendSms(Guid scheduledSmsGuid)
		{
			return ExecuteSPCommand("ResendSms", "@Guid", scheduledSmsGuid);
		}

		public bool ProcessRequest(Guid guid,
															 long id,
															 ScheduledSmsStatus scheduledSmsStatus,
															 SmsSendType smsSendType,
															 int smsSenderAgentreference,
															 Guid privateNumberGuid,
															 Guid userGuid,
															 bool sendSmsAlert,
															 TimeSpan startSendTime,
															 TimeSpan endSendTime)
		{
			return ExecuteSPCommand("ProcessRequest",
															"@Guid", guid,
															"@ID", id,
															"@Status", (int)scheduledSmsStatus,
															"@SendType", (int)smsSendType,
															"@SmsSenderAgentReference", smsSenderAgentreference,
															"@PrivateNumberGuid", privateNumberGuid,
															"@UserGuid", userGuid,
															"@SendSmsAlert", sendSmsAlert,
															"@StartSendTime", startSendTime,
															"@EndSendTime", endSendTime);
		}

		public DataTable GetQueue(int count, SmsSenderAgentReference senderAgentRefrence)
		{
			return FetchSPDataTable("GetQueue",
															"@Count", count,
															"@SmsSenderAgentReference", (int)senderAgentRefrence);
		}

		public bool InsertBulkRequest(Common.ScheduledSms scheduledSms, DataTable dtRecipients)
		{
			return ExecuteSPCommand("InsertBulkRequest",
															"@Guid", scheduledSms.ScheduledSmsGuid,
															"@PrivateNumberGuid", scheduledSms.PrivateNumberGuid,
															"@ReferenceGuid", scheduledSms.ReferenceGuid,
															"@SmsText", scheduledSms.SmsText,
															"@SmsLen", scheduledSms.SmsLen,
															"@PresentType", scheduledSms.PresentType,
															"@Encoding", scheduledSms.Encoding,
															"@TypeSend", scheduledSms.TypeSend,
															"@Status", scheduledSms.Status,
															"@DateTimeFuture", scheduledSms.DateTimeFuture,
															"@UserGuid", scheduledSms.UserGuid,
															"@RequestXMl", scheduledSms.RequestXML,
															"@IPAddress", scheduledSms.IP,
															"@Browser", scheduledSms.Browser,
															"@Recipients", dtRecipients);
		}

		public bool InsertP2PSms(Common.ScheduledSms scheduledSms)
		{
			return ExecuteSPCommand("InsertP2PSms",
															"@Guid", scheduledSms.ScheduledSmsGuid,
															"@PrivateNumberGuid", scheduledSms.PrivateNumberGuid,
															"@FilePath", scheduledSms.FilePath,
															"@SmsPattern", scheduledSms.SmsPattern,
															"@TypeSend", scheduledSms.TypeSend,
															"@Status", scheduledSms.Status,
															"@DateTimeFuture", scheduledSms.DateTimeFuture,
															"@UserGuid", scheduledSms.UserGuid,
															"@IPAddress", scheduledSms.IP,
															"@Browser", scheduledSms.Browser);
		}
	}
}
