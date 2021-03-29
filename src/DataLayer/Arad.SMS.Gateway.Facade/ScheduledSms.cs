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
using Arad.SMS.Gateway.SqlLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;

namespace Arad.SMS.Gateway.Facade
{
	public class ScheduledSms : FacadeEntityBase
	{
		public static bool InsertSms(Common.ScheduledSms scheduledSms, List<string> lstNumbers)
		{
			Business.ScheduledSms scheduledSmsController = new Business.ScheduledSms();
			return scheduledSmsController.InsertSms(scheduledSms, string.Join(",", lstNumbers.ToArray()));
		}

		public static bool InsertGroupSms(Common.ScheduledSms scheduledSms)
		{
			Business.ScheduledSms scheduledSmsController = new Business.ScheduledSms();
			return scheduledSmsController.InsertGroupSms(scheduledSms);
		}

		public static bool InsertPeriodSms(Common.ScheduledSms scheduledSms, List<string> lstNumbers)
		{
			Business.ScheduledSms scheduledSmsController = new Business.ScheduledSms();
			scheduledSms.ScheduledSmsGuid = Guid.NewGuid();
			scheduledSms.TypeSend = (int)SmsSendType.SendPeriodSms;
			return scheduledSmsController.InsertPeriodSms(scheduledSms, string.Join(",", lstNumbers.ToArray()));
		}

		public static bool InsertGradualSms(Common.ScheduledSms scheduledSms, List<string> lstNumbers)
		{
			Business.ScheduledSms scheduledSmsController = new Business.ScheduledSms();
			scheduledSms.ScheduledSmsGuid = Guid.NewGuid();
			scheduledSms.TypeSend = (int)SmsSendType.SendGradualSms;
			return scheduledSmsController.InsertGradualSms(scheduledSms, string.Join(",", lstNumbers.ToArray()));
		}

		public static bool InsertFormatSms(Common.ScheduledSms scheduledSms, ref string formatInfo)
		{
			Business.ScheduledSms scheduledSmsController = new Business.ScheduledSms();
			scheduledSms.ScheduledSmsGuid = Guid.NewGuid();
			Transaction.GetSendFormatPrice(scheduledSms.UserGuid, Helper.GetGuid(scheduledSms.ReferenceGuid), scheduledSms.PrivateNumberGuid, ref formatInfo);
			scheduledSms.TypeSend = (int)SmsSendType.SendFormatSms;
			return scheduledSmsController.InsertFormatSms(scheduledSms);
		}

		public static bool GarbageCollector()
		{
			Business.ScheduledSms scheduledSmsController = new Business.ScheduledSms();
			return scheduledSmsController.GarbageCollector();
		}

		public static DataTable GetQueue(int count, SmsSenderAgentReference senderAgentRefrence)
		{
			Business.ScheduledSms scheduledSmsController = new Business.ScheduledSms();
			return scheduledSmsController.GetQueue(count, senderAgentRefrence);
		}

		public static DataTable GetUserQueue(Guid userGuid, string query, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.ScheduledSms scheduledSmsController = new Business.ScheduledSms();
			return scheduledSmsController.GetUserQueue(userGuid, query, sortField, pageNo, pageSize, ref resultCount);
		}

		public static DataTable GetPagedUsersQueue(Guid userGuid, string query, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.ScheduledSms scheduledSmsController = new Business.ScheduledSms();
			return scheduledSmsController.GetPagedUsersQueue(userGuid, query, sortField, pageNo, pageSize, ref resultCount);
		}

		public static bool RejectSms(Guid scheduledSmsGuid)
		{
			Business.ScheduledSms scheduledSmsController = new Business.ScheduledSms();
			return scheduledSmsController.Delete(scheduledSmsGuid);
		}

		public static bool ResendSms(Guid scheduledSmsGuid)
		{
			Business.ScheduledSms scheduledSmsController = new Business.ScheduledSms();
			return scheduledSmsController.ResendSms(scheduledSmsGuid);
		}

		public static bool ProcessRequest(Guid guid,
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
			Business.ScheduledSms scheduledSmsController = new Business.ScheduledSms();
			return scheduledSmsController.ProcessRequest(guid, id, scheduledSmsStatus, smsSendType, smsSenderAgentreference,
																									 privateNumberGuid, userGuid, sendSmsAlert, startSendTime, endSendTime);
		}

		public static Common.ScheduledSms Load(Guid scheduledSmsGuid)
		{
			Business.ScheduledSms scheduledSmsController = new Business.ScheduledSms();
			Common.ScheduledSms scheduledSms = new Common.ScheduledSms();
			scheduledSmsController.Load(scheduledSmsGuid, scheduledSms);
			return scheduledSms;
		}

