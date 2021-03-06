﻿// --------------------------------------------------------------------
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
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Data;

namespace Arad.SMS.Gateway.Business
{
	public class ScheduledBulkSms : BusinessEntityBase
	{
		public ScheduledBulkSms(DataAccessBase dataAccessProvider = null)
			: base(TableNames.ScheduledBulkSmses.ToString(), dataAccessProvider)
		{ }

		public DataTable GetQueue(int count)
		{
			return FetchSPDataTable("GetQueue", "@Count", count);
		}

		public bool ProcessRequest(Guid guid,
															 long id,
															 ScheduledSmsStatus scheduledSmsStatus,
															 Guid privateNumberGuid,
															 Guid userGuid,
															 int smsSenderAgentReference,
															 bool sendBulkIsAutomatic,
															 bool sendSmsAlert,
															 TimeSpan startSendTime,
															 TimeSpan endSendTime)
		{
			return ExecuteSPCommand("ProcessRequest",
															"@Guid", guid,
															"@ID", id,
															"@Status", (int)scheduledSmsStatus,
															"@PrivateNumberGuid", privateNumberGuid,
															"@UserGuid", userGuid,
															"@SmsSenderAgentReference", smsSenderAgentReference,
															"@SendBulkIsAutomatic", sendBulkIsAutomatic,
															"@SendSmsAlert", sendSmsAlert,
															"@StartSendTime", startSendTime,
															"@EndSendTime", endSendTime);
		}
	}
}
