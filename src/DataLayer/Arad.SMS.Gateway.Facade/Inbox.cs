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

using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Data;

namespace Arad.SMS.Gateway.Facade
{
	public class Inbox : FacadeEntityBase
	{
		public static DataTable GetPagedSmses(Guid userGuid, Guid inboxGroupGuid, string query, int pageNo, int pageSize, string sortField, ref int resultCount)
		{
			Business.Inbox inboxController = new Business.Inbox();
			return inboxController.GetPagedSmses(userGuid, inboxGroupGuid, query, pageNo, pageSize, sortField, ref resultCount);
		}

		public static DataTable GetPagedUserSmses(Guid userGuid, string query, int pageNo, int pageSize, string sortField, ref int resultCount)
		{
			Business.Inbox inboxController = new Business.Inbox();
			return inboxController.GetPagedUserSmses(userGuid, query, pageNo, pageSize, sortField, ref resultCount);
		}

		public static void InsertSms(Common.Inbox receiveSms)
		{
			Business.Inbox inboxController = new Business.Inbox();
		}

		public static DataTable GetChartDetailsAtSpecificDate(Guid userGuid, DateTime fromDateTime, DateTime toDateTime, int pageNo, int pageSize, ref int rowCount)
		{
			Business.Inbox inboxController = new Business.Inbox();
			return inboxController.GetChartDetailsAtSpecificDate(userGuid, fromDateTime, toDateTime, pageNo, pageSize, ref rowCount);
		}

		public static long GetCountNumberOfGroup(Guid groupGuid)
		{
			Business.Inbox inboxController = new Business.Inbox();
			return inboxController.GetCountNumberOfGroup(groupGuid);
		}

		public static Common.Inbox Load(Guid guid)
		{
			Business.Inbox inboxController = new Business.Inbox();
			Common.Inbox inbox = new Common.Inbox();
			inboxController.Load(guid, inbox);
			return inbox;
		}

		public static bool ChangeInboxGroup(Guid inboxGuid, Guid inboxGroupGuid)
		{
			Business.Inbox inboxController = new Business.Inbox();
			return inboxController.ChangeInboxGroup(inboxGuid, inboxGroupGuid);
		}

		public static bool Delete(Guid guid)
		{
			Business.Inbox inboxController = new Business.Inbox();
			return inboxController.Delete(guid);
		}

		public static DataTable InsertReceiveMessage(Common.ReceiveMessage receiveMessage)
		{
			Business.Inbox inboxController = new Business.Inbox();
			return inboxController.InsertReceiveSms(receiveMessage);
		}

		public static bool DeleteMultipleRow(string guids)
		{
			Business.Inbox inboxController = new Business.Inbox();
			return inboxController.DeleteMultipleRow(guids);
		}

		public static DataTable GetPagedParserSms(Guid parserGuid, Guid formulaGuid, int lottery, string sender, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.Inbox inboxController = new Business.Inbox();
			return inboxController.GetPagedParserSms(parserGuid, formulaGuid, lottery, sender, sortField, pageNo, pageSize, ref resultCount);
		}

		public static DataTable GetParserSmsReport(Guid ParserGuid)
		{
			Business.Inbox inboxController = new Business.Inbox();
			return inboxController.GetParserSmsReport(ParserGuid);
		}
	}
}