		public static bool InsertBulkRequest(Common.ScheduledSms scheduledSms, string recipients)
		{
			Business.ScheduledSms scheduledSmsController = new Business.ScheduledSms();
			DataTable dtRecipients = new DataTable();
			dtRecipients.Columns.Add("ZoneGuid", typeof(Guid));
			dtRecipients.Columns.Add("ZipCode", typeof(string));
			dtRecipients.Columns.Add("Prefix", typeof(string));
			dtRecipients.Columns.Add("Type", typeof(int));
			dtRecipients.Columns.Add("Operator", typeof(int));
			dtRecipients.Columns.Add("FromIndex", typeof(int));
			dtRecipients.Columns.Add("Count", typeof(int));
			dtRecipients.Columns.Add("ScopeCount", typeof(int));

			try
			{
				if (scheduledSms.HasError)
					throw new Exception(scheduledSms.ErrorMessage);

				if (scheduledSms.DateTimeFuture < DateTime.Now)
					scheduledSms.DateTimeFuture = DateTime.Now.AddMinutes(2);

				var receivers = new XElement("Receivers");

				DataRow row;

				for (int counter = 0; counter < Helper.ImportIntData(recipients, "resultCount"); counter++)
				{
					row = dtRecipients.NewRow();
					receivers.Add(new XElement("Receiver", new XAttribute("ZoneGuid", Helper.ImportGuidData(recipients, ("ZoneGuid" + counter))),
																								 new XAttribute("ZipCode", Helper.ImportData(recipients, ("ZipCode" + counter))),
																								 new XAttribute("Prefix", Helper.ImportData(recipients, ("Prefix" + counter))),
																								 new XAttribute("Type", Helper.ImportData(recipients, ("Type" + counter))),
																								 new XAttribute("Operator", Helper.ImportData(recipients, ("Operator" + counter))),
																								 new XAttribute("FromIndex", Helper.ImportData(recipients, ("FromIndex" + counter))),
																								 new XAttribute("Count", Helper.ImportData(recipients, ("Count" + counter))),
																								 new XAttribute("ScopeCount", Helper.ImportData(recipients, ("ScopeCount" + counter))),
																								 new XAttribute("Title", Helper.ImportData(recipients, ("Title" + counter))),
																								 new XAttribute("Price", Helper.ImportData(recipients, ("SendPrice" + counter)))));

					row["ZoneGuid"] = Helper.ImportGuidData(recipients, ("ZoneGuid" + counter));
					row["ZipCode"] = Helper.ImportData(recipients, ("ZipCode" + counter));
					row["Prefix"] = Helper.ImportData(recipients, ("Prefix" + counter));
					row["Type"] = Helper.ImportIntData(recipients, ("Type" + counter));
					row["Operator"] = Helper.ImportIntData(recipients, ("Operator" + counter));
					row["FromIndex"] = Helper.ImportIntData(recipients, ("FromIndex" + counter));
					row["Count"] = Helper.ImportIntData(recipients, ("Count" + counter));
					row["ScopeCount"] = Helper.ImportIntData(recipients, ("ScopeCount" + counter));

					dtRecipients.Rows.Add(row);
				}

				if (dtRecipients.Rows.Count == 0)
					throw new Exception(Language.GetString("RecieverListIsEmpty"));

				XDocument doc = new XDocument();
				XElement root = new XElement("NewDataSet");
				XElement element = new XElement("Table");
				element.Add(new XElement("Guid", Guid.NewGuid()));
				element.Add(new XElement("PrivateNumberGuid", scheduledSms.PrivateNumberGuid));
				element.Add(new XElement("SmsText", scheduledSms.SmsText));
				element.Add(new XElement("SmsLen", Helper.GetSmsCount(scheduledSms.SmsText)));
				element.Add(new XElement("PresentType", scheduledSms.PresentType));
				element.Add(new XElement("Encoding", scheduledSms.Encoding));
				element.Add(new XElement("TypeSend", scheduledSms.TypeSend));
				element.Add(new XElement("DateTimeFuture", scheduledSms.DateTimeFuture));
				element.Add(new XElement("UserGuid", scheduledSms.UserGuid));
				element.Add(new XElement("IP", scheduledSms.IP));
				element.Add(new XElement("Browser", scheduledSms.Browser));
				element.Add(receivers);
				root.Add(element);

				doc.Add(root);

				scheduledSms.RequestXML = doc.ToString();

				return scheduledSmsController.InsertBulkRequest(scheduledSms, dtRecipients);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static bool InsertP2PSms(Common.ScheduledSms scheduledSms)
		{
			Business.ScheduledSms scheduledSmsController = new Business.ScheduledSms();
			return scheduledSmsController.InsertP2PSms(scheduledSms);
		}

		public static bool UpdateStatus(Guid scheduledSmsGuid, ScheduledSmsStatus status)
		{
			Business.ScheduledSms scheduledSmsController = new Business.ScheduledSms();
			return scheduledSmsController.UpdateStatus(scheduledSmsGuid, status);
		}
	}
}
